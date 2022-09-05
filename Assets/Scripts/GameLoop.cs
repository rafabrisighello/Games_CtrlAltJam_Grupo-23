using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;

public class GameLoop : MonoBehaviour
{
    [SerializeField]
    private int reputacao;

    [SerializeField]
    private Client client;

    // Eventos
    public event Action OnGameStartAction;
    public event Action OnChangeClientAction;
    public event Action OnColateralEffectAction;
    public event Action OnClientEmptyAction;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        client = GameObject.FindWithTag("Cliente").GetComponent<Client>();
        client.OnMedicineChosenAction += ColateralEffect;
        client.OnClientEndAction += ChangeClient;
    }


    private void Start()
    {
        OnGameStartAction();
    }

    private void ColateralEffect()
    {
        OnColateralEffectAction();
    }

    private void ChangeClient()
    {
        //OnChangeClientAction();
    }

}
