using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentCoin;

    private void Start()
    {
        currentCoin.text = $"x {PlayerPrefs.GetInt("coin"), 0}";
        Time.timeScale = 1;
    }

    public void ToMissionSelect()
    {
        SceneManager.LoadScene("MissionSelect");
    }
}
