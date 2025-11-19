using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class CoinAudioManager : MonoBehaviour
{
    public static CoinAudioManager Instance;

    [Header("FMOD Settings")]
    [SerializeField] private EventReference coinCollectEvent;

    [SerializeField] private string parameterName = "FollowingCoins";

    [Header("Combo Settings")]
    [SerializeField] private float resetTime = 2.0f;
    [SerializeField] private float valueIncrement = 1.0f;
    [SerializeField] private float maxParameterValue = 20.0f;

    private float currentParameterValue = 0f;
    private float lastPickupTime;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (Time.time - lastPickupTime > resetTime)
        {
            currentParameterValue = 0f;
        }
    }

    public void PlayCoinSound()
    {
        lastPickupTime = Time.time;

        currentParameterValue = Mathf.Clamp(currentParameterValue + valueIncrement, 0f, maxParameterValue);

        EventInstance coinInstance = RuntimeManager.CreateInstance(coinCollectEvent);

        coinInstance.setParameterByName(parameterName, currentParameterValue);

        coinInstance.start();
        coinInstance.release(); // Se destruye sola cuando termina el audio
    }
}