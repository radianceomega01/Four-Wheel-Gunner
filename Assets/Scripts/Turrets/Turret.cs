using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform gunPosition;
    [SerializeField] Transform axle;

    private float criticalDistance = 5f;
    float rotationSpeed = 0.5f;
    private Gun gun;

    Vector3 targetDir;

    public static event Action OnDestroyed;
    public void Initialize(Gun turretGun)
    {
        gun = Instantiate(turretGun, gunPosition.position, Quaternion.identity, gunPosition);
        axle.LookAt(GameManager.Instance.GetPlayerCar());
    }

    private void FixedUpdate()
    {
        if ((GameManager.Instance.GetPlayerCar().position - transform.position).magnitude < criticalDistance)
        {
            targetDir =  GameManager.Instance.GetPlayerCar().position - axle.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            axle.rotation = Quaternion.Lerp(axle.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            
            //gun.PrepareToShoot(true);
        }
        else
        {
            gun.PrepareToShoot(false);
        }
    }
}
