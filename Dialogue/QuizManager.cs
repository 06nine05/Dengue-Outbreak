using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private RectTransform quizPanel;

    public List<Quiz> quizList;
    public GameObject[] options;
    public int currentQuestion;
    public TextMeshProUGUI questionText;
    public int score;
    public bool isEnd;

    private void Start()
    {
        score = 0;
    }

    public void nextQuestion()
    {
        quizList.RemoveAt(currentQuestion);
        generateQuestion();
    }

    private void SetAnswers()
    {
        for(int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = quizList[currentQuestion].Answers[i];

            if (quizList[currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    public void StartQuiz()
    {
        isEnd = false;

        quizPanel.gameObject.SetActive(true);

        Time.timeScale = 0;

        generateQuestion();
    }

    public void generateQuestion()
    {
        if (quizList.Count == 0)
        {
            EndQuiz();
            return;
        }

        currentQuestion = Random.Range(0,quizList.Count);

        SetAnswers();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(quizList[currentQuestion].Question));
    }

    private void EndQuiz()
    {
        quizPanel.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        isEnd = true;
    }

    IEnumerator TypeSentence(string sentence)
    {
        questionText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            questionText.text += letter;
            yield return null;
        }
    }
}
