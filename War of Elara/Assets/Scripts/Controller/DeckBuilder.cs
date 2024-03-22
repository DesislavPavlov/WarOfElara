using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuilder : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    private List<Card> basicCards;
    private Deck currentDeck;

    [SerializeField] PlayerDataModel playerDataModel;
    [SerializeField] GameObject cardInListPrefab;
    [SerializeField] GameObject cardInDeckPrefab;
    [SerializeField] GameObject viewScrollContent;
    [SerializeField] GameObject deckContainer;
    [SerializeField] AudioClip addCardSound;
    [SerializeField] AudioClip removeCardSound;
    [SerializeField] GameObject nameForm;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] DeckBuilderMessageBox messageBox;



    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.audioSource = GetComponent<AudioSource>();
        this.basicCards = new List<Card>();

        this.nameForm.SetActive(false);

        CreateCardsForDeckbuilding(GetCards.instance.GetAllBasicCards());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (GetComponent<Canvas>().isActiveAndEnabled)
                CloseDeckBuilder();
    }

    // Visualisation
    public void OpenNewDeckBuilder()
    {
        OpenDeckBuilder(new Deck());
    }
    public void OpenDeckBuilder(Deck deck)
    {
        this.animator.SetTrigger("Open");
        this.currentDeck = deck;

        this.nameText.text = deck.Name;

        DestroyPreviousCards();
        CreateCardsInDeckOnDeckBuilderOpen();
    }
    public void CloseDeckBuilder()
    {
        this.animator.SetTrigger("Close");
    }
    private void CreateCardsForDeckbuilding(List<string>[] data)
    {
        // Create Card objects
        for (int row = 0; row < data[0].Count; row++)
        {
            int id = int.Parse(data[0][row]);
            string name = data[1][row];
            string description = data[2][row];
            int imageId = int.Parse(data[3][row]);

            Card newCard = new Card(id, name, description, imageId);
            this.basicCards.Add(newCard);
        }

        // Create CardInList GameObjects
        foreach (Card card in this.basicCards)
        {
            GameObject cardInList = Instantiate(cardInListPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            cardInList.transform.SetParent(viewScrollContent.transform, false);
            SetCardInListProperties script = cardInList.GetComponent<SetCardInListProperties>();
            script.card = card;
            script.Initialize();
            cardInList.GetComponent<Button>().onClick.AddListener(() => { AddCard(card); });
        }
    }
    private void CreateCardsInDeckOnDeckBuilderOpen()
    {
        foreach (Card card in this.currentDeck.GetCards())
        {
            CreateCardInDeckObject(card);
        }
    }

    // Card Control
    public void AddCard(Card card)
    {
        if(this.currentDeck.AddCard(card))
        {
            CreateCardInDeckObject(card);
            audioSource.clip = addCardSound;
            audioSource.Play();
        }
    }
    private void CreateCardInDeckObject(Card card)
    {
        GameObject cardInDeck = Instantiate(cardInDeckPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        cardInDeck.transform.SetParent(deckContainer.transform, false);
        cardInDeck.GetComponent<Image>().sprite = CardSprites.instance.GetSprite(card.ImageId);
        cardInDeck.GetComponent<Button>().onClick.AddListener(() => { RemoveCard(card, cardInDeck); });
    }
    private void DestroyPreviousCards()
    {
        foreach (Transform child in deckContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }
    public void RemoveCard(Card card, GameObject caller)
    {
        if(this.currentDeck.RemoveCard(card))
        {
            Destroy(caller);
            audioSource.clip = removeCardSound;
            audioSource.Play();
        }
    }

    // Deck Operations
        // Rename
    public void OpenRenameDeckForm()
    {
        nameForm.SetActive(true);
    }
    public void SubmitRenameDeck()
    {
        string newName = this.nameForm.GetComponentInChildren<TMP_InputField>().text;
 
        if (playerDataModel.GetDeckNames().Contains(newName))
            return;

        this.currentDeck.SetName(newName);
        this.nameText.text = newName;
        this.nameForm.SetActive(false);
    }

        // Save
    public async void SaveDeck()
    {
        if (this.currentDeck.Cards.Count != 30)
        {
            messageBox.DisplayMessage("One deck must contain exactly 30 cards.");
            return;
        }

        List<Deck> decks = await playerDataModel.GetDecks();
        List<string> deckNames = playerDataModel.GetDeckNames();

        if (deckNames.Contains(this.currentDeck.Name))
        {
            int index = deckNames.IndexOf(this.currentDeck.Name);
            decks.RemoveAt(index);
        }

        decks.Add(this.currentDeck);
    }

        // Delete
    public void DeleteDeck()
    {
        messageBox.AskForConfirmation("Are you certain you want to delete this deck?", Delete);

        async void Delete()
        {
            List<Deck> decks = await playerDataModel.GetDecks();
            decks.Remove(this.currentDeck);
        }
    }
}
