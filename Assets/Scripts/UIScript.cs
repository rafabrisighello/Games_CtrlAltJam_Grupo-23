using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManager;

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

    public void NextScreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartScreen()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
