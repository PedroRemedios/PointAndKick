using UnityEngine;
using UnityEngine.Video;

namespace GameFlow.EventChannels.SpecificEC {
    [CreateAssetMenu(fileName = "VideoChannel", menuName = "Events/Video Channel", order = 0)]
    public class VideoChannelSO : ScriptableObject {
        public delegate void VideoClipCallback(VideoClip videoClip);
        public event VideoClipCallback OnVideoClipUpdated;

        public delegate void VoidCallback();
        public event VoidCallback OnVideoPlay;
        public event VoidCallback OnVideoPause;
        public event VoidCallback OnVideoEnd;

        public void UpdateVideoClip(VideoClip videoClip) {
            OnVideoClipUpdated?.Invoke(videoClip);
        }

        public void PlayVideo() {
            OnVideoPlay?.Invoke();
        }

        public void PauseVideo() {
            OnVideoPause?.Invoke();
        }

        public void EndVideo() {
            OnVideoEnd?.Invoke();
        }
    }
}