using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPositionsParent;
    [SerializeField] Transform DropsParent;
    [SerializeField] GameObject cratePrefab;

    private float timeToRespawn = 15;
    private bool startTimer;
    private float timeLeft;
    private int randomPositionIndex;
    private void Start()
    {
        GameManager.Instance.OnCarSpawned += Initialize;
    }

    private void Initialize()
    {
        startTimer = true;
        timeLeft = timeToRespawn;
    }
    void Update()
    {
        if (startTimer)
        {

            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }
            else
            {
                SpawnCrate();
                timeLeft = timeToRespawn;
            }
        }

    }

    private void SpawnCrate()
    {
        randomPositionIndex = Random.Range(0, spawnPositionsParent.childCount);
        Instantiate(cratePrefab, spawnPositionsParent.GetChild(randomPositionIndex).position, Quaternion.identity, DropsParent);
    }
}
