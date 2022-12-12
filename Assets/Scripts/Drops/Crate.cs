using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] private GameObject collectablePrefab;

    private float health = 5;
    public void TakeDamage(float value)
    {
        health -= value;
        if (health <= 0)
        {
            SpawnCollectable();
        }
    }

    private void SpawnCollectable()
    {
        int randomCollectableType = UnityEngine.Random.Range(0, Enum.GetNames(typeof(Collectables.Type)).Length);
        GameObject instObj = Instantiate(collectablePrefab, transform.position, Quaternion.identity);
        Collectables collectable = instObj.GetComponent<Collectables>();
        collectable.Initialize((Collectables.Type)randomCollectableType);
        StartCoroutine(WaitBeforeDestroy());
    }

    IEnumerator WaitBeforeDestroy()
    {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }
}
