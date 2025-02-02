using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importar TextMeshPro

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider sensitivitySlider;

    [SerializeField] private GameObject mainMenuUI; // Menú principal
    [SerializeField] private GameObject scoreMenuUI; // Menú de score final

    [SerializeField] private TextMeshProUGUI volumeText; // Referencia al TextMeshPro para volumen
    [SerializeField] private TextMeshProUGUI sensitivityText; // Referencia al TextMeshPro para sensibilidad
    private PlayerMovement playerMovement; // Referencia al script de movimiento
    private InputManager playerInput;
    private bool isPaused = false;

    void Start()
    {
        // Cargar valores guardados (si existen)
        float savedVolume = PlayerPrefs.GetFloat("Volume", .5f);
        float savedSensitivity = PlayerPrefs.GetFloat("Sensitivity", 1f);

        volumeSlider.value = savedVolume;
        sensitivitySlider.value = savedSensitivity;

        // Actualizar los textos con los valores guardados
        volumeText.text = (savedVolume * 100).ToString("F0") + "%"; // Actualiza el texto del volumen
        sensitivityText.text = savedSensitivity.ToString("F2"); // Actualiza el texto de sensibilidad

        // Aplicar valores guardados
        AdjustVolume(savedVolume);
        AdjustSensitivity(savedSensitivity);

        // Suscribir eventos de sliders
        volumeSlider.onValueChanged.AddListener(AdjustVolume);
        sensitivitySlider.onValueChanged.AddListener(AdjustSensitivity);

        // Obtener referencia al PlayerMovement
        playerMovement = GameManager.Instance.GetPlayer().GetComponent<PlayerMovement>();
        playerInput= GameManager.Instance.GetPlayer().GetComponent<InputManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !mainMenuUI.activeInHierarchy && !scoreMenuUI.activeInHierarchy)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;  // Pausa el tiempo
        Cursor.visible = isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;

        // Desactivar o activar el movimiento del jugador
        if (playerMovement != null)
        {
            playerMovement.enabled = !isPaused;
            playerInput.enabled = !isPaused;

        }
    }

    public void AdjustVolume(float volume)
    {
        AudioListener.volume = volume; // Ajusta el volumen global
        PlayerPrefs.SetFloat("Volume", volume); // Guarda la configuración
        volumeText.text = (volume * 100).ToString("F0") + "%"; // Muestra el valor del volumen en el TextMeshPro
    }

    public void AdjustSensitivity(float sensitivity)
    {
        if (playerMovement != null)
        {
            playerMovement.mouseSensitivity = sensitivity; // Ajusta la sensibilidad
            PlayerPrefs.SetFloat("Sensitivity", sensitivity); // Guarda la configuración
            sensitivityText.text = sensitivity.ToString("F2"); // Muestra el valor de la sensibilidad en el TextMeshPro
        }
    }
}
