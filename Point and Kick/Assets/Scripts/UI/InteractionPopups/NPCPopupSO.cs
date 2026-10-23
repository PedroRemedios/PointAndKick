using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/UI/Interaction Popup/NPCPopup")]
public class NPCPopupSO : InteractionPopupSO
{
    private void Awake() {
        hasDialogue = true;
        hasLookAt = true;
    }
}
