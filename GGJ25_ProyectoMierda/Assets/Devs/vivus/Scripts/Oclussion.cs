using UnityEngine;
using System.Collections; // Necesario para usar Coroutines
using FMODUnity; // Asegúrate de tener este 'using' si usas la API de FMOD

public class Oclussion : MonoBehaviour
{
    public string fmodParameterName = "Oclusion";
    public float transitionDuration = 2.0f;
    private Coroutine transitionCoroutine;

    // Se llama cuando OTRO Collider entra en este trigger
    private void OnTriggerEnter(Collider other)
    {
        // Usar el componente específico (PlayerMovement) como estás haciendo
        if (other.GetComponent<PlayerMovement>() != null)
        {
            // 1. Detiene cualquier transición anterior (la que subía a 1)
            if (transitionCoroutine != null)
            {
                StopCoroutine(transitionCoroutine);
            }

            // 2. Inicia la Coroutine para la TRANSICIÓN SUAVE de 1 a 0
            // (Usa la misma duración para que la transición sea simétrica)
            transitionCoroutine = StartCoroutine(TransitionFMODParameter(1f, 0f, transitionDuration));
        }
    }

    // Se llama cuando OTRO Collider sale de este trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            // Detiene cualquier transición anterior que pueda estar corriendo
            if (transitionCoroutine != null)
            {
                StopCoroutine(transitionCoroutine);
            }

            // Inicia la Coroutine para la transición de 0 a 1
            transitionCoroutine = StartCoroutine(TransitionFMODParameter(0f, 1f, transitionDuration));
        }
    }

    // Coroutine para cambiar el valor del parámetro suavemente a lo largo del tiempo
    private IEnumerator TransitionFMODParameter(float startValue, float endValue, float duration)
    {
        float startTime = Time.time;
        float elapsedTime = 0f;

        // Establecer el valor inicial (si no está ya en el valor de inicio)
        RuntimeManager.StudioSystem.setParameterByName(fmodParameterName, startValue);

        // Bucle hasta que el tiempo transcurrido sea mayor o igual a la duración
        while (elapsedTime < duration)
        {
            elapsedTime = Time.time - startTime;

            // Calcula el progreso (0.0 a 1.0)
            float t = Mathf.Clamp01(elapsedTime / duration);

            // Interpola el valor (puede usar Mathf.Lerp o una curva personalizada si lo deseas)
            float currentValue = Mathf.Lerp(startValue, endValue, t);

            // Establece el parámetro Global de FMOD
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName(fmodParameterName, currentValue);

            yield return null; // Espera al siguiente frame
        }

        // Asegúrate de que el valor final sea EXACTAMENTE 1f
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(fmodParameterName, endValue);

        // La coroutine ha terminado, limpiamos la referencia
        transitionCoroutine = null;
    }
}