using GameFlow.Questing;

namespace UI.OptionMenus.DialogueOptions {
    public interface OptionInterface {
        public string GetName();
        public TaskSO GetTask();
        public InteractionSO GetInteraction();
        public InteractionSO SelectOption();
    }
}