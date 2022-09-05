﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameLoop : MonoBehaviour
{
    [SerializeField]
    private int reputacao;

    [SerializeField]
    private Client client;

    [SerializeField]
    private Button[] medButtons;

    // Eventos
    public event Action OnGameStartAction;
    public event Action OnChangeClientAction;
    public event Action OnColateralEffectAction;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        client = GameObject.FindWithTag("Cliente").GetComponent<Client>();
        client.OnClientLoaded += EnableChoices;
        client.OnMedicineChosenAction += ClearChoices;
        client.OnClientEmptyAction += FinalResults;
        client.OnClientEndAction += ClientSwap;

        foreach(Button button in medButtons)
        {
            button.gameObject.SetActive(false);
        }
    }


    private void Start()
    {
        StartCoroutine(Intro());
    }

    private void EnableChoices()
    {
        StartCoroutine(MedDisplay());
    }

    private void ClearChoices()
    {
        StartCoroutine(ButtonClear());
    }

    private void ClientSwap()
    {
        StartCoroutine(ClientChange());
    }

    private void FinalResults()
    {
        StartCoroutine(WaitEnd());
    }

    IEnumerator Intro()
    {
        yield return new WaitForSeconds(2.0f);
        OnGameStartAction();
    }

    IEnumerator MedDisplay()
    {
        yield return new WaitForSeconds(1.0f);
        medButtons[0].gameObject.SetActive(true);
        medButtons[0].GetComponent<Button>().enabled = false;
        yield return new WaitForSeconds(1.0f);
        medButtons[1].gameObject.SetActive(true);
        medButtons[1].GetComponent<Button>().enabled = false;
        yield return new WaitForSeconds(1.0f);
        medButtons[2].gameObject.SetActive(true);
        medButtons[2].GetComponent<Button>().enabled = false;
        yield return new WaitForSeconds(1.0f);
        medButtons[0].GetComponent<Button>().enabled = true;
        medButtons[1].GetComponent<Button>().enabled = true;
        medButtons[2].GetComponent<Button>().enabled = true;
    }

    IEnumerator ButtonClear()
    {
        medButtons[2].gameObject.SetActive(false);
        medButtons[1].gameObject.SetActive(false);
        medButtons[0].gameObject.SetActive(false);
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator ClientChange()
    {
        yield return new WaitForSeconds(2.0f);
        OnChangeClientAction();
    }

    IEnumerator WaitEnd()
    {
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
