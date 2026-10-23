using GameFlow.EventChannels.GeneralEC;
using GameFlow.EventChannels.SpecificEC;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Video;

namespace Events {
    public class VideoEvent : MonoBehaviour {
        [Header("Video")]
        [SerializeField] private VideoClip videoClip;
        [SerializeField] private VideoChannelSO videoChannel;

        [Header("Event Start Trigger")]
        [SerializeField] private VoidEventChannelSO eventStartTrigger;
        
        [Header("Event End Trigger")]
        [SerializeField] private VoidEventChannelSO eventEndTrigger; 

        private void Awake() {
            eventStartTrigger.OnVoidEventRaised += StartEvent;
        }

        private void OnDestroy() {
            eventStartTrigger.OnVoidEventRaised -= StartEvent;
        }

        private void StartEvent() {
            videoChannel.OnVideoEnd += EndEvent;
            videoChannel.UpdateVideoClip(videoClip);
        }

        private void EndEvent() {
            videoChannel.OnVideoEnd -= EndEvent;
            eventEndTrigger.RaiseEvent();
        }
    }
}