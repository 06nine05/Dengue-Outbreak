using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class Mission11 : Mission_Base
{
    [SerializeField] private DialogueTrigger startDialogueTrigger;
    [SerializeField] private DialogueTrigger winDialogueTrigger;
    [SerializeField] private DialogueTrigger loseDialogueTrigger;
    [SerializeField] private DialogueTrigger endDialogueTrigger;
    [SerializeField] private RectTransform dialoguePanel;
    [SerializeField] private RectTransform choicePanel;
    [SerializeField] private RectTransform cutScenePanel;
    [SerializeField] private RectTransform cutScenePanel2;
    [SerializeField] private RectTransform cutScenePanel3;
    [SerializeField] private RectTransform portrait;

    private bool isEnd;
    private bool isWin;
    private bool isLose;
    private bool isChoice;
    private bool isCutScene;
    private bool isDialogue;

    // Start is called before the first frame update
    void Start()
    {
        startDialogueTrigger.TriggerDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnd && !dialoguePanel.gameObject.activeInHierarchy)
        {
            isEnd = true;
        }

        if (isEnd && !isChoice && !choicePanel.gameObject.activeInHierarchy && !dialoguePanel.gameObject.activeInHierarchy)
        {
            isChoice = true;
            _playerController.StopWalkSound();
            choicePanel.gameObject.SetActive(true);
        }

        if (isWin && !isCutScene && !dialoguePanel.gameObject.activeInHierarchy)
        {
            isCutScene = true;
            cutScenePanel.gameObject.SetActive(true);
        }

        if (isDialogue && !dialoguePanel.gameObject.activeInHierarchy)
        {
            Win();

            if (PlayerPrefs.GetInt("StageUnlock", 0) <= 12)
            {
                PlayerPrefs.SetInt("StageUnlock", 12);
            }
        }

        if (isLose && !dialoguePanel.gameObject.activeInHierarchy)
        {
            Lose();
        }
    }

    public void paraButton()
    {
        choicePanel.gameObject.SetActive(false);
        isWin = true;
        winDialogueTrigger.TriggerDialogue();
    }

    public void asprButton()
    {
        choicePanel.gameObject.SetActive(false);
        isLose = true;
        loseDialogueTrigger.TriggerDialogue();
    }

    public void ToCutscene2()
    {
        cutScenePanel2.gameObject.SetActive(true);
        cutScenePanel.gameObject.SetActive(false);
    }

    public void ToCutscene3()
    {
        cutScenePanel3.gameObject.SetActive(true);
        cutScenePanel2.gameObject.SetActive(false);
    }

    public void CutsceneClose()
    {
        cutScenePanel3.gameObject.SetActive(false);
        portrait.gameObject.SetActive(false);
        endDialogueTrigger.TriggerDialogue();
        isDialogue = true;
    }

    public override void RetryMission()
    {
        SceneManager.LoadScene("Chapter-11");
    }
}
