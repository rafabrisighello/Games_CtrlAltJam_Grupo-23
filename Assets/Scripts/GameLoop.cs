using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameLoop : MonoBehaviour
{
    [SerializeField]
    private int reputacao;
    private int ClientIndex { get; set; }

    public event Action OnGameStart;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if(OnGameStart != null)
            {
                OnGameStart();
            }

        }

    }
}
