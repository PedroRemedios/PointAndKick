using GameFlow.EventChannels.SpecificEC;
using GameFlow.Questing;

namespace UI.OptionMenus.DialogueOptions {
    public class TaskOption : OptionInterface {
        private TaskSO task;
        
        public TaskOption(TaskSO newTask) {
            task = newTask;
        }

        public string GetName() {
            return task.name;
        }

        public TaskSO GetTask() {
            return task;
        }

        public InteractionSO GetInteraction() {
            return null;
        }

        public InteractionSO SelectOption() {
            return task.TaskInteractionSelected();
        }
    }
}