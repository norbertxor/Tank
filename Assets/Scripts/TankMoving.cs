using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMoving : MonoBehaviour {
    public float speed = 200f;
    public float rotationSpeed = 15f;
    public Transform tankBody;
    
    private Rigidbody _rb;
    private float _direction = 1;
	
    private void Start() {
        _rb = GetComponent<Rigidbody>();
        
    }

    private void Update(){ 
        if (Input.GetAxis("Vertical") > 0 && _rb.velocity.magnitude < 7) {
            _rb.AddRelativeForce(Vector3.forward * speed);
            _direction = 1;
        }
        if (Input.GetAxis("Vertical") < 0 && _rb.velocity.magnitude < 3) {
            _rb.AddRelativeForce(Vector3.back * speed);
            _direction = -1;
        }
        tankBody.rotation = transform.rotation * Quaternion.Euler( -90 - _rb.velocity.magnitude*_direction,0,0);
        float deltaRotate = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        Quaternion rotateAngle = Quaternion.Euler(0, deltaRotate * Mathf.Sign(Input.GetAxis("Vertical")), 0);
        Quaternion currentAngle = _rb.rotation * rotateAngle;
        _rb.MoveRotation(currentAngle);
    }

}