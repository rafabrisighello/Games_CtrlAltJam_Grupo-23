using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Medicine : MonoBehaviour
{
    [SerializeField]
    private Sprite[] medicineSprites;

    [SerializeField]
    private int currentMed;

    [SerializeField]
    private TextBox textBox;

    public event Action OnMedChosenAction;

    public void ChangeMedInfo(int medChoice)
    {
        SetMed(medChoice);
        textBox.SetCurrentText(GetMedInfo(medChoice));
        Debug.Log("Remedio escolhido: " + medChoice);
        OnMedChosenAction();
    }

    public int GetMed()
    {
        return currentMed;
    }

    public void SetMed(int index)
    {
        currentMed = index;
    }

    private string GetMedInfo(int index)
    {
        string[] medInfo = { "", "", "" };

        int clientIndex = GameObject.FindWithTag("Cliente").GetComponent<Client>().GetClientIndex();

        switch (clientIndex)
        {
            case 0:
                medInfo[0] = "Info0";
                medInfo[1] = "Info1";
                medInfo[2] = "Info2";
                break;
            case 1:
                medInfo[0] = "Info0";
                medInfo[1] = "Info1";
                medInfo[2] = "Info2";
                break;
            case 2:
                medInfo[0] = "Info0";
                medInfo[1] = "Info1";
                medInfo[2] = "Info2";
                break;
            case 3:
                medInfo[0] = "Info0";
                medInfo[1] = "Info1";
                medInfo[2] = "Info2";
                break;
            case 4:
                medInfo[0] = "Info0";
                medInfo[1] = "Info1";
                medInfo[2] = "Info2";
                break;
            case 5:
                medInfo[0] = "Info0";
                medInfo[1] = "Info1";
                medInfo[2] = "Info2";
                break;
            case 6:
                medInfo[0] = "Info0";
                medInfo[1] = "Info1";
                medInfo[2] = "Info2";
                break;
            case 7:
                medInfo[0] = "Info0";
                medInfo[1] = "Info1";
                medInfo[2] = "Info2";
                break;
            case 8:
                medInfo[0] = "Info0";
                medInfo[1] = "Info1";
                medInfo[2] = "Info2";
                break;
            case 9:
                medInfo[0] = "Info0";
                medInfo[1] = "Info1";
                medInfo[2] = "Info2";
                break;
        }

        return medInfo[index];
    }

}
