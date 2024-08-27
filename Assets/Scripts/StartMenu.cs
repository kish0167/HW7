using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    #region Variables

    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;
    private StatisticsHandler _stats;

    #endregion

    #region Unity lifecycle

    private void Start()
    {
        _stats = FindObjectOfType<StatisticsHandler>();

        _startButton.onClick.AddListener(ScenesLoader.LoadGameScene);
        _exitButton.onClick.AddListener(ScenesLoader.ExitGame);
    }

    #endregion
}