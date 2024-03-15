using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using MySql.Data.MySqlClient;
using MySql.Data;


public class DatabaseConnection : MonoBehaviour
{
    public static DatabaseConnection instance;
    private MySqlConnection connection;
    private string server;
    private string database;
    private string uid;
    private string password;

    private void Awake()
    {
        instance = this;
        Initialize();
    }

    public void Initialize()
    {
        server = "sql11.freesqldatabase.com";
        database = "sql11685898";
        uid = "sql11685898";
        password = "PbKvx4YTB1";
        string connectionString;
        connectionString = "SERVER=" + server + ";" + "DATABASE=" +
        database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

        connection = new MySqlConnection(connectionString);
    }

    public bool OpenConnection()
    {
        try
        {
            connection.Open();
            return true;
        }
        catch (MySqlException ex)
        {
            switch (ex.Number)
            {
                case 0:
                    Debug.Log("Cannot connect to server.  Contact administrator");
                    break;

                case 1045:
                    Debug.Log("Invalid username/password, please try again");
                    break;
            }
            return false;
        }
    }

    public bool CloseConnection()
    {
        try
        {
            connection.Close();
            return true;
        }
        catch (MySqlException ex)
        {
            Debug.Log(ex.Message);
            return false;
        }
    }

    public List<string>[] Select(string query)
    {
        List<string>[] list = new List<string>[4];
        list[0] = new List<string>();
        list[1] = new List<string>();
        list[2] = new List<string>();
        list[3] = new List<string>();

        try
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                list[0].Add(dataReader["id"] + "");
                list[1].Add(dataReader["name"] + "");
                list[2].Add(dataReader["description"] + "");
                list[3].Add(dataReader["image"] + "");
            }

            dataReader.Close();
            return list;
        }
        catch (System.Exception ex)
        {
            Debug.Log("Exception caught: " + ex);
            return list;
        }
    }

}
