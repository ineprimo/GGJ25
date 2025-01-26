using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Unity.VisualScripting;

[System.Serializable]
public class PlayerScore
{
    public string playerName;
    public int score;

    public PlayerScore(string name, int score)
    {
        this.playerName = name;
        this.score = score;
    }
} 

public class LeaderboardController : MonoBehaviour
{
   
    private const int MAX_ENTRIES = 6;  
    private List<PlayerScore> leaderboard = new List<PlayerScore>();
    private int totalAmount;


    [SerializeField] private GameObject names;
    [SerializeField] private GameObject scores;

    void Start()
    {
        LoadLeaderboard();
        
        totalAmount = PlayerPrefs.GetInt("TotalAmount", 0);

        Debug.Log("ESTOY AQUIIIIIIIIIIIIIIIIIIIIIIIIIII" + leaderboard.Count);

      
        Debug.Log(totalAmount);
    }

    private void logLeader()
    {
        Debug.Log("LOG DEL LEADEEEER");
        for (int i = 0; i < leaderboard.Count; i++)
        {
            Debug.Log("Player: " + leaderboard[i].playerName + " | Score: " + leaderboard[i].score);
        }
    }
    private void LoadLeaderboard()
    {
        leaderboard.Clear();

        for (int i = 0; i < totalAmount; i++)
        {
            string playerName = PlayerPrefs.GetString("PlayerName_" + i, "");
            int score = PlayerPrefs.GetInt("PlayerScore_" + i, 0);

            if (!string.IsNullOrEmpty(playerName))
                leaderboard.Add(new PlayerScore(playerName, score));
        }

        leaderboard.Sort((x, y) => y.score.CompareTo(x.score));
        Debug.Log("Llamamos a loadLeader");
    }
    private void SaveLeaderboard()
    {
        Debug.Log("Llamamos a saveleader");
        PlayerPrefs.DeleteAll();

        for (int i = 0; i < leaderboard.Count; i++)
        {
            PlayerPrefs.SetString("PlayerName_" + i, leaderboard[i].playerName);
            PlayerPrefs.SetInt("PlayerScore_" + i, leaderboard[i].score);
        }

        PlayerPrefs.SetInt("TotalAmount", leaderboard.Count);
        PlayerPrefs.Save();
    }

    public void PrintInLeaderboard()
    {
        for (int i = 0; i < MAX_ENTRIES; i++)
        {
            // actualiza el text mesh pro
            names.transform.GetChild(i).GetComponent<TextMeshPro>().text = leaderboard[i].playerName;
            scores.transform.GetChild(i).GetComponent<TextMeshPro>().text = leaderboard[i].score.ToString();
        }
    }
    public void AddNewEntry(string playerName, int score)
    {
        Debug.Log("Llamamos a addNewEntry");
        leaderboard.Add(new PlayerScore(playerName, score));

        leaderboard.Sort((x, y) => y.score.CompareTo(x.score));
        
        
        logLeader();
        
        SaveLeaderboard();
        PrintInLeaderboard();
    }

    public List<PlayerScore> GetLeaderboard()
    {
        Debug.Log("Llamamos a getLEaderBoard");
        return leaderboard;
    }

    public int GetTotalAmount()
    {
        Debug.Log("Llamamos a getTotalAmount");
        return totalAmount;
    }

    public void RestartGame()
    {


    }
    // Update is callsed once per frame
    void Update()
    {
        
    }

}
