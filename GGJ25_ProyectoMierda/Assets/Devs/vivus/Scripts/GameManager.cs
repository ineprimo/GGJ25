using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Upgrades
    {
        BULLETS,
        LIFE,
        SPEED,
        DAMAGE
    }

    static private GameManager _instance;

    static public GameManager Instance
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

    // ENEMIES
    [SerializeField] private int maxEnemies = 100;
    private int nEnemies;

    // devuelve true cuando nEnemies sea >= maxEnemies
    public bool getMaxEnemies() { return nEnemies >= maxEnemies; }

    public List<GameObject> SceneEnemies { get; private set; } = new List<GameObject>();

    // UPGRADES
    [SerializeField] int bulletsUpgradeLvl = 0;
    [SerializeField] int lifeUpgradeLvl = 0;
    [SerializeField] int speedUpgradeLvl = 0;
    [SerializeField] int damageUpgradeLvl = 0;

    // CANTIDAD DE BALAS
    public void UpgradeBullets()
    {
        bulletsUpgradeLvl++;
        if(bulletsUpgradeLvl == 1)
        {
            // Salen 2 burbujas
        }
        else if(bulletsUpgradeLvl == 2)
        {
            // salen 3 burbujas
            // m�s distancia
        }
        else if (bulletsUpgradeLvl == 3) 
        {
            //????
        }
    }
    // VIDA
    public void UpgradeLife()
    {
        lifeUpgradeLvl++;
        switch (lifeUpgradeLvl)
        {
            case 1:
                _player.GetComponent<PlayerMovement>().ImproveMaxLife(15);
                break;
            case 2:
                _player.GetComponent<PlayerMovement>().ImproveMaxLife(15);
                _player.GetComponent<BubbleShield>().enabled = true;
                break;
            case 3:
                _player.GetComponent<PlayerMovement>().ImproveMaxLife(15);
                _player.GetComponent<BubbleShield>().UpdateAbility(5.0f);
                break;
        }
    }
    // VELOCIDAD Y RASTRO
    public void UpgradeSpeed()
    {
        speedUpgradeLvl++;
        if (speedUpgradeLvl == 1)
        {
            _player.GetComponent<PlayerMovement>().ImproveSpeed(3);     // Aumentar velocidad de movimiento
            _player.GetComponent<TraceComponent>().ActivateSigned();    // Se empieza a crear el rastro de burbujas
        }
        else if (speedUpgradeLvl == 2)
        {
            _player.GetComponent<PlayerMovement>().ImproveSpeed(2);     // Aumentar velocidad de movimiento
            // el rastro hace da�o
        }
        else if (speedUpgradeLvl == 3)
        {
            _player.GetComponent<PlayerMovement>().ImproveSpeed(3);     // Aumentar velocidad de movimiento
            // + el rastro dura m�s tiempo
        }
    }
    // DA�O Y REBOTES
    public void UpgradeDamage()
    {
        // sumar un componente en la pistola para que las burbujas reboten

        damageUpgradeLvl++;
        if (damageUpgradeLvl == 1)
        {
            // + da�o

            Debug.Log("1 bounce");

            // la pompa rebota 1 vez si hay un enemigo a X distancia
            Shoot pistola = _player.GetComponentInChildren<Shoot>();
            pistola.MakeBouncyBubbles(1);
        }
        else if (damageUpgradeLvl == 2)
        {
            // + da�o
            // la burbuja rebota 2 veces
            Shoot pistola = _player.GetComponentInChildren<Shoot>();
            pistola.MakeBouncyBubbles(2);

            // la pompa rebota 1 vez si hay un enemigo a X distancia

        }
        else if (damageUpgradeLvl == 3)
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


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}