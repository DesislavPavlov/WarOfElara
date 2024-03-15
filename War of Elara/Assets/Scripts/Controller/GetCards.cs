using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCards : MonoBehaviour
{
    public List<string>[] GetAllBasicCards()
    {
        DatabaseConnection.instance.OpenConnection();
        List<string>[] data = DatabaseConnection.instance.Select("SELECT * FROM basicCards;");
        DatabaseConnection.instance.CloseConnection();

        return data;
    }
}
