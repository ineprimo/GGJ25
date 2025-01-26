using System.Collections; // Necesario para IEnumerator
using UnityEngine;

public class OnEnterGachapon : MonoBehaviour
{
    [SerializeField] private GameObject _key;
    [SerializeField] private int _gachaPrice = 50;
    [SerializeField] private bool canPull;
    [SerializeField] private bool gachaOnCooldown;
    [SerializeField] private GachaponBase _gacha;

    [SerializeField] private float currentGachaTime = 0;

    [SerializeField] private Transform _possiblePositions;

    [Header("Rotation and Sound")]
    [SerializeField] private GameObject gachaponWheel; // GameObject que girará
    [SerializeField] private float rotationDegrees = 360f; // Grados que girará en el eje Z
    [SerializeField] private float rotationDuration = 1.0f; // Duración del giro
    [SerializeField] private AudioClip gachaponSound; // Sonido a reproducir
    [SerializeField] private AudioSource audioSource; // Fuente de audio para reproducir el sonido

    private void Start()
    {
        canPull = false;
        gachaOnCooldown = false;

        _gacha.PrepareGacha();
    }

    private void Update()
    {
        if (canPull)
        {
            if (GameManager.Instance.GetCoins() >= _gachaPrice && Input.GetKey(KeyCode.E) && !gachaOnCooldown)
            {
                GameManager.Instance.RemoveCoins(_gachaPrice);

                // Ejecutar el giro y reproducir el sonido
                StartCoroutine(SpinGachaponWheel());
                audioSource.PlayOneShot(gachaponSound);

                // Realizar el pull en el gachapón
                Upgrade up = _gacha.pull();
                gachaOnCooldown = true;

                if (up == null)
                    Debug.Log("No upgrade available");
                else
                    Debug.Log(up.getName());

                updateUpgrades(up.getName());

                // Retrasar el cambio de posición y rotación 5 segundos
                StartCoroutine(DelayMachineMove(5f));
            }

            if (gachaOnCooldown)
            {
                // Manejo del cooldown
                if (_gacha.getCD() <= currentGachaTime)
                {
                    gachaOnCooldown = false;
                    currentGachaTime = 0;
                }
                else
                {
                    currentGachaTime += Time.deltaTime;
                }
            }
        }
    }

    private IEnumerator DelayMachineMove(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Cambiar posición y rotación del padre
        Transform parent = transform.parent;
        Transform newTr = _possiblePositions.GetChild(Random.Range(0, _possiblePositions.childCount));
        parent.position = newTr.position;
        parent.rotation = Quaternion.Euler(0, newTr.eulerAngles.y, 0);
    }

    private IEnumerator SpinGachaponWheel()
    {
        if (gachaponWheel != null)
        {
            float elapsedTime = 0f;
            float startZRotation = gachaponWheel.transform.localEulerAngles.z;
            float targetZRotation = startZRotation + rotationDegrees;

            while (elapsedTime < rotationDuration)
            {
                // Interpolamos suavemente entre el inicio y el objetivo
                float currentZRotation = Mathf.Lerp(startZRotation, targetZRotation, elapsedTime / rotationDuration);
                gachaponWheel.transform.localEulerAngles = new Vector3(
                    gachaponWheel.transform.localEulerAngles.x,
                    gachaponWheel.transform.localEulerAngles.y,
                    currentZRotation
                );

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Asegurarse de llegar exactamente al ángulo final
            gachaponWheel.transform.localEulerAngles = new Vector3(
                gachaponWheel.transform.localEulerAngles.x,
                gachaponWheel.transform.localEulerAngles.y,
                targetZRotation
            );
        }
    }

    // BULLETS, LIFE, SPEED, DAMAGE
    private void updateUpgrades(string up)
    {
        switch (up)
        {
            case "BULLETS":
                GameManager.Instance.UpgradeBullets();
                break;
            case "LIFE":
                GameManager.Instance.UpgradeLife();
                break;
            case "SPEED":
                GameManager.Instance.UpgradeSpeed();
                break;
            case "DAMAGE":
                GameManager.Instance.UpgradeDamage();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si es el jugador
        if (other.gameObject.GetComponentInParent<PlayerMovement>() != null)
        {
            _key.SetActive(true);
            canPull = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Si es el jugador
        if (other.gameObject.GetComponentInParent<PlayerMovement>() != null)
        {
            _key.SetActive(false);
            canPull = false;
        }
    }
}
