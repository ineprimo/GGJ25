using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BubbleShield : MonoBehaviour
{
    [SerializeField] private GameObject _bubble;
    
    [SerializeField] private float _distanceArea;
    [SerializeField] private float _cooldown = 30.0f;
    private float _cd;

    private GameObject _enemy;

    private void OnCollisionEnter(Collision collision)
    {
        _enemy = collision.gameObject;
        Debug.Log(_enemy);

        if (!(_cd <= 0.0f) || !_enemy.TryGetComponent<CaquitaMovement>(out CaquitaMovement c)) return;
        
        Freeze();
        _cd = _cooldown;
    }

    private void Freeze()
    {
        Debug.Log("Freeze");
        
        foreach (GameObject g in GameManager.Instance.SceneEnemies.Where(
                     g => (g.transform.position - gameObject.transform.position).magnitude < _distanceArea
                     ))
        {
            g.GetComponent<CaquitaMovement>().enabled = false;
            Instantiate(_bubble, g.transform.position, Quaternion.identity).transform.localScale =
                new Vector3(5, 5, 5);
        }
    }

    private void Update()
    {
        _cd -= Time.deltaTime;
    }
}
