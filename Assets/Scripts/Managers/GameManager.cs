using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] List<GameObject> carList;
    [SerializeField] List<GameObject> gunList;
    [SerializeField] Transform playerCarParent;
    [SerializeField] Transform playerCarSpawnPos;
    [SerializeField] Transform bulletPool;
    [SerializeField] Transform activeBulletsParent;

    public event Action OnCarSpawned;
    private void Awake()
    {
        if (Instance == null || Instance != this)
            Instance = this;
    }

    private void Start()
    {
        SpawnPlayerCar();
    }

    public Transform GetBulletPool() => bulletPool;
    public Transform GetActiveBulletsParent() => activeBulletsParent;
    private void SpawnPlayerCar()
    {
        GameObject instantiatedCar =  Instantiate(carList[CarSpawnDetails.Instance.CarIndex], 
            playerCarSpawnPos.position, Quaternion.identity, playerCarParent);

        CarGunPositions carGunPositions = instantiatedCar.GetComponent<CarGunPositions>();

        for (int i = 0; i < carGunPositions.GetGunPositionTransform().childCount; i++)
        {
            if(CarSpawnDetails.Instance.GunPosition[i] != -1)
                Instantiate(gunList[CarSpawnDetails.Instance.GunPosition[i]],
                    carGunPositions.GetPosition(i).position, Quaternion.identity, carGunPositions.GetPosition(i));
        }
        instantiatedCar.tag = "Player";
        OnCarSpawned.Invoke();
    }
}
