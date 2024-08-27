public static class StatisticsHandler
{
    #region Variables

    private static int _correctGuesses;
    private static int _hints;
    private static int _hp;

    private static int _wrongGuesses;

    #endregion

    #region Properties

    public static int CorrectGuesses
    {
        get => _correctGuesses;
        set => _correctGuesses = value;
    }

    public static int Hints
    {
        get => _hints;
        set => _hints = value;
    }

    public static int Hp
    {
        get => _hp;
        set => _hp = value;
    }

    public static int WrongGuesses
    {
        get => _wrongGuesses;
        set => _wrongGuesses = value;
    }

    #endregion

    #region Private methods

    public static void Reset(int startHP, int startHints)
    {
        _correctGuesses = 0;
        _wrongGuesses = 0;
        _hp = startHP;
        _hints = startHints;
    }

    #endregion
}