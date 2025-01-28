using System.Collections;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool _inputActive = false; // Para activar o desactivar el input
    public void CanInput() { _inputActive = true; }

    private PlayerMovement _playerMovement; // Referencia al PlayerMovement
    [SerializeField] private GameObject _gunObject; // Referencia al arma
    private Shoot _shootComponent; // Componente de disparo

    [SerializeField] private float timeBetweenShots = 2.0f;
    [SerializeField] private float timeBetweenShotsM = 0.2f; // Metralleta
    [SerializeField] private float lastShootTime = 0f;
    [SerializeField] private float delayBeforeShot = 0.5f;
    [SerializeField] private bool isShooting = false;
    [SerializeField] private bool animended = true;

    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>(); // Referencia al script de movimiento
        _shootComponent = _gunObject.GetComponent<Shoot>();
    }

    void Update()
    {
        if (!_inputActive) return; // Si el input está desactivado, salimos del método

        // MOVIMIENTO //
        _playerMovement.HandleMovement(); // Movemos al personaje

        // ROTACIÓN DE LA CÁMARA //
        _playerMovement.HandleMouseLook(); // Rotamos la cámara

        // DISPARO //
        if (Input.GetMouseButton(0) && Time.time - lastShootTime >= timeBetweenShots)
        {
            if (!isShooting)
            {
                GameManager.Instance.GetAnimationManager().attackAnim(true);
                animended = false;
                isShooting = true;

                bool a = Input.GetKey(KeyCode.S); // Detectar tecla S
                StartCoroutine(ContinuousShoot(a));
            }
        }

        // Deja de disparar si suelta el botón del ratón
        if (Input.GetMouseButtonUp(0))
        {
            isShooting = false;
        }

        // Control de animaciones
        if (!isShooting && GameManager.Instance.GetAnimationManager().GetAnimator().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            GameManager.Instance.GetAnimationManager().attackAnim(false);
            animended = true;
        }

        // Mejoras (opcional)
        if (Input.GetKeyDown(KeyCode.O)) GameManager.Instance.UpgradeBullets();
        if (Input.GetKeyDown(KeyCode.P)) GameManager.Instance.UpgradeSpeed();
        if (Input.GetKeyDown(KeyCode.M)) GameManager.Instance.UpgradeLife();
        if (Input.GetKeyDown(KeyCode.N)) GameManager.Instance.UpgradeDamage();
    }

    private IEnumerator ContinuousShoot(bool a)
    {
        yield return new WaitForSeconds(delayBeforeShot);

        while (isShooting)
        {
            a = Input.GetKey(KeyCode.S); // Actualiza el valor de 'a' mientras dispara

            if (_shootComponent.gunLevel == 4)
            {
                if (Time.time - lastShootTime >= timeBetweenShotsM)
                {
                    _shootComponent.shootWeapon(a);
                    lastShootTime = Time.time;
                }
            }
            else
            {
                if (Time.time - lastShootTime >= timeBetweenShots)
                {
                    _shootComponent.shootWeapon(a);
                    lastShootTime = Time.time;
                }
            }

            yield return new WaitForSeconds(timeBetweenShotsM);
        }
    }

}
