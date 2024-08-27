using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndgameMenu : MonoBehaviour
{
    #region Variables

    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;
    [SerializeField] private Image _resultImage;
    [SerializeField] private Sprite _loseSprite;
    [SerializeField] private Sprite _winSprite;
    [SerializeField] private TMP_Text _statsText;

    #endregion

    #region Unity lifecycle

    private void Start()
    {
        if (StatisticsHandler.Hp <= 0)
        {
            _resultImage.sprite = _loseSprite;
        }
        else
        {
            _resultImage.sprite = _winSprite;
        }
        _yesButton.onClick.AddListener(ScenesLoader.LoadStartScene);
        _noButton.onClick.AddListener(ScenesLoader.ExitGame);
        
        UpdateStatsText();
    }

    private void UpdateStatsText()
    {
        _statsText.text = $"correct answers: {StatisticsHandler.CorrectGuesses}\n" +
                          $"wrong answers: {StatisticsHandler.WrongGuesses}";
    }
    
    #endregion
}