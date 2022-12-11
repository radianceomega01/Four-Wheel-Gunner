using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform gunPosition;
    [SerializeField] Transform axle;

    private float criticalDistance = 15f;
    private float shootDistance = 12f;
    private float rotationSpeed = 0.5f;
    private bool shootState;
    private Gun gun;

    private Vector3 targetDir;

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
        }
        if ((GameManager.Instance.GetPlayerCar().position - transform.position).magnitude < shootDistance)
        {
            shootInput(true);
        }
        else
        {
            shootInput(false);
        }
    }

    private void shootInput(bool value)
    {
        if (shootState != value)
        {
            gun.PrepareToShoot(value);
            shootState = value;
        }
    }
}
