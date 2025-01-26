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


    // Start is called before the first frame update
    void Start()
    {

        string stringjson = PlayerPrefs.GetString("Leaderboard");

        string json = JsonUtility.FromJson<Jugador>(stringjson);

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
        PlayerPrefs.

    }
}
