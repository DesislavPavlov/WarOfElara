using System;
using System.Collections.Generic;
using UnityEngine;

public class CardSprites : MonoBehaviour
{
    public static CardSprites instance;
    private List<int> ids = new List<int>();
    private List<Sprite> sprites = new List<Sprite>();

    private void Start()
    {
        instance = this;
        List<string>[] data = GetCards.instance.GetCardImagesInBase64();

        for (int row = 0; row < data[0].Count; row++)
        {
            this.ids.Add(int.Parse(data[0][row]));
            this.sprites.Add(ConvertBase64ToSprite(data[1][row]));
        }
    }

    public Sprite ConvertBase64ToSprite(string base64)
    {
        byte[] imageBytes = Convert.FromBase64String(base64);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(imageBytes);
        Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

        return sprite;
    }

    public Sprite GetSprite(int id)
    {
        int index = this.ids.IndexOf(id);
        return this.sprites[index];
    }
}
