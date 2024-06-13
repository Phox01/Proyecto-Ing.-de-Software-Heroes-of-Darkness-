using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagement : MonoBehaviour
{
    [SerializeField] private AudioClip[] audios;
    private AudioSource controlAudio;
    public static MusicManagement instance;

    private void Awake()
    {
          if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
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
    }

    public void StopAudio()
    {
        controlAudio.Stop();
    }

    
}
