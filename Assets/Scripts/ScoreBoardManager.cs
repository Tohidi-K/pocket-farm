using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ScoreBoardManager : MonoBehaviour
{
    public static ScoreBoardManager Instance;
    private string savePath;
    public List<ScoreEntry> highScores = new List<ScoreEntry>();
    public int maxEntries = 4;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            savePath = Application.persistentDataPath + "/scores.json";
            LoadScores();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsHighScore(int score)
    {
        if (highScores.Count < maxEntries) return true;
        return score > highScores[highScores.Count - 1].score;
    }

    public void AddScore(string name, int score)
    {
        highScores.Add(new ScoreEntry(name, score));
        highScores = highScores.OrderByDescending(e => e.score).Take(maxEntries).ToList();
        SaveScores();
    }

    private void SaveScores()
    {
        string json = JsonUtility.ToJson(new ScoreListWrapper(highScores), true);
        File.WriteAllText(savePath, json);
    }

    private void LoadScores()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            ScoreListWrapper wrapper = JsonUtility.FromJson<ScoreListWrapper>(json);
            highScores = wrapper.scores;
        }
    }

    [System.Serializable]
    private class ScoreListWrapper
    {
        public List<ScoreEntry> scores;
        public ScoreListWrapper(List<ScoreEntry> list)
        {
            scores = list;
        }
    }
}
