using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputScore;
    [SerializeField] private TMP_InputField inputName;

    public UnityEvent<string, int> SubmitScoreEvent;
    public void SubmitScore()
    {
        SubmitScoreEvent.Invoke(inputName.text, int.Parse(inputScore.text));
        Debug.Log("score = " + inputScore.text);
    }
}
