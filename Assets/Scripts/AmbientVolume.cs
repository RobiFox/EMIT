using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AmbientVolume : MonoBehaviour {
    public float ambientVolume;

    private float _ambientVelocity;

    public AudioSource AudioSource { get; private set; }

    void Start() {
        if (FindObjectsOfType<AmbientVolume>().Length > 1) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        
        AudioSource = GetComponent<AudioSource>();
        AudioSource.volume = 0;
        ambientVolume = SettingsManager.instance.AmbientVolume;
    }

    void Update() {
        AudioSource.volume = Mathf.SmoothDamp(AudioSource.volume, ambientVolume, ref _ambientVelocity, 1f);
    }
}
