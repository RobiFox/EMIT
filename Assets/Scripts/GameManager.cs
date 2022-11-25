using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using Mechanics;
using Mechanics.Time_Mechanic;
using Menu;
using Player;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {

    public const float SlowestTime = 0.05f;
    private bool _rewind = false;
    private bool _slow = false;
    private PostProcessVolume _profile;
    private ChromaticAberration _chro;
    private LensDistortion _lens;
    private Grain _grain;

    private float _targetChro;
    private float _velChro;

    public float TargetChro {
        get => _targetChro;
        set => _targetChro = value;
    }

    private float _targetLens;
    private float _velLens;

    public float TargetLens {
        get => _targetLens;
        set => _targetLens = value;
    }

    private float _defFov;
    private float _targetFov;
    private float _velFov;

    public float TargetFov {
        get => _targetFov;
        set => _targetFov = value;
    }

    public float DefFov => _defFov;

    private float _targetGrain;
    private float _velGrain;

    public float TargetGrain {
        get => _targetGrain;
        set => _targetGrain = value;
    }

    private float _targetTime;
    private float _velTime;
    
    private Camera _camera;

    private bool _deltaNeeded;

    private float _targetFixedTime;
    private float _velFixedTime;

    public static GameObject Player;
    public static ObjectGrabber ObjectGrabber;

    public bool canRewind = true;
    public bool canSlow = true;

    private bool _stopAction;
    private PauseMenu _pause;

    public AudioPlayer slowTimeSound;
    public AudioPlayer tickSound;
    private float _slowTimeVelocity;
    private float _slowTimeVolumeVelocity;

    private float _nextTick;
    private float _tickFrequency;
    private float _tickVolume;
    private float _tickFrequencySpeed;
    private float _tickVolumeSpeed;

    public bool StopAction {
        get => _stopAction;
        set {
            if (value && (_rewind || _slow))
                _stopAction = true;
            else _stopAction = false;
        }
    }

    public void SetCamera() {
        _camera = Camera.main;
    }

    public void Start() {
        Setup();
    }

    public void Setup() {
        _nextTick = float.MaxValue;
        _tickFrequency = 0f;
        
        SetCamera();
        _pause = GetComponent<PauseMenu>();
        _profile = /*GameObject.FindWithTag("ItemCamera").*/_camera.GetComponent<PostProcessVolume>();
        _chro = _profile.profile.GetSetting<ChromaticAberration>();
        _lens = _profile.profile.GetSetting<LensDistortion>();
        _grain = _profile.profile.GetSetting<Grain>();

        _targetChro = 0;
        _targetFov = _defFov = 60;

        _targetTime = 1f;

        _targetGrain = 0f;

        _deltaNeeded = false;

        Player = FindObjectOfType<PlayerLook>().gameObject;
        ObjectGrabber = Player.GetComponent<ObjectGrabber>();

        PlayerPrefs.SetString(PlayerValues.CurrentProgress, SceneManager.GetActiveScene().name);

        _targetFixedTime = 0.02f;
        
        //InvokeRepeating(nameof(HeartBeat), 0, 1/70f);
        InvokeRepeating(nameof(RepeatingHeartBeat), 0, 0.1f);
    }

    void RepeatingHeartBeat() {
        if (_slow) {
            HeartBeat();
        }
    }
    
    void HeartBeat() {
        slowTimeSound.PlayAudio(1f);
    }

    void Update() {
        if (_pause.ShowMenu) return;
        if (Input.GetKeyDown(KeyCode.R)) {
            RestartGame();
            return;
        }
        _grain.intensity.value = Mathf.SmoothDamp(_grain.intensity, _targetGrain + (_targetGrain > 0.02f ? Mathf.Clamp(Random.Range(-0.25f, 0.25f), 0, 1) : 0), ref _velGrain, 0.3f, Mathf.Infinity, Time.unscaledDeltaTime);
        _chro.intensity.value = Mathf.SmoothDamp(_chro.intensity, _targetChro, ref _velChro, 0.3f, Mathf.Infinity, Time.unscaledDeltaTime);
        _lens.intensity.value = Mathf.SmoothDamp(_lens.intensity, _targetLens, ref _velLens, 0.3f, (IsRewinding() || IsSlowingTime()) ? 3f : Mathf.Infinity, Time.unscaledDeltaTime);
        slowTimeSound.Pitch = Mathf.SmoothDamp(slowTimeSound.Pitch, _slow ? 0.6f : 1, ref _slowTimeVelocity, 0.3f);
        slowTimeSound.Volume = Mathf.SmoothDamp(slowTimeSound.Volume, _slow ? 1f : 0, ref _slowTimeVolumeVelocity, 1f);
        if (Math.Abs(_targetLens) < 0.1f && _lens.intensity.value < 0.01) {
            _lens.intensity.value = 0;
        }
        _camera.fieldOfView = Mathf.SmoothDamp(_camera.fieldOfView, _targetFov, ref _velFov, 0.6f, Mathf.Infinity, Time.unscaledDeltaTime);
        Time.timeScale = Mathf.SmoothDamp(Time.timeScale, _targetTime, ref _velTime, 0.4f, Mathf.Infinity, Time.unscaledDeltaTime);
        Time.fixedDeltaTime = Mathf.SmoothDamp(Time.fixedDeltaTime, _targetFixedTime, ref _velFixedTime, 0.3f, Mathf.Infinity, Time.unscaledDeltaTime);
        if (_deltaNeeded && Math.Abs(Time.timeScale - 1f) < 0.1f && !IsSlowingTime()) {
            Time.fixedDeltaTime = 0.02F;
            _deltaNeeded = false;
        }

        if (Input.GetMouseButtonUp(0) || StopAction) {
            if (canRewind) {
                _targetChro = 0;
                _targetLens = 0;
                _targetFov = _defFov;
                _rewind = false;
                foreach (RewindableObject go in FindObjectsOfType<RewindableObject>()) {
                    go.IsRewinding = false;
                    go.OnRewindEnd();
                }
                foreach (PressureActivator go in FindObjectsOfType<PressureActivator>()) {
                    go.VerifyStatus();
                    //go.InvokeEvent();
                }
                /*foreach (AudioSource go in FindObjectsOfType<AudioSource>()) {
                    go.pitch *= -1;
                }*/
            }
        } else if (Input.GetMouseButtonDown(0)) {
            if (ObjectGrabber.IsHolding) {
                ObjectGrabber.DropCube();
                return;
            }

            if (canRewind) {
                _nextTick = Time.time + 0.2f;
                
                _targetChro = 1;
                _targetLens = 50;
                _targetFov = _defFov * 1.1f;
                _rewind = true;
                foreach (RewindableObject go in FindObjectsOfType<RewindableObject>()) {
                    go.IsRewinding = true;
                    go.OnRewindStart();
                }
                
                /*foreach (AudioSource go in FindObjectsOfType<AudioSource>()) {
                    go.pitch *= -1;
                }*/
                slowTimeSound.PlayAudio(1f);
                for (int i = 0; i < 5; i++) {
                    Invoke(nameof(HeartBeat), i * 0.1f);
                }

                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02f;
            }
        }

        /*if(Input.GetMouseButtonDown(0)) {
            foreach (RewindableObject go in FindObjectsOfType<RewindableObject>()) {
                go.SetRewind(true);
                go.rigidBody.constraints = RigidbodyConstraints.FreezeAll;
            }
        } else if(Input.GetMouseButtonUp(0)) {
            foreach (RewindableObject go in FindObjectsOfType<RewindableObject>()) {
                go.SetRewind(false);
                go.rigidBody.constraints = RigidbodyConstraints.None;
            }
        }*/
        if(!_rewind && canSlow)
            if (Input.GetMouseButtonUp(1) || StopAction) {
                _targetChro = 0;
                _targetLens = 0;
                _targetFov = _defFov;
                _slow = false;
                _targetTime = 1f;
                _targetFixedTime = 0.02f;
                _deltaNeeded = true;
                slowTimeSound.PlayAudio(1f);
                for (int i = 0; i < 5; i++) {
                    Invoke(nameof(HeartBeat), i * 0.5f);
                }
            } else if (Input.GetMouseButtonDown(1)) {
                if (ObjectGrabber.IsHolding && !StopAction) {
                    ObjectGrabber.DropCube();
                    return;
                }
                _targetChro = 1;
                _targetLens = -50;
                _targetFov = _defFov / 1.1f;
                _slow = true;
                _targetTime = SlowestTime;
                _targetFixedTime = 0.02f * SlowestTime;
                slowTimeSound.PlayAudio(1f);
                for (int i = 0; i < 5; i++) {
                    Invoke(nameof(HeartBeat), i * 0.1f);
                }
            }

        _tickVolume = Mathf.SmoothDamp(_tickVolume, _rewind ? 1 : 0, ref _tickVolumeSpeed, _rewind ? 1f : 0.3f);
        _tickFrequency = Mathf.SmoothDamp(_tickFrequency, _rewind ? 10 : 1f, ref _tickFrequencySpeed, _rewind ? 1f : 0.3f);
        
        if (Time.time > _nextTick) {
            _nextTick = Time.time + 1 / _tickFrequency;
            tickSound.Pitch = Random.Range(0.6f, 1.4f);
            tickSound.PlayAudio(_tickVolume);
        }
        
        StopAction = false;
        //Debug.Log("Time scale: " + Time.timeScale);
    }

    public void NextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetRewindAbility(bool ability) {
        canRewind = ability;
    }
    
    public void SetSlowAbility(bool ability) {
        canSlow = ability;
    }

    public bool IsRewinding() {
        return _rewind;
    }

    public bool IsSlowingTime() {
        return _slow;
    }

    private bool _restarting = false;
    
    public void RestartGame() {
        if (_restarting) return;
        _restarting = true;
        StartCoroutine(FadeAndRestart());
    }

    IEnumerator FadeAndRestart() {
        CanvasManager cm = FindObjectOfType<CanvasManager>();
        cm.RestartFadePanel();
        cm.HideMidText();
        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
