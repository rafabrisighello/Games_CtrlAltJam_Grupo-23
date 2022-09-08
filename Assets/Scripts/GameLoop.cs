using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{
    // Pontos de Reputação
    [SerializeField]
    private int reputation;

    // Ícones de Reputação
    [SerializeField]
    private GameObject[] stars;

    // Referência da Classe Cliente e Remédio
    [SerializeField]
    private Client client;
    [SerializeField]
    private Medicine medicine;

    // Referência ao botão para prosseguir no jogo
    [SerializeField]
    private GameObject proceedButton;

    // Referência ao botão para aplicar o remédio
    [SerializeField]
    private GameObject applyButton;

    // Eventos
    public event Action OnGameStartAction; // Início do jogo
    public event Action OnMedAppliedAction; // Remédio foi escolhido
    public event Action OnColateralEffectAction; // Apresentação de efeitos colaterais
    public event Action OnChangeClientAction; // Mudança de cliente

    private void Awake()
    {
        // Objeto permanente até o final do jogo
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        proceedButton.GetComponent<Button>().enabled = false;
        proceedButton.GetComponent<Image>().enabled = false;
        applyButton.GetComponent<Button>().enabled = false;
        applyButton.GetComponent<Image>().enabled = false;

        // Subscrição dos métodos aos eventos do cliente
        client.OnClientEmptyAction += FinalResults;
        client.OnClientEndAction += EnableProceed;

        // Subscrição dos métodos aos eventos do Remédio
        medicine.OnMedChosenAction += EnableApplyButton;
    }


    private void Start()
    {
        reputation = 0;
        StartCoroutine(Intro());
    }

    private void EnableProceed()
    {
        StartCoroutine(Proceed());
    }

    public void ClientSwap()
    {
        StartCoroutine(ClientChange());
    }

    private void FinalResults()
    {
        StartCoroutine(WaitEnd());
    }

    private void EnableApplyButton()
    {
        StartCoroutine(ApplyButton());
    }

    IEnumerator Intro()
    {
        yield return new WaitForSeconds(1.0f);
        OnGameStartAction();
    }

    IEnumerator ClientChange()
    {
        yield return new WaitForSeconds(1.0f);
        OnChangeClientAction();
    }

    IEnumerator WaitEnd()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(4);
    }

    IEnumerator ApplyButton()
    {
        yield return new WaitForSeconds(0.5f);
        applyButton.SetActive(true);
        applyButton.GetComponent<Button>().enabled = true;
        applyButton.GetComponent<Image>().enabled = true;
    }

    IEnumerator Proceed()
    {
        yield return new WaitForSeconds(3.0f);
        proceedButton.SetActive(true);
        proceedButton.GetComponent<Button>().enabled = true;
        proceedButton.GetComponent<Image>().enabled = true;
    }

    public void MedChoice()
    {
        OnMedAppliedAction();
    }

    public void SetReputation(int delta)
    {
        reputation += delta;
    }

    public int GetReputation()
    {
        return reputation;
    }
}
