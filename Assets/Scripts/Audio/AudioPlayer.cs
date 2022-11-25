using UnityEngine;

namespace Audio {
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour {
        private GameManager _gameManager;
        public AudioClip[] clips;
        private float _originalVolume;

        public float OriginalVolume => _originalVolume;

        public float Pitch {
            get {
                return _audioSource.pitch;
            }
            set {
                _audioSource.pitch = value;
            }
        }
        
        public float Volume {
            get {
                return _audioSource.volume;
            }
            set {
                _audioSource.volume = value;
            }
        }

        private AudioSource _audioSource;

        void Start() {
            _audioSource = GetComponent<AudioSource>();
            _originalVolume = _audioSource.volume;
            _gameManager = FindObjectOfType<GameManager>();

            _audioSource.dopplerLevel = 0f;
        }
        
        public void PlayAudio(float volume) {
            AudioClip clip = clips[Random.Range(0, clips.Length)];
            PlayAudio(clip, volume);
        }
        
        public void PlayAudio(AudioClip clip, float volume) {
            if (_audioSource.pitch < 0) {
                _audioSource.time = clip.length;
            } else {
                _audioSource.time = 0;
            }

            _audioSource.pitch = Pitch;
            _audioSource.PlayOneShot(clip, volume);
        }
    }
}
