using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    private List<Card> cards;
    private string name;

    public string Name
    {
        get { return name; }
        private set { this.name = value; }
    }

    public Deck()
    {
        cards = new List<Card>();
        this.Name = "New Deck";
    }

    public void SetName(string name)
    {
        this.Name = name;
    }

    public bool AddCard(Card card)
    {
        if (this.cards.Count < 30)
        {
            this.cards.Add(card);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool RemoveCard(Card card)
    {
        return this.cards.Remove(card);
    }
}
