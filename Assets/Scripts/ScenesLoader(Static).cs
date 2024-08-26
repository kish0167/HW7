using UnityEngine;
using UnityEngine.SceneManagement;

public static class ScenesLoader
{
    #region Public methods

    public static void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public static void LoadFinaleScene()
    {
        SceneManager.LoadScene("FinaleScene");
    }

    public static void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public static void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    #endregion
}