using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecuperarQuizUI : MonoBehaviour
{
    [SerializeField] private QuizUI quizUI;
    [SerializeField] private Text score;
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.Instance.GetScore(score);
        GameManager.Instance.NextPregunta(quizUI);
    }
}
