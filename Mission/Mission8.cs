using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission8 : Mission_Base
{
    [SerializeField] private DialogueTrigger startDialogueTrigger;
    [SerializeField] private DialogueTrigger winDialogueTrigger;
    [SerializeField] private DialogueTrigger loseDialogueTrigger;
    [SerializeField] private RectTransform dialoguePanel;
    [SerializeField] private RectTransform quizPanel;
    [SerializeField] private RectTransform scorePanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private TextMeshProUGUI winScore;
    [SerializeField] private TextMeshProUGUI loseScore;

    private bool isEnd;
    private bool isEndDialogue;
    private bool isEndScore;
    private bool isWin;
    private bool isLose;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        startDialogueTrigger.TriggerDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEndDialogue && !dialoguePanel.gameObject.activeInHierarchy)
        {
            isEndDialogue = true;
        }

        if (isEndDialogue && !quizPanel.gameObject.activeInHierarchy && !dialoguePanel.gameObject.activeInHierarchy)
        {
            _playerController.StopWalkSound();
            quizManager.StartQuiz();
        }

        if (!isEnd && quizManager.isEnd && !dialoguePanel.gameObject.activeInHierarchy && !quizPanel.gameObject.activeInHierarchy)
        {
            UpdateScore();
            scorePanel.gameObject.SetActive(true);
            isEnd = true;
        }

        if (isWin && !dialoguePanel.gameObject.activeInHierarchy)
        {
            winScore.text = $"Score : {score}";
            Win();

            if (PlayerPrefs.GetInt("StageUnlock", 0) <= 9)
            {
                PlayerPrefs.SetInt("StageUnlock", 9);
            }
        }

        if (isLose && !dialoguePanel.gameObject.activeInHierarchy)
        {
            loseScore.text = $"Score : {score}";
            Lose();
        }
    }

    private void UpdateScore()
    {
        score = quizManager.score;
        scoreText.text = $"Score : {score}";
    }

    public void CloseShowScore()
    {
        scorePanel.gameObject.SetActive(false);
        CheckMission();
    }

    private void CheckMission()
    {
        if (score >= 7)
        {
            _playerController.StopWalkSound();
            winDialogueTrigger.TriggerDialogue();

            isWin = true;
        }

        else
        {
            _playerController.StopWalkSound();
            loseDialogueTrigger.TriggerDialogue();

            isLose = true;
        }
    }

    public override void RetryMission()
    {
        SceneManager.LoadScene("Chapter-8");
    }
}
