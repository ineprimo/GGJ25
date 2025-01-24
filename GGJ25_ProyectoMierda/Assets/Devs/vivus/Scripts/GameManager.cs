using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    // UPGRADES
    int lifeUpgradeLvl = 0;
    int velocityUpgradeLvl = 0;
    int shootUpgradeLvl = 0;
    int singedUpgradeLvl = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}