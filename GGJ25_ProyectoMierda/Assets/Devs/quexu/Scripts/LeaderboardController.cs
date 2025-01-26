using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public List<TextMeshProUGUI> names;
    public List<TextMeshProUGUI> scores;
   
    private const int MAX_ENTRIES = 10;  // Número máximo de entradas en el leaderboard
    private List<PlayerScore> leaderboard = new List<PlayerScore>();

    // Cargar los datos del leaderboard desde PlayerPrefs
    void Start()
    {
        LoadLeaderboard();
    }

    private void LoadLeaderboard()
    {
        leaderboard.Clear(); // Limpiar la lista antes de cargar

        // Cargar los datos de los PlayerPrefs
        for (int i = 0; i < MAX_ENTRIES; i++)
        {
            string playerName = PlayerPrefs.GetString("PlayerName_" + i, "");
            int score = PlayerPrefs.GetInt("PlayerScore_" + i, 0);

            if (!string.IsNullOrEmpty(playerName))
            {
                leaderboard.Add(new PlayerScore(playerName, score));
            }
        }

        // Ordenar la lista de mayor a menor por puntuación
        leaderboard.Sort((x, y) => y.score.CompareTo(x.score));
    }
    private void SaveLeaderboard()
    {
        for (int i = 0; i < leaderboard.Count; i++)
        {
            PlayerPrefs.SetString("PlayerName_" + i, leaderboard[i].playerName);
            PlayerPrefs.SetInt("PlayerScore_" + i, leaderboard[i].score);
        }
        PlayerPrefs.Save();
    }

    // Añadir una nueva entrada al leaderboard
    public void AddNewEntry(string playerName, int score)
    {
        leaderboard.Add(new PlayerScore(playerName, score));

        // Ordenar la lista de mayor a menor
        leaderboard.Sort((x, y) => y.score.CompareTo(x.score));

        // Asegurarse de que no haya más de MAX_ENTRIES entradas
        if (leaderboard.Count > MAX_ENTRIES)
        {
            leaderboard.RemoveAt(leaderboard.Count - 1);
        }

        // Guardar el leaderboard actualizado
        SaveLeaderboard();
    }

    // Obtener el leaderboard
    public List<PlayerScore> GetLeaderboard()
    {
        return leaderboard;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadEntries()
    {
        
    }
    public void setEntry(string username, int score)
    {


    }
}
