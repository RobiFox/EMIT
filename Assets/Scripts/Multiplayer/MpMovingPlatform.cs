using Mechanics;
using UnityEngine;

namespace Multiplayer {
    public class MpMovingPlatform : MovingPlatform {
        private NetworkToggleable _movingPlatform;
        private LocalPlayer _player;
        public bool isForHost;
        public Material otherMaterial;
        
        public new bool status {
            get {
                return _movingPlatform.status;
            }
            set {
                if (!_movingPlatform.isServer) return;
                _movingPlatform.status = value;
                base.SetStatus(value);
            }
        }

        public new void Start() {
            _movingPlatform = GetComponent<NetworkToggleable>();
            base.Start();
        }

        public new void Update() {
            if (_player == null) {
                if((_player = FindObjectOfType<LocalPlayer>()) != null)
                    if(!IsForThisPlayer()) {
                        GetComponent<MeshRenderer>().material = TargetMaterial = activatedMaterial = deactivatedMaterial = otherMaterial;
                        BoxCollider boxCollider = GetComponent<BoxCollider>();
                        var size = boxCollider.size;
                        size = new Vector3(size.x * 0.9f, size.y * 0.99f, size.z * 0.9f);
                        boxCollider.size = size;
                    }
            }

            base.Update();
        }
        
        public new void FixedUpdate() {
            //transform.localPosition = Vector3.Lerp(transform.localPosition, _target, speed * Time.fixedDeltaTime);
            Move(Target, ref current);
        }

        public new void Move(Vector3 target, ref Vector3 current) {
            transform.position = Vector3.SmoothDamp(transform.position, target, ref current, 0.5f, speed, Time.fixedDeltaTime);
        }
        
        private bool IsForThisPlayer() {
            return _player.isServer && isForHost || !_player.isServer && !isForHost;
        }

        public new void SetStatus(bool s) {
            status = s;
            base.SetStatus(s);
        }
        
        protected override void Rewind() {
            /*if (!IsForThisPlayer()) return;
            base.Rewind();*/
        }

        protected override void Record() {
            /*if (!IsForThisPlayer()) return;
            base.Record();*/
        }

        public override int PositionCount() {
            /*if (!IsForThisPlayer()) return 0;
            return base.PositionCount();*/
            return 0;
        }
        
        public override bool IsPositionsNull() {
            /*if (!IsForThisPlayer()) return true;
            return base.IsPositionsNull();*/
            return true;
        }
    }
}