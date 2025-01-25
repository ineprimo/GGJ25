using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomSoundClip : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private float _maxReplayTime = 5f;

    private float _currentTime = 0f;
    private void Start()
    {
        _maxReplayTime += Random.RandomRange(-2.0f, 2.0f);
    }
    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _maxReplayTime)
        {
            _currentTime = 0f;
            _maxReplayTime += Random.RandomRange(-2.0f, 2.0f);

            if (_clips.Length > 0)
            {
                int randomIndex = Random.Range(0, _clips.Length);
                _audioSource.clip = _clips[randomIndex];
                _audioSource.Play();
            }
        }
    }
}
