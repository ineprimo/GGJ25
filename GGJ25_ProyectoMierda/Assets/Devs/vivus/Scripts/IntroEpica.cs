using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroEpica : MonoBehaviour
{
    [SerializeField] private Transform _destination;

    void Start()
    {
        IntroGame();
    }

    private IEnumerator IntroGame()
    {

        yield return new WaitForSeconds(1);
        // FINAL ANIMACION
        GameManager.Instance.GetPlayer().GetComponent<InputManager>().CanInput();   // Reactivamos el INPUT
    }
}
