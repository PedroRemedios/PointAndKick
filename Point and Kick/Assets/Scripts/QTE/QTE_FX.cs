using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTE_FX : MonoBehaviour
{
    #region Variables
    [SerializeField] private Image mImage;
    [SerializeField] private Sprite[] mSprites;
    [SerializeField] private int frameRate;
    [SerializeField] private int mCurrentFrame;

    private float mTimePerFrame;
    #endregion

    #region Awake, Start, Update, OnEnable

    // Runs before Start. Used to initialize components
    private void Awake()
    {
        // Initialize variables with Unity Components
        mImage = GetComponent<Image>();

        //Initialize other variables
        mTimePerFrame = 0.0f;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        mCurrentFrame = 0;
        //This should not run once the Start function is called.
        this.gameObject.SetActive(false);

        //Function to Load the frames of the spritesheet for the positive fx 
        LoadSpriteSheet();
    }

    //This will run everytime the Object that holds this script will become enable again
    void OnEnable()
    {
        StartCoroutine(MakeSpriteAppearOnScreen());
    }

    // Update is called once per frame
    private void Update()
    {
        //If we run out of frames to display, stop coroutine that will show them
        if (mCurrentFrame >= 20)
        {
            StopCoroutine(MakeSpriteAppearOnScreen());
            mCurrentFrame = 0;
        }
        
    }
    #endregion

    #region Other Functions

    /// <summary>
    /// Function that activates the Sprite and shows it on screen
    /// </summary>
    /// <returns></returns>
    IEnumerator MakeSpriteAppearOnScreen()
    {
        //Do while instead of While - Because It's necessary to run the 
        do
        {
            ++mCurrentFrame;
            SetSprite();

            if (mCurrentFrame >= mSprites.Length)
            {
                mCurrentFrame = 0;
                this.gameObject.SetActive(false);

            }
            yield return new WaitForSeconds(mTimePerFrame);

        } while (mCurrentFrame < mSprites.Length);
    }

    /// <summary>
    /// Function to Set the Sprite as the Image to appear on the screen
    /// </summary>
    void SetSprite()
    {
        //Set the correct current sprite
        if (mCurrentFrame >= 0 && mCurrentFrame < mSprites.Length)
        {

            mImage.sprite = mSprites[mCurrentFrame];
        }
    }

    /// <summary>
    /// Load all sprites available to make them appear on screen when the time comes
    /// </summary>
    void LoadSpriteSheet()
    {
        //There is frames to load? Yes or no?
        if (mSprites != null && mSprites.Length > 0)
        {
            mTimePerFrame = 1f / frameRate;
            Play();
        }
        else
        {
            Debug.LogError("Failed to Load SpriteSheet");
        }
    }

    /// <summary>
    /// Function to Activate the gameobject that holds this script
    /// </summary>
    public void Play()
    {
        //Activate the Object... Means, run OnEnable() function
        this.gameObject.SetActive(true);
    }
    #endregion
}
