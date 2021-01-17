using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfterGame : MonoBehaviour
{
    [SerializeField] Text finalText;
    [SerializeField] PlayerStatsSO player;

    // Start is called before the first frame update
    void Start()
    {
        string final = "Game Over\n";

        if (player.Life > 0) final += "С победой!";
        else final += "C поражением!";

        final += "\n Остаток жизней: " + player.Life;
        final += "\n Правильных ответов: " + player.CorrectAnswers;
        final += "\n Неправильных ответов: " + player.WrongAnswers;

        final += "\n\n Для подолжения жми на экран.";

        finalText.text = final;
    }
}
