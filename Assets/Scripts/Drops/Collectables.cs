using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public enum Type
    {
        smallHp,
        largeHp,
        smallAmmo,
        largeAmmo
    }

    private float increaseAmount;
    private Type type;
    public void Initialize(Type type)
    {
        this.type = type;
        switch (type)
        { 
            case Type.smallHp:
                increaseAmount = 25;
                break;
            case Type.largeHp:
                increaseAmount = 50;
                break;
            case Type.smallAmmo:
                increaseAmount = 40;
                break;
            case Type.largeAmmo:
                increaseAmount = 80;
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (type == Type.smallHp || type == Type.largeHp)
                collision.gameObject.GetComponent<Car>().IncreaseHealth(increaseAmount);
            else if (type == Type.smallAmmo || type == Type.largeAmmo)
            {
                CarGunPositions carGunPositions = collision.gameObject.GetComponent<CarGunPositions>();
                for (int i = 0; i < carGunPositions.GetGunPositionTransform().childCount; i++)
                {
                    carGunPositions.GetPosition(i).GetChild(0).GetComponent<Gun>().IncreaseBullets((int)increaseAmount);
                }   
            }
            Destroy(gameObject);
        }
    }

}
