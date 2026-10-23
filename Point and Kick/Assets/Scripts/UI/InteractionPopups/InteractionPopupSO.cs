using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionPopupSO : ScriptableObject
{
    public Vector3 popupOffset;
    [HideInInspector] public bool hasDialogue;
    [HideInInspector] public bool hasLookAt;
}
