using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFliker : MonoBehaviour
{
    public Light oldLight; // Referencia a la luz (puedes asignarla desde el inspector)
    public float minFlickerTime = 0.1f; // Tiempo mínimo entre parpadeos
    public float maxFlickerTime = 0.5f; // Tiempo máximo entre parpadeos
    public float lightIntensity = 1.0f; // Intensidad normal de la luz
    public float offIntensity = 0.0f; // Intensidad cuando está apagada

    private float timeToNextFlicker; // Tiempo restante para el próximo parpadeo

    void Start()
    {
        if (oldLight == null)
        {
            oldLight = GetComponent<Light>();
        }

        SetNextFlickerTime();
    }

    void Update()
    {
        // Reduce el tiempo hasta el próximo parpadeo
        timeToNextFlicker -= Time.deltaTime;

        // Si es hora de parpadear, cambia la intensidad de la luz
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
            oldLight.intensity = lightIntensity; // Luz encendida
        }
        else
        {
            oldLight.intensity = offIntensity; // Luz apagada
        }
    }

    void SetNextFlickerTime()
    {
        // Determina un tiempo aleatorio para el próximo parpadeo
        timeToNextFlicker = Random.Range(minFlickerTime, maxFlickerTime);
    }
}
