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

        this.decks = await LoadDecks();
    }

    private async void OnDestroy()
    {
        await SaveDecks();
    }


    public async Task<List<Deck>> GetDecks()
    {
        if (this.decks == null)
            await LoadDecks();

        return this.decks;
    }

    public List<string> GetDeckNames()
    {
        List<string> names = new List<string>();
        foreach (Deck deck in this.decks)
        {
            names.Add(deck.Name);
        }

        return names;
    }

    public async Task<List<Deck>> LoadDecks()
    {
        var playerDecks = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "decks" });

        if (playerDecks.TryGetValue("decks", out var decks))
        {
            string json = decks.Value.GetAsString();
            List<Deck> listOfDecks = JsonConvert.DeserializeObject<List<Deck>>(json);
            return listOfDecks;
        }
        else
        {
            return new List<Deck>();
        }
    }

    public async Task SaveDecks()
    {
        string dataStringified = JsonConvert.SerializeObject(this.decks, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });

        var playerDecks = new Dictionary<string, object>{
          {"decks", dataStringified}
        };

        await CloudSaveService.Instance.Data.Player.SaveAsync(playerDecks);
    }
}
