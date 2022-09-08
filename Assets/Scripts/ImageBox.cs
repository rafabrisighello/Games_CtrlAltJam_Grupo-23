using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageBox : MonoBehaviour
{
    private Image imageBox;

    private void OnEnable()
    {
        imageBox = GetComponent<Image>();
    }

    public void SetSprite(Sprite sprite)
    {
        imageBox.sprite = sprite;
    }
}
