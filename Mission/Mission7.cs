using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission7 : Mission_Base
{
    [SerializeField] private DialogueTrigger startDialogueTrigger;
    [SerializeField] private DialogueTrigger winDialogueTrigger;
    [SerializeField] private DialogueTrigger loseDialogueTrigger;
    [SerializeField] private DialogueTrigger dieDialogueTrigger;
    [SerializeField] private RectTransform dialoguePanel;

    private int fakeCount;
    private bool isEnd;
    private bool isWin;
    private bool isLose;
    private bool isDie;

    // Start is called before the first frame update
    void Start()
    {
        startDialogueTrigger.TriggerDialogue();
        GameObject[] fakeEnemies = GameObject.FindGameObjectsWithTag("Fake");
        fakeCount = fakeEnemies.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWin && !dialoguePanel.gameObject.activeInHierarchy)
        {
            Win();

            if (PlayerPrefs.GetInt("StageUnlock", 0) <= 8)
            {
                PlayerPrefs.SetInt("StageUnlock", 8);
            }
        }

        if (isLose && !dialoguePanel.gameObject.activeInHierarchy)
        {
            Lose();
        }

        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            if (!isDie)
            {
                dieDialogueTrigger.TriggerDialogue();

                isDie = true;
            }
        }

        if (isDie && !dialoguePanel.gameObject.activeInHierarchy)
        {
            Lose();
        }

        CheckEnemy();
    }

    private void CheckEnemy()
    {
        if (!isEnd)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            int enemiesLeft = enemies.Length;

            if (enemiesLeft == 0)
            {
                isEnd = true;
                _playerController.StopWalkSound();
                winDialogueTrigger.TriggerDialogue();
                isWin = true;
                return;
            }

            GameObject[] fakeEnemies = GameObject.FindGameObjectsWithTag("Fake");
            int fakeEnemiesLeft = fakeEnemies.Length;

            if (fakeEnemiesLeft < fakeCount)
            {
                isEnd = true;
                _playerController.StopWalkSound();
                loseDialogueTrigger.TriggerDialogue();
                isLose = true;
            }
        }
    }

    public override void RetryMission()
    {
        SceneManager.LoadScene("Chapter-7");
    }
}
