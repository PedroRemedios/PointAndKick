using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Narration/Narration Line")]
public class NarrationLine : ScriptableObject
{
    public NarrationSpeaker dialogueSpeaker;
    [TextArea(2, 5)]
    public string dialogueLine;
    public NarrationLine nextLine;

    public bool IsFollowedByLine(NarrationLine line) {
        return nextLine == line;
    }
}
