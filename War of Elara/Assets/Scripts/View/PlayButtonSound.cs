using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSound : MonoBehaviour
{
    public AudioSource audioSource;

    public void playSoundEffect()
    {
        audioSource.Play();
    }
}
