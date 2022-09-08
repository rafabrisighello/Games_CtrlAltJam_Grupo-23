using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] songArray;

    private static AudioManager audioManagerInstance = null;

    private void Awake()
    {
        if (audioManagerInstance == null)
        {
            audioManagerInstance = this;
            DontDestroyOnLoad(this);
        }
        else if (audioManagerInstance != this)
        {
            Destroy(gameObject);
        }
    }

}
