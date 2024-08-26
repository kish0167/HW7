using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    #region Variables

    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;
    private ScenesLoader2Variant _scenesLoader2Variant;

    #endregion

    #region Unity lifecycle

    private void Start()
    {
        /*_scenesLoader = FindObjectOfType<ScenesLoader>();
        _startButton.onClick.AddListener(_scenesLoader.LoadGameScene);
        _exitButton.onClick.AddListener(_scenesLoader.ExitGame);*/
        
        _startButton.onClick.AddListener(ScenesLoader.LoadGameScene);
        _exitButton.onClick.AddListener(ScenesLoader.ExitGame);
        
    }

    #endregion
}