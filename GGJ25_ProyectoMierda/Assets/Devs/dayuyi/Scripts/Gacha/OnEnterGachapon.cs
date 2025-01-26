using System.Collections; // Necesario para IEnumerator
using TMPro;
using UnityEngine;
using UnityEngine.Playables; // Necesario para PlayableDirector
using UnityEngine.UI; // Necesario para actualizar el texto UI

public class OnEnterGachapon : MonoBehaviour
{
    [SerializeField] private GameObject _key;
    [SerializeField] private int _gachaPrice = 10;
    private int[] updatePrices = { 20, 30, 40, 45, 50, 55, 60};
    private int countUpdate = 0;
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

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI resultText; // Texto donde se mostrará el nombre de la recompensa
    [SerializeField] private GameObject playableDirector; // Playable Director que controla el Timeline

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
                _gachaPrice = updatePrices[countUpdate];
                if (countUpdate<updatePrices.Length)countUpdate++;

                // Ejecutar el giro, reproducir el sonido y comenzar el Timeline
                StartCoroutine(SpinGachaponWheel());
                audioSource.PlayOneShot(gachaponSound);
                playableDirector.SetActive(true);
                playableDirector.GetComponent<PlayableDirector>().Play();

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
                if (GameManager.Instance.GetBulletsLvl() == 1)
                    resultText.text = "Mejora de pompero";
                else if (GameManager.Instance.GetBulletsLvl() == 2)
                    resultText.text = "Mejora de pompero +";
                else if (GameManager.Instance.GetBulletsLvl() == 3)
                    resultText.text = "Mejora de pompero ++";
                break;
            case "LIFE":
                GameManager.Instance.UpgradeLife();
                
                if (GameManager.Instance.GetHealthLvl() == 1)
                    resultText.text = "Mejora de vida\nCarcel jabonosa";
                else if (GameManager.Instance.GetHealthLvl() == 2)
                    resultText.text = "Mejora de vida\nCarcel jabonosa +";
                else if (GameManager.Instance.GetHealthLvl() == 3)
                    resultText.text = "Mejora de vida\nCarcel jabonosa ++";
                break;
            case "SPEED":
                GameManager.Instance.UpgradeSpeed();

                if (GameManager.Instance.GetSpeedLvl() == 1)
                    resultText.text = "Mejora de velocidad\nCaminar espumoso";
                else if (GameManager.Instance.GetSpeedLvl() == 2)
                    resultText.text = "Mejora de velocidad\nCaminar espumoso +";
                else if (GameManager.Instance.GetSpeedLvl() == 3)
                    resultText.text = "Mejora de velocidad\nCaminar espumoso ++";
                break;
            case "DAMAGE":
                GameManager.Instance.UpgradeDamage();

                if (GameManager.Instance.GetDamageLvl() == 1)
                    resultText.text = "Daño mejorado";
                else if (GameManager.Instance.GetDamageLvl() == 2)
                    resultText.text = "Daño mejorado +";
                else if (GameManager.Instance.GetDamageLvl() == 3)
                    resultText.text = "Daño mejorado ++";
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
