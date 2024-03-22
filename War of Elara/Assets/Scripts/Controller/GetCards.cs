using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCards : MonoBehaviour
{
    public static GetCards instance;

    private void Awake()
    {
        instance = this;
    }

    public List<string>[] GetAllBasicCards()
    {
        DatabaseConnection.instance.OpenConnection();
        List<string>[] data = DatabaseConnection.instance.Select("SELECT * FROM basicCards;", 4, "id", "name", "description", "image_id");
        DatabaseConnection.instance.CloseConnection();

        return data;
    }

    public List<string>[] GetCardImagesInBase64()
    {
        DatabaseConnection.instance.OpenConnection();
        List<string>[] data = DatabaseConnection.instance.Select("SELECT * FROM cardImagesBase64;", 2, "image_id", "image_base64");
        DatabaseConnection.instance.CloseConnection();

        return data;
    }
}
