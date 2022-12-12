using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] List<GameObject> carList;
    [SerializeField] List<GameObject> gunList;
    [SerializeField] Transform playerCarParent;
    [SerializeField] Transform playerCarSpawnPos;
    [SerializeField] Transform bulletPool;
    [SerializeField] Transform activeBulletsParent;

    [Header("Buttons")]
    [SerializeField] Button backBtn;
    [SerializeField] Button pauseBtn;
    [SerializeField] Button resumeBtn;
    [SerializeField] Button lostAndRetryBtn;
    [SerializeField] Button wonAndRetryBtn;

    [Header("Health")]
    [SerializeField] Slider healthBar;
    [SerializeField] Image healthBarFill;
    [SerializeField] Gradient healthColorGradient;

    [Header("Bullets")]
    [SerializeField] TextMeshProUGUI bullets;

    [Header("Panels")]
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject sorryPanel;
    [SerializeField] GameObject victoryPanel;

    private Car playerCar;
    private int totalBulletCount;
    public event Action OnCarSpawned;
    private void Awake()
    {
        if (Instance == null || Instance != this)
            Instance = this;

        backBtn.onClick.AddListener(() => SceneManager.LoadScene("Car Selection"));
        pauseBtn.onClick.AddListener(PauseGame);
        resumeBtn.onClick.AddListener(ResumeGame);
        lostAndRetryBtn.onClick.AddListener(RetryGame);
        wonAndRetryBtn.onClick.AddListener(RetryGame);

        Time.timeScale = 1f;

    }

    private void Start()
    {
        TimerScript.Instance.onTimeUp += OnTimeComplete;
        SpawnPlayerCar();
        SetGameTime();
    }

    public Transform GetBulletPool() => bulletPool;
    public Transform GetActiveBulletsParent() => activeBulletsParent;
    public Transform GetPlayerCar() => playerCarParent.GetChild(0);
    public List<GameObject> GetGunList() => gunList;
    private void SpawnPlayerCar()
    {
        GameObject instantiatedCar =  Instantiate(carList[CarSpawnDetails.Instance.CarIndex], 
            playerCarSpawnPos.position, Quaternion.identity, playerCarParent);

        playerCar = instantiatedCar.GetComponent<Car>();
        CarGunPositions carGunPositions = instantiatedCar.GetComponent<CarGunPositions>();

        for (int i = 0; i < carGunPositions.GetGunPositionTransform().childCount; i++)
        {
            if(CarSpawnDetails.Instance.GunPosition[i] != -1)
                Instantiate(gunList[CarSpawnDetails.Instance.GunPosition[i]],
                    carGunPositions.GetPosition(i).position, Quaternion.identity, carGunPositions.GetPosition(i));
        }
        instantiatedCar.tag = "Player";
        healthBar.maxValue = playerCar.Health;
        healthBar.value = healthBar.maxValue;
        OnCarSpawned.Invoke();
    }

    private void SetGameTime()
    {
        float time = CarSpawnDetails.Instance.GameTime;
        if (time <= 60)
        {
            TimerScript.Instance.SetTime(CarSpawnDetails.Instance.GameTime);
            TimerScript.Instance.SetStatusOfTime(true);
        }
    }

    public void UpdateHealthBar(float value)
    {
        healthBar.value = value;
        healthBarFill.color = healthColorGradient.Evaluate(value/ playerCar.GetSO().actualHP);
    }

    public void UpdateBulletCount(int value)
    {
        totalBulletCount += value;
        bullets.text = totalBulletCount.ToString();
    }

    public void OnCarDestroyed()
    {
        Task.Delay(500);
        Time.timeScale = 0f;
        sorryPanel.SetActive(true);
    }

    public void OnGameWon()
    {
        Time.timeScale = 0f;
        victoryPanel.SetActive(true);
    }

    private void OnTimeComplete()
    {
        if (playerCar.Health > 0)
            OnGameWon();
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    private void RetryGame()
    { 
        SceneManager.LoadScene("Gameplay");
    }
}
