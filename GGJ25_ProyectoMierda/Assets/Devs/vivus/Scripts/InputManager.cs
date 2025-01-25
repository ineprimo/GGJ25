using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerMovement _playerMovement;
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private GameObject _gunObject;
    private Shoot _shootComponent;

    [SerializeField] private float timeBetweenShots = 2f;
    [SerializeField] private float timeBetweenShotsAnim = 2.5f;
    [SerializeField] private float timeBetweenShotsM = 0.2f; //Metralleta
    [SerializeField] private float lastShootTime = 0f;
    private bool isShooting = false;

    private bool shootInput = false;


    private float shootCurrentTime = 0f;
    private float shootCd = 1f;

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


        if (Input.GetMouseButtonDown(0))
            shootInput = true;  

            // DISPARO //
        if (shootInput)
        {
            // si puede disparar
             if(Time.time - lastShootTime >= timeBetweenShots)
            {

                GameManager.Instance.GetAnimationManager().attackAnim(true);

                // si la animacion ha llegado
                if (Time.time - lastShootTime >= timeBetweenShotsAnim)
                {
                    shootCurrentTime = 0;
                    shootInput = false;
                    // esperar
                    _shootComponent.shootWeapon();
                    lastShootTime = Time.time;
                    isShooting = true;
                    if (_shootComponent.gunLevel == 4)
                    {
                        StartCoroutine(ContinuousShoot());
                    }
                }
               
            }

           


        }

        // Deja de disparar si suelta el bot�n del rat�n
        if (Input.GetMouseButtonUp(0))
        {
            isShooting = false;

            // animation
            GameManager.Instance.GetAnimationManager().attackAnim(false);

        }
        // Haztelo como arriba mas o menos

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.UpgradeBullets();
            
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameManager.Instance.UpgradeSpeed();

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

    private IEnumerator waitfor(float time)
    {


        yield return new WaitForSeconds(time);
    }
}
