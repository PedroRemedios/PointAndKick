using System;
using GameFlow.Questing;

namespace UI.OptionMenus.DialogueOptions {
    public class InteractionOption : OptionInterface {
        private InteractionSO interaction;
        
        public InteractionOption(InteractionSO interact) {
            interaction = interact;
        }

        public string GetName() {
            return interaction.name;
        }

        public TaskSO GetTask() {
            return null;
        }

        public InteractionSO GetInteraction() {
            return interaction;
        }

        public InteractionSO SelectOption() {
            return interaction;
        }
    }
}