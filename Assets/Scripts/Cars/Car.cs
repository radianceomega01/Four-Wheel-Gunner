using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] CarsSO carSO;

    public CarsSO GetSO() => carSO;
}
