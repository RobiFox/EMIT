using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelessZoneSound : MonoBehaviour {
    private float _vel;
    private float _velDistort;
    private AudioSource _source;
    private AudioDistortionFilter _distortion;
    void Start() {
        DontDestroyOnLoad(gameObject);

        _source = GetComponent<AudioSource>();
        _distortion = GetComponent<AudioDistortionFilter>();
        
        Destroy(gameObject, 5f);
    }

    void Update() {
        _source.volume = Mathf.SmoothDamp(_source.volume, 0f, ref _vel, 1f);
        if (_distortion != null) {
            _distortion.distortionLevel = Mathf.SmoothDamp(_distortion.distortionLevel, 0f, ref _velDistort, 1f);
            if(_distortion.distortionLevel < 0.05f)
                Destroy(_distortion);
        }
    }
}
