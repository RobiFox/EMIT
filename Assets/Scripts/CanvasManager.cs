using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Mechanics;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {
    public TextMeshProUGUI midText;
    public GameObject midTextPanel;
    private Animator _midTextAnimator;

    public GameObject fadePanel;
    public GameObject crosshair;

    private Image _crosshair;

    private ObjectGrabber _objectGrabber;

    public ObjectGrabber ObjectGrabber {
        get => _objectGrabber;
        set => _objectGrabber = value;
    }

    private Coroutine _timer;
    private static readonly int Trigger = Animator.StringToHash("trigger");
    private static readonly int Show = Animator.StringToHash("show");
    private static readonly int Restart = Animator.StringToHash("restart");
    private static readonly int White = Animator.StringToHash("white");

    void Start() {
        _midTextAnimator = midTextPanel.GetComponent<Animator>();
        
        _objectGrabber = FindObjectOfType<ObjectGrabber>();

        _crosshair = crosshair.GetComponent<Image>();
        crosshair.SetActive(false);
    }

    void Update() {
        if (SettingsManager.instance.CrosshairType == 0) return;
        RaycastHit raycast;
        bool canInteract = false;
        if (_objectGrabber == null) return;
        crosshair.SetActive((canInteract = (raycast = _objectGrabber.Raycast()).collider != null 
                                           && ((raycast.collider.GetComponent<GrabbableCube>() != null 
                                           && raycast.collider.GetComponent<GrabbableCube>().IsAvailableForGrab()) 
                                           || raycast.collider.GetComponent<ButtonActivator>() != null))
                            || SettingsManager.instance.CrosshairType == 2 
                            || _objectGrabber.IsHolding);
        if (SettingsManager.instance.CrosshairType == 2) {
            Color c = _crosshair.color;
            c.a = canInteract ? 1f : 0.33f;
            _crosshair.color = c;
        }
    }
    
    public void SetTutorialGrab() {
        SetMidText(Messages.Instance.GetMessage("tutorial.grab_cube", "[E]"));
    }
    
    public void SetTutorialRestart() {
        SetMidText(Messages.Instance.GetMessage("tutorial.restart", "[R]"), 60f);
    }
    
    public void SetTutorialInteract() {
        SetMidText(Messages.Instance.GetMessage("tutorial.interact", "[E]"));
    }

    public void SetTutorialJump() {
        SetMidText(Messages.Instance.GetMessage("tutorial.jump", "[" + Messages.Instance.GetMessage("input.space") + "]"));
    }
    
    public void SetTutorialSlow() {
        SetMidText(Messages.Instance.GetMessage("tutorial.slow_time", "[" + Messages.Instance.GetMessage("input.right_mouse_button") + "]"));
    }
    
    public void SetTutorialRewind() {
        SetMidText(Messages.Instance.GetMessage("tutorial.rewind_time", "[" + Messages.Instance.GetMessage("input.left_mouse_button") + "]"), 5f);
    }
    
    public void SetTutorialRewindTip() {
        SetMidText(Messages.Instance.GetMessage("tutorial.rewind_time_tip"));
    }
    
    public void SetTutorialThrow() {
        SetMidText(Messages.Instance.GetMessage("tutorial.throwing"));
    }
    
    public void SetTutorialAntiCube() {
        SetMidText(Messages.Instance.GetMessage("tutorial.anti_cube"));
    }
    
    public void SetTutorialSpatialCube() {
        SetMidText(Messages.Instance.GetMessage("tutorial.spatial_cube"));
    }

    public void SetMidText(string text, float time = 10f) {
        if (_timer != null) {
            HideMidText();
        }

        _timer = StartCoroutine(ShowMessage(text, time));
    }

    public void HideMidText() {
        if(_timer != null) StopCoroutine(_timer);
        if (!_midTextAnimator.GetBool(Show)) return;
        midText.text = "";
        _midTextAnimator.SetTrigger(Trigger);
        _midTextAnimator.SetBool(Show, false);
    }

    IEnumerator ShowMessage(string text, float time) {
        _midTextAnimator.SetTrigger(Trigger);
        _midTextAnimator.SetBool(Show, true);
        var match = Regex.Matches(text, @"{key.\w+}");
        for (int i = 0; i < match.Count; i++) {
            //text = text.Replace(match[i].Name, Input.GetButtonDown()); TODO
        }
        midText.text = text;
        yield return new WaitForSeconds(time);
        HideMidText();
    }

    public void RestartFadePanel() {
        fadePanel.GetComponent<Animator>().SetTrigger(Restart);
    }
    
    public void SetWhite() {
        fadePanel.GetComponent<Animator>().SetTrigger(White);
    }
}
