using System;
using Audio;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Mechanics {
    public class ButtonActivator : Toggleable {
        [Serializable] public class OnActivate : UnityEvent { }
        [SerializeField] private OnActivate onActivate = new OnActivate();
        [SerializeField] private OnActivate onDeactivate = new OnActivate();
        
        private Animator _animator;
        private AudioPlayer _audioPlayer;
        private static readonly int Click = Animator.StringToHash("click");

        public override void Start() {
            _animator = GetComponent<Animator>();
            _audioPlayer = GetComponent<AudioPlayer>();
            
            base.Start();
        }

        public override void SetStatus(bool s) {
            if (status == s) return;
            base.SetStatus(s);
            if(s) onActivate.Invoke();
            else onDeactivate.Invoke();
            _animator.SetTrigger(Click);
            _audioPlayer.Pitch = Random.Range(0.9f, 1.1f);
            _audioPlayer.PlayAudio(0.5f);
        }
    }
}
