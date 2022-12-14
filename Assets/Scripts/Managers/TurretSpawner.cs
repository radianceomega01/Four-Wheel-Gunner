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
    private int destroyThreshold = 20;
    private int activeTurrets;

    private void Start()
    {
        GameManager.Instance.OnCarSpawned += Spawn;
        Turret.OnDestroyed += CheckForReSpawn;
    }

    private void Spawn()
    {
        for (int i = activeTurrets; i < maxTurrets; i++)
        {
            randomTurretGun = Random.Range(0, GameManager.Instance.GetGunList().Count);
            GetRandomSpawnPoint();

            Turret instTurret = Instantiate(turret,
                turretSpawnPoints.GetChild(randomSpawnPoint).position, Quaternion.identity,
                turretsParent).GetComponent<Turret>();

            Gun gun = GameManager.Instance.GetGunList()[randomTurretGun].GetComponent<Gun>();
            instTurret.Initialize(gun);
            activeTurrets++;
        }
    }

    private void GetRandomSpawnPoint()
    {
        randomSpawnPoint = Random.Range(0, turretSpawnPoints.childCount);
    }

    private void CheckForReSpawn()
    {
        activeTurrets--;
        destroyThreshold--;
        if (destroyThreshold <= 0)
            GameManager.Instance.OnGameWon();
        if (activeTurrets <= spawnThreshold)
        {
            Spawn();
        }
    }
}
