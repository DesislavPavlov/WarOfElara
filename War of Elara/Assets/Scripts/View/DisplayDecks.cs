using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        foreach (var deck in this.decks)
        {
            GameObject button = Instantiate(deckButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            button.GetComponentInChildren<TextMeshProUGUI>().text = deck.Name;
            button.transform.SetParent(this.transform, false);
            Button buttonComponent = button.GetComponent<Button>();
            buttonComponent.onClick.AddListener(() => deckBuilder.OpenDeckBuilder(deck));
        }

        if (this.decks.Count < 5)
        {
            GameObject button = Instantiate(newDeckButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            button.transform.SetParent(this.transform, false);
            Button buttonComponent = button.GetComponent<Button>();
            buttonComponent.onClick.AddListener(() => deckBuilder.OpenNewDeckBuilder());
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
