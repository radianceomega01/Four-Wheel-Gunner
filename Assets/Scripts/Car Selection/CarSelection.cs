
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarSelection : MonoBehaviour
{
    [SerializeField] Button backBtn;
    [SerializeField] Button nextBtn;
    [SerializeField] GameObject carsModule;
    [SerializeField] GameObject gunsModule;

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
        if (gunsModule.activeInHierarchy)
            SceneManager.LoadScene("Gameplay");
        else
        {
            carsModule.SetActive(false);
            gunsModule.SetActive(true);
        }
    }
}
