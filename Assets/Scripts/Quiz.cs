using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    #region Variables

    [Header("Text fields")]
    [SerializeField] private TMP_Text _questionNumberLabel;
    [SerializeField] private TMP_Text _questionLabel;
    [SerializeField] private List<TMP_Text> _answerLabels;

    [Header("Buttons")]
    [SerializeField] private List<Button> _buttons;

    [Header("All questions")]
    [SerializeField] private List<QuestionConfig> _questions;


    private int _hitPoints;
    private int _currentQuestionNumber;
        
    #endregion

    #region Unity lifecycle

    private void Start()
    {
        _hitPoints = 3;
        _currentQuestionNumber = 1;
    }

    #endregion

    #region Private methods

    private void LoadQuestion(QuestionConfig questionConfig, int numberToShow)
    {
        for (int i = 0; i < 3; i++)
        {
            _answerLabels[i].text = questionConfig.Answers[i];
        }

        _questionLabel.text = questionConfig.QuestionText;
        _questionNumberLabel.text = $"#{numberToShow}";
    }

    #endregion
}