using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Статы игрока")]
public class PlayerStatsSO : ScriptableObject
{
    public int startLife;

    int _correctAnswers;
    int _wrongAnswers;
    int _life;

    public int Life { get => _life; private set => _life = value; }
    public int CorrectAnswers { get => _correctAnswers; private set => _correctAnswers = value; }
    public int WrongAnswers { get => _wrongAnswers; private set => _wrongAnswers = value; }

    public void Zeroing()
    {
        _correctAnswers = 0;
        _wrongAnswers = 0;
        _life = startLife;
    }

    public void CorrectAnswer()
    {
        _correctAnswers++;
    }

    public void WrongAnswer()
    {
        _wrongAnswers++;
        _life--;
    }

}
