using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Effects : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioSource sfxSource;

    [SerializeField]
    private Slider[] sliders;

    private void OnEnable()
    {
        audioSource = FindObjectOfType<AudioManager>().GetComponent<AudioSource>();
        sfxSource = FindObjectOfType<SFXManager>().GetComponent<AudioSource>();

        sliders[0].value = audioSource.volume;
        sliders[1].value = sfxSource.volume;
    }


    public void SetAmbientSound()
    {
        audioSource.volume = sliders[0].value;
    }

    public void SetSFXSound()
    {
        sfxSource.volume = sliders[1].value;
    }
}
