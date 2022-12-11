
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GunsModule : MonoBehaviour
{
    [Header("Gun Position Buttons")]
    [SerializeField] Button top;
    [SerializeField] Button left;
    [SerializeField] Button right;
    [Header("Gun Position Marker")]
    [SerializeField] RectTransform indicator;
    [Header("Gun Buttons")]
    [SerializeField] Button lightGun;
    [SerializeField] Button steadyGun;
    [SerializeField] Button heavyGun;
    [Header("Stats")]
    [SerializeField] TextMeshProUGUI gunName;
    [SerializeField] Slider fireRateSlider;
    [SerializeField] Slider damageSlider;
    [SerializeField] Slider criticalHitSlider;
    [Header("List of Guns(In their respective order)")]
    [SerializeField] List<GameObject> gunList;
    [Header("Cars Parent")]
    [SerializeField] Transform carsParent;

    private int activeGunPos = 0;
    private CarGunPositions carGunPosition;
    private void Awake()
    {
        top.onClick.AddListener(() =>
        {
           OnGunPositionBtnClicked(0, top);
        });
        left.onClick.AddListener(() =>
        {
            OnGunPositionBtnClicked(1,left);
        });
        right.onClick.AddListener(() =>
        {
            OnGunPositionBtnClicked(2, right);
        });

        lightGun.onClick.AddListener(delegate { DisplayGuns(activeGunPos, 0); });
        steadyGun.onClick.AddListener(delegate { DisplayGuns(activeGunPos, 1); });
        heavyGun.onClick.AddListener(delegate { DisplayGuns(activeGunPos, 2); });
    }

    private void Start()
    {
        carGunPosition = carsParent.GetChild(0).GetComponent<CarGunPositions>();
        DisplayGuns(activeGunPos, 0);
    }

    private void MoveIndicator(Button button)
    {
        indicator.localPosition = new Vector3(indicator.localPosition.x, button.GetComponent<RectTransform>().localPosition.y);
    }

    private void OnGunPositionBtnClicked(int pos, Button button)
    {
        activeGunPos = pos;
        MoveIndicator(button);
        if (carGunPosition.GetPosition(activeGunPos).childCount != 0)
        {
            StartCoroutine(SetStats(pos));
        }
        else
        {
            DisplayGuns(activeGunPos, 0);
        }
    }

    private void DisplayGuns(int gunPosIndex, int gunIndex)
    {
        CarSpawnDetails.Instance.GunPosition[gunPosIndex] = gunIndex;

        if(carGunPosition.GetPosition(gunPosIndex).childCount != 0)
            Destroy(carGunPosition.GetPosition(gunPosIndex).GetChild(0).gameObject);
        Instantiate(gunList[gunIndex], carGunPosition.GetPosition(gunPosIndex));
        StartCoroutine(SetStats(gunPosIndex));
    }

    private IEnumerator SetStats(int gunPosIndex)
    {
        yield return new WaitForEndOfFrame();
        GunsSO gunSo = carGunPosition.GetPosition(gunPosIndex).GetChild(0).GetComponent<Gun>().GetSO();
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
