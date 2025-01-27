using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroEpica : MonoBehaviour
{
    [SerializeField] private Transform _destination; // Destino al que se mueve el jugador
    private GameObject _player; // Referencia al jugador
    [SerializeField] private GameObject _door; // Puerta que se abrir�/cerrar�

    void Start()
    {
        
        _player = GameManager.Instance.GetPlayer(); // Obtener el jugador desde el GameManager
    }

    public void Intro()
    {
        StartCoroutine(IntroGame()); // Comenzar la introducci�n del juego
    }

    private IEnumerator IntroGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Vector3 startPosition = _player.transform.position; // Posici�n inicial del jugador
        Vector3 destinationPosition = _destination.position; // Posici�n de destino
        float journeyTime = 5f; // Tiempo en segundos para la animaci�n de movimiento
        float elapsedTime = 0f;

        _door.GetComponent<DoorController>().ToggleDoor(); // Abrir la puerta al inicio

        // Movimiento del jugador desde su posici�n inicial hasta la posici�n de destino
        while (elapsedTime < journeyTime)
        {
            elapsedTime += Time.deltaTime; // Incrementar el tiempo transcurrido
            float t = elapsedTime / journeyTime; // Calcular el porcentaje del recorrido
            Vector3 newPosition = Vector3.Lerp(startPosition, destinationPosition, t); // Interpolaci�n de la posici�n
            _player.transform.position = newPosition; // Actualizar la posici�n del jugador
            yield return null; // Esperar un cuadro
        }

        _player.transform.position = destinationPosition; // Asegurarse de que el jugador termine en la posici�n de destino

        _door.GetComponent<DoorController>().ToggleDoor(); // Cerrar la puerta despu�s del movimiento

        yield return new WaitForSeconds(1); // Esperar 1 segundo

        _player.GetComponent<InputManager>().enabled = true; // Habilitar los controles del jugador
        _player.GetComponent<PlayerMovement>().IntroDone(); // Llamar a la funci�n para finalizar la intro

        // Activar los spawners o cualquier otra cosa necesaria
        SpawnersManager.Instance.activateSpawners();
    }
}
