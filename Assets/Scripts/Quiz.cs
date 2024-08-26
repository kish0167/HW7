using System;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

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

    [SerializeField] private List<int> _answersMap;

    public List<int> AnswersMap => _answersMap;

    private int _currentQuestionNumber;

    private int _hitPoints;

    #endregion

    #region Events

    public event Action OnCorrectAnswer;
    public event Action OnNewQuewstion;
    public event Action OnWrongAnswer;

    #endregion

    #region Unity lifecycle

    private void Start()
    {
        _answersMap = new List<int> { 0, 0, 0, 0 };
        if (_buttons.Count != 4 || _answerLabels.Count != 4)
        {
            Debug.LogError("something wrong with Quiz instance");
            return;
        }

        _hitPoints = 3;
        _currentQuestionNumber = 0;
        ExecuteRandomQuestion();
    }

    #endregion

    #region Private methods

    private void CorrectAnswerHandling()
    {
        OnCorrectAnswer?.Invoke();
        ExecuteRandomQuestion();
    }

    private void ExecuteRandomQuestion()
    {
        if (_questions.Count == 0)
        {
            ScenesLoader.LoadFinaleScene();
            return;
        }

        OnNewQuewstion?.Invoke();

        Random rnd = new();
        int randomInt = rnd.Next(0, _questions.Count);
        _currentQuestionNumber++;
        LoadQuestion(_questions[randomInt], _currentQuestionNumber);
        _questions.RemoveAt(randomInt);
    }

    private void LoadQuestion(QuestionConfig questionConfig, int numberToShow)
    {
        if (questionConfig.Answers.Count != 4)
        {
            Debug.LogError("something wrong with current question config");
            return;
        }

        RandomizeQuestionsMap();

        for (int i = 0; i < 4; i++)
        {
            _answerLabels[i].text = questionConfig.Answers[_answersMap[i]];
        }

        _questionLabel.text = questionConfig.QuestionText;
        _questionNumberLabel.text = $"#{numberToShow}";

        SetButtons();
    }

    private void RandomizeQuestionsMap()
    {
        Random rnd = new();
        int randomInt;
        List<int> map = new() { 0, 1, 2, 3 };
        for (int i = 0; i < 4; i++)
        {
            randomInt = rnd.Next(0, 4 - i); // WHY?
            _answersMap[i] = map[randomInt];
            map.RemoveAt(randomInt);
        }
    }

    private void SetButtons()
    {
        for (int i = 0; i < 4; i++)
        {
            _buttons[i].onClick.RemoveAllListeners();
            if (_answersMap[i] == 0)
            {
                _buttons[i].onClick.AddListener(CorrectAnswerHandling);
            }
            else
            {
                _buttons[i].onClick.AddListener(WrongAnswerHandling);
            }
        }
    }

    private void WrongAnswerHandling()
    {
        OnWrongAnswer?.Invoke();
        ScenesLoader.LoadFinaleScene();
    }

    #endregion
}