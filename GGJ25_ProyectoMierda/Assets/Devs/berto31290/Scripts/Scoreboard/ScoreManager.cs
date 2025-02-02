using TMPro;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputScore;
    [SerializeField] private TMP_InputField inputName;

    

    public UnityEvent<string, int> SubmitScoreEvent;

    public void SubmitScore()
    {
        string playerName = inputName.text.Trim(); // Elimina espacios en blanco alrededor
        int playerScore = int.Parse(inputScore.text);

        if (playerName.Length < 2)
        { 
           SubmitScoreEvent.Invoke("Desconocido", playerScore);
        }
        else
        {
            SubmitScoreEvent.Invoke(inputName.text, playerScore);

        }
    }

   
}
