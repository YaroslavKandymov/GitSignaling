using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]

public class Signaling : MonoBehaviour
{
    [SerializeField] private float _speedSound;

    private AudioSource _signaling;
    private Coroutine _coroutine;
    private bool maxVolumeReached = false;

    private void Awake()
    {
        _signaling = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            _signaling.Play();
            _coroutine = StartCoroutine(FadeIn());
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        _signaling.Stop();
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private IEnumerator FadeIn()
    {
        _signaling.volume = 0;
        while (true)
        {
            if (_signaling.volume >= 0 && maxVolumeReached == false)
            {
                _signaling.volume = Mathf.MoveTowards(_signaling.volume, 1, _speedSound * Time.deltaTime);
                if (_signaling.volume >= 1)
                {
                    maxVolumeReached = true;
                }
            }

            if (_signaling.volume <= 1 && maxVolumeReached)
            {
                _signaling.volume = Mathf.MoveTowards(_signaling.volume, 0, _speedSound * Time.deltaTime);
                if (_signaling.volume <= 0)
                {
                    maxVolumeReached = false;
                }
            }

            yield return null;
        }
    }
}
