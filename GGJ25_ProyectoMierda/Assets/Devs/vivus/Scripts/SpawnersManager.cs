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

    bool initSpawners;
    bool activated;
    bool tutorialActivated;

    public int GetCurrentLvl() { return _currentLvl; }

    private static SpawnersManager _instance;

    public static SpawnersManager Instance
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


    // info guarra
    int nLevels = 12;

    // velocidades en progresion
    float[] meleeSpeeds = { 2f, 2f, 2f, 2f, 2f, 3f, 3f, 3f, 3f, 4f, 4f, 4f };
    float[] throwerSpeeds = { 2f, 2f, 2f, 2f, 2f, 3f, 3f, 3f, 3f, 3f,3f,3f };

    float[] meleeHealth = { 100f, 100f, 100f, 100f, 120f, 120f, 120f, 140f, 140f, 140f, 160f, 160f };
    float[] throwerHealth = { 80f, 80f, 80f, 80f, 100f, 100f, 100f, 120f, 120f, 120f, 140f, 140f};

    float[] meleeDamage = { 50f, 50f, 80f, 80f, 100f, 100f, 120f, 120f, 140f, 140f, 160f, 160f};
    float[] throwerDamage = { 30f, 30f, 40f, 40f, 50f, 50f, 60f, 60f, 80f, 80f, 100f, 100f};

    float[] meleeCoins = { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
    float[] throwerCoins = { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };

    float[] spawnTime = { 30f, 30f, 30f, 20f, 20f, 20f, 20f, 10f, 10f, 10f, 10f, 10f };


    // Start is called before the first frame update
    void Start()
    {
        initLevelUpTime = _levelUpTime;

        initSpawners = false;
        activated = false;
        tutorialActivated = false;

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
                if (_currentLvl != nLevels) _currentLvl++;

                UpgradeAllSpawners();

                _levelUpTime = initLevelUpTime;
            }
        }
    }

    public void UpgradeAllSpawners()
    {
        for(int i = 0; i < _spawns.Length; ++i)
        {
            _spawns[i].gameObject.GetComponent<CaquitaSpawn>().Upgrade
                (
                        meleeSpeeds[_currentLvl - 1], throwerSpeeds[_currentLvl - 1],
                        meleeHealth[_currentLvl - 1], throwerHealth[_currentLvl - 1],
                        meleeDamage[_currentLvl - 1], throwerDamage[_currentLvl - 1],
                        meleeCoins[_currentLvl - 1], throwerCoins[_currentLvl - 1],
                        spawnTime[_currentLvl - 1]
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
}
