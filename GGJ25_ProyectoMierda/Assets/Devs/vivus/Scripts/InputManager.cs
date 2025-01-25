using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerMovement _playerMovement;
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private GameObject _gunObject;
    private Shoot _shootComponent;

    [SerializeField] private float timeBetweenShots = 1f;
    [SerializeField] private float lastShootTime = 0f;

    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _shootComponent = _gunObject.GetComponent<Shoot>();
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
        if(Input.GetMouseButtonDown(0) && Time.time - lastShootTime >= timeBetweenShots)
        {
            _shootComponent.shootWeapon();
            lastShootTime = Time.time;
        }
        // Haztelo como arriba mas o menos

    }
}
