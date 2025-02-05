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
    private const string GAME_VERSION = "1.0.7"; // Cambia esto según la versión del juego

    private string lastSubmittedName = ""; // Almacena el último nombre enviado

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) => {
            List<(string Username, int Score)> filteredEntries = new List<(string, int)>();

            // Filtrar solo las entradas con la misma versión
            foreach (var entry in msg)
            {
                if (entry.Extra == GAME_VERSION) // Verifica la versión
                {
                    filteredEntries.Add((entry.Username, entry.Score));
                }
            }

            int loopLength = Mathf.Min(filteredEntries.Count, names.Count);

            for (int i = 0; i < loopLength; ++i)
            {
                string displayedName = RemoveUniqueSuffix(filteredEntries[i].Username);
                names[i].text = displayedName;
                scores[i].text = filteredEntries[i].Score.ToString();
                names[i].gameObject.SetActive(true);
                scores[i].gameObject.SetActive(true);

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

        // Enviar versión del juego en el campo "extra"
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, uniqueUsername, score, GAME_VERSION, ((msg) => {
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
