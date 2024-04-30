using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Articy.Unity;
using Articy.Unity.Interfaces;
using TMPro;

public class DialogueManager : MonoBehaviour, IArticyFlowPlayerCallbacks
{
     [Header("UI")] 
     [SerializeField] private GameObject dialogueWidget;
    [SerializeField] private TextMeshProUGUI dialogueText;
    //[SerializeField] private TextMeshProUGUI dialogueSpeaker;
    
    public bool DialogueActive { get; set; }
    private ArticyFlowPlayer _flowPlayer;

    private void Start()
    {
        _flowPlayer = GetComponent<ArticyFlowPlayer>();
    }

    public void StartDialogue(string aDialogueLine, string aSpeaker)
    {
        DialogueActive = true;
        dialogueWidget.SetActive(DialogueActive);

        dialogueText.text = aDialogueLine;
        //dialogueSpeaker.text = aSpeaker;
    }

    public void CloseDialogueBox()
    {
        DialogueActive = false;
        dialogueWidget.SetActive(DialogueActive);
    }

    public void OnFlowPlayerPaused(IFlowObject aObject)
    {
        dialogueText.text = string.Empty;
        var objectWithText = aObject as IObjectWithText;
        if (objectWithText != null) dialogueText.text = objectWithText.Text;
    }

    public void OnBranchesUpdated(IList<Branch> aBranches)
    {
        throw new NotImplementedException();
    }
}
