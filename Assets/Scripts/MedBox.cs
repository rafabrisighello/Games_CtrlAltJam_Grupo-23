using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class MedBox : MonoBehaviour
{
    [SerializeField]
    private GameObject[] buttonObjectRefs;

    [SerializeField]
    private Sprite[] spriteOptions;

    [SerializeField]
    private Client client;

    [SerializeField]
    private GameLoop gameloop;

    [SerializeField]
    private bool enable;

    private void OnEnable()
    {
        client.OnClientLoaded += EnableChoice;
        gameloop.OnMedAppliedAction += DisableChoice;
        DisableChoice();
    }

    public void Highlight(int buttonIndex)
    {
        for (int i = 0; i < buttonObjectRefs.Length; i++)
        {
            if (i == buttonIndex)
            {
                buttonObjectRefs[buttonIndex].GetComponent<Image>().sprite = spriteOptions[1];
            }
            else
            {
                buttonObjectRefs[i].GetComponent<Image>().sprite = spriteOptions[0];
            }
        }
    }

    private void EnableChoice()
    {
        foreach (GameObject button in buttonObjectRefs)
        {
            button.GetComponent<Button>().interactable = true;
            button.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
        }
        buttonObjectRefs[0].GetComponent<Image>().sprite = spriteOptions[1];
    }

    private void DisableChoice()
    {
        foreach (GameObject button in buttonObjectRefs)
        {
            button.GetComponent<Button>().interactable = false;
            button.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            button.GetComponent<Image>().sprite = spriteOptions[0];
        }
    }

}
