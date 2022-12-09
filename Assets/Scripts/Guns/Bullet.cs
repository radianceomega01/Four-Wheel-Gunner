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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Turret")
        {

        }
        else if (collision.gameObject.tag == "Player")
        { 
        
        }
        gameObject.SetActive(false);
        transform.position = Vector3.zero;
        transform.SetParent(GameManager.Instance.GetBulletPool());
    }
}
