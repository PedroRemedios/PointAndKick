using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QTE_Antenna : MonoBehaviour
{

    #region Variables

    //Enumerator that defines the state machine of this Quick Time Event
    private enum QTE_State { REACHING, SUS, HOLD, UNSCREW, FINAL }

    [SerializeField] private QTE_State qteState;
    [SerializeField] private QTE_State lastQteState;
    [SerializeField] private Vector2 qteCooldownTime;
    [SerializeField] private Vector2 susCooldownTime;
    [SerializeField] private GameObject businessMan;
    [SerializeField] private GameObject handPlayer;
    [SerializeField] private GameObject antenna;
    [SerializeField] private GameObject susEffect;
    [SerializeField] private GameObject _QTE_Lose;
    [SerializeField] private GameObject _QTE_Reaching;
    [SerializeField] private GameObject _QTE_Unscrew;

    [SerializeField] private Button restartButton;

    [SerializeField] List<Sprite> _spritesBusinessMan;
    [SerializeField] List<Sprite> _spritesHand;
    [SerializeField] List<Sprite> _spritesAntenna;
    [SerializeField] List<Sprite> _spritesJoystick;
    [SerializeField] private Sprite susEffectSprite;

    [SerializeField] private Vector2 analogStickValues;

    private RectTransform rtQTE_Reaching;
    private RectTransform rtQTE_Unscrew;
    private RectTransform rtHandPlayer;

    private JoystickHandling accessScript;

    Image imgBusinessMan;
    Image imgHandPlayer;
    Image imgAntenna;
    Image imgSusEffect;
    Image imgQTE_Reaching;
    Image imgQTE_Unscrew;

    private PlayerInputActions playerInputActions;

    private float waitSusTime;
    private float waitQTETime;
    private float timePassedForQTE;
    private float timePassedForSus;

    private int controlJoystickSprite;

    [SerializeField] private int winCondition = 20;
    private int lastCompleteJoystickFullCircle;

    Vector2 currentJoystickInput;
    Vector2 lastJoystickInput;

    private bool unscrewFlag; // this is a variable to control the changing of the hand / antenna sprites while unscrewing

    /// <summary>
    /// Public GET to send the check value (true / false) depending if the current QTE state is UNSCREW
    /// </summary>
    public bool CurrentlyOnUnscrew
    {
        get
        {
            if (qteState == QTE_State.UNSCREW)
                return true;
            else
                return false;
        }
    }
    #endregion

    #region Awake, Start, Update
    // Runs before Start. Used to initialize components
    void Awake()
    {
        // Initialize variables with Unity Components
        if (_QTE_Reaching != null) rtQTE_Reaching = _QTE_Reaching.GetComponent<RectTransform>();
        if (_QTE_Unscrew != null) rtQTE_Unscrew = _QTE_Unscrew.GetComponent<RectTransform>();
        if (handPlayer != null) rtHandPlayer = handPlayer.GetComponent<RectTransform>();

        if (businessMan != null) imgBusinessMan = businessMan.GetComponent<Image>();
        if (handPlayer != null) imgHandPlayer = handPlayer.GetComponent<Image>();
        if (antenna != null) imgAntenna = antenna.GetComponent<Image>();
        if (susEffect != null) imgSusEffect = susEffect.GetComponent<Image>();


        if (_QTE_Reaching != null) imgQTE_Reaching = _QTE_Reaching.GetComponent<Image>();
        if (_QTE_Unscrew != null) imgQTE_Unscrew = _QTE_Unscrew.GetComponent<Image>();

        // Initialize other types of variables
        accessScript = GetComponent<JoystickHandling>();

        controlJoystickSprite = 0;
        timePassedForQTE = 0;
        timePassedForSus = 0;
        lastCompleteJoystickFullCircle = 0;
        unscrewFlag = false;

        currentJoystickInput = Vector2.zero;
        lastJoystickInput = Vector2.zero;

    }

    // Start is called before the first frame update
    void Start()
    {
        // Just making sure the current game state is QTE
        GameManager.Instance.SetGameState(GameState.QTE);

        // Initialize the scene with the proper sprites
        if(imgBusinessMan != null) imgBusinessMan.sprite = _spritesBusinessMan[0];
        if(imgSusEffect != null) imgSusEffect.sprite = susEffectSprite;
        if(imgAntenna != null) imgAntenna.sprite = _spritesAntenna[0];

        //The restart button is just a placeholder to easily restart the scene
        if (restartButton != null) restartButton.gameObject.SetActive(false);

        //This Win State is displayed only when the QTE with the bar is finished
        if (_QTE_Lose != null) _QTE_Lose.gameObject.SetActive(false);
        if (_QTE_Reaching != null) _QTE_Reaching.gameObject.SetActive(true);
        if (_QTE_Unscrew != null) _QTE_Unscrew.gameObject.SetActive(false);

        //Initialize the variable that holds the access to the configured input actions
        playerInputActions = InputManager.Instance.GetPlayerInputActions();
        DefineQTEActions(playerInputActions);

        //We are starting the QTE so we won't show the lose state right now
        _QTE_Lose.SetActive(false);

        //Let's start the QTE without the business man being sus about us, the players
        susEffect.SetActive(false);
        qteState = QTE_State.REACHING;
        imgHandPlayer.sprite = _spritesHand[0];

        //Position the Hand object / Sprite in the right position
        rtHandPlayer.anchoredPosition = new Vector3(rtHandPlayer.anchoredPosition.x, (rtHandPlayer.rect.height / 8) > 0 ? (rtHandPlayer.rect.height / 8) * -1 : (rtHandPlayer.rect.height / 8));

        //Start Coroutine that animates the Joystick UI Sprite to display the 4 positions (North - UP; East - Left; South - Down; West - Right)
        StartCoroutine(AnimateJoystickSprite());

        //Change the QTE player time and the sus time depending on a range of values. From this range of values, a number will be chosen randomly for each round
        waitQTETime = ChangeWaitSusTime(qteCooldownTime);
        waitSusTime = ChangeWaitSusTime(susCooldownTime);
    }

    // Update is called once per frame
    void Update()
    {
        // Call of the function that will control the sus time of the business man
        SuspiciousBusinessMan();

        /*The next comment lines are not needed (obviously). I just want to keep them because it may come in handy eventually...
         * 
        //Debug.Log(qteState);
        Vector2 stuff = playerInputActions.QTE.JoystickInteraction.ReadValue<Vector2>();
        //Debug.Log("Angle: " + (Mathf.Atan2(playerInputActions.QTE.JoystickInteraction.ReadValue<Vector2>().x, playerInputActions.QTE.JoystickInteraction.ReadValue<Vector2>().y) * Mathf.Rad2Deg));
        Debug.Log("Angle: " + Vector2.SignedAngle(stuff, lastAngle));
        lastAngle = stuff;
         *
         */

        // If statement that controls some of the configurations needed in two of the QTE States
        if (qteState == QTE_State.REACHING)
        {
            //If it's REACHING, it means the object that holds the image of the button that needs to be pressed must appear on screen and shake
            _QTE_Reaching.SetActive(true);
            _QTE_Reaching.GetComponent<ShakeThingsUp>().Trigger(500.0f, 0.25f);
        }
        else if (qteState == QTE_State.UNSCREW)
        {
            //If it's UNSCREW, it means we must guarantee that the hand is in the right position to start the unscrew animation.
            if (rtHandPlayer.anchoredPosition.x != 1245)
            {
                rtHandPlayer.anchoredPosition = new Vector3(1245, rtHandPlayer.anchoredPosition.y);
            }

            //It also means the object that holds the image of the button that needs to be pressed (joystick) must appear on screen and shake
            _QTE_Unscrew.SetActive(true);
            _QTE_Unscrew.GetComponent<ShakeThingsUp>().Trigger(500.0f, 0.25f);
        }
    }

    #endregion

    #region Other Functions
    /// <summary>
    /// Function that will animate the Joystick UI Image (switching between 4 sprites)
    /// </summary>
    /// <returns></returns>
    private IEnumerator AnimateJoystickSprite()
    {
        //This means it will run for the all QTE
        while (true)
        {
            //This will guarantee that each sprite will stay on scree for half a minute
            yield return new WaitForSeconds(0.5f);

            //this If statement guarantees that all the logic that changes sprites will only run if we are of the UNSCREW game state. It doesn't need to run otherwise.
            if (qteState == QTE_State.UNSCREW)
            {
                //If statement that guarantee that we won't surpass the index values of the array that holds the sprites
                if (controlJoystickSprite >= _spritesJoystick.Count)
                {
                    controlJoystickSprite = 0;
                }

                //Change sprite
                if (_QTE_Unscrew != null) imgQTE_Unscrew = _QTE_Unscrew.GetComponentsInChildren<Image>()[0];
                if (imgQTE_Unscrew != null) imgQTE_Unscrew.sprite = _spritesJoystick[controlJoystickSprite];
                controlJoystickSprite++;

            }
        }
    }

    /// <summary>
    /// Function that enable the Input actions for the QTE and starts performing 
    /// </summary>
    /// <param name="playerInputActions">variable that holds the access to the configured player actions</param>
    private void DefineQTEActions(PlayerInputActions playerInputActions)
    {
        playerInputActions.QTE.BasicInteraction.Enable();
        playerInputActions.QTE.FinalInteraction.Enable();
        playerInputActions.QTE.JoystickInteraction.Enable();
        playerInputActions.QTE.UpKeyboardInteraction.Enable();
        playerInputActions.QTE.LeftKeyboardInteraction.Enable();
        playerInputActions.QTE.DownKeyboardInteraction.Enable();
        playerInputActions.QTE.RightKeyboardInteraction.Enable();
        playerInputActions.QTE.BasicInteraction.performed += QTE_BasicInput;
        playerInputActions.QTE.JoystickInteraction.performed += QTE_BasicInput;
        playerInputActions.QTE.UpKeyboardInteraction.performed += QTE_BasicInput;
        playerInputActions.QTE.LeftKeyboardInteraction.performed += QTE_BasicInput;
        playerInputActions.QTE.DownKeyboardInteraction.performed += QTE_BasicInput;
        playerInputActions.QTE.RightKeyboardInteraction.performed += QTE_BasicInput;
    }

    /// <summary>
    /// Function that disable the Input actions for the QTE and starts performing 
    /// </summary>
    /// <param name="playerInputActions">variable that holds the access to the configured player actions</param>
    private void StopQTEActions(PlayerInputActions playerInputActions)
    {
        playerInputActions.QTE.BasicInteraction.Disable();
        playerInputActions.QTE.FinalInteraction.Disable();
        playerInputActions.QTE.JoystickInteraction.Disable();
        playerInputActions.QTE.UpKeyboardInteraction.Disable();
        playerInputActions.QTE.LeftKeyboardInteraction.Disable();
        playerInputActions.QTE.DownKeyboardInteraction.Disable();
        playerInputActions.QTE.RightKeyboardInteraction.Disable();
    }

    /// <summary>
    /// Function that controls all the logic based on game state and player inputs
    /// </summary>
    /// <param name="callbackContext">variable that holds the Event</param>
    private void QTE_BasicInput(InputAction.CallbackContext callbackContext)
    {
        //If game state is currently REACHING
        if (qteState == QTE_State.REACHING)
        {
            //In case player has trigger the button
            if (playerInputActions.QTE.BasicInteraction.ReadValue<float>() > 0)
            {
                //We'll check if the next hand position after the input trigger is going to be higher than the treshold to change sprite
                if (rtHandPlayer.anchoredPosition.x + 50 >= 900)
                {
                    //Debug.Log("HOLD - STATE");
                    //If so, we position the hand at the defined position with the correct sprite and change the game state to HOLD
                    rtHandPlayer.anchoredPosition = new Vector3(1245, rtHandPlayer.anchoredPosition.y);
                    //StartCoroutine(AnimateHand(rtHandPlayer.anchoredPosition, new Vector3(1245, rtHandPlayer.anchoredPosition.y), 1f));
                    qteState = QTE_State.HOLD;
                    imgHandPlayer.sprite = _spritesHand[1];
                }
                else
                {
                    //Debug.Log("Coming closer...");
                    //rtHandPlayer.anchoredPosition = new Vector3(rtHandPlayer.anchoredPosition.x + 50, rtHandPlayer.anchoredPosition.y);

                    //Otherwise, it needs to lerp to a new position over time
                    StartCoroutine(AnimateHand(rtHandPlayer.anchoredPosition, new Vector3(rtHandPlayer.anchoredPosition.x + 50, rtHandPlayer.anchoredPosition.y), 0.5f));

                }
            }
        }
        //If game state is currently HOLD
        else if (qteState == QTE_State.HOLD)
        {
            //In case player has trigger the button
            if (playerInputActions.QTE.BasicInteraction.ReadValue<float>() > 0)
            {
                //Debug.Log("UNSCREW - STATE");

                //you can start unscrew
                qteState = QTE_State.UNSCREW;
                //change hand sprite
                imgHandPlayer.sprite = _spritesHand[2];
            }
        }
        //If game state is currently SUS
        else if (qteState== QTE_State.SUS)
        {
            //If the last game state was UNSCREW and the player is now trying to use the joystick / keyboard mapping that substitutes controller while on SUS mode OR
            //If the last game state was HOLD or REACHING and the player is now trying to use the action button while on SUS mode
            //The player will lose the QTE
            if ((lastQteState == QTE_State.UNSCREW && (playerInputActions.QTE.JoystickInteraction.ReadValue<Vector2>() != Vector2.zero || CheckKeyboardInteraction())) || 
                ((lastQteState == QTE_State.HOLD || lastQteState == QTE_State.REACHING) && playerInputActions.QTE.BasicInteraction.triggered))
            {
                StopAllCoroutines();
                StopQTEActions(playerInputActions);
                _QTE_Lose.SetActive(true);
                if (restartButton != null) restartButton.gameObject.SetActive(true);
            }

        }
        //If game state is currently UNSCREW
        else if (qteState == QTE_State.UNSCREW)
        {
            //Read current input joystick values
            currentJoystickInput = GetCurrentUnscrewInput();
            //analogStickValues.x = currentJoystickInput.x; analogStickValues.y = currentJoystickInput.y;

            //If player is rotating Joystick and the motion is not clockwise
            if (currentJoystickInput != Vector2.zero )//&& Vector2.SignedAngle(currentJoystickInput, lastJoystickInput) < 0)
            {
                RotatingCorrectly();

            }
            lastJoystickInput = currentJoystickInput;
        }
    }

    /// <summary>
    /// Function that performs all the necessary steps for the SUS game state
    /// </summary>
    private void SuspiciousBusinessMan()
    {
        //If it's not time to be SUS
        if (timePassedForSus == 0)
        {
            //It's time for the player to do some actions
            timePassedForQTE += Time.deltaTime;

            //If the time for the player actions is superior or equal than the defined time to perform the QTE, it's time to change to SUS
            if (timePassedForQTE >= waitQTETime)
            {
                timePassedForQTE = 0;
                waitQTETime = ChangeWaitSusTime(qteCooldownTime);
                //Animate Hand with Antenna - Use of Coroutine to be able to animate the movement without relation to the frames
                StartCoroutine(MakeSuspiciousBusinessManOnScreen());
            }
        }
        
        
    }

    /// <summary>
    /// Function that activates the Sprite and shows it on screen
    /// </summary>
    /// <returns></returns>
    IEnumerator MakeSuspiciousBusinessManOnScreen()
    {
        if (imgSusEffect != null && imgBusinessMan != null)
        {
            //Sus effect appears on scree to warn the player to stop what is being done
            susEffect.SetActive(true);

            yield return new WaitForSeconds(1); //waits one second

            susEffect.SetActive(false);


            lastQteState = qteState;

            //It's SUS time
            while (timePassedForSus < waitSusTime) 
            {
                qteState = QTE_State.SUS;

                _QTE_Reaching.SetActive(false);
                _QTE_Unscrew.SetActive(false);
                rtQTE_Reaching.anchoredPosition = Vector2.zero;
                rtQTE_Unscrew.anchoredPosition = Vector2.zero;


                timePassedForSus += Time.deltaTime;

                imgBusinessMan.sprite = _spritesBusinessMan[1];

                yield return null;

            }
            qteState = lastQteState;

            if (qteState == QTE_State.REACHING || qteState == QTE_State.HOLD)
            {
                _QTE_Reaching.SetActive(true);

            }
            else if (qteState == QTE_State.UNSCREW)
            {
                _QTE_Unscrew.SetActive(true);
            }

            timePassedForSus = 0;
            waitSusTime = ChangeWaitSusTime(susCooldownTime);
            imgBusinessMan.sprite = _spritesBusinessMan[0];
            yield return null;

        }

    }

    /// <summary>
    /// Function that retrieves a random value between a range of values give by a Vector2 variable
    /// </summary>
    /// <param name="cooldown">variable that has the range of values</param>
    /// <returns>returns the random number between the range of values</returns>
    private float ChangeWaitSusTime(Vector2 cooldown)
    {
        return cooldown == Vector2.zero ? Random.Range(1, 5) : Random.Range(cooldown.x, cooldown.y);
    }

    /// <summary>
    /// Function to animate the hand - Moving and rotating the sprite
    /// </summary>
    /// <returns></returns>
    void AnimateUnscrew()
    {
        //This if statement controls the position of the antenna. If the antenna is currently in the normal position, next time it changes the sprite will change accordingly
        if (!unscrewFlag)
        {
            imgHandPlayer.sprite = _spritesHand[3];
            imgAntenna.sprite = _spritesAntenna[1];
            unscrewFlag = true;

        }
        else
        {
            imgHandPlayer.sprite = _spritesHand[2];
            imgAntenna.sprite = _spritesAntenna[0];
            unscrewFlag = false;

        }
    }

    /// <summary>
    /// Function to restart the level
    /// </summary>
    public void RestartLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Function to animate the hand - Moving and rotating the sprite
    /// </summary>
    /// <param name="origin">Origin position of the sprite</param>
    /// <param name="target">Destiny position of the sprite</param>
    /// <param name="duration">Variable that holds the timespan to shift the sprite</param>
    /// <returns></returns>
    IEnumerator AnimateHand(Vector3 origin, Vector3 target, float duration)
    {
        
        //Variation of time during animation
        float journey = 0f;

        //Execute movement
        while (journey <= duration)
        {
            if (qteState != QTE_State.SUS)
            {

                //Calculations of time and percentage of the completion of the movement in the time 'duration'
                journey = journey + Time.deltaTime;
                float percent = Mathf.Clamp01(journey / duration);

                //Use Lerp to Position and Rotate smoothly
                rtHandPlayer.anchoredPosition = Vector3.Lerp(origin, target, percent);
               
            }
            else
            {
                journey = duration;
            }
            yield return null;
        }
    }

    /// <summary>
    /// Function that executes once the player wins the QTE and retrieve the Antenna. Then, we change Scene to the other QTE.
    /// </summary>
    void Win()
    {
        SceneManager.LoadScene("QTECamille");
    }

    /// <summary>
    /// Function that checks if the Keyboard is being used as a substitute for a controller
    /// </summary>
    /// <returns></returns>
    bool CheckKeyboardInteraction()
    {
        if (playerInputActions.QTE.UpKeyboardInteraction.ReadValue<float>() != 0.0f ||
            playerInputActions.QTE.LeftKeyboardInteraction.ReadValue<float>() != 0.0f ||
            playerInputActions.QTE.DownKeyboardInteraction.ReadValue<float>() != 0.0f ||
            playerInputActions.QTE.RightKeyboardInteraction.ReadValue<float>() != 0.0f)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Function that returns Current Unscrew Input - Joystick from Controller or Keyboard input
    /// </summary>
    /// <returns></returns>
    Vector2 GetCurrentUnscrewInput()
    {
        float temporaryValueFromInput;

        if (playerInputActions.QTE.UpKeyboardInteraction.ReadValue<float>() != 0.0f)
        {
            temporaryValueFromInput = playerInputActions.QTE.UpKeyboardInteraction.ReadValue<float>();
            return new Vector2(temporaryValueFromInput, temporaryValueFromInput);
        }
        if (playerInputActions.QTE.LeftKeyboardInteraction.ReadValue<float>() != 0.0f)
        {
            temporaryValueFromInput = playerInputActions.QTE.LeftKeyboardInteraction.ReadValue<float>();
            return new Vector2(temporaryValueFromInput, temporaryValueFromInput);
        }
        if (playerInputActions.QTE.DownKeyboardInteraction.ReadValue<float>() != 0.0f)
        {
            temporaryValueFromInput = playerInputActions.QTE.DownKeyboardInteraction.ReadValue<float>();
            return new Vector2(temporaryValueFromInput, temporaryValueFromInput);
        }
        if (playerInputActions.QTE.RightKeyboardInteraction.ReadValue<float>() != 0.0f)
        {
            temporaryValueFromInput = playerInputActions.QTE.RightKeyboardInteraction.ReadValue<float>();
            return new Vector2(temporaryValueFromInput, temporaryValueFromInput);
        }
        if (playerInputActions.QTE.JoystickInteraction.ReadValue<Vector2>() != Vector2.zero)
        {
            return playerInputActions.QTE.JoystickInteraction.ReadValue<Vector2>();
        }
        return Vector2.zero;

    }

    /// <summary>
    /// Function that controls the antenna animation and counts how many successful turns the player did. If it reached the treshold, the player wins
    /// </summary>
    void RotatingCorrectly()
    {
        //Debug.Log("UNSCREW - STATE");
        // Debug.Log("Actually using the joystick right now... Angle = " + (Mathf.Atan2(playerInputActions.QTE.JoystickInteraction.ReadValue<Vector2>().x, playerInputActions.QTE.JoystickInteraction.ReadValue<Vector2>().y) * Mathf.Rad2Deg));

        //Animate Hand with Antenna - Use of Coroutine to be able to animate the movement without relation to the frames

        //We will access the NumberSpinInARow variable that holds the complete 360º spins and see if we need to perform more spins to win
        if (lastCompleteJoystickFullCircle < accessScript.NumberSpinsInARow)
        {
            //Get last number of spins
            lastCompleteJoystickFullCircle = accessScript.NumberSpinsInARow;

            //If the numer of complete spins is equal or superior than the number of spins needed to retrieve the antenna
            if (lastCompleteJoystickFullCircle >= winCondition)
                Win(); // Player completed the QTE
            else
                AnimateUnscrew(); //Otherwise, Antenna rotates (change sprite)
        }
    }

    #endregion
}
