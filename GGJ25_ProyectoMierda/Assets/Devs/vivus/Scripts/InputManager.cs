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
        if (!_inputActive) return;

        // MOVIMIENTO //
        _playerMovement.HandleMovement();

        // ROTACIÓN DE LA CÁMARA //
        _playerMovement.HandleMouseLook();

        // DISPARO //
        if (Input.GetButtonDown("Fire1"))
        {
            if (!isShooting)
            {
                GameManager.Instance.GetAnimationManager().ResetAnim("idle");
                GameManager.Instance.GetAnimationManager().ResetAnim("reload");
                GameManager.Instance.GetAnimationManager().attackAnim();
                isShooting = true;
                _shootComponent.isShooting = true;
                bool a = Input.GetKey(KeyCode.S);
                StartCoroutine(ContinuousShoot(a));
            }
        }

        // Cuando deja de disparar, ejecuta la animación de recarga
        if (Input.GetButtonUp("Fire1"))
        {
            isShooting = false;
            _shootComponent.isShooting = false;
            GameManager.Instance.GetAnimationManager().ResetAnim("attack");
            GameManager.Instance.GetAnimationManager().rechargeAnim();
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.O)) GameManager.Instance.UpgradeBullets();
        if (Input.GetKeyDown(KeyCode.P)) GameManager.Instance.UpgradeSpeed();
        if (Input.GetKeyDown(KeyCode.M)) GameManager.Instance.UpgradeLife();
        if (Input.GetKeyDown(KeyCode.N)) GameManager.Instance.UpgradeDamage();
#endif
    }

    private IEnumerator ContinuousShoot(bool a)
    {
        yield return new WaitForSeconds(delayBeforeShot);

        while (isShooting)
        {
            a = Input.GetKey(KeyCode.S);
            _shootComponent.shootWeapon(a);
            yield return new WaitForSeconds(_shootComponent.timeBetweenShots);
        }
    }

    // Animación de recarga cuando deja de disparar
   

}
