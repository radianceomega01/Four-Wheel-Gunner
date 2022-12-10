using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    [SerializeField] GameObject turret;
    [SerializeField] Transform turretsParent;
    [SerializeField] Transform turretSpawnPoints;

    private int randomSpawnPoint;
    private int randomTurretGun;
    private int maxTurrets = 3;
    private int spawnThreshold = 1;
    private int activeTurrets;

    private void Start()
    {
        GameManager.Instance.OnCarSpawned += Spawn;
    }

    private void Spawn()
    {
        for (int i = activeTurrets; i < maxTurrets; i++)
        {
            randomTurretGun = Random.Range(0, GameManager.Instance.GetGunList().Count);
            GetRandomSpawnPoint();

            while (turretSpawnPoints.GetChild(randomSpawnPoint).childCount > 0)
                GetRandomSpawnPoint();

            Turret instTurret = Instantiate(turret,
                turretSpawnPoints.GetChild(randomSpawnPoint).position, Quaternion.identity,
                turretsParent).GetComponent<Turret>();

            Gun gun = GameManager.Instance.GetGunList()[randomTurretGun].GetComponent<Gun>();
            instTurret.Initialize(gun);
        }
        activeTurrets = maxTurrets;
    }

    private void GetRandomSpawnPoint()
    {
        randomSpawnPoint = Random.Range(0, turretSpawnPoints.childCount);
    }

}
