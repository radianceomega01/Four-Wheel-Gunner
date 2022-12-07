using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] Button playBtn;

    private void Awake()
    {
        playBtn.onClick.AddListener(() => SceneManager.LoadScene("Car Selection"));
    }

    private void OnDestroy()
    {
        playBtn.onClick.RemoveAllListeners();
    }
}
