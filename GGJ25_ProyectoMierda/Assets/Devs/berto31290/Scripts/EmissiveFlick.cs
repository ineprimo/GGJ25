using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissiveFlick : MonoBehaviour
{
    public Material emissiveMaterial; // Material con emisión
    public Color emissiveColor = Color.white; // Color de emisión
    public float maxEmissionIntensity = 2.0f; // Intensidad máxima de emisión
    public float flickerInterval = 0.5f; // Tiempo fijo entre parpadeos

    private bool isEmitting = true; // Controla si el material está emitiendo o no
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
                Debug.LogError("No se encontró un Renderer ni se asignó un material. Por favor, asigna un material al script.");
                enabled = false;
                return;
            }
        }

        // Asegúrate de que el material tenga el keyword de Emission activado
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
            emissiveMaterial.SetColor("_EmissionColor", emissiveColor * 0); // Apagar emisión
        }
        else
        {
            emissiveMaterial.SetColor("_EmissionColor", emissiveColor * maxEmissionIntensity); // Encender emisión
        }

        isEmitting = !isEmitting; // Alternar el estado de emisión
    }
}
