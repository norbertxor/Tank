using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting : MonoBehaviour {

    public GameObject shootVFX;
    public Transform firePlace;
    public GameObject bullet;
    public GameObject pointer;
    public GameObject reloadAnimation;
    public float shootRecoilForce;
    
    
    private Rigidbody _rb;
    private float _reloadTimer = 0;

    void OnGUI() {
        if (GUI.Button(new Rect(10, 10, 100, 50), "Lock Cursor")) {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Start() {
        _rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;
        _reloadTimer = Mathf.Clamp(_reloadTimer - Time.deltaTime, 0f, 5f);
        if(_reloadTimer > 0)
            reloadAnimation.SetActive(true);
        else 
            reloadAnimation.SetActive(false);
        if (Input.GetMouseButtonDown(0) && _reloadTimer == 0) {
            Vector3 direction = transform.forward - firePlace.up * 10;
            direction.y /= 8;
            _rb.AddForce(direction * -1 * shootRecoilForce, ForceMode.Impulse);
            Instantiate(shootVFX, firePlace.position, Quaternion.identity);
            Instantiate(bullet, firePlace.position, firePlace.rotation);
            _reloadTimer = 3f;
            Debug.Log(direction);
        }
       //прицел
        RaycastHit hit;
        Ray ray = new Ray(firePlace.position - new Vector3(0,1f,0), firePlace.up*-1);
        if (Physics.Raycast(ray, out hit, 100f)) {
            pointer.SetActive(true);
            pointer.transform.position = hit.point;
        }
        else
            pointer.SetActive(false);
    }
}
