using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission_Base : MonoBehaviour
{
    [SerializeField] private RectTransform winPanel;
    [SerializeField] private RectTransform losePanel;
    [SerializeField] private TextMeshProUGUI coinNumberText;
    [SerializeField] private int coinReward;
    [SerializeField] private RectTransform pausePanel;
    private bool isRecievedCoin = false;
    private int currentCoin;
    protected PlayerController _playerController;

    protected void Awake()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    protected void Win()
    {
        Time.timeScale = 0;
        _playerController.StopWalkSound();
        winPanel.gameObject.SetActive(true);
        if (!isRecievedCoin)
        {
            coinNumberText.text = $"x {coinReward}";
            currentCoin = PlayerPrefs.GetInt("coin", 0);
            PlayerPrefs.SetInt("coin", currentCoin += coinReward);
            isRecievedCoin = true;
        }
    }

    protected void Lose()
    {
        Time.timeScale = 0;
        _playerController.StopWalkSound();
        losePanel.gameObject.SetActive(true);
    }

    public virtual void RetryMission()
    {
        
    }

    public virtual void Pause()
    {
        Time.timeScale = 0;
        pausePanel.gameObject.SetActive(true);
    }

    public virtual void UnPause()
    {
        Time.timeScale = 1;
        pausePanel.gameObject.SetActive(false);
    }

    public void ExitMission()
    {
        //Time.timeScale = 1;
        SceneManager.LoadScene("MissionSelect");
    }
}
