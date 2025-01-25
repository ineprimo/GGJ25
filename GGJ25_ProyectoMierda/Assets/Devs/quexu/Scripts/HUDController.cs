using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bulletText;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] TextMeshProUGUI damageText;
    [SerializeField] TextMeshProUGUI coinsText;
    // Start is called before the first frame update
    void Start()
    {
        coinsText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null)
        {
            //bulletText.text =  GameManager.Instance.GetBulletsLvl().ToString();
            //healthText.text =  GameManager.Instance.GetHealthLvl().ToString();
            //speedText.text =  GameManager.Instance.GetSpeedLvl().ToString();
            //damageText.text =  GameManager.Instance.GetDamageLvl().ToString();
            coinsText.text = GameManager.Instance.GetCoins().ToString();
            
        }
    }

}