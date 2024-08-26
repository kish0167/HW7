using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = nameof(QuestionConfig), menuName = "Configs/Game/Question Config")]
public class QuestionConfig : ScriptableObject
{
    [Header("Content")]
    [TextArea(3,10)]
    [SerializeField] private string questionText;
    [SerializeField] private Sprite _questionSprite;
    [Header("1st answer will be considered correct")]
    [SerializeField] private List<string> _answers;

    public string QuestionText => questionText;
    public Sprite Sprite=> _questionSprite;
    public List<string> Answers => _answers;
}
