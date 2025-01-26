using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class LeaderboardController : MonoBehaviour
{
    public List<TextMeshProUGUI> names;
    public List<TextMeshProUGUI> scores;

    private string publicKey = "03314c31f589727710c97c2dac8426eadaa912851548f3ee07ab844555abf46e";
    
    // Start is called before the first frame update
    void Start()
    {
        LoadEntries();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadEntries()
    {
        Leaderboards.TechnoPoopy.GetEntries(entry =>
        {
            foreach (TextMeshProUGUI name in names)
            {
                name.text = "";
            }
            foreach (TextMeshProUGUI score in scores)
            {
                score.text = "";
            }

            float length = Mathf.Min(names.Count, entry.Length);

            for (int i = 0; i < length; i++)
            {
                names[i].text = entry[i].Username;
                scores[i].text = entry[i].Score.ToString();
            }
        });
    }
    public void setEntry(string username, int score)
    {
        Leaderboards.TechnoPoopy.UploadNewEntry(username, score, isSuccessful =>
        {
            if(isSuccessful)
            {
                LoadEntries();
            }
        });
    }
}
