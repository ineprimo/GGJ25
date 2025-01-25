using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class SpawnersManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _spawns;
    [SerializeField] private int _currentLvl;
    public int GetCurrentLvl() { return _currentLvl; }

    //struct InfoSpawners
    //{
    //    float[] vidas = { 100, 100, 101};
    //    float[] vidas = { 100, 100, 101};
    //    float[] vidas = { 100, 100, 101};
    //    float[] vidas = { 100, 100, 101};
    //}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeAllSpawners()
    {
        for(int i = 0; i < _spawns.Length; ++i)
        {
            //_spawns[i].gameObject.GetComponent<CaquitaSpawn>().
        }
    }
}
