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
    public List<int> clientsLeft = new List<int>();

    [SerializeField]
    private int currentIndex;

    private Image currentImage;
    private Image balao;
    private TextMeshProUGUI balaoFrase;
    private TextMeshProUGUI instructions;

    [SerializeField]
    private string[] currentFrases;

    [SerializeField]
    private TextMeshProUGUI currentText;

    [SerializeField]
    private int chosenMedicine;
    private int[] currentResults;

    [SerializeField]
    private GameLoop gameloop;

    private System.Random random;

    public event Action OnClientLoaded;
    public event Action OnMedicineChosenAction;
    public event Action OnClientEndAction;
    public event Action OnClientEmptyAction;


    private void Awake()
    {
        foreach (int index in clientIndexes)
        {
            clientsLeft.Add(index);
        }

        Debug.Log(clientsLeft.Count);

        currentImage = GetComponent<Image>();
        balao = GameObject.FindWithTag("Balao").GetComponent<Image>();
        balaoFrase = balao.GetComponentInChildren<TextMeshProUGUI>();
        instructions = GameObject.FindWithTag("Instructions").GetComponent<TextMeshProUGUI>();
        currentText = GameObject.FindWithTag("Frase").GetComponent<TextMeshProUGUI>();
        currentFrases = new string[4] { "", "", "", "" };
        colateralArray = new Sprite[4,3];
        AssetInitialize();
    }

    private void OnEnable()
    {
        gameloop = GameObject.FindWithTag("GameManager").GetComponent<GameLoop>();
        gameloop.OnGameStartAction += ChangeClient;
        gameloop.OnChangeClientAction += ChangeClient;

        random = new System.Random();
    }

    private void Start()
    {
        currentImage.enabled = false;
        balao.enabled = false;
        balaoFrase.enabled = false;
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
        int[] deltaReputation = new int[3];

        switch (index)
        {
            case 0:
                frases[0] = "Preciso de um remédio para resfriado!";
                frases[1] = "Tive uma diarréia fortíssima!";
                deltaReputation[0] = -2;
                frases[2] = "Estou me sentindo muito fraco! ARRGH";
                deltaReputation[1] = -3;
                frases[3] = "Não paro de suar, mas isso está atraindo mais olhares ";
                deltaReputation[2] = 4;
                break;

            case 1:
                frases[0] = "Preciso de um remédio para resfriado!";
                frases[1] = "Tive uma diarréia fortíssima!";
                deltaReputation[0] = -2;
                frases[2] = "Estou me sentindo muito fraco! ARRGH";
                deltaReputation[1] = -3;
                frases[3] = "Não paro de suar, mas isso está atraindo mais olhares ";
                deltaReputation[2] = 4;
                break;

            case 2:
                frases[0] = "Preciso de um remédio para resfriado!";
                frases[1] = "Tive uma diarréia fortíssima!";
                deltaReputation[0] = -2;
                frases[2] = "Estou me sentindo muito fraco! ARRGH";
                deltaReputation[1] = -3;
                frases[3] = "Não paro de suar, mas isso está atraindo mais olhares ";
                deltaReputation[2] = 4;
                break;
            case 3:
                frases[0] = "Preciso de um remédio para resfriado!";
                frases[1] = "Tive uma diarréia fortíssima!";
                deltaReputation[0] = -2;
                frases[2] = "Estou me sentindo muito fraco! ARRGH";
                deltaReputation[1] = -3;
                frases[3] = "Não paro de suar, mas isso está atraindo mais olhares ";
                deltaReputation[2] = 4;
                break;
        }

        currentFrases = frases;
        currentResults = deltaReputation;
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
            clientsLeft.Remove(currentIndex);
            currentImage.sprite = avatarArray[currentIndex];
            GetClientInfo(currentIndex);
            currentText.text = currentFrases[0];
            Debug.Log(clientsLeft.Count);
        }
        StartCoroutine(ClientLoaded());

    }

    private int RandomChoose()
    {
        int randomIndex = random.Next(clientsLeft.Count);
        int clientChosen = clientsLeft[randomIndex];
        return clientChosen;
    }

    public void SetMedicine1()
    {
        chosenMedicine = 1;
        StartCoroutine(ColateralRoutine(0));
    }

    public void SetMedicine2()
    {
        chosenMedicine = 2;
        StartCoroutine(ColateralRoutine(1));
    }

    public void SetMedicine3()
    {
        chosenMedicine = 3;
        StartCoroutine(ColateralRoutine(2));
    }

    private void ClientCheck()
    {
        if (clientsLeft.Count > 0)
        {
            OnClientEndAction();
        }
        else OnClientEmptyAction();
    }

    IEnumerator ClientLoaded()
    {
        yield return new WaitForSeconds(2.0f);
        currentImage.enabled = true;
        yield return new WaitForSeconds(1.0f);
        balao.enabled = true;
        balaoFrase.enabled = true;
        yield return new WaitForSeconds(2.0f);
        instructions.text = "Escolha o remédio apropriado: ";
        OnClientLoaded();
    }

    IEnumerator ColateralRoutine(int choice)
    {
        OnMedicineChosenAction();
        currentImage.enabled = false;
        balao.enabled = false;
        balaoFrase.enabled = false;
        yield return new WaitForSeconds(2.0f);
        currentImage.sprite = colateralArray[currentIndex, chosenMedicine - 1];
        GetClientInfo(currentIndex);
        currentText.text = currentFrases[chosenMedicine];
        currentImage.enabled = true;
        balao.enabled = true;
        balaoFrase.enabled = true;
        yield return new WaitForSeconds(3.0f);
        gameloop.SetReputation(currentResults[choice]);
        yield return new WaitForSeconds(2.0f);
        ClientCheck();
    }

}
