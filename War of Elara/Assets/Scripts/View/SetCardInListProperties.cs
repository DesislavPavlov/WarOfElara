using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetCardInListProperties : MonoBehaviour
{
    public Card card;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI nameText;

    public void Initialize()
    {
        print(card.Name);

        image.sprite = Base64ToImage.ConvertBase64ToSprite(card.Base64Image);
        nameText.text = card.Name;
    }
}
