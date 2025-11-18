using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MenuSounds : MonoBehaviour
{
    [Header("Configuración de FMOD")]
    [SerializeField] EventReference menuEvent;

    // El nombre del parámetro en FMOD que controla la capa del botón
    private const string BUTTONS_PARAMETER_NAME = "NeonButtons";
    private const string NEON_PARAMETER_NAME = "InMenu";

    private EventInstance menuInstance;

    // Valor actual del parámetro para control (no es necesario exponerlo en el Inspector si solo se llama desde aquí)
    private float currentButtonState = 0f;

    void Start()
    {
        // 1. Crear e iniciar la instancia de la música del menú
        menuInstance = RuntimeManager.CreateInstance(menuEvent);
        menuInstance.start();

        // 2. Establecer el estado inicial (Apagado = 0f)
        SetButtons(currentButtonState);
    }

    private void OnDestroy()
    {
        if (menuInstance.isValid())
        {
            menuInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            menuInstance.release();
        }
    }

    /// <summary>
    /// Establece el valor del parámetro de FMOD (0 = Apagado, 1 = Encendido).
    /// </summary>
    /// <param name="newState">El nuevo valor para el parámetro NeonButtons.</param>
    public void SetButtons(float newState)
    {
        if (menuInstance.isValid() && currentButtonState != newState)
        {
            currentButtonState = newState; // Actualiza el estado local
            Debug.Log($"Actualizando FMOD NeonButtons a: {currentButtonState}");

            // Establecer el parámetro en la instancia de FMOD
            menuInstance.setParameterByName(BUTTONS_PARAMETER_NAME, currentButtonState);
        }
    }

    // Método para ser llamado por los eventos de ratón
    public void playNeon(bool isHovering)
    {
        if (isHovering)
        {
            // El ratón ha entrado en un botón (Encendido = 1f)
            SetButtons(1f);
        }
        else
        {
            // El ratón ha salido de un botón (Apagado = 0f)
            SetButtons(0f);
        }
    }

    public void stopNeon()
    {
        menuInstance.setParameterByName(BUTTONS_PARAMETER_NAME, 1); // En teoría quita el sonido del neon
    }
}