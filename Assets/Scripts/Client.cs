using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
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
    private Sprite[,] colateralArray;

    [SerializeField]
    private string[] frases;

    [SerializeField]
    public ArrayList clientsLeft = new ArrayList();

    [SerializeField]
    private int currentIndex;

    private Image currentImage;

    [SerializeField]
    private string[] currentFrases;

    [SerializeField]
    private TextMeshProUGUI currentText;

    [SerializeField]
    private int chosenMedicine;

    [SerializeField]
    private GameLoop gameloop;

    public event Action OnMedicineChosenAction;
    public event Action OnClientEndAction;


    private void Awake()
    {
        foreach (int index in clientIndexes)
        {
            clientsLeft.Add(index);
        }

        Debug.Log(clientsLeft.Count);

        currentImage = GetComponent<Image>();
        currentText = GameObject.FindWithTag("Frase").GetComponent<TextMeshProUGUI>();
        currentFrases = new string[4] { "", "", "", "" };
        colateralArray = new Sprite[4,3];
        AssetInitialize();
    }

    private void OnEnable()
    {
        gameloop = GameObject.FindWithTag("GameManager").GetComponent<GameLoop>();
        gameloop.OnGameStartAction += ChangeClient;
        gameloop.OnColateralEffectAction += ClientColateral;
        //gameloop.OnChangeClientAction += ChangeClient;
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

    private void AssetInitialize()
    {
        colateralArray[0,0] = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/Clients/player-hurt-1.png", typeof(Sprite));
        colateralArray[0,1] = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/Clients/player-crouch-1.png", typeof(Sprite));
        colateralArray[0,2] = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/Clients/player-jump-1.png", typeof(Sprite));

        colateralArray[1,0] = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/Clients/eagle-attack-2.png", typeof(Sprite));
        colateralArray[1,1] = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/Clients/eagle-attack-3.png", typeof(Sprite));
        colateralArray[1,2] = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/Clients/eagle-attack-4.png", typeof(Sprite));

        colateralArray[2,0] = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/Clients/frog-idle-2.png", typeof(Sprite));
        colateralArray[2,1] = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/Clients/frog-idle-3.png", typeof(Sprite));
        colateralArray[2,2] = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/Clients/frog-jump-1.png", typeof(Sprite));

        colateralArray[3,0] = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/Clients/opossum-3.png", typeof(Sprite));
        colateralArray[3,1] = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/Clients/opossum-4.png", typeof(Sprite));
        colateralArray[3,2] = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/Clients/opossum-6.png", typeof(Sprite));

    }

    public void ChangeClient()
    {
        Debug.Log("Change Client");
        if (clientsLeft.Count > 0)
        {
            currentIndex = RandomChoose();
            currentImage.sprite = avatarArray[currentIndex];
            GetClientInfo(currentIndex);
            currentText.text = currentFrases[0];
            Debug.Log(clientsLeft.Count);
        }
    }

    private void ClientColateral()
    {
        Debug.Log("Client Colateral");
        currentImage.sprite = colateralArray[currentIndex, chosenMedicine - 1];
        GetClientInfo(currentIndex);
        currentText.text = currentFrases[chosenMedicine];
        clientsLeft.Remove(currentIndex);
        OnClientEndAction();
    }

    private int RandomChoose()
    {
        int randIndex = UnityEngine.Random.Range(0, 101) % clientsLeft.Count;

        return randIndex;
    }

    public void SetMedicine1()
    {
        chosenMedicine = 1;
        OnMedicineChosenAction();
    }

    public void SetMedicine2()
    {
        chosenMedicine = 2;
        OnMedicineChosenAction();
    }

    public void SetMedicine3()
    {
        chosenMedicine = 3;
        OnMedicineChosenAction();
    }

}
