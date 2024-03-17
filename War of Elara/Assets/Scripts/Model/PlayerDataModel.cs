using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;
using UnityEngine;
using Unity.Services.CloudSave.Models.Data.Player;
using Unity.Services.Authentication;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class PlayerDataModel : MonoBehaviour
{
    private List<Deck> decks;

    //public List<Deck> Decks
    //{
    //    get
    //    {
    //        return decks;
    //    }
    //    private set
    //    {
    //        this.decks = value;
    //    }
    //}

    async void Awake()
    {
        try
        {
            await UnityServices.InitializeAsync();
        }
        catch (Exception ex)
        {
            Debug.Log("Services initialization error" + ex);
        }

        if (!AuthenticationService.Instance.IsSignedIn)
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync("admin", "Admin_420");
    }

    public async Task<List<Deck>> GetDecks()
    {
        var playerDecks = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "decks" });

        if (playerDecks.TryGetValue("decks", out var decks))
        {
            List<Deck> listOfDecks = decks.Value.GetAs<List<Deck>>();
            print("has decks");
            return listOfDecks;
        }
        else
        {
            var emptyDecks = new Dictionary<string, object>
            {
                {"decks", new List<Deck>() }
            };

            await CloudSaveService.Instance.Data.Player.SaveAsync(emptyDecks);

            print("no decks");
            return (List<Deck>)emptyDecks["decks"];
        }
    }

    public async void SaveNewDeck(Deck deck)
    {
        this.decks.Add(deck);

        var playerDecks = new Dictionary<string, object>{
          {"decks", this.decks}
        };
        await CloudSaveService.Instance.Data.Player.SaveAsync(playerDecks);
        Debug.Log($"Saved data {string.Join(',', playerDecks)}");
    }

    public async void SaveExistingDeck(Deck deck)
    {

    }
}
