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

    [Header("Question Image")]
    [SerializeField] private Image _image;

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

    [Header("prefs")]
    [SerializeField] private float _delay;
    [SerializeField] private int _startHP;
    [SerializeField] private int _startHints;

    private List<int> _answersMap;

    private int _currentQuestionNumber;

    private Coroutine _quizCoroutine;

    private StatisticsHandler _stats;

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
        _stats = FindObjectOfType<StatisticsHandler>();
        _stats.ResetState(_startHP, _startHints);

        _currentQuestionNumber = 0;
        _answersMap = new List<int> { 0, 0, 0, 0 };

        _hintButton.onClick.AddListener(HintUsageHandling);

        if (_buttons.Count != 4 || _answerLabels.Count != 4)
        {
            Debug.LogError("something wrong with Quiz instance");
            return;
        }

        OnWrongAnswer += UpdateHPNumberLabel;
        OnHintUsed += UpdateHintsNLabel;
        OnNewQuestion += CheckEndOfGame;

        UpdateHintsNLabel();
        UpdateHPNumberLabel();
        ExecuteRandomQuestion();
    }

    private void OnDestroy()
    {
        OnWrongAnswer -= UpdateHPNumberLabel;
        OnHintUsed -= UpdateHintsNLabel;
    }

    #endregion

    #region Private methods

    private void AnswerCorrectHandling()
    {
        OnCorrectAnswer?.Invoke();
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
        _quizCoroutine = StartCoroutine(AnswerWrongSequence());
    }

    private IEnumerator AnswerWrongSequence()
    {
        yield return new WaitForSeconds(_delay);
        OnNewQuestion?.Invoke();
    }

    private void CheckEndOfGame()
    {
        if (_questions.Count <= 0)
        {
            ScenesLoader.LoadFinaleScene();
        }

        CheckHp();
    }

    private void CheckHp()
    {
        if (_stats.Hp <= 0)
        {
            ScenesLoader.LoadFinaleScene();
        }
    }

    private void ExecuteRandomQuestion()
    {
        OnNewQuestion?.Invoke();

        Random rnd = new();
        int randomInt = rnd.Next(0, _questions.Count);
        _currentQuestionNumber++;
        LoadQuestion(_questions[randomInt], _currentQuestionNumber);
        _questions.RemoveAt(randomInt);
    }

    private void HintUsageHandling()
    {
        if (_stats.Hints <= 0)
        {
            return;
        }

        OnHintUsed?.Invoke();
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
        _image.sprite = questionConfig.Sprite;

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
        _hintsNumerLabel.text = $"{_stats.Hints}";
    }

    private void UpdateHPNumberLabel()
    {
        _HPNumberLabel.text = $"{_stats.Hp}";
    }

    #endregion
}