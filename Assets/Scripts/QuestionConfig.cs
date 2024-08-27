using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(QuestionConfig), menuName = "Configs/Game/Question Config")]
public class QuestionConfig : ScriptableObject
{
    #region Variables

    [Header("Content")]
    [TextArea(3, 10)]
    [SerializeField] private string questionText;
    [SerializeField] private Sprite _questionSprite;
    [Header("1st answer will be considered correct")]
    [SerializeField] private List<string> _answers;

    #endregion

    #region Properties

    public List<string> Answers => _answers;

    public string QuestionText => questionText;
    public Sprite Sprite => _questionSprite;

    #endregion
}