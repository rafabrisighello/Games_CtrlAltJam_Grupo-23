using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Client : MonoBehaviour
{
    [SerializeField]
    private int[] clientIndexes = { 0, 1, 2, 3 };

    [SerializeField]
    private Sprite[] avatarArray;

    [SerializeField]
    private string[] frases;

    [SerializeField]
    private ArrayList clientsLeft = new ArrayList();

    [SerializeField]
    private int currentIndex;

    private Image currentImage;

    [SerializeField]
    private string[] currentFrases;

    [SerializeField]
    private TextMeshProUGUI currentText;


    private ArrayList randomIndexes = new ArrayList();

    // Eventos
    public event Action OnClientReady;


    private void Start()
    {
        foreach(int index in clientIndexes)
        {
            clientsLeft.Add(index);
            
        }
        currentImage = GetComponent<Image>();
        currentText = GameObject.FindWithTag("Frase").GetComponent<TextMeshProUGUI>();
        currentFrases = new string[4]{"","","",""};
        ChangeClient();
    }

    enum Casos
    {
        BodyBuilder,
        Nadador,
        Religiosa,
        Palhaço
    };

    public void GetClientInfo(int index)
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

        currentFrases = frases;
    }

    private void ChangeClient()
    {
        if (clientsLeft.Count > 0)
        {
            currentIndex = RandomChoose(clientsLeft);
            SetAvatarImage();
            GetClientInfo(currentIndex);
            SetAvatarText();
            clientsLeft.Remove(currentIndex);
        }
    }

    private int RandomChoose(ArrayList clientsLeft)
    {
        int randIndex = UnityEngine.Random.Range(0, 100 * clientsLeft.Count) % clientsLeft.Count;

        return randIndex;
    }

    private void SetAvatarImage()
    {
        currentImage.sprite = avatarArray[currentIndex];
    }

    private void SetAvatarText()
    {
        currentText.text = currentFrases[0];
    }

}
