using System;
using Audio;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menu {
    public class EpicButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        public RectTransform whitePanel;
        public TextMeshProUGUI text;

        private Color _targetTextColor;
        private float _targetSize;
        private float _initialSize;
        private Vector2 _initialPos;
        
        private float speed = 5f;

        private bool _firstStart = true;

        private AudioPlayer _player;

        private void OnEnable() {
            _targetTextColor = Color.white;
            if (_firstStart) return;
            Vector2 size = whitePanel.offsetMax;
            size.x = _initialSize;
            _targetSize = _initialSize;
            whitePanel.offsetMax = size;
        }

        void Start() {
            _player = GetComponent<AudioPlayer>();

            _firstStart = false;
            _targetTextColor = Color.white;
            _targetSize = _initialSize = whitePanel.sizeDelta.x;
            _initialPos = whitePanel.anchoredPosition;

            GetComponent<Button>().onClick.AddListener(Click);
            GetComponent<Button>().onClick.AddListener(NoHover);
        }

        void Click() {
            _player.PlayAudio(_player.clips[0], 0.5f);
        }

        private float vel;
        
        void Update() {
            Vector2 size = whitePanel.offsetMax;
            //size.x = Mathf.Lerp(size.x, _targetSize, Time.deltaTime * speed);
            size.x = Mathf.SmoothDamp(size.x, _targetSize, ref vel, 0.15f, Mathf.Infinity, Time.unscaledDeltaTime);
            whitePanel.offsetMax = size;
            /*Vector2 size = whitePanel.sizeDelta;
            size.x = Mathf.Lerp(size.x, targetSize, Time.deltaTime * speed);
            whitePanel.sizeDelta = size;*/
            /*Vector2 pos = whitePanel.anchoredPosition;
            pos.x = initialPos.x - Math.Abs(size.x) * 2;
            whitePanel.anchoredPosition = pos;
            Debug.Log(size.x + " / " + size.y);*/
            text.faceColor = Color.Lerp(text.faceColor, _targetTextColor, Time.unscaledDeltaTime * speed);
        }

        private void NoHover() {
            Hovered(false);
        }
        
        private void Hovered(bool hover) {
            _targetTextColor = hover ? Color.black : Color.white;
            _targetSize = hover ? 0 : _initialSize;
            if (hover) {
                _player.PlayAudio(_player.clips[1], 0.5f);
            }
        }

        public void OnPointerEnter(PointerEventData eventData) {
            Hovered(true);
        }

        public void OnPointerExit(PointerEventData eventData) {
            Hovered(false);
        }
    }
}
