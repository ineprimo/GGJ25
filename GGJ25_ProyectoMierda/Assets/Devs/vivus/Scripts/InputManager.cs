using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool _inputActive = false;
    public void CanInput() {  _inputActive = true; }

    PlayerMovement _playerMovement;
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private GameObject _gunObject;
    private Shoot _shootComponent;

    [SerializeField] private float timeBetweenShots = 2.0f;
    [SerializeField] private float timeBetweenShotsM = 0.2f; //Metralleta
    [SerializeField] private float lastShootTime = 0f;
    [SerializeField] private float delayBeforeShot = 0.5f;
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
                GameManager.Instance.GetAnimationManager().attackAnim(true);
                isShooting = true;
                StartCoroutine(ContinuousShoot());

            }
            // Deja de disparar si suelta el bot�n del rat�n
            if (Input.GetMouseButtonUp(0))
            {
                isShooting = false;
                // animation
                GameManager.Instance.GetAnimationManager().attackAnim(false);

            }
            if (Input.GetKeyDown(KeyCode.O))
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

        yield return new WaitForSeconds(delayBeforeShot);

      
        while (isShooting)
        {
    

            if (_shootComponent.gunLevel == 4)
            {
                if (Time.time - lastShootTime >= timeBetweenShotsM)
                {
                    _shootComponent.shootWeapon();
                    lastShootTime = Time.time;
                }
            }
            else
            {
                if (Time.time - lastShootTime >= timeBetweenShots)
                {
                    _shootComponent.shootWeapon();
                    lastShootTime = Time.time;
                }
            }


           
            yield return new WaitForSeconds(timeBetweenShotsM);
        }
    }


}
