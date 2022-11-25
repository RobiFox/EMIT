using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager {
    private static SettingsManager _instance;
    public static SettingsManager instance {
        get { return _instance ?? (_instance = new SettingsManager()); }
    }
    
    private bool _invertX = PlayerPrefs.GetInt("InvertX", 0) == 1;
    private bool _invertY = PlayerPrefs.GetInt("InvertY", 0) == 1;
    private float _mouseSensitivity = PlayerPrefs.GetFloat("Sensitivity", 2);
    private float _masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1);
    private float _ambientVolume = PlayerPrefs.GetFloat("AmbientVolume", 0.1f);
    
    private int _showCube = PlayerPrefs.GetInt("ShowCube", 2); // Show In FPS - Show Both - Show Helper only
    private int _crosshairType = PlayerPrefs.GetInt("Crosshair", 2); // Never - Sometimes - Always

    public bool InvertX {
        get => _invertX;
        set {
            _invertX = value;
            PlayerPrefs.SetInt("InvertX", value ? 1 : 0);
        }
    }
    
    public bool InvertY {
        get => _invertY;
        set {
            _invertY = value;
            PlayerPrefs.SetInt("InvertY", value ? 1 : 0);
        }
    }

    public float MouseSensitivity {
        get => _mouseSensitivity;
        set {
            _mouseSensitivity = value;
            PlayerPrefs.SetFloat("Sensitivity", value);
        }
    }
    
    public float MasterVolume {
        get => _masterVolume;
        set {
            _masterVolume = value;
            PlayerPrefs.SetFloat("MasterVolume", value);
            AudioListener.volume = value;
        }
    }
    
    public float AmbientVolume {
        get => _ambientVolume;
        set {
            _ambientVolume = value;
            PlayerPrefs.SetFloat("AmbientVolume", value);
            AmbientVolume am = Object.FindObjectOfType<AmbientVolume>();
            if (am != null) {
                am.ambientVolume = value;
                am.AudioSource.volume = value;
            }
        }
    }

    public int ShowCube {
        get => _showCube;
        set {
            _showCube = value;
            PlayerPrefs.SetInt("ShowCube", value);
        }
    }

    public int CrosshairType {
        get => _crosshairType;
        set {
            _crosshairType = value;
            PlayerPrefs.SetInt("Crosshair", value);
        }
    }
}
