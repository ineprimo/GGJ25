using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissiveFlick : MonoBehaviour
{
    public Material emissiveMaterial; // Material con emisi�n
    public Color emissiveColor = Color.white; // Color de emisi�n
    public float maxEmissionIntensity = 2.0f; // Intensidad m�xima de emisi�n
    public float flickerInterval = 0.5f; // Tiempo fijo entre parpadeos

    private bool isEmitting = true; // Controla si el material est� emitiendo o no
    private float timer; // Temporizador para gestionar el parpadeo

    void Start()
    {
        if (emissiveMaterial == null)
        {
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                emissiveMaterial = renderer.material;
            }
            else
            {
                enabled = false;
                return;
            }
        }

        // Aseg�rate de que el material tenga el keyword de Emission activado
        emissiveMaterial.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= flickerInterval)
        {
            FlickerEmission();
            timer = 0; // Reinicia el temporizador
        }
    }

    void FlickerEmission()
    {
        if (isEmitting)
        {
            emissiveMaterial.SetColor("_EmissionColor", emissiveColor * 0); // Apagar emisi�n
        }
        else
        {
            emissiveMaterial.SetColor("_EmissionColor", emissiveColor * maxEmissionIntensity); // Encender emisi�n
        }

        isEmitting = !isEmitting; // Alternar el estado de emisi�n
    }
}
