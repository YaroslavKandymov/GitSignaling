using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]

public class Signaling : MonoBehaviour
{
    [Range(0, 20)]
    [SerializeField] private float _speedSound = 10;

    private AudioSource _signaling;
    private Coroutine _coroutine;
    private bool value = false;

    private void Awake()
    {
        _signaling = GetComponent<AudioSource>();
        _signaling.volume = 0;
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
            if (_signaling.volume >= 0 && value == false)
            {
                _signaling.volume += 0.001f * _speedSound;
                if (_signaling.volume >= 1)
                {
                    value = true;
                }
            }

            if (_signaling.volume <= 1 && value)
            {
                _signaling.volume -= 0.001f * _speedSound;
                if (_signaling.volume <= 0)
                {
                    value = false;
                }
            }

            yield return null;
        }
    }
}
