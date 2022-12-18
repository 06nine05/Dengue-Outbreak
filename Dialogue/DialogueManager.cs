using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private RectTransform dialogPanel;

    private Queue<string> sentences = new Queue<string>();

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public void StartDialogue(Dialogue dialogue)
    {
        dialogPanel.gameObject.SetActive(true);

        Time.timeScale = 0;

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        dialogPanel.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
