using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class LightFliker : MonoBehaviour
{
    public Light oldLight; // Referencia a la luz
    public float minFlickerTime = 0.1f; // Tiempo mínimo entre parpadeos
    public float maxFlickerTime = 0.5f; // Tiempo máximo entre parpadeos
    public float lightIntensity = 1.0f; // Intensidad normal
    public float offIntensity = 0.0f; // Intensidad cuando está apagada

    [SerializeField] private EventReference lightOnSound;


    // FMOD
    private FMOD.Studio.EventInstance lightSoundInstance;
    private float timeToNextFlicker; // Tiempo restante para el próximo parpadeo
    private bool isLightOn = false; // Estado actual de la luz

    void Start()
    {
        if (oldLight == null)
        {
            oldLight = GetComponent<Light>();
        }

        lightSoundInstance = RuntimeManager.CreateInstance(lightOnSound);
        lightSoundInstance.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));

        SetNextFlickerTime();
    }

    void OnDestroy()
    {
        lightSoundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        lightSoundInstance.release();
    }

    void Update()
    {
        // Reduce el tiempo hasta el próximo parpadeo
        timeToNextFlicker -= Time.deltaTime;

        if (timeToNextFlicker <= 0)
        {
            FlickerLight();
            SetNextFlickerTime();
        }
    }

    void FlickerLight()
    {
        // Alterna entre encender y apagar la luz de forma aleatoria
        if (Random.value > 0.5f)
        {
            //Luz Encendida 
            oldLight.intensity = lightIntensity;

            if (!isLightOn)
            {
                // Inicia el sonido solo si se acaba de encender
                lightSoundInstance.start();
                isLightOn = true;
            }
        }
        else
        {
            // Luz Apagada
            oldLight.intensity = offIntensity;

            if (isLightOn)
            {
                lightSoundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE); //STOP_MODE.FADEOUT queda peor creo yo
                isLightOn = false;
            }
        }
    }

    void SetNextFlickerTime()
    {
        // Determina un tiempo aleatorio para el próximo parpadeo
        timeToNextFlicker = Random.Range(minFlickerTime, maxFlickerTime);
    }
}