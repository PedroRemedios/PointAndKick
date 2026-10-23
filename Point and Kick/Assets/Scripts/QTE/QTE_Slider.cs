using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QTE_Slider : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject businessMan;
    [SerializeField] private GameObject businessManPoked;
    [SerializeField] private GameObject handWithAntenna;
    [SerializeField] private GameObject handWithAntennaPoke;
    [SerializeField] private GameObject barTriggerLeft;
    [SerializeField] private GameObject barTriggerRight;
    [SerializeField] private GameObject pointer;
    [SerializeField] private GameObject barZones;
    [SerializeField] private GameObject internalBarZone;
    [SerializeField] private GameObject externaLeftlBarZone;
    [SerializeField] private GameObject externalRightBarZone;
    [SerializeField] private GameObject _QTE_Objects;
    [SerializeField] private GameObject _QTE_Win;
    [SerializeField] private GameObject _QTE_PositiveFX;

    [SerializeField] private Button restartButton;

    [SerializeField] private float speed;
    [SerializeField] float handWithAntennaRotation;

    [SerializeField] bool hardMode;

    RectTransform rtPointer;
    BoxCollider2D pointerCollider;

    BoxCollider2D barTriggerRightCollider;
    BoxCollider2D barTriggerLeftCollider;

    RectTransform rtBarZones;
    RectTransform rtHandWithAntenna;

    BoxCollider2D internalBarZoneCollider;
    BoxCollider2D externaLeftBarZoneCollider;
    BoxCollider2D externaRightBarZoneCollider;

    private PlayerInputActions playerInputActions;

    float leftBarLimit;
    float rightBarLimit;

    short winCondition;

    float handWithAntennaPosition;

    Vector2 lastHandAnchoredPosition;
    Vector2 firstHandAnchoredPosition;

    Vector3 lastHandRotation;
    Vector3 firstHandRotation;

    #endregion

    #region Awake, Start, Update
    // Runs before Start. Used to initialize components
    void Awake()
    {
        // Initialize variables with Unity Components
        if (pointer != null) rtPointer = pointer.GetComponent<RectTransform>();
        if (pointer != null) pointerCollider = pointer.GetComponent<BoxCollider2D>();

        if (barTriggerRight != null) barTriggerRightCollider = barTriggerRight.GetComponent<BoxCollider2D>();
        if (barTriggerLeft != null) barTriggerLeftCollider = barTriggerLeft.GetComponent<BoxCollider2D>();

        if (barZones != null) rtBarZones = barZones.GetComponent<RectTransform>();
        if (handWithAntenna != null) rtHandWithAntenna = handWithAntenna.GetComponent<RectTransform>();

        if (internalBarZone != null) internalBarZoneCollider = internalBarZone.GetComponent<BoxCollider2D>();
        if (externaLeftlBarZone != null) externaLeftBarZoneCollider = externaLeftlBarZone.GetComponent<BoxCollider2D>();
        if (externalRightBarZone != null) externaRightBarZoneCollider = externalRightBarZone.GetComponent<BoxCollider2D>();


    }

    // Start is called before the first frame update
    void Start()
    {

        // Just making sure the current game state is QTE
        GameManager.Instance.SetGameState(GameState.QTE);

        //Initialize other variables
        leftBarLimit = -850.0f;
        rightBarLimit = 1030.0f;

        winCondition = 8;
        //speed = 4f;

        handWithAntennaPosition = 0;
        //handWithAntennaRotation = -45;

        lastHandAnchoredPosition = Vector2.zero;
        firstHandAnchoredPosition = Vector2.zero;

        lastHandRotation = Vector3.zero;
        firstHandRotation = Vector3.zero;

        //Position of the Hand With Antenna sprite on the Canvas
        handWithAntennaPosition = rtHandWithAntenna.sizeDelta.x / 2;

        rtHandWithAntenna.anchoredPosition = new Vector2(handWithAntennaPosition, rtHandWithAntenna.anchoredPosition.y);
        lastHandAnchoredPosition = rtHandWithAntenna.anchoredPosition;
        firstHandAnchoredPosition = rtHandWithAntenna.anchoredPosition;

        rtHandWithAntenna.eulerAngles = new Vector3(rtHandWithAntenna.eulerAngles.x, rtHandWithAntenna.eulerAngles.y, handWithAntennaRotation);
        lastHandRotation = rtHandWithAntenna.eulerAngles;
        firstHandRotation = rtHandWithAntenna.eulerAngles;

        //Position and Rotation values to be incremented according to the winCondition value. 
        //Example: If the Win Condition is 8, the Hand will move 8 times on the canvas until reach the final destionation - the bag. The same with rotation.
        handWithAntennaPosition = handWithAntennaPosition / winCondition;
        handWithAntennaRotation = handWithAntennaRotation / winCondition;

        //The restart button is just a placeholder to easily restart the scene
        if (restartButton != null) restartButton.gameObject.SetActive(false);

        //This Win State is displayed only when the QTE with the bar is finished
        if (_QTE_Win != null) _QTE_Win.gameObject.SetActive(false);

        playerInputActions = InputManager.Instance.GetPlayerInputActions();
        DefineQTEActions(playerInputActions);
    }

    // Update is called once per frame
    void Update()
    {

        // If there are still QTE actions to be completed, the variable "winCondition" will have a value higher than 0.
        // Otherwise, the player is ready to complete the QTE.
        if (winCondition > 0)
        {
            //Function that will make the pointer move alongside the bar
            GetThePointerMoving();
            
        }
        else
        {
            //After the QTE with the bar, this state will popup. The bar and pointer will disappear.
            Win();

            if (playerInputActions.QTE.FinalInteraction.triggered) ChangeSceneToTrainStation();
        }

    }
    #endregion

    #region Other Functions

    /// <summary>
    /// Function that will make the pointer move alongside the bar
    /// </summary>
    void GetThePointerMoving()
    {
        //If Pointer RectTransform exists and it's not stopped (speed is not zero)
        if (rtPointer != null && speed != 0)
        {
            //I used two colliders on both sides of the bar. Once the pointer interacts with one of them, it redirects the direction to the opposite collider.
            //If pointer touches left collider
            if (pointerCollider.IsTouching(barTriggerLeftCollider))
            {
                //Once the pointer touches the left trigger it deactivates it and activate the right one
                barTriggerRight.SetActive(true);
                barTriggerLeft.SetActive(false);

                //This changes the direction of the pointer. The speed is negative so it changes to positive to redirect it to the right collider
                speed *= -1;
                Debug.Log("Entrou no Esquerdo");
            }

            //If pointer touches left collider
            if (pointerCollider.IsTouching(barTriggerRightCollider))
            {
                //Once the pointer touches the right trigger it deactivates it and activate the left one
                barTriggerRight.SetActive(false);
                barTriggerLeft.SetActive(true);

                //This changes the direction of the pointer. The speed is positive so it changes to negative to redirect it to the left collider
                speed *= -1;
                Debug.Log("Entrou no Direito");
            }

            //Makes the pointer position change
            rtPointer.anchoredPosition = new Vector3(rtPointer.anchoredPosition.x + speed, rtPointer.anchoredPosition.y);

        }

    }

    /// <summary>
    /// Function that enable the Input actions for the QTE and starts performing the checks for Bar available actions
    /// </summary>
    /// <param name="playerInputActions">variable that holds the access to the configured player actions</param>
    private void DefineQTEActions(PlayerInputActions playerInputActions)
    {
        playerInputActions.QTE.BasicInteraction.Enable();
        playerInputActions.QTE.BasicInteraction.performed += BarCheckAction;
        playerInputActions.QTE.FinalInteraction.Enable();
    }
    
    /// <summary>
    /// Function that calls the check for the available Bar Actions 
    /// </summary>
    /// <param name="callbackContext">variable that holds the Event</param>
    private void BarCheckAction(InputAction.CallbackContext callbackContext)
    {
        Action();
    }

    /// <summary>
    /// Function that will trigger once the player used the action button to try to complete the QTE
    /// </summary>
    void Action()
    {
        Vector2 newPosition = new Vector2();

        //There is one collider that exists between the pink bars. This is used to interact with the pointer and differentiate from the other zones.
        if (pointerCollider.IsTouching(internalBarZoneCollider))
        {
            //Change the Random seed. This will allow a different positioning every time end up in here. Every time the player can successfully interact with the QTE, the bars will change place.
            Random.InitState((int)System.DateTime.Now.Ticks);
            Debug.Log("Win Zone - Speed = " + speed);

            //Getting the QTE in this zone will subtract 2 to the win condition variable. This means that the player will be 2 times closer to win the QTE.
            winCondition -= 2;

            lastHandAnchoredPosition = rtHandWithAntenna.anchoredPosition;

            lastHandRotation = rtHandWithAntenna.eulerAngles;

            newPosition = new Vector2(CheckAntennaLocation(rtHandWithAntenna.anchoredPosition.x - (handWithAntennaPosition * 2)), rtHandWithAntenna.anchoredPosition.y);
            //Animate Hand with Antenna - Use of Coroutine to be able to animate the movement without relation to the frames
            StartCoroutine(AnimateHand(rtHandWithAntenna.anchoredPosition, newPosition, 1f, handWithAntennaRotation * 2, rtHandWithAntenna.eulerAngles));

            newPosition = Vector2.zero;

            //Function that position the bars on a new area of the blue bar
            SetBarThresholds(Random.Range(leftBarLimit, rightBarLimit));

            //Function to trigger the FX Positive Effect over the pointer
            PositiveEffect();

        }
        //There are two colliders that exist between the bordeaux bars. They are used to interact with the pointer and differentiate from the other zones.
        else if (pointerCollider.IsTouching(externaLeftBarZoneCollider) || pointerCollider.IsTouching(externaRightBarZoneCollider))
        {
            //Change the Random seed. This will allow a different positioning every time end up in here. Every time the player can successfully interact with the QTE, the bars will change place.
            Random.InitState((int)System.DateTime.Now.Ticks);
            Debug.Log("Still Win but Danger Zone - Speed = " + speed);

            //The player got the QTE right but it was on the danger zone, so the speed of the pointer will be increased by 30%. Max Value = 25 || -25
            speed *= 1.30f; if(speed < -25) speed = -25; if (speed > 25) speed = 25;

            //Function that position the bars on a new area of the blue bar
            SetBarThresholds(Random.Range(leftBarLimit, rightBarLimit));

            //Animate Bar and respective components - Shake effect 
            _QTE_Objects.GetComponent<ShakeThingsUp>().Trigger(5610.0f, 0.25f);

            if (hardMode)
            {
                if (rtHandWithAntenna.anchoredPosition.x != firstHandAnchoredPosition.x && rtHandWithAntenna.eulerAngles.z != firstHandRotation.z)
                {

                    Vector3 otherPosition = new Vector3(rtHandWithAntenna.anchoredPosition.x + (handWithAntennaPosition * 2), rtHandWithAntenna.anchoredPosition.y);
                        
                    lastHandRotation = rtHandWithAntenna.eulerAngles;

                    //Animate Hand with Antenna - Use of Coroutine to be able to animate the movement without relation to the frames
                    StartCoroutine(AnimateHand(rtHandWithAntenna.anchoredPosition, otherPosition, 1f, handWithAntennaRotation * -2, lastHandRotation));

                    lastHandAnchoredPosition = otherPosition;

                    //Getting the QTE in this zone will add 2 to the win condition variable. This means that the player will be 2 times away to win the QTE.
                    winCondition += 2;
                    

                }
                

            }
            else
            {
                //Getting the QTE in this zone will subtract 1 to the win condition variable. This means that the player will be 1 times closer to win the QTE.
                winCondition -= 1;

                newPosition = new Vector2(CheckAntennaLocation(rtHandWithAntenna.anchoredPosition.x - (handWithAntennaPosition * 1)), rtHandWithAntenna.anchoredPosition.y);

                lastHandAnchoredPosition = rtHandWithAntenna.anchoredPosition;

                lastHandRotation = rtHandWithAntenna.eulerAngles;

                //Animate Hand with Antenna - Use of Coroutine to be able to animate the movement without relation to the frames
                StartCoroutine(AnimateHand(rtHandWithAntenna.anchoredPosition, newPosition, 1f, handWithAntennaRotation * 1, rtHandWithAntenna.eulerAngles));

                newPosition = Vector2.zero;

            }

        }
        //No collider interated with the pointer... This means the player couldn't get the timing right. The player Lost.
        else
        {
            Debug.Log("Lose Zone - Speed = " + speed);
            //The player lost, so we need to change the sprites...
            if (businessMan != null && businessManPoked != null && handWithAntenna != null && handWithAntennaPoke != null)
            {
                //Deactivate the current sprites of the businessman and hand with antenna and replace them with the ones from the lose condition
                businessMan.SetActive(false);
                businessManPoked.SetActive(true);
                handWithAntenna.SetActive(false);
                handWithAntennaPoke.SetActive(true);

                //Make the placeholder restart button to appear
                if (restartButton != null)
                {
                    restartButton.gameObject.SetActive(true);
                }

                //Freeze the pointer and speed to zero
                pointer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                speed = 0;
            }

            playerInputActions.QTE.BasicInteraction.performed -= BarCheckAction;
        }
    }

    /// <summary>
    /// Function to animate the hand - Moving and rotating the sprite
    /// </summary>
    /// <param name="origin">Origin position of the sprite</param>
    /// <param name="target">Destiny position of the sprite</param>
    /// <param name="duration">Variable that holds the timespan to shift the sprite</param>
    /// <param name="rotationToIntegrate">Variable that hold the angle to rotate</param>
    /// <param name="rtHandWithAntennaAngleRotation">Current location of the sprite</param>
    /// <returns></returns>
    IEnumerator AnimateHand(Vector3 origin, Vector3 target, float duration, float rotationToIntegrate, Vector3 rtHandWithAntennaAngleRotation)
    {
        //Variation of time during animation
        float journey = 0f;

        //Execute movement
        while (journey <= duration)
        {
            //Calculations of time and percentage of the completion of the movement in the time 'duration'
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / duration);

            //Use Lerp to Position and Rotate smoothly
            rtHandWithAntenna.anchoredPosition = Vector3.Lerp(origin, target, percent);
            rtHandWithAntenna.eulerAngles = Vector3.Lerp(rtHandWithAntennaAngleRotation, new Vector3(rtHandWithAntennaAngleRotation.x, rtHandWithAntennaAngleRotation.y, (rtHandWithAntennaAngleRotation.z - rotationToIntegrate) > 360 ? 360 : (rtHandWithAntennaAngleRotation.z - rotationToIntegrate)), percent);

            yield return null;
        }
    }

    /// <summary>
    /// Activate the script to execute the FX for positive feedback
    /// </summary>
    void PositiveEffect()
    {
        _QTE_PositiveFX.SetActive(true);
    }

    /// <summary>
    /// //Function that position the bars on a new area of the blue bar
    /// </summary>
    /// <param name="offset">Variable that holds a random position alongside the blue bar</param>
    void SetBarThresholds(float offset)
    {
        // If Bars exist
        if (rtBarZones != null)
        {
            //If the new random location to move the bars is positive and the speed is also positive, we'll make sure the offset will appear on the opposite direction the pointer is going at the moment.
            if (offset > 0 && speed > 0)
            {
                //Realocate to the opossite location
                offset *= -1;

                //If the new offset is out of bound, realocate it to the position of the left collider
                if (offset < leftBarLimit)
                {
                    offset = leftBarLimit;
                }
            }

            //If the new random location to move the bars is negative and the speed is also negative, we'll make sure the offset will appear on the opposite direction the pointer is going at the moment.
            if (offset <= 0 && speed < 0)
            {
                //Realocate to the opossite location
                offset *= -1;

                //If the new offset is out of bound, realocate it to the position of the right collider
                if (offset > rightBarLimit)
                {
                    offset = rightBarLimit;
                }
            }

            //Reposition the Bar Zones
            rtBarZones.anchoredPosition = new Vector3(0 + offset, rtBarZones.anchoredPosition.y);
        }
    }

    /// <summary>
    /// Function to reposition the Hand with the Antenna if some of the calculations go out of bounds
    /// </summary>
    /// <param name="nextLocation"></param>
    /// <returns></returns>
    float CheckAntennaLocation(float nextLocation)
    {
        //If out of bounds
        if (nextLocation <= 0)
        {
            winCondition = 0;
            return 0;
        }
        return nextLocation;
    }

    /// <summary>
    /// Function to Activate Win State. After the QTE with the bar, this state will popup. The bar and pointer will disappear.
    /// </summary>
    public void Win()
    {
        playerInputActions.QTE.BasicInteraction.performed -= BarCheckAction;

        barZones.gameObject.SetActive(false);
        pointer.gameObject.SetActive(false);
        _QTE_Objects.gameObject.SetActive(false);
        _QTE_Win.gameObject.SetActive(true);

        //Shake an icon to press a button in the future... in order to finish the QTE
        _QTE_Win.GetComponent<ShakeThingsUp>().Trigger(300.0f, 0f);


        
    }

    /// <summary>
    /// Function to restart the level
    /// </summary>
    public void RestartLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Function to restart the level
    /// </summary>
    private void ChangeSceneToTrainStation()
    {
        GameManager.Instance.SetGameState(GameState.EXPLORATION);

        playerInputActions.QTE.Disable();
        RestartLevel("TrainStationScene");
            

    }

    #endregion
}
