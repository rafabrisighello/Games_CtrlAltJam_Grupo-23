using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Reputation : MonoBehaviour
{
    [SerializeField]
    private Sprite[] repImages;

    [SerializeField]
    private Image image;

    private void OnEnable()
    {
        image = GetComponent<Image>();
        if(GameObject.FindWithTag("GameManager"))
        {
            image.sprite = repImages[GameObject.FindWithTag("GameManager").GetComponent<GameLoop>().GetReputation()];
        }
    }
}
