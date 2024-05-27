using System;
using System.Collections;
using System.Collections.Generic;
using Prologue;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Utility : MonoBehaviour
{
    private int count = 0;
    private Canvas _canvas;
    public GameObject canvasChat { get; set; }
    public GameObject canvasWeb { get; set; }
    [SerializeField]private DialogueManagerPrologue DialogueManagerPrologue;

    private bool prologue = false;
    private Animator _animator;


    public void CloseCanvas(Canvas canvas)
    {
        canvas.enabled = false;
        if (SceneManager.GetActiveScene().name == "Prologue")
        {
            if(_canvas==null) _canvas = canvas;
            _animator = _canvas.GetComponentInChildren<Animator>();
            
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
                _animator.Play("OpenChat");
                count = 0;
            }
            else
                count++;
        }
    }

    public void NextScenePrologue()
    {
        DialogueManagerPrologue.FinishCurrentPausedObject();    
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        
    }
     public void NextScene()
    {
        GetComponent<DialogueManager>().CloseDialogueBox();     
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

     

    public void CloseChatOpenWeb()
    {
        prologue = false;
        canvasChat.SetActive(false);
        canvasWeb.SetActive(true);
       // canvasWeb.GetComponentInChildren<Animator>().Play("OpenChat");

    }
}
