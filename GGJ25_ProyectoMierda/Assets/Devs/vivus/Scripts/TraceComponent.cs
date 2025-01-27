using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceComponent : MonoBehaviour
{
    [SerializeField] private float _maxBubbleGenerationTime = 0.05f;
    private float _currentBubbleGenerationTime = 0.0f;

    public float _currentBubbleDamage;
    private float _currentBubbleLifeTime = 3.0f;

    [SerializeField] private GameObject _bubblePrefab;

    private bool _canSinged;

    private bool _singed;

    // M�todos para activar el estado de "singed" y la habilidad de generar burbujas
    public void ActivateSinged()
    {
        _singed = true;
    }

    public void SetCurrentBubbleDamage(float dm)
    {
        _currentBubbleDamage = dm;
    }

    public void SetCurrentBubbleLifeTime(float tm)
    {
        _currentBubbleLifeTime = tm;
    }

    // Start is called before the first frame update
    void Start()
    {
        _canSinged = true;
        _singed = true;  // Inicialmente est� habilitado para generar burbujas
    }

    // Update is called once per frame
    void Update()
    {
        // Si el personaje est� "firmado" y ha pasado el tiempo suficiente para generar una burbuja
        if (_singed && _maxBubbleGenerationTime <= _currentBubbleGenerationTime)
        {
            // Crear la burbuja en la posici�n del personaje
            GameObject auxBubble = Instantiate(_bubblePrefab, transform.position, Quaternion.identity);

            // A�adir un desplazamiento aleatorio a la posici�n de la burbuja para variaci�n
            Vector3 auxOffset = new Vector3(
                Random.Range(-0.5f, 0.5f),
                Random.Range(-0.5f, 0.5f),
                Random.Range(-0.5f, 0.5f)
            );
            auxBubble.transform.position += auxOffset;

            // Configurar los da�os y el tiempo de vida de la burbuja
            auxBubble.GetComponent<BubbleSignedComponent>().SetDamage(_currentBubbleDamage);
            auxBubble.GetComponent<LifeTimeComponent>().SetMaxDeleteTime(_currentBubbleLifeTime);

            // Reiniciar el contador de generaci�n de burbujas
            _currentBubbleGenerationTime = 0.0f;
        }

        // Aumentar el tiempo de generaci�n de burbujas
        _currentBubbleGenerationTime += Time.deltaTime;
    }
}
