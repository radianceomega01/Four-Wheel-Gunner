
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarSelection : MonoBehaviour
{
    [SerializeField] Button backBtn;
    [SerializeField] Button nextBtn;
    [SerializeField] CarsModule carsModule;
    [SerializeField] GunsModule gunsModule;

    private void Awake()
    {
        backBtn.onClick.AddListener(OnBackBtnClicked);
        nextBtn.onClick.AddListener(OnNextBtnClicked);
    }

    private void OnBackBtnClicked()
    {
        SceneManager.LoadScene("Splash");
    }

    private void OnNextBtnClicked()
    {
        if (gunsModule.gameObject.activeInHierarchy)
        {
            SceneManager.LoadScene("Gameplay");
        }
        else
        {
            carsModule.gameObject.SetActive(false);
            gunsModule.gameObject.SetActive(true);
        }
    }
}
