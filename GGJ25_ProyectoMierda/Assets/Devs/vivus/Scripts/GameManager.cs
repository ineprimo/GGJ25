using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{

    // ANIMATION
    [SerializeField] AnimationManager _animationManager;

    public enum Upgrades
    {
        BULLETS,
        LIFE,
        SPEED,
        DAMAGE
    }

    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    //LEADERBOARD
    private LeaderboardController leaderboard;

    // UI
    [SerializeField] private GameObject UIManager;
    [SerializeField] private HUDController _hud;
    private int score=0;
    // PLAYER
    [SerializeField] private GameObject _player;
    public GameObject GetPlayer() {  return _player; }

    //WEAPON
    [SerializeField] private GameObject _gun;

    // ENEMIES
    [SerializeField] private int maxEnemies = 100;
    private int nEnemies;

    // devuelve true cuando nEnemies sea >= maxEnemies
    public bool getMaxEnemies() { return nEnemies >= maxEnemies; }
    public int getNEnemies() { return nEnemies; }

    public List<GameObject> SceneEnemies { get; private set; } = new List<GameObject>();

    // UPGRADES
    [Header("UPGRADES")]
    [Header("Weapon")]
    [SerializeField] private int _bulletsUpgradeLvl = 0;
    [SerializeField] private int _gunAmmo = 10;
    [SerializeField] private int _ARAmmo = 15;

    public int getARAmmo() { return _ARAmmo; }
    public int getGunAmmo() { return _gunAmmo; }

    [Header("Health")]
    [SerializeField] private int _healthUpgradeLvl = 0;  
    [SerializeField] private float _healthIncreaseLvl1 = 15.0f;
    [SerializeField] private float _healthIncreaseLvl2 = 30.0f;
    [SerializeField] private float _healthIncreaseLvl3 = 50.0f;
    [SerializeField] private float _shieldCooldownReduction = 15.0f;
    
    [Header("Speed")]
    [SerializeField] private int _speedUpgradeLvl = 0;
    [SerializeField] private float _speedIncreaseLvl1 = 30.0f;
    [SerializeField] private float _speedIncreaseLvl2 = 30.0f;
    [SerializeField] private float _speedIncreaseLvl3 = 30.0f;

    [SerializeField] private float _traceDamageLvl1 = 1.0f;
    [SerializeField] private float _traceDamageLvl2 = 3.0f;
    [SerializeField] private float _traceBubbleLifeTimeLvl3 = 5.0f;

    [Header("Damage")]
    [SerializeField] private int _damageUpgradeLvl = 0;
    [SerializeField] private float _atkIncreaseLvl1 = 3.0f;
    [SerializeField] private float _atkIncreaseLvl2= 6.0f;
    [SerializeField] private float _atkIncreaseLvl3 = 10.0f;


    public AnimationManager GetAnimationManager() { return _animationManager; }



    // CANTIDAD DE BALAS
    public void UpgradeBullets()
    {

        if (_bulletsUpgradeLvl <4 )
        {
            _bulletsUpgradeLvl++;
            _gun.GetComponent<Shoot>().gunLevel = _bulletsUpgradeLvl + 1;

            if(_gun.GetComponent<Shoot>().gunLevel == 4)
            {
                _gun.GetComponent<Shoot>().currentAmmo = _ARAmmo;


                // actualiza el player
                _player.GetComponent<PlayerMovement>().changeWeapon(1);

                // cambia a la pistola
                _animationManager.ChangeCurrentAnimator(1);
            }
            else
            {
                _gun.GetComponent<Shoot>().currentAmmo = _gunAmmo;
            }
           
        }

        _hud.UpdateUI();

        //  UIManager.GetComponentInChildren<HUDController>().UpdateUI();          
    }
    // VIDA
    public void UpgradeLife()
    {
        _healthUpgradeLvl++;
        switch (_healthUpgradeLvl)
        {
            case 1:
                _player.GetComponent<PlayerMovement>().ImproveMaxLife(_healthIncreaseLvl1);
                _player.GetComponent<BubbleShield>().enabled = true;
                break;
            case 2:
                _player.GetComponent<PlayerMovement>().ImproveMaxLife(_healthIncreaseLvl2);
                _player.GetComponent<BubbleShield>().UpdateAbility(_shieldCooldownReduction);
                break;
            case 3:
                _player.GetComponent<PlayerMovement>().ImproveMaxLife(_healthIncreaseLvl3);
                _player.GetComponent<BubbleShield>().UpdateAbility(_shieldCooldownReduction);
                break;
        }
        _hud.UpdateUI();
    }
    // VELOCIDAD Y RASTRO
    public void UpgradeSpeed()
    {
        _speedUpgradeLvl++;
        if (_speedUpgradeLvl == 1)
        {
            _player.GetComponent<PlayerMovement>().ImproveSpeed(_speedIncreaseLvl1);     // Aumentar velocidad de movimiento
            _player.GetComponent<TraceComponent>().enabled = true;   // Se empieza a crear el rastro de burbujas
            _player.GetComponent<TraceComponent>().SetCurrentBubbleDamage(_traceDamageLvl1);   // Setear daño de las burbujas
        }
        else if (_speedUpgradeLvl == 2)
        {
            _player.GetComponent<PlayerMovement>().ImproveSpeed(_speedIncreaseLvl2);     // Aumentar velocidad de movimiento
            _player.GetComponent<TraceComponent>().SetCurrentBubbleDamage(_traceDamageLvl2);   // Setear daño de las burbujas

        }
        else if (_speedUpgradeLvl == 3)
        {
            _player.GetComponent<PlayerMovement>().ImproveSpeed(_speedIncreaseLvl3);     // Aumentar velocidad de movimiento
            _player.GetComponent<TraceComponent>().SetCurrentBubbleLifeTime(_traceBubbleLifeTimeLvl3);   // Setear daño de las burbujas

        }
        _hud.UpdateUI();
    }
    // DA�O Y REBOTES
    public void UpgradeDamage()
    {
        // sumar un componente en la pistola para que las burbujas reboten

        _damageUpgradeLvl++;
        if (_damageUpgradeLvl == 1)
        {
            // + da�o


            _player.GetComponent<PlayerMovement>().ImproveSpeed(_speedIncreaseLvl3);     // Aumentar velocidad de movimiento


            // la pompa rebota 1 vez si hay un enemigo a X distancia
            Shoot pistola = _player.GetComponentInChildren<Shoot>();
            pistola.MakeBouncyBubbles(1);
        }
        else if (_damageUpgradeLvl == 2)
        {
            // + da�o
            // la burbuja rebota 2 veces
            Shoot pistola = _player.GetComponentInChildren<Shoot>();

            // la pompa rebota 1 vez si hay un enemigo a X distancia

        }
        else if (_damageUpgradeLvl == 3)
        {
            // + da�o
            // la burbuja puede volver a rebotar
            Shoot pistola = _player.GetComponentInChildren<Shoot>();
            pistola.MakeBouncyBubbles(3);

            // la pompa rebota 1 vez si hay un enemigo a X distancia

        }
        _hud.UpdateUI();

    }

    // GESTION DE ENEMIGOS
    public void registerEnemy(GameObject e)
    {
        nEnemies++;
        SceneEnemies.Add(e);
    }

    public void deRegisterEnemy(GameObject e)
    {
        SceneEnemies.Remove(e);
        nEnemies--;
    }

    public int GetBulletsLvl()
    {
        return _bulletsUpgradeLvl;
    }
    public int GetHealthLvl()
    {
        return _healthUpgradeLvl;
    }
    public int GetSpeedLvl()
    {
        return _speedUpgradeLvl;
    }
    public int GetDamageLvl()
    {
        return _damageUpgradeLvl;
    }

    public void addCoins(int nCoins)
    {
        _player.GetComponent<PlayerMovement>().addCoins(nCoins);
        _hud.UpdateUI();
    }
    
    public void RemoveCoins(int nCoins)
    {
        _player.GetComponent<PlayerMovement>().subCoins(nCoins);
        _hud.UpdateUI();
    }

    public void increaseScore(int nScore)
    {
        score += nScore;
    }

    public void EndGame()
    {
        UIManager.GetComponent<UIManager>().DesactivarHUD();
        Cursor.lockState = CursorLockMode.None;
        UIManager.GetComponent<UIManager>().ActivarScoreboard(score);
        Time.timeScale = 0;
    }
    public int GetScore()
    {
        return score;
    }
    public int GetCoins()
    {
        return _player.GetComponent<PlayerMovement>().GetCoins();
    }

    // Start is called before the first frame update
    void Start()
    {
        leaderboard = FindFirstObjectByType<LeaderboardController>();
        _player.GetComponent<PlayerMovement>().setCoins(0);
        score = 10;
        Invoke("EndGame", 3.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}