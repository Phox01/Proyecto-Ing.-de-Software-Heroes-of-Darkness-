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
        Debug.Log("is runing maaaan");
    }

    public void StopAudio()
    {
        controlAudio.Stop();
    }

    
}
