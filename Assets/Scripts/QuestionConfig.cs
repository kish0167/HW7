using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = nameof(QuestionConfig), menuName = "Configs/Game/Question Config")]
public class QuestionConfig : ScriptableObject
{
    [FormerlySerializedAs("_questionText")]
    [Header("Content")]
    [TextArea(3,10)]
    [SerializeField] private string questionQuestionText;
    [SerializeField] private Sprite _questionSprite;
    [SerializeField] private List<string> _answers;

    public string QuestionText => questionQuestionText;
    public Sprite Sprite=> _questionSprite;
    public List<string> Answers => _answers;
}
