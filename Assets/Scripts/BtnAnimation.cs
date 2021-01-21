using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnAnimation : MonoBehaviour
{
    [SerializeField] Manager gameManager;

    public void NextQuestion()
    {
        gameManager.NextQuestionBtn();
    }
}
