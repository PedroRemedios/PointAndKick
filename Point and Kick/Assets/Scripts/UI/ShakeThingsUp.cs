using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeThingsUp : MonoBehaviour
{
    #region Variables

    private bool shaking;
    private float _shakeValue;
    private float _waitTime;

    #endregion

    #region Start, Update

    // Start is called before the first frame update
    private void Start()
    {
        shaking = false;
        _shakeValue = 0.0f;
        _waitTime = 0.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        // Use 'shaking' boolean to know if the UI should be currently shaking. If true, then shake it
        if (shaking)
        {
            //Calculate newPosition to give the impression of a shake
            Vector3 newPosition = new Vector3(transform.position.x + Random.insideUnitSphere.x * (Time.deltaTime * _shakeValue), transform.position.y, transform.position.z);

            //Realocate the UI. In other words... Shake it
            transform.position = newPosition;
        }
    }

    #endregion

    #region Other Function

    /// <summary>
    /// Function called to get the values 
    /// </summary>
    /// <param name="shakeValue">Variable that holds the value of the shake</param>
    /// <param name="waitTime">Variable that holds the value of wait time while shaking</param>
    public void Trigger(float shakeValue, float waitTime)
    {
        //Get tha values to the variables on this Script to be used by it
        _shakeValue = shakeValue;
        _waitTime = waitTime;

        //Start Coroutine that will Shake the UI over time
        StartCoroutine(Shake());
    }

    /// <summary>
    /// Coroutine that Shakes the UI over time
    /// </summary>
    /// <returns></returns>
    IEnumerator Shake()
    {
        // Save the original Position of the UI. Once the shaking stops, the UI needs to return to its original place.
        Vector3 originalPosition = transform.position;

        //If not shaking, let's shake it - The shake stuff is on the Update function
        if (!shaking)
        {
            shaking = true;
            
        }

        //If wait time is not over, let's wait that time. 
        if (_waitTime != 0)
        {
            yield return new WaitForSeconds(_waitTime);

            //When we get here, the wait time is over
            shaking = false;
            transform.position = originalPosition;
        }
        //If the _waitTime came as zero, we need to shake infinitely
        else
        {
            yield return new WaitForSeconds(1.0f);
        }
        
    }

    #endregion
}
