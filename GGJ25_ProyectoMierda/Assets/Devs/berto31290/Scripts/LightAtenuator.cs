using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAtenuator : MonoBehaviour
{
    public Light discoLight; // Referencia a la luz (puedes asignarla desde el inspector)
    public float minHoldTime = 0.5f; // Tiempo mínimo que se mantendrá un color
    public float maxHoldTime = 2.0f; // Tiempo máximo que se mantendrá un color

    private float timeToNextChange; // Tiempo restante para cambiar al siguiente color

    void Start()
    {
        if (discoLight == null)
        {
            discoLight = GetComponent<Light>();
        }

        SetRandomColor();
    }

    void Update()
    {
        // Reduce el tiempo hasta el próximo cambio
        timeToNextChange -= Time.deltaTime;

        // Cambia de color cuando el tiempo llegue a 0
        if (timeToNextChange <= 0)
        {
            SetRandomColor();
        }
    }

    void SetRandomColor()
    {
        // Asigna un color aleatorio
        discoLight.color = new Color(
            Random.Range(0f, 1f), // Rojo
            Random.Range(0f, 1f), // Verde
            Random.Range(0f, 1f)  // Azul
        );

        // Determina un tiempo aleatorio hasta el próximo cambio
        timeToNextChange = Random.Range(minHoldTime, maxHoldTime);
    }
}
