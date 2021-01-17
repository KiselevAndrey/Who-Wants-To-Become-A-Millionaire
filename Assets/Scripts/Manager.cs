using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [Header("Картинки вопроса и ответов")]
    [SerializeField] Image questionImage;
    [SerializeField] Image answerAImage;
    [SerializeField] Image answerBImage;
    [SerializeField] Image answerCImage;
    [SerializeField] Image answerDImage;

    [Header("Тексты вопроса и ответов")]
    [SerializeField] Text questionText;
    [SerializeField] Text answerAText;
    [SerializeField] Text answerBText;
    [SerializeField] Text answerCText;
    [SerializeField] Text answerDText;

    [Header("Списки вопросов")]
    [SerializeField] CollectionOfQuestionsSO easyQuestions;

    CollectionOfQuestionsSO _currentCollection;
    QuestionSO _currentQuestion;

    // Start is called before the first frame update
    void Start()
    {
        _currentCollection = easyQuestions;
        _currentCollection.Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextQuestion()
    {
        _currentQuestion = _currentCollection.NextQuestion();
    }
}
