using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Narration/Narration Speaker")]
public class NarrationSpeaker : ScriptableObject
{
    [Header("Actor Name")]
    public string speakerName;
    [Header("Actor Portrait")]
    public Sprite speakerSprite;

    [Header("Interaction Options Offsets")]
    public Vector2 offsets;
}
