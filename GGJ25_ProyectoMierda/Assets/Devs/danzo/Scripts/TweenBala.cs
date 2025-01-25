using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenBala : MonoBehaviour
{
    public float tiempoAntesDeJuggle = 2f;  
    public float amplitudMovimiento = 1f;   
    public float velocidadMovimiento = 2f; 

    private Vector3 posicionOriginal;
    private float tiempoTranscurrido = 0f;  
    private bool empezarOscilacion = false;  

    // Inicia el movimiento de la bala
    void Start()
    {
        posicionOriginal = transform.position;  
    }

    // Actualización por frame
    void Update()
    {
        if (empezarOscilacion)
        {
           
            float movimientoX = Mathf.Sin(Time.time * velocidadMovimiento) * amplitudMovimiento;
            float movimientoY = Mathf.Cos(Time.time * velocidadMovimiento) * amplitudMovimiento;

            transform.position = new Vector3(transform.position.x + movimientoX, transform.position.y + movimientoY, transform.position.z);
        }
        else
        {
            
            tiempoTranscurrido += Time.deltaTime;

           
            if (tiempoTranscurrido >= tiempoAntesDeJuggle)
            {
                empezarOscilacion = true;
            }
        }
    }
}
