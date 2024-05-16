using System;
using System.Collections;
using System.Collections.Generic;
using Prologue;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utility : MonoBehaviour
{
    private int count = 0;
    private Canvas _canvas;
    private bool prologue = false;

    public void CloseCanvas(Canvas canvas)
    {
        canvas.enabled = false;
        if (SceneManager.GetActiveScene().name == "Prologue")
        {
            _canvas = canvas;
            prologue = true;
        }
        else prologue = false;
    }

    private void FixedUpdate()
    {
        if (prologue)
        {
            if (count >= 60)
            {
                _canvas.enabled = true;
                count = 0;
            }
            else
                count++;
        }
    }

    public void NextScenePrologue()
    {
        GetComponent<DialogueManagerPrologue>().CloseDialogueBox();     
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
     public void NextScene()
    {
        GetComponent<DialogueManager>().CloseDialogueBox();     
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }



}
