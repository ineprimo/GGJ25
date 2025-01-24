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

    // UPGRADES
    int bulletsUpgradeLvl = 0;
    int lifeUpgradeLvl = 0;
    int speedUpgradeLvl = 0;
    int damageUpgradeLvl = 0;

    // CANTIDAD DE BALAS
    public void upgradeBullets()
    {
        bulletsUpgradeLvl++;
        if(bulletsUpgradeLvl == 1)
        {
            // Salen 2 burbujas
        }
        else if(bulletsUpgradeLvl == 2)
        {
            // salen 3 burbujas
            // más distancia
        }
        else if (bulletsUpgradeLvl == 3) 
        {
            //????
        }
    }
    // VIDA
    public void upgradeLife()
    {
        lifeUpgradeLvl++;
        if (lifeUpgradeLvl == 1)
        {
            // + vida 
            // burbuja enemigos en area (cada n segs)
        }
        else if (lifeUpgradeLvl == 2)
        {
            //  + vida
            // reducir tiempo habilidad
        }
        else if (lifeUpgradeLvl == 3)
        {
            // + vida
            // reducir tiempo habilidad
        }
    }
    // VELOCIDAD Y RASTRO
    public void upgradeSpeed()
    {
        speedUpgradeLvl++;
        if (speedUpgradeLvl == 1)
        {
            // + speed 
            _player.GetComponent<TraceComponent>().ActivateSigned();    // Se empieza a crear el rastro de burbujas
        }
        else if (speedUpgradeLvl == 2)
        {
            // + speed
            // el rastro hace daño
        }
        else if (speedUpgradeLvl == 3)
        {
            // + speed
            // + el rastro dura más tiempo
        }
    }
    // DAÑO Y REBOTES
    public void upgradeDamage()
    {
        damageUpgradeLvl++;
        if (damageUpgradeLvl == 1)
        {
            // + daño
            // la pompa rebota 1 vez si hay un enemigo a X distancia
        }
        else if (damageUpgradeLvl == 2)
        {
            // + daño
            // la burbuja rebota 2 veces
        }
        else if (damageUpgradeLvl == 3)
        {
            // + daño
            // la burbuja puede volver a rebotar
        }
    }

    // GESTION DE ENEMIGOS
    public void registerEnemy()
    {
        nEnemies++;
        //Debug.Log(nEnemies);
    }

    public void deRegisterEnemy()
    {
        nEnemies--;
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