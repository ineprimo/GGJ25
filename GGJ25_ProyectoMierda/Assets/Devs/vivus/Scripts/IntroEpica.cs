using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroEpica : MonoBehaviour
{
    [SerializeField] private Transform _destination;
    private GameObject _player;

    void Start()
    {
        _player = GameManager.Instance.GetPlayer();
        IntroGame();
    }

    private IEnumerator IntroGame()
    {
        while(_player.transform.position.z >= _destination.transform.position.z)
        {
            _player.GetComponent<Rigidbody>().Move(_destination.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(1);
        // FINAL ANIMACION
        _player.GetComponent<InputManager>().CanInput();   // Reactivamos el INPUT
    }
}
