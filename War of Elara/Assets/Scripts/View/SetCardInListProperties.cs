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
        print(this.card.Name);

        image.sprite = CardSprites.instance.GetSprite(this.card.ImageId);
        nameText.text = card.Name;
    }
}
