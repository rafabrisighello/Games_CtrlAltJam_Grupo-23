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

    public event Action OnMedChosenAction;

    public void ChangeMedInfo(int medChoice)
    {
        GetComponent<Image>().sprite = medicineSprites[medChoice + 1];
        SetMed(medChoice);
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
}
