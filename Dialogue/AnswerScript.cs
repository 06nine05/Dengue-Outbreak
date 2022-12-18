using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;

    public void Answer()
    {
        if (isCorrect)
        {
            quizManager.nextQuestion();
            quizManager.score++;
            Debug.Log("Correct");
        }

        else
        {
            quizManager.nextQuestion();
            Debug.Log("Wrong");
        }
    }
}
