using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBox : MonoBehaviour
{
    [SerializeField]
    private string currentText;

    private TextMeshProUGUI textDisplay;

    private void OnEnable()
    {
        textDisplay = GetComponentInChildren<TextMeshProUGUI>();
    }


    private void Update()
    {
        textDisplay.text = currentText;
    }

    public void SetCurrentText(string text)
    {
        currentText = text;
    }
}
