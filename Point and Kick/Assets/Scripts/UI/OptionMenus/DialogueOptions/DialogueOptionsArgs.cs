using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.OptionMenus.DialogueOptions {
    [Serializable]
    public class DialogueOptionsArgs {
        private List<OptionInterface> optionInteractions;
        private Vector3 optionsPosition;
        private NarrationSpeaker optionActor;
        private bool positionedRight;
        
        public DialogueOptionsArgs(List<OptionInterface> interactions, Vector3 position, NarrationSpeaker actor, bool isRight) {
            optionInteractions = interactions;
            optionsPosition = position;
            optionActor = actor;
            positionedRight = isRight;
        }

        public List<OptionInterface> GetOptionInteractions() {
            return optionInteractions;
        }
        
        public Vector3 GetOptionPositions() {
            return optionsPosition;
        }
        
        public NarrationSpeaker GetActor() {
            return optionActor;
        }
        
        public bool IsPositionedRight() {
            return positionedRight;
        }
    }
}