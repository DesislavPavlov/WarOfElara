using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using TMPro;

public class LoginAndRegisterModel : MonoBehaviour
{
    public TMP_InputField loginNameInput;
    public TMP_InputField loginPasswordInput;
    public TMP_InputField registerNameInput;
    public TMP_InputField registerPasswordInput;
    public RegisterView registerView;
    public LoginView loginView;

    private string username;
    private string password;
    private Dictionary<string, string> errorMessages = new Dictionary<string, string>()
    {
        {"wrongNameLength",  "Username must be between 3 and 20 characters." },
        {"wrongPasswordLength", "Password must be between 8 and 30 characteers." },
        {"wrongNameChars",  "Username can only contain the symbols '.', '-', '_', '@'." },
        {"wrongPasswordChars", "Password must have at least one lowercase and one uppercase letter, one symbol and one number" },
        {"userExists", "A user with the same name already exists." },
        {"invalidCredentials", "Invalid username or password." }
    };

    public string Username 
    { 
        get { return username; }
        set 
        {
            if (value.Length < 3 || value.Length > 20)
            {
                this.username = null;
                return;
            }

            this.username = value;
        }
    }

    public string Password
    {
        get { return password; }
        set
        {
            if (value.Length < 8 || value.Length > 30)
            {
                this.password = null;
                return;
            }

            this.password = value;
        }
    }

    private async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
        }
        catch (Exception ex)
        {
            Debug.Log("Services initialization error" + ex);
        }
    }


    public bool SetValues(bool isRegistering)
    {
        if (isRegistering)
        {
            this.Username = registerNameInput.text;
            this.Password = registerPasswordInput.text;

            if (this.Username == null)
            {
                registerView.DisplayMessage(this.errorMessages["wrongNameLength"]);
                return false;
            }
            else if (this.Password == null)
            {
                registerView.DisplayMessage(this.errorMessages["wrongPasswordLength"]);
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            this.Username = loginNameInput.text;
            this.Password = loginPasswordInput.text;
            return true;
        }

        
    }

    public async void Register()
    {
        if (SetValues(true))
            await SignUpWithUsernamePassword(this.Username, this.Password);
    }

    public async void Login()
    {
        if (SetValues(false))
            await SignInWithUsernamePasswordAsync(this.Username, this.Password);
    }

    private async Task SignUpWithUsernamePassword(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password); 
            GameManager.instance.UpdateGameState(GameManager.GameState.MainMenu);
            Debug.Log("SignUp is successful.");
        }
        catch (Unity.Services.Authentication.AuthenticationException ex)
        {
            registerView.DisplayMessage(this.errorMessages["userExists"]);
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            registerView.DisplayMessages(this.errorMessages["wrongNameChars"], this.errorMessages["wrongPasswordChars"]);
            Debug.LogException(ex);
        }
    }

    private async Task SignInWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            GameManager.instance.UpdateGameState(GameManager.GameState.MainMenu);
            Debug.Log("SignIn is successful.");
        }
        catch (Unity.Services.Authentication.AuthenticationException ex)
        {
            loginView.DisplayMessage(this.errorMessages["invalidCredentials"]);
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            loginView.DisplayMessage(this.errorMessages["invalidCredentials"]);
            Debug.LogException(ex);
        }
    }

}
