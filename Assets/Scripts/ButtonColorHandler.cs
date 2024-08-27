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
        _quiz.OnNewQuestion += DePaintButtons;
        _quiz.OnHintUsed += Paint2ButtonsRed;
    }

    private void OnDestroy()
    {
        _quiz.OnCorrectAnswer -= PaintButtons;
        _quiz.OnWrongAnswer -= PaintButtons;
        _quiz.OnNewQuestion -= DePaintButtons;
        _quiz.OnHintUsed -= Paint2ButtonsRed;
    }

    #endregion

    #region Private methods

    private void DePaintButtons()
    {
        foreach (Button button in _buttons)
        {
            ColorBlock colors = button.colors;
            colors.normalColor = Color.white;
            button.colors = colors;
        }
    }

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

    private void Paint2ButtonsRed()
    {
        for (int i = 0; i < 4; i++)
        {
            if (_quiz.AnswersMap[i] == 2 || _quiz.AnswersMap[i] == 3)
            {
                MakeButtonRed(_buttons[i]);
            }
        }
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