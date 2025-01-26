using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject Scoreboard;
    void Start()
    {
        
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

    public void ActivarScoreboard()
    {
        Scoreboard.SetActive(true);
    }
    public void DesactivarScoreboard()
    {
        Scoreboard.SetActive(false);
    }
    
    public GameObject Hud() {return HUD;}
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
