using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> scores;

    private string publicLeaderboardKey = "2d9b8566133582240b46c86a93a0a7775f9671185d5f5d8d47b8c2773d6f0372";

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) => {
            int loopLength = Mathf.Min(msg.Length, names.Count);

            // Rellenar los textos visibles
            for (int i = 0; i < loopLength; ++i)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
                names[i].gameObject.SetActive(true);
                scores[i].gameObject.SetActive(true);
            }

            // Ocultar los textos restantes que no tienen datos
            for (int i = loopLength; i < names.Count; ++i)
            {
                names[i].gameObject.SetActive(false);
                scores[i].gameObject.SetActive(false);
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        Debug.Log("" + username + " " + score);
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((msg) => {
            GetLeaderboard();
        }));
    }
}
