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

    [Header("Цвета для правильного и неправильного ответов")]
    [SerializeField] Color correctAnswerColor;
    [SerializeField] Color wrongAnswerColor;

    [Header("Списки вопросов")]
    [SerializeField] CollectionOfQuestionsSO easyQuestions;

    [Header("Доп видимые объекты")]
    [SerializeField] Text lifeText;
    [SerializeField] Button nextQuestion;

    [Header("Доп невидимые объекты")]
    [SerializeField] Object afterGameScene;
    [SerializeField] PlayerStatsSO player;
    [SerializeField, Tooltip("Кол-во вопросов до победы")] int countQuestion;

    CollectionOfQuestionsSO _currentCollection;
    QuestionSO _currentQuestion;
    Color _defaultColor;

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

        _defaultColor = answerBtns[0].image.color;

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
        UpdateBtns();
        UpdateBtnsColor();
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

    void UpdateBtns()
    {
        if (!_fiftyfifty) return;

        foreach (var btn in answerBtns)
        {
            btn.gameObject.SetActive(true);
        }

        _fiftyfifty = false;
    }

    void UpdateBtnsColor()
    {
        foreach (var btn in answerBtns)
        {
            btn.image.color = _defaultColor;
        }
    }
    #endregion

    #region Work With Btns
    public void Answer(Button btn)
    {
        Text text = btn.GetComponentInChildren<Text>();

        if (IsWrongAnswer(text.text))
        {
            player.WrongAnswer();
            UpdateLife();
            btn.image.color = wrongAnswerColor;
        }
        else
        {
            player.CorrectAnswer();
        }

        ViewCorrectAnswer();
        NextQuestionBtnTakeOverActivate();     

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

    void ViewCorrectAnswer()
    {
        for (int i = 0; i < _answerTexts.Count; i++)
        {
            if (!IsWrongAnswer(_answerTexts[i].text))
            {
                answerBtns[i].image.color = correctAnswerColor;
                break;
            }
        }
    }

    public void FiftyFifty(Button btn)
    {
        _answers.Shuffle();

        int n = 2;

        for (int i = 0; i < _answers.Count; i++)
        {
            if (IsWrongAnswer(_answers[i]))
            {
                _answers.RemoveAt(i);
                n--;
            }

            if (n <= 0) break;
        }

        for (int i = 0; i < answerBtns.Count; i++)
        {
            bool check = false;

            for (int j = 0; j < _answers.Count; j++)
            {
                if (_answerTexts[i].text == _answers[j])
                {
                    check = true;
                    break;
                }
            }

            if (!check)
                answerBtns[i].gameObject.SetActive(false);
        }

        btn.gameObject.SetActive(false);
        _fiftyfifty = true;
    }

    void UpdateLife() => lifeText.text = player.Life.ToString();

    void NextQuestionBtnTakeOverActivate() => nextQuestion.gameObject.SetActive(!nextQuestion.gameObject.activeSelf);

    public void NextQuestionBtn()
    {
        NextQuestionBtnTakeOverActivate();

        if (player.Life > 0)
        {
            _numberOfQuestion++;
            if (_numberOfQuestion < countQuestion)
            {
                if (_numberOfQuestion % (countQuestion / 3) == 0)
                {
                    _difficult++;
                    NewCurrentCollection();
                }

                NextQuestion();
            }
            else
                ManagerSceneStatic.LoadScene(afterGameScene);
        }
        else
        {
            ManagerSceneStatic.LoadScene(afterGameScene);
        }
    }
    #endregion
}
