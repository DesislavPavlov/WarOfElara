using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    private int id;
    private string name;
    private string description;
    private string base64image;

    public int Id
    {
        get { return id; }
        set { this.id = value; }
    }

    public string Name
    {
        get { return this.name; }
        set { this.name = value; }
    }

    public string Description
    {
        get { return this.description; }
        set { this.description = value; }
    }

    public string Base64Image
    {
        get { return this.base64image; }
        set { this.base64image = value; }
    }

    public Card(int id, string name, string description, string base64image)
    {
        this.Id = id;
        this.Name = name;
        this.Description = description;
        this.Base64Image = base64image;
    }
}
