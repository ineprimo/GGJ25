using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsText;

    [SerializeField] Image imagenBullet;
    [SerializeField] Sprite bulletSprite2;
    [SerializeField] Sprite bulletSprite3;

    [SerializeField] Image imagenHealth;
    [SerializeField] Sprite healthSprite2;
    [SerializeField] Sprite healthSprite3;

    [SerializeField] Image imagenSpeed;
    [SerializeField] Sprite speedSprite2;
    [SerializeField] Sprite speedSprite3;

    [SerializeField] Image imagenDamage;
    [SerializeField] Sprite damageSprite2;
    [SerializeField] Sprite damageSprite3;

    [SerializeField] private List<GameObject> _firstSplash;
    [SerializeField] private List<GameObject> _secondSplash;
    [SerializeField] private List<GameObject> _thirdSplash;
    [SerializeField] private List<GameObject> _fourthSplash;
    
    
    // Start is called before the first frame update
    void Start()
    {
        coinsText.text = "0";
        imagenBullet.enabled = false;
        imagenHealth.enabled = false;
        imagenSpeed.enabled = false;
        imagenDamage.enabled = false;
    }

    public void UpateSplash(int splashes, bool b)
    {
        switch (splashes)
        {
            case 1:
                foreach (GameObject splash in _firstSplash)
                {
                    splash.SetActive(b);
                }
                break;
            case 2:
                foreach (GameObject splash in _secondSplash)
                {
                    splash.SetActive(b);
                }
                break;
            case 3:
                foreach (GameObject splash in _thirdSplash)
                {
                    splash.SetActive(b);
                }
                break;
            case 4:
                foreach (GameObject splash in _fourthSplash)
                {
                    splash.SetActive(b);
                }
                break;
        }
    }

    public void UpdateUI()
    {

        if (GameManager.Instance != null)
        {
            if(GameManager.Instance.GetBulletsLvl() == 1) imagenBullet.enabled = true;
            else if(GameManager.Instance.GetBulletsLvl() == 2) imagenBullet.sprite = bulletSprite2;
            else if(GameManager.Instance.GetBulletsLvl() == 3) imagenBullet.sprite = bulletSprite3;

            if (GameManager.Instance.GetHealthLvl() == 1) imagenHealth.enabled = true;
            else if (GameManager.Instance.GetHealthLvl() == 2) imagenHealth.sprite = healthSprite2;
            else if (GameManager.Instance.GetHealthLvl() == 3) imagenHealth.sprite = healthSprite3;

            if (GameManager.Instance.GetSpeedLvl() == 1) imagenSpeed.enabled = true;
            else if (GameManager.Instance.GetSpeedLvl() == 2) imagenSpeed.sprite = speedSprite2;
            else if (GameManager.Instance.GetSpeedLvl() == 3) imagenSpeed.sprite = speedSprite3;

            if (GameManager.Instance.GetDamageLvl() == 1) imagenDamage.enabled = true;
            else if (GameManager.Instance.GetDamageLvl() == 2) imagenDamage.sprite = damageSprite2;
            else if (GameManager.Instance.GetDamageLvl() == 3) imagenDamage.sprite = damageSprite3;
            
            coinsText.text = GameManager.Instance.GetCoins().ToString();
            
            
        }
    }

}