using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelFinish : MonoBehaviour {
    private Image _panel;
    private PlayerMove _player;
    private float tolerance = 7.0f;

    private float _beginTime;

    private float vel;

    [Scene] public string scene;

    void Start() {
        _panel = GameObject.FindGameObjectWithTag("Fade").GetComponent<Image>();
        _player = FindObjectOfType<PlayerMove>();
        tolerance *= tolerance;
        _beginTime = Time.time;
    }
    
    void Update() {
        float distance = GetDistanceSquared(transform.position, _player.transform.position);
        Color c = _panel.color;
        
        if (distance < GetDistanceSquared(2.55f)) {
            PlayerPrefs.SetFloat("Time " + SceneManager.GetActiveScene().name, (_beginTime - Time.time));
            PlayerPrefs.SetInt("Completed " + SceneManager.GetActiveScene().name, 1);
            c.a = Mathf.SmoothDamp(c.a, 1, ref vel, 0.05f);
            SceneManager.LoadScene(scene);
        } else if (distance < tolerance) {
            c.a = 1f - (distance-0.0625f)/tolerance;
        } else {
            c.a = 0;
        }
        _panel.color = c;
    }
    
    private float GetDistanceSquared(float dis) {
        return dis * dis;
    }

    private float GetDistanceSquared(Vector3 v1, Vector3 v2) {
        return (float) (Math.Pow(v2.x - v1.x, 2) + Math.Pow(v2.z - v1.z, 2));
    }
}
