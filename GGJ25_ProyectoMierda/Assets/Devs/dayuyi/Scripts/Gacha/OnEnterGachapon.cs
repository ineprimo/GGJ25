using System.Collections; // Necesario para IEnumerator
using TMPro;
using UnityEngine;
using UnityEngine.Playables; // Necesario para PlayableDirector
using UnityEngine.UI; // Necesario para actualizar el texto UI

public class OnEnterGachapon : MonoBehaviour
{
    [SerializeField] private GameObject _key;
    public float _gachaPrice => GameManager.Instance.gachaPrice;
    [SerializeField] private bool canPull;
    [SerializeField] private bool gachaOnCooldown;
    [SerializeField] private GachaponBase _gacha;

    [SerializeField] private float currentGachaTime = 0;

    [SerializeField] private Transform _possiblePositions;

    [Header("Rotation and Sound")]
    [SerializeField] private GameObject gachaponWheel; // GameObject que girar�
    [SerializeField] private float rotationDegrees = 360f; // Grados que girar� en el eje Z
    [SerializeField] private float rotationDuration = 1.0f; // Duraci�n del giro
    [SerializeField] private AudioClip gachaponSound; // Sonido a reproducir
    [SerializeField] private AudioSource audioSource; // Fuente de audio para reproducir el sonido

    [Header("UI Elements")]

    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private GameObject priceText; // Texto donde se mostrar� el nombre de la recompensa

    [SerializeField] private GameObject playableDirector; // Playable Director que controla el Timeline

    private void Start()
    {
        canPull = false;
        gachaOnCooldown = false;
        
        _gacha.PrepareGacha();
        priceText.GetComponent<TextMeshPro>().text = "" + _gachaPrice;
    }

    private void Update()
    {
        if (canPull && GameManager.Instance.canUpdate())
        {
            if (GameManager.Instance.GetCoins() >= _gachaPrice && Input.GetKey(KeyCode.E) && !gachaOnCooldown)
            {
                int pricePayed = (int)_gachaPrice;
                GameManager.Instance.RemoveCoins(pricePayed);
                

                // Ejecutar el giro, reproducir el sonido y comenzar el Timeline
                StartCoroutine(SpinGachaponWheel());
                audioSource.PlayOneShot(gachaponSound);
                playableDirector.SetActive(true);
                playableDirector.GetComponent<PlayableDirector>().Play();

                // Realizar el pull en el gachap�n
                Upgrade up = _gacha.pull();
                gachaOnCooldown = true;

                updateUpgrades(up.getName());
                GameManager.Instance.updateGachaPrice();



                // Retrasar el cambio de posici�n y rotaci�n3 segundos
                StartCoroutine(DelayMachineMove(2.5f));
            }

            
        }
    }

    private IEnumerator DelayMachineMove(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Cambiar posici�n y rotaci�n del padre
        Transform parent = transform.parent;
        Transform newTr = _possiblePositions.GetChild(Random.Range(0, _possiblePositions.childCount));
        parent.position = newTr.position;
        parent.rotation = Quaternion.Euler(0, newTr.eulerAngles.y, 0);
        gachaOnCooldown = false;
        priceText.GetComponent<TextMeshPro>().text =""+ _gachaPrice;
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

            // Asegurarse de llegar exactamente al �ngulo final
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
                    resultText.text = "Ataque mejorado";
                else if (GameManager.Instance.GetDamageLvl() == 2)
                    resultText.text = "Ataque mejorado +";
                else if (GameManager.Instance.GetDamageLvl() == 3)
                    resultText.text = "Ataque mejorado ++";
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
