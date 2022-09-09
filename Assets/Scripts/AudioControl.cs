using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{
    [SerializeField]
    private AudioManager audioManager;

    [SerializeField]
    private SFXManager sfxManager;

    [SerializeField]
    private List<AudioSource> audioSources = new List<AudioSource>();

    private void OnEnable()
    {
        audioManager = FindObjectOfType<AudioManager>();
        sfxManager = FindObjectOfType<SFXManager>();

        audioSources.Add(audioManager.GetComponent<AudioSource>());
        audioSources.Add(sfxManager.GetComponent<AudioSource>());
    }

    public void RaiseSound()
    {
        foreach(AudioSource source in audioSources)
        {
            source.volume += 0.1f;
        }
    }

    public void LowerSound()
    {
        foreach (AudioSource source in audioSources)
        {
            source.volume -= 0.1f;
        }
    }

    public void Mute()
    {
        foreach (AudioSource source in audioSources)
        {
            if (!source.mute)
            {
                source.mute = true;
            }
            else source.mute = false;
        }
    }
}
