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

    private string lastSubmittedName = ""; // Almacena el último nombre enviado

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) => {
            int loopLength = Mathf.Min(msg.Length, names.Count);

            for (int i = 0; i < loopLength; ++i)
            {
                string displayedName = RemoveUniqueSuffix(msg[i].Username); // Nombre sin sufijo
                names[i].text = displayedName;
                scores[i].text = msg[i].Score.ToString();
                names[i].gameObject.SetActive(true);
                scores[i].gameObject.SetActive(true);

                // Si el nombre coincide con el último nombre agregado, se resalta en amarillo
                if (displayedName == lastSubmittedName)
                {
                    names[i].color = Color.yellow;
                    scores[i].color = Color.yellow;
                }
                else
                {
                    names[i].color = Color.white;
                    scores[i].color = Color.white;
                }
            }

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

        lastSubmittedName = username; // Guarda el último nombre ingresado

        // Agregar sufijo único para evitar nombres duplicados
        string uniqueUsername = username + "_" + System.Guid.NewGuid().ToString("N").Substring(4, 6);

        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, uniqueUsername, score, ((msg) => {
            GetLeaderboard();
        }));
    }

    private string RemoveUniqueSuffix(string username)
    {
        // Si el nombre tiene un sufijo con "_" seguido de 6 caracteres hexadecimales, lo eliminamos
        int index = username.LastIndexOf('_');
        if (index != -1 && username.Length >= index + 7)
        {
            return username.Substring(0, index);
        }
        return username;
    }
}
