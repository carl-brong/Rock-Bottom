
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIAudioConnector : MonoBehaviour
{
    [SerializeField] private AudioClip _selectionAudio;
    [SerializeField] private AudioClip _submitAudio;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        _audioSource.PlayOneShot(_selectionAudio);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        _audioSource.PlayOneShot(_submitAudio);
    }

    public void OnSliderSelect(float _)
    {
        _audioSource.PlayOneShot(_selectionAudio);
    }
}
