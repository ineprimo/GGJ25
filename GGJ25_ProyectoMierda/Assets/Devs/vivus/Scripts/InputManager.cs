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
    [SerializeField] private float timeBetweenShotsM = 0.2f; //Metralleta
    [SerializeField] private float lastShootTime = 0f;
    private bool isShooting = false;

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
        if (Input.GetMouseButtonDown(0) && Time.time - lastShootTime >= timeBetweenShots)
        {
            _shootComponent.shootWeapon();
            lastShootTime = Time.time;
            isShooting = true; 
            if (_shootComponent.gunLevel == 4)
            {
                StartCoroutine(ContinuousShoot());
            }
        }

        // Deja de disparar si suelta el botón del ratón
        if (Input.GetMouseButtonUp(0))
        {
            isShooting = false;
        }
        // Haztelo como arriba mas o menos

        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.UpgradeBullets();
            
        }

    }

    private IEnumerator ContinuousShoot()
    {
        while (isShooting)
        {
            if (Time.time - lastShootTime >= timeBetweenShotsM)
            {
                _shootComponent.shootWeapon();
                lastShootTime = Time.time;
            }
            yield return new WaitForSeconds(timeBetweenShotsM);
        }
    }
}
