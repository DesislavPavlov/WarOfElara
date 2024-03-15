using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDecks : MonoBehaviour
{
    private List<Deck> decks;
    [SerializeField] private PlayerDataModel playerDataModel;
    [SerializeField] private DeckBuilder deckBuilder;
    [SerializeField] GameObject deckButtonPrefab;
    [SerializeField] GameObject newDeckButtonPrefab;



    public async void OnDeckButtonPressed()
    {
        DestroyChildren();
        this.decks = await playerDataModel.GetDecks();
        DisplayDeckButtons();
    }

    private void DisplayDeckButtons()
    { 
        int count = 0;
        foreach (var deck in this.decks)
        {
            GameObject button = Instantiate(deckButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            button.transform.SetParent(this.transform, false);
            Button buttonComponent = button.GetComponent<Button>();
            buttonComponent.onClick.AddListener(() => deckBuilder.OpenDeckBuilder(deck));

            print("deck number " + ++count);
        }

        if (this.decks.Count < 5)
        {
            GameObject button = Instantiate(newDeckButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            button.transform.SetParent(this.transform, false);
            Button buttonComponent = button.GetComponent<Button>();
            buttonComponent.onClick.AddListener(() => deckBuilder.OpenNewDeckBuilder());

            print("new deck button created");
        }
    }

    private void DestroyChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
