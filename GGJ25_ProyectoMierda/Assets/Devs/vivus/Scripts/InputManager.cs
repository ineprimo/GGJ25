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
   // [SerializeField] private float timeBetweenShotsAnim = 2.5f;
    [SerializeField] private float timeBetweenShotsM = 0.2f; //Metralleta
    [SerializeField] private float lastShootTime = 0f;
    [SerializeField] private float delayBeforeShot = 0.5f;
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
        if (_inputActive)
        {
            Debug.Log("InputActivo");
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
                if (Time.time - lastShootTime >= timeBetweenShots)
                {

                    GameManager.Instance.GetAnimationManager().attackAnim(true);

                    // si la animacion ha llegado
                    //if (Time.time - lastShootTime >= timeBetweenShotsAnim)
                    //{
                    //    shootCurrentTime = 0;
                    //    shootInput = false;
                    //    // esperar
                    //    _shootComponent.shootWeapon();
                    //    lastShootTime = Time.time;
                    //    isShooting = true;
                    //    if (_shootComponent.gunLevel == 4)
                    //    {
                    //        StartCoroutine(ContinuousShoot());
                    //    }
                    //}
                }
            }
            // Deja de disparar si suelta el bot�n del rat�n
            if (Input.GetMouseButtonUp(0))
            {
                isShooting = false;
                // animation
                GameManager.Instance.GetAnimationManager().attackAnim(false);

            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.Instance.UpgradeBullets();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                GameManager.Instance.UpgradeSpeed();
            }
        }      
        

        // DISPARO //
        if (Input.GetMouseButtonDown(0) && Time.time - lastShootTime >= timeBetweenShots)
        {
            //GameManager.Instance.GetAnimationManager().attackAnim(true);

            isShooting = true;
            StartCoroutine(ContinuousShoot());

        }

        // Deja de disparar si suelta el bot�n del rat�n
        if (Input.GetMouseButtonUp(0))
        {
            isShooting = false;

            // animation
            //GameManager.Instance.GetAnimationManager().attackAnim(false);

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


        yield return new WaitForSeconds(delayBeforeShot);

        Debug.Log("awanabanbanban");
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

    private IEnumerator waitfor(float time)
    {


        yield return new WaitForSeconds(time);
    }


}
