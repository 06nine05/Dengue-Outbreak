using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission5 : Mission_Base
{
    [SerializeField] private DialogueTrigger startDialogueTrigger;
    [SerializeField] private DialogueTrigger winDialogueTrigger;
    [SerializeField] private RectTransform dialoguePanel;

    private bool isWin;

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

            if (PlayerPrefs.GetInt("StageUnlock", 0) <= 6)
            {
                PlayerPrefs.SetInt("StageUnlock", 6);
            }
        }

        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Lose();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _playerController.StopWalkSound();
            winDialogueTrigger.TriggerDialogue();
            isWin = true;
        }
    }

    public override void RetryMission()
    {
        SceneManager.LoadScene("Chapter-5");
    }
}
