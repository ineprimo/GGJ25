using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject Scoreboard;
    [SerializeField] private GameObject Scoreboard2;

    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TMP_InputField nombrePlayer;
    [SerializeField] private Button submit;
    [SerializeField] private GameObject leaderboard;
    void Start()
    {
        submit.onClick.AddListener(() =>
        {
            saveScore();
        });
    }
    public void ActivarMenu()
    {
        Menu.SetActive(true);
    }
    public void DesactivarMenu()
    {
        Menu.SetActive(false);
    }

    public void ActivarHUD()
    {
        HUD.SetActive(true);
    }
    public void DesactivarHUD()
    {
        HUD.SetActive(false);
    }

    public void ActivarScoreboard(int updateScore)
    {
        Scoreboard.SetActive(true);
        score.text = updateScore.ToString();
    }
    public void DesactivarScoreboard()
    {
        Scoreboard.SetActive(false);
    }
    public void ActivarScoreboard2()
    {
        Scoreboard2.SetActive(true);
    }
    public void DesactivarScoreboard2()
    {
        Scoreboard2.SetActive(false);
    }
    public void saveScore()
    {
        if (nombrePlayer.text != "" && nombrePlayer.text.Length < 13)
        {
            leaderboard.GetComponent<LeaderboardController>().AddNewEntry(nombrePlayer.text, GameManager.Instance.GetScore());
            DesactivarScoreboard();
            ActivarScoreboard2();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
