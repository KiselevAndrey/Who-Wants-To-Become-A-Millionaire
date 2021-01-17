using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectionOfQuestionsDiff", menuName = "Коллекция вопросов")]
public class CollectionOfQuestionsSO : ScriptableObject
{
    [SerializeField] List<QuestionSO> questions;
    
    int i;

    public void Shuffle()
    {
        questions.Shuffle();
        i = 0;
    }

    public QuestionSO NextQuestion()
    {
        QuestionSO tempQ = questions[i++];

        if (i >= questions.Count) Shuffle();
        
        return tempQ;
    }
}
