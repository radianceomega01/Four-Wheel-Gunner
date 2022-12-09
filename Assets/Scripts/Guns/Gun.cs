using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform nozzle;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GunsSO gunSO;
    public GunsSO GetSO() => gunSO;

    private int bulletCount;
    private bool isHold;
    private float shootForce = 4f;
    private GameManager gameManager;

    private void Start()
    {
        bulletCount = gunSO.bulletCount;
        gameManager = GameManager.Instance;
    }
    public void PrepareToShoot(bool hold)
    {
        isHold = hold;
        /*if (!isHold)
            return;
        else
            Debug.Log("Shoot");*/
        if (!isHold)
            return;
        else
            StartCoroutine(Shoot(gunSO.fireInterval));
    }

    IEnumerator Shoot(float interval)
    {
        if (!isHold)
            yield return null;
        print(isHold);
        GameObject bullet;
        if (gameManager.GetBulletPool().childCount != 0)
        {
            bullet = gameManager.GetBulletPool().GetChild(0).gameObject;
            bullet.transform.position = nozzle.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.SetActive(true);
            bullet.transform.SetParent(gameManager.GetActiveBulletsParent());
            
        }
        else
        {
            bullet = Instantiate(bulletPrefab, nozzle.position, Quaternion.identity, gameManager.GetActiveBulletsParent());
        }

        bullet.GetComponent<Bullet>().Initialize(gunSO.damagePerBullet);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce, ForceMode.Impulse);
        bulletCount--;
        yield return new WaitForSeconds(interval);
        if (bulletCount != 0)
        {
            StartCoroutine(Shoot(interval));
        }
    }
}
