using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuUI : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject nameInputPanel;
    public TMP_InputField nameInputField;
    public GameObject scoreboardPanel;
    public TMP_Text scoreboardText;

    private void Start()
    {
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
        scoreboardText.text = "";

        foreach (var entry in ScoreBoardManager.Instance.highScores)
        {
            scoreboardText.text += $"{entry.playerName} - {entry.score}\n\n";
        }
    }
}
