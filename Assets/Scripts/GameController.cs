using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static int CurGameLevel;
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private GameObject Level;

    public AudioSource soundSource; // Ses kaynaðý
    private bool isSoundPlaying = true;

    void Start()
    {

        CurGameLevel = LevelCreatorScript.CurGameLevel;
        LevelCreatorScript.instance.CreateTutorialLevel();

        soundSource = GetComponent<AudioSource>(); 
        if (soundSource != null)
        {
            soundSource.Play(); 
        }
    }

    public void AgainGame()
    {
        Level.gameObject.SetActive(true);
        LevelCreatorScript.instance.RemoveOldLevel();
    }

    public void CreateMenu()
    {
        menuCanvas.gameObject.SetActive(true);
        Level.gameObject.SetActive(false); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleSound()
    {
        if (soundSource != null)
        {
            if (isSoundPlaying)
            {
                soundSource.Stop();
                isSoundPlaying = false;
                Debug.LogError("Ses kapatýldý!");
            }
            else
            {
                soundSource.Play();
                isSoundPlaying = true;
                Debug.LogError("Ses açýldý!");
            }
        }
        else
        {
            Debug.LogError("Ses kaynaðý bulunamýyor!");
        }
    }
}