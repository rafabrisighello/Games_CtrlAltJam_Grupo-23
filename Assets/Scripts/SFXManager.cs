using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SFXManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] sfxArray;

    [SerializeField]
    private List<Button> buttons = new List<Button>();

    [SerializeField]
    private GameLoop gameloop;

    [SerializeField]
    private Client client;

    [SerializeField]
    private AudioManager audioManager;

    private static SFXManager sfxManagerInstance = null;

    private void Awake()
    {
        UpdateListeners();

        foreach(Button button in buttons)
        {
            SubscribeEffect(button);
        }
        

        if (sfxManagerInstance == null)
        {
            sfxManagerInstance = this;
            DontDestroyOnLoad(this);
        }
        else if (sfxManagerInstance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void OnLevelWasLoaded(int level)
    {
        UpdateListeners();

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            gameloop = GameObject.FindWithTag("GameManager").GetComponent<GameLoop>();
            gameloop.OnChangeClientAction += delegate { PlayEffect(9); };
            gameloop.OnColateralEffectAction += delegate { PlayEffect(10); };
        }

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            client = GameObject.FindWithTag("Cliente").GetComponent<Client>();
            client.OnClientLoaded += delegate { PlayEffect(11); };
        }
    }

    public void PlayEffect(int sfxIndex)
    {
        GetComponent<AudioSource>().PlayOneShot(sfxArray[sfxIndex]);
        Debug.Log("Play Sound");
    }

    private int GetSFXIndex(string tag) {

        int sfxIndex;

        switch(tag)
        {
            case "PlayButton":
                sfxIndex = 0;
                break;
            case "InfoButton":
                sfxIndex = 1;
                break;
            case "OptionsButton":
                sfxIndex = 2;
                break;
            case "BackButton":
                sfxIndex = 3;
                break;
            case "ProceedButton":
                sfxIndex = 4;
                break;
            case "ApplyButton":
                sfxIndex = 5;
                break;
            case "Med1":
                sfxIndex = 6;
                break;
            case "Med2":
                sfxIndex = 7;
                break;
            case "Med3":
                sfxIndex = 8;
                break;
            default:
                sfxIndex = 0;
                break;
        }

        return sfxIndex;
    }

    private void SubscribeEffect(Button button)
    {
        button.GetComponent<Button>().onClick.AddListener(delegate { PlayEffect(GetSFXIndex(button.gameObject.tag)); });
    }

    private void UpdateListeners()
    {
        buttons.Clear();

        buttons.AddRange(FindObjectsOfType<Button>());

        foreach (Button button in FindObjectsOfType<Button>())
        {
            SubscribeEffect(button);
        }
    }
    
}
