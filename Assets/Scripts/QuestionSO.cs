using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Вопрос")]
public class QuestionSO : ScriptableObject
{
    [Header("Фон вопроса и ответов")]
    public Sprite questionSprite;
    public Sprite answerSprite;

    [Header("Вопрос и ответы")]
    [TextArea(1, 5)] public string question;
    [TextArea(1, 5)] public string correctAnswer;
    [TextArea(1, 5)] public string wrongAnswerA;
    [TextArea(1, 5)] public string wrongAnswerB;
    [TextArea(1, 5)] public string wrongAnswerC;

    [Header("Цвета для текста")]
    public Color questionTextColor;
    public Color answerTextColor;
}
