using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting : MonoBehaviour {

    public GameObject shootVFX;
    public Transform firePlace;
    public GameObject bullet;
    public float shootForce;
    public GameObject pointer;
    
    
    private Rigidbody _rb;
    private float _shootTimer = 0;
    void Start() {
        _rb = GetComponent<Rigidbody>();
        
    }

    void Update() {
        _shootTimer = Mathf.Clamp(_shootTimer - Time.deltaTime, 0f, 5f);
        
        //стреляем
        if (Input.GetMouseButtonDown(0) && _shootTimer == 0) {
            //получаем направление выстрела, делаем отдачу,перзаряжаемся
            Vector3 direction = transform.forward - firePlace.forward * 10;
            direction.y = 0;
            _rb.AddRelativeForce(direction  * shootForce, ForceMode.Impulse);
            Instantiate(shootVFX, firePlace.position, Quaternion.identity);
            Instantiate(bullet, firePlace.position, firePlace.rotation);
            _shootTimer = 3f;
            Debug.Log(direction);
        }

        //рисуем прицел
        RaycastHit hit;
        Ray ray = new Ray(firePlace.position, firePlace.up*-1);
        if (Physics.Raycast(ray, out hit, 50f)) {
            pointer.SetActive(true);
            pointer.transform.position = hit.point;
        }
        else
            pointer.SetActive(false);
    }
}
