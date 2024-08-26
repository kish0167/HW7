using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoader2Variant : MonoBehaviour
{
    #region Unity lifecycle

    private void Start()
    {
        if (FindObjectOfType<ScenesLoader2Variant>() != this)
        {
            DontDestroyOnLoad(this);
        }
    }

    #endregion

    #region Public methods

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void LoadFinaleScene()
    {
        SceneManager.LoadScene("FinaleScene");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    #endregion
}