using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mission1_Tutorial : Mission_Base
{
    [SerializeField] private DialogueTrigger startDialogueTrigger;
    [SerializeField] private DialogueTrigger winDialogueTrigger;
    [SerializeField] private DialogueTrigger dieDialogueTrigger;
    [SerializeField] private RectTransform dialoguePanel;

    private bool isEnd;
    private bool isWin;
    private bool isDie;

    // Start is called before the first frame update
    void Start()
    {
        startDialogueTrigger.TriggerDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWin && !dialoguePanel.gameObject.activeInHierarchy)
        {
            Win();

            if (PlayerPrefs.GetInt("StageUnlock", 0) <= 2)
            {
                PlayerPrefs.SetInt("StageUnlock", 2);
            }
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
            }
        }
    }

    public override void RetryMission()
    {
        SceneManager.LoadScene("Chapter-1");
    }
}
