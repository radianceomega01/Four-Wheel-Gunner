
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GunsModule : MonoBehaviour
{
    [Header("Gun Position Buttons")]
    [SerializeField] Button top;
    [SerializeField] Button left;
    [SerializeField] Button right;
    [Header("Gun Position Marker")]
    [SerializeField] Image indicator;
    [Header("Gun Buttons")]
    [SerializeField] Button lightGun;
    [SerializeField] Button steadyGun;
    [SerializeField] Button heavyGun;
    [Header("Stats")]
    [SerializeField] TextMeshProUGUI gunName;
    [SerializeField] Slider fireRateSlider;
    [SerializeField] Slider damageSlider;
    [SerializeField] Slider criticalHitSlider;
    [Header("List of Gun Positions(In their respective order)")]
    [SerializeField] List<GameObject> gunPositionList;
    [Header("List of Guns(In their respective order)")]
    [SerializeField] List<GameObject> gunList;
    [Header("Cars Parent")]
    [SerializeField] Transform carsParent;

    private int activeGunPos = 0;
    private CarGunPositions carGunPosition;
    private void Awake()
    {
        top.onClick.AddListener(() => activeGunPos = 0);
        left.onClick.AddListener(() => activeGunPos = 1);
        right.onClick.AddListener(() => activeGunPos = 2);

        lightGun.onClick.AddListener(delegate { DisplayGuns(activeGunPos, 0); });
        steadyGun.onClick.AddListener(delegate { DisplayGuns(activeGunPos, 0); });
        heavyGun.onClick.AddListener(delegate { DisplayGuns(activeGunPos, 0); });
    }

    private void Start()
    {
        carGunPosition = carsParent.GetChild(0).GetComponent<CarGunPositions>();
    }
    private void DisplayGuns(int gunPosIndex, int gunIndex)
    {
        if(carGunPosition.GetPosition(gunPosIndex).childCount != 0)
            Destroy(carGunPosition.GetPosition(gunPosIndex).GetChild(0).gameObject);
        Instantiate(gunList[gunIndex], carGunPosition.GetPosition(gunPosIndex));
        SetStats(gunPosIndex);
    }

    private void SetStats(int gunPosIndex)
    {
        GunsSO gunSo = carGunPosition.GetPosition(gunPosIndex).GetChild(0).GetComponent<GunsSO>();
        gunName.text = gunSo.gunName;
        fireRateSlider.value = gunSo.fireRate;
        damageSlider.value = gunSo.damage;
        criticalHitSlider.value = gunSo.criticalHit;
    }

    private void OnDestroy()
    {
        top.onClick.RemoveAllListeners();
        left.onClick.RemoveAllListeners();
        right.onClick.RemoveAllListeners();

        lightGun.onClick.RemoveAllListeners();
        steadyGun.onClick.RemoveAllListeners();
        heavyGun.onClick.RemoveAllListeners();
    }
}
