using PlasticPipe.PlasticProtocol.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuilderMessageBox : MonoBehaviour
{
    private Action function;
    private Animator animator;
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;
    void Start()
    {
        animator = GetComponent<Animator>();
        EnableButtons(0);
    }

    public void DisplayMessage(string message)
    {
        messageText.text = message;
        this.function = null;
        EnableButtons(1);
        OpenAnimator();
    }

    public void AskForConfirmation(string message, Action function)
    {
        messageText.text = message;
        this.function = function;
        EnableButtons(2);
        OpenAnimator();
    }

    public void Confirmed()
    {
        if(this.function != null)
            this.function();
        CloseAnimator();
    }

    public void Denied()
    {
        CloseAnimator();
    }

    private void EnableButtons(int amount)
    {
        switch (amount)
        {
            case 0:
                yesButton.gameObject.SetActive(false);
                noButton.gameObject.SetActive(false);
                break;
            case 1:
                yesButton.gameObject.SetActive(true);
                noButton.gameObject.SetActive(false);
                break;
            case 2: 
                yesButton.gameObject.SetActive(true);
                noButton.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void OpenAnimator()
    {
        this.animator.SetTrigger("Open");
    }

    private void CloseAnimator()
    {
        this.animator.SetTrigger("Close");
    }
}
