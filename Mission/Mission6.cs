using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission6 : Mission_Base
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

            if (PlayerPrefs.GetInt("StageUnlock", 0) <= 7)
            {
                PlayerPrefs.SetInt("StageUnlock", 7);
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

        CheckQuest();
    }

    private void CheckQuest()
    {
        if (!isEnd)
        {
            GameObject[] vases = GameObject.FindGameObjectsWithTag("Vase");
            int vasesLeft = vases.Length;

            if (vasesLeft == 0)
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
        SceneManager.LoadScene("Chapter-6");
    }
}
