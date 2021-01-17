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
    [SerializeField] Object afterGameScene;
    [SerializeField] PlayerStatsSO player;
    [SerializeField] int countQuestion;

    CollectionOfQuestionsSO _currentCollection;
    QuestionSO _currentQuestion;

    List<Image> _answerImages;
    List<Text> _answerTexts;
    List<string> _answers;

    int _numberOfQuestion;    

    void Start()
    {
        player.Zeroing();
        UpdateLife();
        _numberOfQuestion = 0;

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
            player.WrongAnswer();
            UpdateLife();
        }
        else
        {
            player.CorrectAnswer();
        }


        if (player.Life > 0)
        {
            _numberOfQuestion++;
            if (_numberOfQuestion < countQuestion)
                NextQuestion();
            else
                ManagerSceneStatic.LoadScene(afterGameScene);
        }
    }

    bool IsWrongAnswer(string answer) => answer != _currentQuestion.correctAnswer;

    void UpdateLife()
    {
        lifeText.text = player.Life.ToString();

        if (player.Life <= 0)
        {
            ManagerSceneStatic.LoadScene(afterGameScene);
        }
    }
}
