using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void SelectButton()
    {
        string name = this.GetComponentInChildren<TextMeshProUGUI>().text;
        switch (name)
        {
            case "Settings":
            case "Ajustes":
                SettingsButton();
                break;
            case "Audio":
                AudioButton();
                break;
            case "Video":
            case "Vídeo":
                VideoButton();
                break;
            case "Main menu":
            case "Menú principal":
                MainMenuButton();
                break;
            case "Play":
            case "Jugar":
                PlayButton();
                break;
            case "Continue":
            case "Continuar":
                ContinueButton();
                break;
            case "Exit":
            case "Salir":
                ExitButton();
                break;
        }
    }

    void SettingsButton()
    {
        Debug.Log("setting");
    }

    void AudioButton()
    {
        Debug.Log("audio");
    }

    void VideoButton()
    {
        Debug.Log("video");
    }

    void MainMenuButton()
    {
        Debug.Log("mainmenu");
        SceneManager.LoadScene("Main menu");
    }

    void PlayButton()
    {
        Debug.Log("play");
        SceneManager.LoadScene("Prologue");
    }

    void ContinueButton()
    {
        Debug.Log("continue");
    }

    void ExitButton()
    {
        Debug.Log("exit");
        Application.Quit();
    }
}
