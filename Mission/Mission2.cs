using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Mission2 : Mission_Base
{
    [SerializeField] private DialogueTrigger startDialogueTrigger;
    [SerializeField] private DialogueTrigger winDialogueTrigger;
    [SerializeField] private RectTransform dialoguePanel;
    [SerializeField] private RectTransform quizPanel;
    [SerializeField] private RectTransform scorePanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private TextMeshProUGUI winScore;

    private bool isEnd;
    private bool isEndDialogue;
    private bool isWin;
    private bool isEndScore;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        startDialogueTrigger.TriggerDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (quizManager.isEnd && isEndScore && !dialoguePanel.gameObject.activeInHierarchy && !quizPanel.gameObject.activeInHierarchy)
        {
            winScore.text = $"Score : {score}";
            Win();

            if (PlayerPrefs.GetInt("StageUnlock", 0) <= 3)
            {
                PlayerPrefs.SetInt("StageUnlock", 3);
            }
        }
        
        if (quizManager.isEnd && !dialoguePanel.gameObject.activeInHierarchy && !isEndDialogue)
        {
            isEndDialogue = true;

            UpdateScore();

            scorePanel.gameObject.SetActive(true);
        }

        if (isWin && !quizPanel.gameObject.activeInHierarchy)
        {
            _playerController.StopWalkSound();
            quizManager.StartQuiz();
        }

        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Lose();
        }

        CheckMission();
    }

    private void UpdateScore()
    {
        score = quizManager.score;
        scoreText.text = $"Score : {score}";
    }

    public void CloseShowScore()
    {
        _playerController.StopWalkSound();
        winDialogueTrigger.TriggerDialogue();
        scorePanel.gameObject.SetActive(false);
        isEndScore = true;
    }

    private void CheckMission()
    {
        if (!isEnd)
        {
            GameObject[] laptop = GameObject.FindGameObjectsWithTag("Interaction");
            int laptopLeft = laptop.Length;

            if (laptopLeft == 0)
            {
                isEnd = true;
                isWin = true;
            }
        }
    }

    public override void RetryMission()
    {
        SceneManager.LoadScene("Chapter-2");
    }
}
