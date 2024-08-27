using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private TMP_Text _hintsNumerLabel;
    [SerializeField] private TMP_Text _HPNumberLabel;

    [Header("Buttons")]
    [SerializeField] private List<Button> _buttons;
    [SerializeField] private Button _hintButton;

    [Header("All questions")]
    [SerializeField] private List<QuestionConfig> _questions;

    [SerializeField] private List<int> _answersMap;

    [SerializeField] private float _delay;
    [SerializeField] private int _startHP;
    [SerializeField] private int _startHints;

    private int _currentQuestionNumber;

    private Coroutine _quizCoroutine;

    #endregion

    #region Events

    public event Action OnCorrectAnswer;
    public event Action OnHintUsed;
    public event Action OnNewQuestion;
    public event Action OnWrongAnswer;

    #endregion

    #region Properties

    public List<int> AnswersMap => _answersMap;

    #endregion

    #region Unity lifecycle

    private void Start()
    {
        _hintButton.onClick.AddListener(HintUsageHandling);
        StatisticsHandler.Reset(_startHP, _startHints);
        _currentQuestionNumber = 0;
        _answersMap = new List<int> { 0, 0, 0, 0 };

        if (_buttons.Count != 4 || _answerLabels.Count != 4)
        {
            Debug.LogError("something wrong with Quiz instance");
            return;
        }

        UpdateHintsNLabel();
        UpdateHPNumberLabel();
        ExecuteRandomQuestion();
    }

    #endregion

    #region Private methods

    private void AnswerCorrectHandling()
    {
        OnCorrectAnswer?.Invoke();
        StatisticsHandler.CorrectGuesses++;
        _quizCoroutine = StartCoroutine(AnswerCorrectSequence());
    }

    private IEnumerator AnswerCorrectSequence()
    {
        yield return new WaitForSeconds(_delay);
        ExecuteRandomQuestion();
    }

    private void AnswerWrongHandling()
    {
        OnWrongAnswer?.Invoke();
        StatisticsHandler.Hp--;
        StatisticsHandler.WrongGuesses++;
        UpdateHPNumberLabel();
        _quizCoroutine = StartCoroutine(AnswerWrongSequence());
    }

    private IEnumerator AnswerWrongSequence()
    {
        yield return new WaitForSeconds(_delay);
        
        if (StatisticsHandler.Hp <= 0)
        {
            ScenesLoader.LoadFinaleScene();
        }

        OnNewQuestion?.Invoke();
    }

    private void CheckEndOfGame()
    {
        if (_questions.Count == 0 || StatisticsHandler.Hp <= 0)
        {
            ScenesLoader.LoadFinaleScene();
        }
    }

    private void ExecuteRandomQuestion()
    {
        CheckEndOfGame();

        OnNewQuestion?.Invoke();

        Random rnd = new();
        int randomInt = rnd.Next(0, _questions.Count);
        _currentQuestionNumber++;
        LoadQuestion(_questions[randomInt], _currentQuestionNumber);
        _questions.RemoveAt(randomInt);
    }

    private void HintUsageHandling()
    {
        if (StatisticsHandler.Hints == 0)
        {
            return;
        }

        OnHintUsed?.Invoke();
        StatisticsHandler.Hints--;
        UpdateHintsNLabel();
    }

    private void LoadQuestion(QuestionConfig questionConfig, int numberToShow)
    {
        if (questionConfig.Answers.Count != 4)
        {
            //Debug.LogError("something wrong with current question config");
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
                _buttons[i].onClick.AddListener(AnswerCorrectHandling);
            }
            else
            {
                _buttons[i].onClick.AddListener(AnswerWrongHandling);
            }
        }
    }

    private void UpdateHintsNLabel()
    {
        _hintsNumerLabel.text = $"{StatisticsHandler.Hints}";
    }

    private void UpdateHPNumberLabel()
    {
        _HPNumberLabel.text = $"{StatisticsHandler.Hp}";
    }

    #endregion
}