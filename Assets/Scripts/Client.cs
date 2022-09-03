using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Client : MonoBehaviour
{
    private int[] clientIndexes = { 0, 1, 2, 3 };

    [SerializeField]
    private Sprite[] avatarArray;

    [SerializeField]
    private string[] frases;

    private ArrayList clientsLeft = new ArrayList();

    private int currentIndex;
    private Sprite currentImage;
    private string[] currentFrases;
    private TextMeshProUGUI currentText;


    private ArrayList randomIndexes = new ArrayList();


    private void Start()
    {
        currentImage = GetComponent<Image>().sprite;
        currentText = GameObject.FindWithTag("Frase").GetComponent<TextMeshProUGUI>();

        GetComponent<GameLoop>().OnGameStart += InitializeClients;
    }

    enum Casos
    {
        BodyBuilder,
        Nadador,
        Religiosa,
        Palhaço
    };

    public string[] GetClientInfo(int index)
    {
        string[] frases = new string[4];

        switch (index)
        {
            case 0:
                frases[0] = "Preciso de um remédio para resfriado!";
                frases[1] = "Tive uma diarréia fortíssima!";
                frases[2] = "Estou me sentindo muito fraco! ARRGH";
                frases[3] = "Não paro de suar, mas isso está atraindo mais olhares ";
                break;

            case 1:
                frases[0] = "Preciso de um remédio para resfriado!";
                frases[1] = "Tive uma diarréia fortíssima!";
                frases[2] = "Estou me sentindo muito fraco! ARRGH";
                frases[3] = "Não paro de suar, mas isso está atraindo mais olhares ";
                break;

            case 2:
                frases[0] = "Preciso de um remédio para resfriado!";
                frases[1] = "Tive uma diarréia fortíssima!";
                frases[2] = "Estou me sentindo muito fraco! ARRGH";
                frases[3] = "Não paro de suar, mas isso está atraindo mais olhares ";
                break;
            case 3:
                frases[0] = "Preciso de um remédio para resfriado!";
                frases[1] = "Tive uma diarréia fortíssima!";
                frases[2] = "Estou me sentindo muito fraco! ARRGH";
                frases[3] = "Não paro de suar, mas isso está atraindo mais olhares ";
                break;
        }

        return null;
    }

    private void InitializeClients()
    {
        
    }

    private void ChangeClient()
    {
        
    }

    private int RandomChoose(ArrayList clientsLeft)
    {
        int randIndex = UnityEngine.Random.Range(0, clientsLeft.Count);

        return randIndex;
    }
}
