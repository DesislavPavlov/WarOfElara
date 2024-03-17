using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonSound : MonoBehaviour
{
    public AudioSource audioSource;

    private void Awake()
    {
        this.GetComponent<Button>().onClick.AddListener(() => playSoundEffect());
    }
    public void playSoundEffect()
    {
        audioSource.Play();
    }
}
