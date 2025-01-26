using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LeaderboardController : MonoBehaviour
{
    public List<TextMeshProUGUI> names;
    public List<TextMeshProUGUI> scores;
    struct Jugador
    {
        string name; 
        int score;
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
