using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class SpawnersManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _spawns;
    [SerializeField] private int _currentLvl = 1;
    [SerializeField] private float _levelUpTime = 10.0f;

    float initLevelUpTime;

    [SerializeField] bool initSpawners;
    [SerializeField] bool activated;
    bool tutorialActivated;

    public int GetCurrentLvl() { return _currentLvl; }

    private static SpawnersManager _instance;

    public static SpawnersManager Instance
    {
        get { return _instance; }
    }


    // velocidades en progresion

    float minSpawnTime = 5f;

    float incrementalSpeed = 0.1f;
    float incrementalHealth = 10f;
    float incrementalDamage = 10f;
    int incrementalnumEnemies = 10;
    float incrementalSpawnTime = 0.25f;

    float totalIncrementalSpeed = 3f;
    float totalIncrementalHealth = 100f;
    float totalIncrementalDamage = 100f;
    float totalIncrementalCoins = 5f;
    int totalIncrementalnumEnemies = 5;
    float totalIncrementalSpawnTime = 8f;


    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject); // Si ya existe una instancia, destruye esta
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject); // Hace que no se destruya al cambiar de escena
    }

    // Start is called before the first frame update
    void Start()
    {
        initLevelUpTime = _levelUpTime;

        initSpawners = false;
        activated = false;
        tutorialActivated = false;
        GameManager.Instance.ChangeActualRound(_currentLvl);
        GameManager.Instance.SetMaxEnemies(totalIncrementalnumEnemies);
        UpgradeAllSpawners();

        for (int i = 0; i < _spawns.Length; ++i)
        {
            _spawns[i].gameObject.GetComponent<CaquitaSpawn>().enabled = false;
        }
        _spawns[0].gameObject.GetComponent<TutorialSpawner>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!activated && initSpawners)
        {
            for (int i = 0; i < _spawns.Length; ++i)
            {
                _spawns[i].gameObject.GetComponent<CaquitaSpawn>().enabled = true;
            }
            _spawns[0].gameObject.GetComponent<TutorialSpawner>().enabled = true;
            activated = true;
        }

        if (activated)
        {
            _levelUpTime -= Time.deltaTime;

            if (_levelUpTime < 0)
            {
                _currentLvl++;
                GameManager.Instance.ChangeActualRound(_currentLvl);
                totalIncrementalSpeed += incrementalSpeed;
                totalIncrementalHealth += incrementalHealth;
                totalIncrementalDamage += incrementalDamage;
                totalIncrementalnumEnemies += (incrementalnumEnemies * 20 / 100 + 1);
                totalIncrementalSpawnTime -= incrementalSpawnTime;

                UpgradeAllSpawners();

                _levelUpTime = initLevelUpTime;
            }
        }
    }

    public void UpgradeAllSpawners()
    {
        GameManager.Instance.SetMaxEnemies(totalIncrementalnumEnemies);
        for (int i = 0; i < _spawns.Length; ++i)
        {
            _spawns[i].gameObject.GetComponent<CaquitaSpawn>().Upgrade
                (
                        totalIncrementalSpeed, totalIncrementalSpeed,
                        totalIncrementalHealth, totalIncrementalHealth,
                        totalIncrementalDamage, totalIncrementalDamage,
                        totalIncrementalCoins, totalIncrementalCoins,
                        Mathf.Max(totalIncrementalSpawnTime,minSpawnTime)
                );

        }
        
    }

    public void activateSpawners()
    {
        if (!initSpawners)
        {
            initSpawners = true;
        }
    }

    public void StopSpawnning()
    {
        for (int i = 0; i < _spawns.Length; ++i)
        {
            _spawns[i].gameObject.GetComponent<CaquitaSpawn>().enabled = false;
        }
    }
}
