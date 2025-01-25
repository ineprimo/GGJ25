using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundWithVariation : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private float _pitchVariationRange = 0.2f;

    private void Start()
    {
        _audioSource = GameManager.Instance.GetPlayer().GetComponent<AudioSource>();
    }
    public void Reproduce()
    {
        if (_audioSource != null && _audioClip != null)
        {
            _audioSource.pitch = 1f + Random.Range(-_pitchVariationRange, _pitchVariationRange);
            _audioSource.clip = _audioClip;
            _audioSource.Play();
        }
    }
}
