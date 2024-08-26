using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndgameMenu : MonoBehaviour
{
    #region Variables

    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;

    #endregion

    #region Unity lifecycle

    private void Start()
    {
        _yesButton.onClick.AddListener(ScenesLoader.LoadStartScene);
        _noButton.onClick.AddListener(ScenesLoader.ExitGame);
    }
    
    #endregion
}