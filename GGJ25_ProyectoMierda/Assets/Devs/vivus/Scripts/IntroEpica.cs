using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroEpica : MonoBehaviour
{
    [SerializeField] private Transform _destination;
    private GameObject _player;
    [SerializeField] private GameObject _door;
    void Start()
    {
        _player = GameManager.Instance.GetPlayer();
        StartCoroutine(IntroGame());
    }

    private IEnumerator IntroGame()
    {
        Rigidbody playerRigidbody = _player.GetComponent<Rigidbody>();
        Vector3 startPosition = _player.transform.position;
        Vector3 destinationPosition = _destination.position;
        float journeyTime = 5f;
        float elapsedTime = 0f;

        _door.GetComponent<DoorController>().ToggleDoor();

        while (elapsedTime < journeyTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / journeyTime;
            Vector3 newPosition = Vector3.Lerp(startPosition, destinationPosition, t);
            playerRigidbody.MovePosition(newPosition);
            yield return null;
        }

        playerRigidbody.MovePosition(destinationPosition);
        _door.GetComponent<DoorController>().ToggleDoor();

        Debug.Log("Casi Fin Anim");
        yield return new WaitForSeconds(1);

        _player.GetComponent<InputManager>().CanInput();
        playerRigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;
        // COSAS DEL JUEGO SI QUEREIS PONERLAS
    }
}
