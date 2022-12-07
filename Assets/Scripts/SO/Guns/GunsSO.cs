
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/Guns", order = 2)]
public class GunsSO : ScriptableObject
{
    public int id;
    public string gunName;

    [Header("Stats values(0 to 100)")]
    public float fireRate;
    public float damage;
    public float criticalHit;

    [Header("Implementational values")]
    public float fireInterval;
    public float damagePerBullet;
    public float criticalHitRate;
}
