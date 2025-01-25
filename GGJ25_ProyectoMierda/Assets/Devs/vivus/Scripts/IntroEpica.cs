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
        Rigidbody playerRigidbody = _player.GetComponent<Rigidbody>();
        Vector3 startPosition = _player.transform.position;
        Vector3 destinationPosition = _destination.position;
        float journeyTime = 2f; // Duración del movimiento
        float elapsedTime = 0f;

        while (elapsedTime < journeyTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / journeyTime;
            Vector3 newPosition = Vector3.Lerp(startPosition, destinationPosition, t);
            playerRigidbody.MovePosition(newPosition);
            yield return null;
        }

        playerRigidbody.MovePosition(destinationPosition);
        yield return new WaitForSeconds(1);

        _player.GetComponent<InputManager>().CanInput(); // Reactivamos el INPUT
    }

}
