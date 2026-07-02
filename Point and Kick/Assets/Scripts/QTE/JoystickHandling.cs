using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoystickHandling : MonoBehaviour
{
    #region Variables
    private enum JoystickDirections { North, East, South, West }
    Dictionary<JoystickDirections, Vector2> range = new Dictionary<JoystickDirections, Vector2>();
    [SerializeField] bool[] flagPosition = new bool[] { false, false, false, false };
    [SerializeField] JoystickDirections currentJoystickDirection;

    QTE_Antenna accessQTEAntenna;
    
    private PlayerInputActions playerInputActions;

    private Vector2 joyStickInput = Vector2.zero; // Register the current input of the joystick
    private Vector2 lastJoyStickInput = Vector2.zero; // Register the last input of the joystick
    [SerializeField] private float joyStickAngle = 0.0f; // Register the current angle of the joystick

    [SerializeField] private int countFullCircle = 0;

    public int NumberSpinsInARow
    {
        get
        {
            return countFullCircle;
        }
    }
    #endregion

    #region Start, Update

    // Start is called before the first frame update
    private void Start()
    {

        range.Add(JoystickDirections.North, new Vector2(-45, 45));
        range.Add(JoystickDirections.East, new Vector2(-135, -45));
        range.Add(JoystickDirections.South, new Vector2(135, -135));
        range.Add(JoystickDirections.West, new Vector2(45, 135));

        accessQTEAntenna= GetComponent<QTE_Antenna>();

        playerInputActions = InputManager.Instance.GetPlayerInputActions();

        playerInputActions.QTE.JoystickInteraction.Enable();
        playerInputActions.QTE.UpKeyboardInteraction.Enable();
        playerInputActions.QTE.LeftKeyboardInteraction.Enable();
        playerInputActions.QTE.DownKeyboardInteraction.Enable();
        playerInputActions.QTE.RightKeyboardInteraction.Enable();
        // Getting our input
        playerInputActions.QTE.JoystickInteraction.performed += JoyStickInput;
        playerInputActions.QTE.UpKeyboardInteraction.performed += JoyStickInput;
        playerInputActions.QTE.LeftKeyboardInteraction.performed += JoyStickInput;
        playerInputActions.QTE.DownKeyboardInteraction.performed += JoyStickInput;
        playerInputActions.QTE.RightKeyboardInteraction.performed += JoyStickInput;
    }

    // Update is called once per frame
    void Update()
    {
         
        CheckJoyStickSpinning(); // Check spinning here

        
    }

    #endregion

    #region Other Functions
    private void JoyStickInput(InputAction.CallbackContext callbackContext)
    {

        float temporaryValueFromInput;

        if (playerInputActions.QTE.UpKeyboardInteraction.ReadValue<float>() != 0.0f)
        {
            temporaryValueFromInput = playerInputActions.QTE.UpKeyboardInteraction.ReadValue<float>();
            joyStickInput = new Vector2(temporaryValueFromInput, temporaryValueFromInput);

            joyStickAngle = 10;
        }
        else if (playerInputActions.QTE.LeftKeyboardInteraction.ReadValue<float>() != 0.0f)
        {
            temporaryValueFromInput = playerInputActions.QTE.LeftKeyboardInteraction.ReadValue<float>();
            joyStickInput = new Vector2(temporaryValueFromInput, temporaryValueFromInput);

            joyStickAngle = -60;
        }
        else if (playerInputActions.QTE.DownKeyboardInteraction.ReadValue<float>() != 0.0f)
        {
            temporaryValueFromInput = playerInputActions.QTE.DownKeyboardInteraction.ReadValue<float>();
            joyStickInput = new Vector2(temporaryValueFromInput, temporaryValueFromInput);

            joyStickAngle = -145;
        }
        else if (playerInputActions.QTE.RightKeyboardInteraction.ReadValue<float>() != 0.0f)
        {
            temporaryValueFromInput = playerInputActions.QTE.RightKeyboardInteraction.ReadValue<float>();
            joyStickInput = new Vector2(temporaryValueFromInput, temporaryValueFromInput);

            joyStickAngle = 60;
        }
        else if (playerInputActions.QTE.JoystickInteraction.ReadValue<Vector2>() != Vector2.zero)
        {

            joyStickInput = playerInputActions.QTE.JoystickInteraction.ReadValue<Vector2>();

            joyStickAngle = (Mathf.Atan2(joyStickInput.x, joyStickInput.y) * Mathf.Rad2Deg);
        }
    }

    private void CheckJoyStickSpinning()
    {
        StartCoroutine(JoyStickSpinningDetection());

        
    }

    IEnumerator JoyStickSpinningDetection()
    {

        // Check if the angle between the old input and current one is more or equal to the given angle limit value
        CheckNorthEastSouthWest();

        CheckJoystickMotion();

        if(CheckIfFullCircle() && accessQTEAntenna.CurrentlyOnUnscrew)
        {
            countFullCircle++;
            for (int i = 0; i < flagPosition.Length; i++)
            {
                flagPosition[i] = false;
            }
        }


        // Copy current input value to use it after the coroutine waits
        lastJoyStickInput = joyStickInput;

        yield return new WaitForSeconds(0.1f);
    }

    void CheckNorthEastSouthWest()
    {

        //Is it North?
        if (joyStickAngle >= range[JoystickDirections.North].x && joyStickAngle < range[JoystickDirections.North].y)
        {
            //if (lastJoyStickAngle < range[JoystickDirections.North].x || lastJoyStickAngle > range[JoystickDirections.North].y)
            {
                flagPosition[0] = true;
                currentJoystickDirection = JoystickDirections.North;
            }
        }
        if (joyStickAngle >= range[JoystickDirections.East].x && joyStickAngle < range[JoystickDirections.East].y)
        {
            //if (lastJoyStickAngle < range[JoystickDirections.East].x || lastJoyStickAngle > range[JoystickDirections.East].y)
            {
                flagPosition[1] = true;
                currentJoystickDirection = JoystickDirections.East;

            }
        }
        if (joyStickAngle >= range[JoystickDirections.South].x || joyStickAngle < range[JoystickDirections.South].y)
        {
            //if (lastJoyStickAngle < range[JoystickDirections.South].x || lastJoyStickAngle > range[JoystickDirections.South].y)
            {
                flagPosition[2] = true;
                currentJoystickDirection = JoystickDirections.South;
            }
        }
        if (joyStickAngle >= range[JoystickDirections.West].x && joyStickAngle < range[JoystickDirections.West].y)
        {
            //if (lastJoyStickAngle < range[JoystickDirections.West].x || lastJoyStickAngle > range[JoystickDirections.West].y)
            {
                flagPosition[3] = true;
                currentJoystickDirection = JoystickDirections.West;
            }
        }

    }

    void CheckJoystickMotion()
    {
        for (int i = 1; i < flagPosition.Length; i++)
        {
            if (!flagPosition[i - 1] && flagPosition[i])
            {
                for (int x = 0; x < flagPosition.Length; x++)
                {
                    flagPosition[x] = false;
                }
            }
        }

    }


    bool CheckIfFullCircle()
    {
        foreach (bool item in flagPosition)
        {
            if (!item) return false;
        }
        return true;
    }

    #endregion
}
