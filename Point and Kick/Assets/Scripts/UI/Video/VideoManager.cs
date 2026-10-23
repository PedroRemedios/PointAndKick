using GameFlow.EventChannels.SpecificEC;
using UnityEngine;
using UnityEngine.Video;

namespace UI.Video {
    [RequireComponent(typeof(VideoPlayer))]
    public class VideoManager : MonoBehaviour {
        [SerializeField] private VideoChannelSO videoChannel;
        [SerializeField] private GameObject videoCanvas;
        private VideoPlayer videoPlayer;

        private void Awake() {
            videoPlayer = GetComponent<VideoPlayer>();
            videoPlayer.loopPointReached += EndVideo;
            
            videoChannel.OnVideoClipUpdated += UpdateVideoClip;
            videoChannel.OnVideoPlay += PlayVideo;
            videoChannel.OnVideoPause += PauseVideo;
            
            videoCanvas.SetActive(false);
        }

        private void OnDestroy() {
            videoChannel.OnVideoClipUpdated -= UpdateVideoClip;
            videoChannel.OnVideoPlay -= PlayVideo;
            videoChannel.OnVideoPause -= PauseVideo;
        }

        private void EndVideo(VideoPlayer source) {
            videoChannel.EndVideo();
            videoCanvas.SetActive(false);
        }

        private void UpdateVideoClip(VideoClip videoClip) {
            videoPlayer.clip = videoClip;
            PlayVideo();
        }

        private void PlayVideo() {
            videoCanvas.SetActive(true);
            videoPlayer.Play();
        }
        
        private void PauseVideo() {
            videoPlayer.Pause();
        }
    }
}