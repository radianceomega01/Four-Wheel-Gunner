
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarsModule : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] Button redSedan;
    [SerializeField] Button blueSedan;
    [SerializeField] Button greenTruck;
    [Header("Stats")]
    [SerializeField] Slider hpSlider;
    [SerializeField] Slider speedSlider;
    [SerializeField] Slider controlsSlider;
    [Header("List of Cars(In their respective order)")]
    [SerializeField] List<GameObject> carList;
    [Header("Cars Parent")]
    [SerializeField] Transform carsParent;
    [SerializeField] TextMeshProUGUI carName;

    private void Awake()
    {
        redSedan.onClick.AddListener(delegate { DisplayCar(0); });
        blueSedan.onClick.AddListener(delegate { DisplayCar(1); });
        greenTruck.onClick.AddListener(delegate { DisplayCar(2); });
    }

    private void DisplayCar(int index)
    {
        Destroy(carsParent.GetChild(0).gameObject);
        Instantiate(carList[index],carsParent);
        SetStats();
    }

    private void SetStats()
    {
        CarsSO carSO = carsParent.GetChild(0).GetComponent<CarsSO>();
        carName.text = carSO.carName;
        hpSlider.value = carSO.maxHP;
        speedSlider.value = carSO.maxSpeed;
        controlsSlider.value = carSO.controls;
    }

    private void OnDestroy()
    {
        redSedan.onClick.RemoveAllListeners();
        blueSedan.onClick.RemoveAllListeners();
        greenTruck.onClick.RemoveAllListeners();
    }
}
