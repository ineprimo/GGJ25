using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
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

    public List<GameObject> SceneEnemies { get; private set; } = new List<GameObject>();

    // UPGRADES
    [Header("UPGRADES")]
    [Header("Weapon")]
    [SerializeField] private int _bulletsUpgradeLvl = 0;
    
    [Header("Health")]
    [SerializeField] private int _healthUpgradeLvl = 0;  
    [SerializeField] private float _healthIncreaseLvl1 = 15.0f;
    [SerializeField] private float _healthIncreaseLvl2 = 30.0f;
    [SerializeField] private float _healthIncreaseLvl3 = 50.0f;
    [SerializeField] private float _shieldCooldownReduction = 15.0f;
    
    [Header("Speed")]
    [SerializeField] private int _speedUpgradeLvl = 0;
    [SerializeField] private float _speedIncreaseLvl1 = 3.0f;
    [SerializeField] private float _speedIncreaseLvl2 = 6.0f;
    [SerializeField] private float _speedIncreaseLvl3 = 10.0f;
    
    [Header("Damage")]
    [SerializeField] private int _damageUpgradeLvl = 0;
    

    // CANTIDAD DE BALAS
    public void UpgradeBullets()
    {

        if (_bulletsUpgradeLvl < 3)
        {
            _bulletsUpgradeLvl++;
            _gun.GetComponent<Shoot>().gunLevel = _bulletsUpgradeLvl + 1;
            _gun.GetComponent<Shoot>().currentAmmo = 10;
        }
        if (_bulletsUpgradeLvl == 1)
        {
            // Salen 2 burbujas
        }
        else if(_bulletsUpgradeLvl == 2)
        {
            // salen 3 burbujas
            // m�s distancia
        }
        else if (_bulletsUpgradeLvl == 3) 
        {
            //????
        }
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
    }
    // VELOCIDAD Y RASTRO
    public void UpgradeSpeed()
    {
        Debug.Log("ENtroooo");
        _speedUpgradeLvl++;
        if (_speedUpgradeLvl == 1)
        {
            _player.GetComponent<PlayerMovement>().ImproveSpeed(_speedIncreaseLvl1);     // Aumentar velocidad de movimiento
            _player.GetComponent<TraceComponent>().ActivateSigned();    // Se empieza a crear el rastro de burbujas
        }
        else if (_speedUpgradeLvl == 2)
        {
            _player.GetComponent<PlayerMovement>().ImproveSpeed(_speedIncreaseLvl2);     // Aumentar velocidad de movimiento
            // el rastro hace da�o
        }
        else if (_speedUpgradeLvl == 3)
        {
            _player.GetComponent<PlayerMovement>().ImproveSpeed(_speedIncreaseLvl3);     // Aumentar velocidad de movimiento
            // + el rastro dura m�s tiempo
        }
    }
    // DA�O Y REBOTES
    public void UpgradeDamage()
    {
        // sumar un componente en la pistola para que las burbujas reboten

        _damageUpgradeLvl++;
        if (_damageUpgradeLvl == 1)
        {
            // + da�o

            Debug.Log("1 bounce");

            // la pompa rebota 1 vez si hay un enemigo a X distancia
            Shoot pistola = _player.GetComponentInChildren<Shoot>();
            pistola.MakeBouncyBubbles(1);
        }
        else if (_damageUpgradeLvl == 2)
        {
            // + da�o
            // la burbuja rebota 2 veces
            Shoot pistola = _player.GetComponentInChildren<Shoot>();
            pistola.MakeBouncyBubbles(2);

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
    }

    // GESTION DE ENEMIGOS
    public void registerEnemy(GameObject e)
    {
        nEnemies++;
        SceneEnemies.Add(e);
        //Debug.Log(nEnemies);
    }

    public void deRegisterEnemy()
    {
        nEnemies--;
    }

    // la caca lanzada toca al jugador:
    public void playerHit()
    {
        Debug.Log("damage player");
        // vida -- (poli: sigo mañana)
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

    // Start is called before the first frame update
    void Start()
    {
        Invoke("UpgradeSpeed", 3f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}