using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Base64ToImage
{
    public static Sprite ConvertBase64ToSprite(string base64)
    {
        byte[] imageBytes = Convert.FromBase64String(base64);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(imageBytes);
        Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

        return sprite;
    }
}
