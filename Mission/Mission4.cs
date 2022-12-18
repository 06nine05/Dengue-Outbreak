using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission4 : Mission_Base
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
        GameObject[] fakeHerbs = GameObject.FindGameObjectsWithTag("Fake");
        fakeCount = fakeHerbs.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWin && !dialoguePanel.gameObject.activeInHierarchy)
        {
            Win();

            if (PlayerPrefs.GetInt("StageUnlock", 0) <= 5)
            {
                PlayerPrefs.SetInt("StageUnlock", 5);
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

        CheckMission();
    }

    private void CheckMission()
    {
        if (!isEnd)
        {
            GameObject[] herbs = GameObject.FindGameObjectsWithTag("Interaction");
            int herbsLeft = herbs.Length;

            if (herbsLeft == 3)
            {
                isEnd = true;
                _playerController.StopWalkSound();
                winDialogueTrigger.TriggerDialogue();
                isWin = true;
                return;
            }

            GameObject[] fakeHerbs = GameObject.FindGameObjectsWithTag("Fake");
            int fakeHerbsLeft = fakeHerbs.Length;

            if (fakeHerbsLeft < fakeCount)
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
        SceneManager.LoadScene("Chapter-4");
    }
}