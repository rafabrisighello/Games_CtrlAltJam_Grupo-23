using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManager;

    [SerializeField]
    private int currentSceneIndex;

    [SerializeField]
    private int previousSceneIndex;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (gameManager != null)
            {
                Instantiate(gameManager);
            }
        }
        
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            DestroyImmediate(GameObject.FindWithTag("GameManager"));
        }
    }

    public void PlayScreen()
    {
        SceneManager.LoadScene(3);
    }

    public void InfoScreen()
    {
        SceneManager.LoadScene(1);
    }

    public void OptionScreen()
    {
        SceneManager.LoadScene(2);
    }

    public void StartScreen()
    {
        SceneManager.LoadScene(0);
    }

    public void GoBack()
    {
        SceneManager.LoadScene(previousSceneIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void GetPreviousSceneIndex()
    {
        previousSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void UpdateSceneIndex()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
}
