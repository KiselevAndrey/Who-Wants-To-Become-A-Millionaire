using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [Header("Картинка и текст вопроса")]
    [SerializeField] Image questionImage;
    [SerializeField] Text questionText;

    [Header("Кнопки ответов")]
    [SerializeField] List<Button> answerBtns;

    [Header("Списки вопросов")]
    [SerializeField] CollectionOfQuestionsSO easyQuestions;

    [Header("Доп объекты")]
    [SerializeField] Text lifeText;

    CollectionOfQuestionsSO _currentCollection;
    QuestionSO _currentQuestion;

    List<Image> _answerImages;
    List<Text> _answerTexts;
    List<string> _answers;

    int _life;

    void Start()
    {
        _life = 3;
        UpdateLife();

        NewBtnImageAndText();

        _currentCollection = easyQuestions;
        _currentCollection.Shuffle();
        NextQuestion();
    }

    void NewBtnImageAndText()
    {
        _answerImages = new List<Image>();
        _answerTexts = new List<Text>();

        foreach (var btn in answerBtns)
        {
            _answerImages.Add(btn.GetComponent<Image>());
            _answerTexts.Add(btn.GetComponentInChildren<Text>());
        }
    }

    #region Next Question
    void NextQuestion()
    {
        _currentQuestion = _currentCollection.NextQuestion();
        UpdateImage();
        UpdateText();
    }

    void UpdateImage()
    {
        questionImage.sprite = _currentQuestion.questionSprite;
        foreach (var image in _answerImages)
        {
            image.sprite = _currentQuestion.answerSprite;
        }
    }

    void UpdateText()
    {
        questionText.text = _currentQuestion.question;
        questionText.color = _currentQuestion.questionTextColor;

        _answers = new List<string>
        {
            _currentQuestion.correctAnswer,
            _currentQuestion.wrongAnswerA,
            _currentQuestion.wrongAnswerB,
            _currentQuestion.wrongAnswerC
        };

        _answers.Shuffle();

        for (int i = 0; i < _answerTexts.Count; i++)
        {
            _answerTexts[i].text = _answers[i];
            _answerTexts[i].color = _currentQuestion.answerTextColor;
        }
    }
    #endregion

    public void Answer(Button btn)
    {
        Text text = btn.GetComponentInChildren<Text>();

        if (IsWrongAnswer(text.text))
        {
            UpdateLife(-1);
        }

        NextQuestion();
    }

    bool IsWrongAnswer(string answer) => answer != _currentQuestion.correctAnswer;

    void UpdateLife(int addedValue = 0)
    {
        _life += addedValue;

        if (_life <= 0)
        {
            _life = 0;
        }

        lifeText.text = _life.ToString();
    }
}
