using System;
using UnityEngine;

public class MouseLook : MonoBehaviour{

    public enum RotationAxes {
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes axes;
    public Camera camera;
    public float sensivityHor = 3f;
    public float sensivityVer = 3f;
    public float minumumVert = -45f;
    public float maximumVert = 45f;
    public float TowerRotateSpeed = 10f;

    private float _rotationX = 0;

    private void Update() {
        
        
        //реализация плавного поворота башни вслед за камерой и движение ствольного орудия
        if (axes == RotationAxes.MouseX) {         
            //получаем вектор направления
            Vector3 targetPoint = camera.transform.position + camera.transform.forward * 500; 
            Vector3 direction = transform.position - targetPoint;
            //следим за камерой
            Quaternion deltaAngle = Quaternion.LookRotation( transform.forward,direction);
            //плавно вращаем башню танка
            transform.rotation = Quaternion.RotateTowards(transform.rotation, deltaAngle, TowerRotateSpeed * Time.deltaTime);
        } else if (axes == RotationAxes.MouseY) { 
            _rotationX -= Input.GetAxis("Mouse Y") * sensivityVer;
            _rotationX = Mathf.Clamp(_rotationX, minumumVert, maximumVert);
            float rotationY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }

    }
}