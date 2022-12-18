using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission9 : Mission_Base
{
    [SerializeField] private DialogueTrigger startDialogueTrigger;
    [SerializeField] private DialogueTrigger winDialogueTrigger;
    [SerializeField] private DialogueTrigger dieDialogueTrigger;
    [SerializeField] private RectTransform dialoguePanel;
    [SerializeField] private RectTransform bossHP;

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
            bossHP.gameObject.SetActive(false);

            Win();

            if (PlayerPrefs.GetInt("StageUnlock", 0) <= 10)
            {
                PlayerPrefs.SetInt("StageUnlock", 10);
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
            bossHP.gameObject.SetActive(false);

            Lose();
        }

        CheckBoss();
    }

    private void CheckBoss()
    {
        if (!isEnd)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Boss");
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

    public override void Pause()
    {
        base.Pause();
        bossHP.gameObject.SetActive(false);
    }

    public override void UnPause()
    {
        base.UnPause();
        bossHP.gameObject.SetActive(true);
    }

    public override void RetryMission()
    {
        SceneManager.LoadScene("Chapter-9");
    }
}
