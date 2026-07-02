using UI.OptionMenus.DialogueOptions;
using UnityEngine;

namespace GameFlow.EventChannels.SpecificEC {
    [CreateAssetMenu(fileName = "DialogueOptionsEventChannel", menuName = "Events/Dialogue Options Channel", order = 3)]
    public class DialogueOptionsChannelSO : ScriptableObject {
        public delegate void OptionsMenuEvent(DialogueOptionsArgs value);
        public event OptionsMenuEvent OnOptionsMenuListed;
        
        public delegate void OptionSelectedEvent(InteractionSO value, NarrationSpeaker actor);
        public event OptionSelectedEvent OnOptionSelected;

        public void ProvideOptions(DialogueOptionsArgs value) {
            OnOptionsMenuListed?.Invoke(value);
        }
        
        public void SelectOption(InteractionSO value, NarrationSpeaker actor) {
            OnOptionSelected?.Invoke(value, actor);
        }
    }
}