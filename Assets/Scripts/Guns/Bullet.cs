using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage { get; private set; }
    public void Initialize(float damage)
    {
        Damage = damage;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Turret")
        {
            collider.GetComponent<Turret>().TakeDamage(Damage);
        }
        else if (collider.tag == "Player")
        {
            collider.GetComponent<Car>().TakeDamage(Damage);
        }
        gameObject.SetActive(false);
        transform.position = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.SetParent(GameManager.Instance.GetBulletPool());
    }
}
