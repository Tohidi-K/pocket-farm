using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject nameInputPanel;
    public TMP_InputField nameInputField;
    public GameObject scoreboardPanel;
    public List<TMP_Text> scores;
    //public TMP_Text scoreboardText;

    private void Start()
    {
        nameInputField.characterLimit = 12;

        if (ScoreManager.Instance != null)
        {
            int finalScore = ScoreManager.Instance.GetGold();

            if (ScoreBoardManager.Instance.IsHighScore(finalScore))
            {
                nameInputPanel.SetActive(true);
            }
            else
            {
                ShowScoreboard();
            }
        }
    }

    public void OnOptionButtonClick()
    {
        if (!optionsPanel.activeInHierarchy)
        {
            optionsPanel.SetActive(true);
        }
        else
        {
            optionsPanel.SetActive(false);
        }
    }

    public void OnTopScoresButtonClick()
    {
        if (!scoreboardPanel.activeInHierarchy)
        {
            ShowScoreboard();
        }
        else
        {
            scoreboardPanel.SetActive(false);
        }
    }

    public void SubmitName()
    {
        string playerName = nameInputField.text;
        int finalScore = ScoreManager.Instance.GetGold();

        ScoreBoardManager.Instance.AddScore(playerName, finalScore);
        nameInputPanel.SetActive(false);
        ShowScoreboard();
    }

    void ShowScoreboard()
    {
        scoreboardPanel.SetActive(true);

        for (int i=0; i<4; i++)
        {
            if (i < ScoreBoardManager.Instance.highScores.Count && ScoreBoardManager.Instance.highScores[i] != null)
            {
                scores[i].text = $"{ScoreBoardManager.Instance.highScores[i].playerName} - {ScoreBoardManager.Instance.highScores[i].score}";
            }
            else
            {
                scores[i].text = "Empty";
            }
        }
    }
}
