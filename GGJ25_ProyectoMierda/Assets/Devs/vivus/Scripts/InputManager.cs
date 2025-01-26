using Mono.Cecil.Cil;
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
    [SerializeField] private bool isShooting = false;
    [SerializeField] private bool animended = true;


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
                animended = false;
                isShooting = true;

                bool a = false; 
                if (Input.GetKeyDown(KeyCode.S)) 
                {
                    a = true;
                    StartCoroutine(ContinuousShoot(a));
                }
                else
                {
                    StartCoroutine(ContinuousShoot(a));
                }
                

            }
            // Deja de disparar si suelta el bot�n del rat�n
            if (Input.GetMouseButtonUp(0))
            {   
                // si ha acabado
                isShooting = false;


            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                GameManager.Instance.UpgradeBullets();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                GameManager.Instance.UpgradeSpeed();
            }
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameManager.Instance.UpgradeLife();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            GameManager.Instance.UpgradeDamage();
        }
        

        if (!isShooting)
        {
            if (GameManager.Instance.GetAnimationManager().GetAnimator().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                // animation
                GameManager.Instance.GetAnimationManager().attackAnim(false);
                animended = true;
            }

        }


    }

    private IEnumerator ContinuousShoot(bool a)
    {

        yield return new WaitForSeconds(delayBeforeShot);

      
        while (!animended)
        {

            a = Input.GetKey(KeyCode.S);
            if (_shootComponent.gunLevel == 4)
            {
                if (Time.time - lastShootTime >= timeBetweenShotsM)
                {
                    if(a)
                        _shootComponent.shootWeapon(a);
                    else
                        _shootComponent.shootWeapon(false);

                    lastShootTime = Time.time;
                }
            }
            else
            {
                if (Time.time - lastShootTime >= timeBetweenShots)
                {
                    if (a)
                        _shootComponent.shootWeapon(a);
                    else
                        _shootComponent.shootWeapon(false);

                    lastShootTime = Time.time;
                }
            }


           
            yield return new WaitForSeconds(timeBetweenShotsM);
        }
    }


}
