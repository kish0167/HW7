using UnityEngine;

public class StatisticsHandler : MonoBehaviour
{
    #region Variables

    private Quiz _quiz;

    #endregion

    #region Properties

    public int CorrectGuesses { get; set; }

    public int Hints { get; set; }

    public int Hp { get; set; }

    public int WrongGuesses { get; set; }

    #endregion

    #region Unity lifecycle

    private void Start()
    {
        if (FindObjectOfType<StatisticsHandler>() == this)
        {
            DontDestroyOnLoad(this);
        }
    }

    #endregion

    #region Public methods

    public void ResetState(int startHP, int startHints)
    {
        _quiz = FindObjectOfType<Quiz>();

        _quiz.OnCorrectAnswer += AddCorrectGuess;
        _quiz.OnWrongAnswer += AddWrongGuess;
        _quiz.OnWrongAnswer += ReduceHP;
        _quiz.OnHintUsed += ReduceHints;

        CorrectGuesses = 0;
        WrongGuesses = 0;
        Hp = startHP;
        Hints = startHints;
    }

    #endregion

    #region Private methods

    private void AddCorrectGuess()
    {
        CorrectGuesses++;
    }

    private void AddWrongGuess()
    {
        WrongGuesses++;
    }

    private void ReduceHints()
    {
        if (Hints > 0)
        {
            Hints--;
        }
    }

    private void ReduceHP()
    {
        if (Hp>0)
        {
            Hp--;
        }
    }

    #endregion
}