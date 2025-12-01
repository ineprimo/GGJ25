using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MusicTest : MonoBehaviour
{
    [Header("Configuración de FMOD")]
    [SerializeField] EventReference musicEvent;

    private const string INTENSITY_PARAMETER_NAME = "Intensity";

    public EventInstance musicInstance;
    [Range(0f, 7f)]
    public int intensity = 1;

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
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            intensity = 5;
            SetIntensity(5f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            intensity = 6;
            SetIntensity(6f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            intensity = 7;
            SetIntensity(7f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            intensity = 8;
            SetIntensity(8f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            intensity = 9;
            SetIntensity(9f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            intensity = 0;
            SetIntensity(0f);
        }
    }
}