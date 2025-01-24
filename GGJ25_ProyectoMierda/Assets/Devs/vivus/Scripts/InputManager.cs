using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerMovement _playerMovement;
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private GameObject _gunObject;

    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // MOVIMIENTO //
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        _playerMovement.Move(moveDirection);

        // ROTACION CAMARA //
        _cameraMovement.RotateCamera(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // DISPARO //

        // Haztelo como arriba mas o menos

    }
}
