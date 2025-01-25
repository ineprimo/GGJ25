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




    // Start is called before the first frame update
    void Start()
    {
        initLevelUpTime = _levelUpTime;
        UpgradeAllSpawners(); // first level
    }

    // Update is called once per frame
    void Update()
    {
        _levelUpTime -= Time.deltaTime;
        //Debug.Log(_levelUpTime);

        if (_levelUpTime < 0)
        {
            //Debug.Log("levelup");

            if(_currentLvl != nLevels) _currentLvl++;
            UpgradeAllSpawners();

            _levelUpTime = initLevelUpTime;
        }
    }

    public void UpgradeAllSpawners()
    {
        for(int i = 0; i < _spawns.Length; ++i)
        {
            _spawns[i].gameObject.GetComponent<CaquitaSpawn>()
                .Upgrade(meleeSpeeds[_currentLvl-1], throwerSpeeds[_currentLvl-1]);
        }
    }
}
