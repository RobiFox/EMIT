using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    private CharacterController charControl;
    public bool enable = true;

    [SerializeField] private float speed = 4.5F;
    private float jumpSpeed = 5.75F;
    private float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    public Vector3 MoveDirection => moveDirection;

    private float _x = 0;
    private float _z = 0;
    private float _xVel = 0;
    private float _zVel = 0;
    private const float SmoothTime = 0.085f;
    private const float SmoothTimeAir = 0.35f;
    void Update() {
        if (!enable) {
            return;
        }
        CharacterController controller = GetComponent<CharacterController>();
        Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), transform.TransformDirection(Vector3.up), Color.red);
        if (controller.isGrounded) {
            _x = Mathf.SmoothDamp(_x, Input.GetAxisRaw("Horizontal"), ref _xVel, SmoothTime, Mathf.Infinity, Time.unscaledDeltaTime);
            _z = Mathf.SmoothDamp(_z, Input.GetAxisRaw("Vertical"), ref _zVel, SmoothTime, Mathf.Infinity, Time.unscaledDeltaTime);
            
            moveDirection = Vector3.ClampMagnitude(new Vector3(_x, 0, _z), 1f);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButtonDown("Jump") && !Raycast()) {
                moveDirection.y = jumpSpeed;
            }
        } else {
            _x = Mathf.SmoothDamp(_x, Input.GetAxisRaw("Horizontal"), ref _xVel, SmoothTimeAir, Mathf.Infinity, Time.unscaledDeltaTime);
            _z = Mathf.SmoothDamp(_z, Input.GetAxisRaw("Vertical"), ref _zVel, SmoothTimeAir, Mathf.Infinity, Time.unscaledDeltaTime);
            
            
            moveDirection = new Vector3(_x, moveDirection.y, _z);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection.x *= speed;
            moveDirection.z *= speed;
        }

        /*if (IsObjectAbove(controller.radius)
                && moveDirection.y > 0) {
            moveDirection.y = 0;
        }*/
        
        if(Raycast() && moveDirection.y > 0) {
            moveDirection.y = 0;
        }
        moveDirection.y -= gravity * Time.unscaledDeltaTime;
        controller.Move(moveDirection * Time.unscaledDeltaTime);
        
        //Debug.Log(Time.unscaledDeltaTime);
    }

	// Use this for initialization
	void Start() {
        charControl = GetComponent<CharacterController>();
	}

    bool Raycast() {
        /*for (float x = -0.5f; x <= 0.5f; x += 0.5f) {
            for (float z = -0.5f; z <= 0.5f; z += 0.5f) {
                float r = 0;
                if (!(x == 0 && z == 0)) {
                    r = 0.5f;
                }
                if ((Mathf.Abs(x) != Mathf.Abs(z) && x != 0) && Physics.Raycast(transform.position + new Vector3(x , 0.5f - r, z),
                    transform.TransformDirection(Vector3.up), out _, 0.3f + r)) {
                        Debug.Log("NEM, A KURVA ANYÁD");
                        return true;
                }
            }
        }*/

        return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out _, 1f);
    }
}
