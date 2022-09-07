using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

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


    // Botões para escolha de Remédios - Array de referências
    [SerializeField]
    private Button[] medButtons;

    // Referência ao botão para prosseguir no jogo
    [SerializeField]
    private GameObject proceedButton;

    // Referência ao botão para confirmar a escolha do remédio
    [SerializeField]
    private GameObject ackButton;

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
        proceedButton.SetActive(false);
        ackButton.SetActive(false);

        // Subscrição dos métodos aos eventos do cliente
        client.OnClientLoaded += EnableProceed;
        client.OnClientEmptyAction += FinalResults;
        client.OnClientEndAction += ClientSwap;

        // Subscrição dos métodos aos eventos do Remédio
        medicine.OnMedChosenAction += EnableAckButton;

        MedHide();
    }


    private void Start()
    {
        reputation = 5;
        StartCoroutine(Intro());
    }

    private void EnableProceed()
    {
        proceedButton.SetActive(true);
    }

    public void Proceed()
    {
        StartCoroutine(MedDisplay());
    }

    private void ClientSwap()
    {
        StartCoroutine(ClientChange());
    }

    private void FinalResults()
    {
        StartCoroutine(WaitEnd());
    }

    private void EnableAckButton()
    {
        StartCoroutine(AckButton());
    }

    IEnumerator Intro()
    {
        yield return new WaitForSeconds(1.0f);
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
        medButtons[0].GetComponent<Button>().enabled = true;
        medButtons[1].GetComponent<Button>().enabled = true;
    }

    private void MedHide()
    {
        foreach (Button button in medButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    IEnumerator ClientChange()
    {
        yield return new WaitForSeconds(1.0f);
        OnChangeClientAction();
    }

    IEnumerator WaitEnd()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator AckButton()
    {
        yield return new WaitForSeconds(0.5f);
        ackButton.SetActive(true);
    }

    public void SetReputation(int delta)
    {
        reputation += delta;
        if(reputation < 1)
        {
            reputation = 0;
        }
        else if(reputation > 10)
        {
            reputation = 10;
        }
    }

    private void ReputationUpdate()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            if (i < reputation)
            {
                stars[i].GetComponent<Image>().enabled = true;
            }
            else stars[i].GetComponent<Image>().enabled = false;
        }
    }

    public void MedChoice()
    {
        MedHide();
        OnMedAppliedAction();
    }

}
