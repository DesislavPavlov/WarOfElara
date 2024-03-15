using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    private int id;
    private string name;
    private string description;
    private Sprite image;

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

    public Sprite Image
    {
        get { return this.image; }
        set { this.image = value; }
    }

    public Card(int id, string name, string description, Sprite image)
    {
        this.Id = id;
        this.Name = name;
        this.Description = description;
        this.Image = image;
    }
}
