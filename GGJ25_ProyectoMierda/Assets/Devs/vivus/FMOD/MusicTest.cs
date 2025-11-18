using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MusicTest : MonoBehaviour
{
    [Header("Configuración de FMOD")]
    [SerializeField] EventReference musicEvent;

    private const string INTENSITY_PARAMETER_NAME = "Intensity";

    private EventInstance musicInstance;
    [Range(0f, 4f)]
    public int intensity = 0;

    void Start()
    {
        musicInstance = RuntimeManager.CreateInstance(musicEvent);

        musicInstance.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));

        musicInstance.start();

        SetIntensity(intensity);

    }


    private void OnDestroy()
    {
        if (musicInstance.isValid())
        {
            musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            musicInstance.release();
        }
    }

    public void SetIntensity(float newIntensity) // Usamos float por seguridad y precisión
    {
        if (musicInstance.isValid())
        {
            Debug.Log($"Actualizando FMOD Intensity a: {newIntensity}");
            musicInstance.setParameterByName(INTENSITY_PARAMETER_NAME, newIntensity);
        }
    }

    void Update()
    {
        // Debugcambiar la intensidad
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            intensity = 1;
            SetIntensity(1f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            intensity = 2;
            SetIntensity(2f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            intensity = 3;
            SetIntensity(3f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            intensity = 4;
            SetIntensity(4f);
        }
    }
}