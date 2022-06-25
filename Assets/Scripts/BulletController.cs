using System;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float shootForce;
    public GameObject explosionVFX;
  
    
    private Rigidbody _rb;


    private void Awake() {
        _rb = GetComponent<Rigidbody>();
    }

    //добавляем скорость при появлении(выстреле)
    private void OnEnable() {
        _rb.AddRelativeForce(Vector3.down * shootForce, ForceMode.VelocityChange);
        Destroy(gameObject, 10f);
    }

    //взрываем снаряд при соприкосновении с колайдером
    private void OnCollisionEnter(Collision collision) {
        Instantiate(explosionVFX, transform.position, Quaternion.Euler(0, 0, 0));
        //делаем взрыв в радиусе попадания
        Collider[] colliders = Physics.OverlapSphere(transform.position, 20f);
        foreach (var item in colliders) {
            var rigidBody = item.GetComponent<Rigidbody>();
            if (rigidBody != null)
                rigidBody.AddExplosionForce(600f, transform.position, 10f);
        }
        Destroy(gameObject);
    }
}