using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GunsSO gunSO;

    public GunsSO GetSO() => gunSO;
}
