using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] songArray;

    [SerializeField]
    private static AudioManager audioManagerInstance = null;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private GameLoop gameloop;

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

        audioSource = audioManagerInstance.GetComponent<AudioSource>();
        audioSource.clip = songArray[4];
        audioSource.Play();
    }

    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            gameloop = GameObject.FindWithTag("GameManager").GetComponent<GameLoop>();
            gameloop.OnGameStartAction += delegate { ChangeMusic(0); };
        }

        if(SceneManager.GetActiveScene().buildIndex == 4)
        {
            ChangeMusic(2);
        }
    }

    private void ChangeMusic(int index)
    {
        audioSource.Stop();
        audioSource.clip = songArray[index];
        audioSource.Play();
    }

}
