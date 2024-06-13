using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagement : MonoBehaviour
{
    [SerializeField] private AudioClip[] audios;
    private AudioSource controlAudio;

    private void Awake()
    {
        
        controlAudio = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);

    }

    public void SeleccionAudio(int indice, float volumen)
    {
        controlAudio.PlayOneShot(audios[indice], volumen);
        
        
    }

    public void AudioLoop(int indice, float volumen)
    {
        controlAudio.clip = audios[indice];
        controlAudio.volume = volumen;
        controlAudio.loop=true;
        controlAudio.Play();
    }

    public void StopAudio()
    {
        controlAudio.Stop();
    }

    
}
