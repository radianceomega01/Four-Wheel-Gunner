using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnDetails : MonoBehaviour
{
    public static CarSpawnDetails Instance;

    public int CarIndex { get; set; }

    public int[] GunPosition { get; set;} 
    private void Awake()
    {
        if (Instance == null || Instance != this)
            Instance = this;

        DontDestroyOnLoad(this);
        GunPosition = new int[3];
    }

    private void Start()
    {
        CarIndex = 0;
        for (int i = 0; i < GunPosition.Length; i++)
            GunPosition[i] = -1;
    }

}
