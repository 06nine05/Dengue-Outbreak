using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissionSelect : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private int stage;

    private void Start()
    {
        if (PlayerPrefs.GetInt("StageUnlock", 1) >= stage)
        {
            _button.interactable = true;
        }   
    }

    public void GoToStage(string stage)
    {
        SceneManager.LoadScene(stage);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
