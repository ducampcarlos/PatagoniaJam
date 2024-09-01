using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundTrigger : MonoBehaviour
{
    public AudioSource audioSource; // Referencia al AudioSource
    public AudioClip clip; // Referencia al AudioClip que se va a reproducir

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica si el jugador es quien activa el evento
        {
            audioSource.PlayOneShot(clip); // Reproduce el sonido
        }
    }
}
