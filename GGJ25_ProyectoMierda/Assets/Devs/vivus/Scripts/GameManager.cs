using System;
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

    //SOUND
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip newTrack;
    private float originalVolume;

    //GACHA
    private int incrementalPrice = 10;
    public int gachaPrice = 10;

    // UI
    [SerializeField] private GameObject UIManager;
    [SerializeField] private HUDController _hud;
    [SerializeField] private int score=0;
    [SerializeField] private int actualRound = 1;

    // PLAYER
    [SerializeField] private GameObject _player;
    public GameObject GetPlayer() {  return _player; }

    //WEAPON
    [SerializeField] private GameObject _gun;

    // ENEMIES
    [SerializeField] private int maxEnemies = 10;
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
    [SerializeField] private float _healthIncreaseLvl1 = 100.0f;
    [SerializeField] private float _healthIncreaseLvl2 = 100.0f;
    [SerializeField] private float _healthIncreaseLvl3 = 200.0f;
    [SerializeField] private float _shieldCooldownReduction = 2f;
    
    [Header("Speed")]
    [SerializeField] private int _speedUpgradeLvl = 0;
    [SerializeField] private float _speedIncreaseLvl1 = 0.1f;
    [SerializeField] private float _speedIncreaseLvl2 = 0.1f;
    [SerializeField] private float _speedIncreaseLvl3 = 0.2f;

    [SerializeField] private float _traceDamageLvl1 = 1.0f;
    [SerializeField] private float _traceDamageLvl2 = 1.5f;
    [SerializeField] private float _traceBubbleLifeTimeLvl3 = 2.5f;

    [Header("Damage")]
    [SerializeField] private int _damageUpgradeLvl = 0;
    [SerializeField] private float _atkIncreaseLvl1 = 7.5f;
    [SerializeField] private float _atkIncreaseLvl2= 15.0f;
    [SerializeField] private float _atkIncreaseLvl3 = 20.0f;
    [SerializeField] public float _actualExtraDmg = 0;

    public int updatesRemained = 12;


    public AnimationManager GetAnimationManager() { return _animationManager; }

    //Gacha

    public void updateGachaPrice()
    {
            gachaPrice += incrementalPrice;
    }

    // CANTIDAD DE BALAS
    public void UpgradeBullets()
    {

        if (_bulletsUpgradeLvl <4 )
        {
            _bulletsUpgradeLvl++;
            _gun.GetComponent<Shoot>().gunLevel = _bulletsUpgradeLvl + 1;

            if(_gun.GetComponent<Shoot>().gunLevel == 4)
            {
                //_gun.GetComponent<Shoot>().currentAmmo = _ARAmmo;
                UpgradeFireRate(1.2f);

                // actualiza el player
                _player.GetComponent<PlayerMovement>().ChangeWeapon(1);

                // cambia a la pistola
                _animationManager.ChangeCurrentAnimator(1);
            }
            else
            {
                UpgradeFireRate(1.5f);
                //_gun.GetComponent<Shoot>().currentAmmo = _gunAmmo;
            }
        }

        updatesRemained--;
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
                _hud.AbilityHudActivate();
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
        updatesRemained--;
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
        updatesRemained--;
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


            //_player.GetComponent<PlayerMovement>().ImproveSpeed(_speedIncreaseLvl1);     // Aumentar velocidad de movimiento


            // la pompa rebota 1 vez si hay un enemigo a X distancia
            _actualExtraDmg = _atkIncreaseLvl1;
            Shoot pistola = _player.GetComponentInChildren<Shoot>();
            //pistola.MakeBouncyBubbles(1);
        }
        else if (_damageUpgradeLvl == 2)
        {
            // + da�o
            // la burbuja rebota 2 veces
            _actualExtraDmg = _atkIncreaseLvl2;
            Shoot pistola = _player.GetComponentInChildren<Shoot>();

            // la pompa rebota 1 vez si hay un enemigo a X distancia

        }
        else if (_damageUpgradeLvl == 3)
        {
            // + da�o
            // la burbuja puede volver a rebotar
            _actualExtraDmg = _atkIncreaseLvl3;
            Shoot pistola = _player.GetComponentInChildren<Shoot>();
            //pistola.MakeBouncyBubbles(3);

            // la pompa rebota 1 vez si hay un enemigo a X distancia

        }
        updatesRemained--;
        _hud.UpdateUI();

    }

    public void AbilityHud()
    {
        _hud.UpdateAbilityHud();
    }

    public bool canUpdate()
    {
        return updatesRemained > 0;
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
        _player.GetComponent<PlayerMovement>().AddCoins(nCoins);
        increaseScore(1);
        _hud.UpdateUI();
    }
    
    public void RemoveCoins(int nCoins)
    {
        _player.GetComponent<PlayerMovement>().SubCoins(nCoins);
        _hud.UpdateUI();
    }

    public void increaseScore(int nScore)
    {
        score += nScore;
        _hud.UpdateUI();
    }

    public int GetScore()
    {
        return score;
    }

    public void EndGame()
    {
        UIManager.GetComponent<UIManager>().DesactivarHUD();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _player.GetComponent<PlayerMovement>().enabled = false;
        _player.GetComponent<InputManager>().enabled = false;
        _player.transform.GetChild(0).GetChild(0).GetComponent<Shoot>().enabled = false;
        _player.transform.GetChild(0).GetChild(0).GetComponent<AudioSource>().enabled = false;
        _player.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

        _player.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        _player.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);

        UIManager.GetComponent<UIManager>().ActivarScoreboard(score);

        Destroy(_gun);

        // Iniciar la transición de música

        SpawnersManager.Instance.StopSpawnning();
        StartCoroutine(ChangeMusicSmoothly(newTrack, 1.5f)); // 1 segundo para la transición
        DestroyAllEnemies();
    }

    private void DestroyAllEnemies()
    {
        int enemyLayer = LayerMask.NameToLayer("Melee"); // Obtener el número de la layer "Enemy"
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == enemyLayer)
            {
                if(obj.GetComponent<Enemy>())
                    obj.GetComponent<Enemy>().Bomba();
            }
        }
    }
    public void ChangeActualRound(int round)
    {
        actualRound = round;
        _hud.UpdateUI();
    }

    public void SetMaxEnemies(int enemies)
    {
        maxEnemies = enemies;
    }

    public int GetRound() { return actualRound; }

    private IEnumerator ChangeMusicSmoothly(AudioClip newClip, float duration)
    {
        if (musicSource == null) yield break;

        originalVolume = musicSource.volume;

        // Bajar el volumen gradualmente
        float elapsed = 0f;
        while (elapsed < duration)
        {
            musicSource.volume = Mathf.Lerp(originalVolume, 0, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        musicSource.volume = 0;

        // Cambiar la canción
        musicSource.clip = newClip;
        musicSource.Play();

        // Subir el volumen gradualmente
        elapsed = 0f;
        while (elapsed < duration)
        {
            musicSource.volume = Mathf.Lerp(0, originalVolume, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        musicSource.volume = originalVolume;
    }


    public int GetCoins()
    {
        return _player.GetComponent<PlayerMovement>().GetCoins();
    }
    public void UpgradeFireRate(float rate)
    {
        _gun.GetComponent<Shoot>().IncreaseFireRate(rate);
    }

    // Start is called before the first frame update
    void Start()
    {
        _player.GetComponent<PlayerMovement>().SetCoins(0);
    }

    public void CollectAllCoins(GameObject player)
    {
        CoinController[] coins = FindObjectsByType<CoinController>(FindObjectsSortMode.None);

        foreach (CoinController coin in coins)
        {
            StartCoroutine(coin.MoveCoinToPlayer(player));
        }
    }

    internal void LateGame(float dmg, float health)
    {
        _actualExtraDmg += dmg;
        _player.GetComponent<PlayerMovement>().ImproveMaxLife(health);
    }
}