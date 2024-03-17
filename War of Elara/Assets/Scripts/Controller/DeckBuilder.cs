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

    [SerializeField] GetCards getCards;
    [SerializeField] PlayerDataModel playerDataModel;
    [SerializeField] GameObject cardInListPrefab;
    [SerializeField] GameObject cardInDeckPrefab;
    [SerializeField] GameObject viewScrollContent;
    [SerializeField] GameObject deckContainer;
    [SerializeField] AudioClip addCardSound;
    [SerializeField] AudioClip removeCardSound;
    [SerializeField] GameObject nameForm;
    [SerializeField] TextMeshProUGUI nameText;



    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.audioSource = GetComponent<AudioSource>();
        this.basicCards = new List<Card>();

        this.nameForm.SetActive(false);
    }

    public void OpenNewDeckBuilder()
    {
        OpenDeckBuilder(new Deck());
    }

    public void OpenDeckBuilder(Deck deck)
    {
        this.animator.SetTrigger("Open");
        CreateObjectsForEachCard(getCards.GetAllBasicCards());
        this.currentDeck = deck;
    }

    private void CreateObjectsForEachCard(List<string>[] data)
    {
        // Create Card objects
        for (int row = 0; row < data[0].Count; row++)
        {

            int id = int.Parse(data[0][row]);
            string name = data[1][row];
            string description = data[2][row];
            Sprite image = ConvertBase64ToSprite(data[3][row]);

            Card newCard = new Card(id, name, description, image);
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

    private Sprite ConvertBase64ToSprite(string base64)
    {
        byte[] imageBytes = Convert.FromBase64String(base64);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(imageBytes);
        Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

        return sprite;
    }

    public void AddCard(Card card)
    {
        if(this.currentDeck.AddCard(card))
        {
            GameObject cardInDeck = Instantiate(cardInDeckPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            cardInDeck.transform.SetParent(deckContainer.transform, false);
            cardInDeck.GetComponent<Image>().sprite = card.Image;
            cardInDeck.GetComponent<Button>().onClick.AddListener(() => { RemoveCard(card, cardInDeck); });
            audioSource.clip = addCardSound;
            audioSource.Play();
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

    public void OpenRenameDeckForm()
    {
        nameForm.SetActive(true);
    }

    public void SubmitRenameDeck()
    {
        string newName = this.nameForm.GetComponentInChildren<TMP_InputField>().text;
        this.currentDeck.SetName(newName);
        this.nameText.text = newName;
        this.nameForm.SetActive(false);
    }
        
    public void SaveDeck()
    {
        
    }
}
