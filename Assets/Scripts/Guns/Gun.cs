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
    private float shootForce = 15f;
    private GameManager gameManager;

    private void Start()
    {
        bulletCount = gunSO.bulletCount;
        gameManager = GameManager.Instance;
        if(gameManager != null && GetComponentInParent<Car>()!= null)
            gameManager.UpdateBulletCount(bulletCount);
    }

    public void IncreaseBullets(int amount)
    {
        bulletCount += amount;
        gameManager.UpdateBulletCount(amount);
    }
    public void PrepareToShoot(bool hold)
    {
        isHold = hold;
        if (!isHold)
            return;
        else
            StartCoroutine(Shoot(gunSO.fireInterval));
    }

    IEnumerator Shoot(float interval)
    {
        if (isHold && bulletCount > 0)
        {
            GameObject bullet;
            if (gameManager.GetBulletPool().childCount != 0)
            {
                bullet = gameManager.GetBulletPool().GetChild(0).gameObject;
                bullet.transform.position = nozzle.position;
                bullet.transform.rotation = nozzle.rotation;
                bullet.SetActive(true);
                bullet.transform.SetParent(gameManager.GetActiveBulletsParent());

            }
            else
            {
                bullet = Instantiate(bulletPrefab, nozzle.position, nozzle.rotation, gameManager.GetActiveBulletsParent());
            }

            bullet.GetComponent<Bullet>().Initialize(gunSO.damagePerBullet);
            bullet.GetComponent<Rigidbody>().isKinematic = false;
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce, ForceMode.Impulse);
            bulletCount--;
            if(GetComponentInParent<Car>() != null)
                gameManager.UpdateBulletCount(-1);
            yield return new WaitForSeconds(interval);
            if (bulletCount > 0)
            {
                StartCoroutine(Shoot(interval));
            }
        }
    }
}
