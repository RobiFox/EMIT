using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level_Specific {
    public class CrystalCube : MonoBehaviour {
        private GameManager _gameManager;
        private PlayerMove _player;
        private CameraShake _cameraShake;

        public AudioSource _source;
        private bool _changing;
        
        private float _vel;

        [Scene]
        public string nextScene;

        void Start() {
            _changing = false;
            
            _gameManager = FindObjectOfType<GameManager>();
            _player = FindObjectOfType<PlayerMove>();
            _cameraShake = FindObjectOfType<CameraShake>();
        }

        private bool _restarting = false;

        void Update() {
            float mag = (_player.transform.position - transform.position).magnitude;
            _gameManager.TargetGrain = Mathf.Clamp(1f - mag * 0.01f, 0f, 1f);
            _gameManager.TargetChro = Mathf.Clamp((1f - mag * 0.01f) * 2, 0f, 1f);
            _gameManager.TargetLens = Mathf.Clamp((1f - mag * 0.01f) * 66, 0f, 50f);
            
            _cameraShake.SetShake(10f, Mathf.Clamp((100 - mag) * 0.6f, 0, 100), (100 - mag) * 0.05f);

            if (mag <= 15) {
                if (_restarting) return;
                FindObjectOfType<CanvasManager>().SetWhite();
                StartCoroutine(NextScene());
                _restarting = true;
            }

            if (_changing) {
                _source.volume = Mathf.SmoothDamp(_source.volume, 0f, ref _vel, 0.1f);
            }
        }

        IEnumerator NextScene() {
            _changing = true;
            yield return new WaitForSeconds(0.75f);
            SceneManager.LoadScene(nextScene);
        }
    }
}
