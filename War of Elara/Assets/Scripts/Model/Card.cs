using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    private int id;
    private string name;
    private string description;
    private int imageId;

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

    public int ImageId
    {
        get { return this.imageId; }
        set { this.imageId = value; }
    }

    public Card(int id, string name, string description, int imageId)
    {
        this.Id = id;
        this.Name = name;
        this.Description = description;
        this.ImageId = imageId;
    }

}
