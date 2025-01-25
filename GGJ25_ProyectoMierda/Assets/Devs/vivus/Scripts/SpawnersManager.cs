using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class SpawnersManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _spawns;
    [SerializeField] private int _currentLvl;
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

    float[] meleeSpeeds = { 2, 2, 2,2,2,3,3,3,3 };
    float[] throwerSpeeds = { 10, 20, 30, 40, 50 };

    // Start is called before the first frame update
    void Start()
    {
        initLevelUpTime = _levelUpTime;
    }

    // Update is called once per frame
    void Update()
    {
        _levelUpTime -= Time.deltaTime;
        if (_levelUpTime < 0)
        {
            Debug.Log("levelup");
            UpgradeAllSpawners();

            _levelUpTime = initLevelUpTime;
        }
    }

    public void UpgradeAllSpawners()
    {
        for(int i = 0; i < _spawns.Length; ++i)
        {
            _spawns[i].gameObject.GetComponent<CaquitaSpawn>().Upgrade();
        }
    }
}
