using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColorHandler : MonoBehaviour
{
    #region Variables

    [SerializeField] private Quiz _quiz;
    [SerializeField] private List<Button> _buttons;

    #endregion

    #region Unity lifecycle

    private void Start()
    {
        _quiz.OnCorrectAnswer += PaintButtons;
        _quiz.OnWrongAnswer += PaintButtons;
        //_quiz.OnNewQuewstion += PaintButtons;
    }

    #endregion

    #region Private methods

    private void MakeButtonGreen(Button button)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = Color.green;
        button.colors = colors;
    }

    private void MakeButtonRed(Button button)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = Color.red;
        button.colors = colors;
    }

    private void PaintButtons()
    {
        for (int i = 0; i < 4; i++)
        {
            if (_quiz.AnswersMap[i] == 0)
            {
                MakeButtonGreen(_buttons[i]);
            }
            else
            {
                MakeButtonRed(_buttons[i]);
            }
        }
    }

    #endregion
}