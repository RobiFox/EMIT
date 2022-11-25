using Audio;
using Multiplayer;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Menu {
    public class PauseMenu : MonoBehaviour {
        private float _previousTime;
        private bool _showMenu;

        public GameObject pauseMenu;

        private PlayerLook _playerLook;
        private PlayerMove _playerMove;
        private ObjectGrabber _objectGrabber;
        
        private GameManager _gameManager;

        public GameObject settings;
        
        public bool ShowMenu {
            get => _showMenu;
            set {
                if (!value && settings.activeSelf) {
                    settings.SetActive(false);
                    return;
                }
                _showMenu = value;
                if(value) ShowPauseMenu();
                else HidePauseMenu();

                if (!(this is MpPauseMenu))
                    foreach (AudioSource source in FindObjectsOfType<AudioSource>()) {
                        float volume = 1f;
                        if (source.GetComponent<AudioPlayer>() != null) {
                            volume = source.GetComponent<AudioPlayer>().OriginalVolume;
                        }
                        source.volume = value ? 0 : volume;
                    }
                
                _playerLook.enabled = !value;
                _playerMove.enabled = !value;
                _objectGrabber.enabled = !value;
                
                //_gameManager.StopAction = true;
            }
        }

        public void Start() {
            pauseMenu.SetActive(false);

            SetPlayerValues();

            _gameManager = GetComponent<GameManager>();
        }

        public virtual void SetPlayerValues() {
            _playerLook = FindObjectOfType<PlayerLook>();
            _playerMove = FindObjectOfType<PlayerMove>();
            _objectGrabber = FindObjectOfType<ObjectGrabber>();
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                ShowMenu = !ShowMenu;
            }
        }

        private void ShowPauseMenu() {
            FreezeTime();
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public virtual void FreezeTime() {
            _previousTime = Time.timeScale;
        }
        
        public virtual void ReturnTime() {
            Time.timeScale = _previousTime;
        }
        
        private void HidePauseMenu() {
            ReturnTime();
            pauseMenu.SetActive(false);
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void ClosePause() {
            ShowMenu = false;
        }

        public void OpenSettings() {
            settings.SetActive(true);
        }

        public void GoToMainMenu() {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu");
        }
    }
}
