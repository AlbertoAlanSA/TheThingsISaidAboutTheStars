using System;
using System.Collections;
using System.Collections.Generic;
using Articy.Tfg;
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
     [SerializeField] private TextMeshProUGUI dialogueSpeaker;
     [SerializeField] private RectTransform branchLayoutPanel;
     [SerializeField] private GameObject branchPrefab;
    public int DialogueActive { get; set; } // 0 es false, 1 es true, 2 es acabado
    private ArticyFlowPlayer flowPlayer;

    private void Start()
    {
        flowPlayer = GetComponent<ArticyFlowPlayer>();
        DialogueActive = 0;
    }

    public void StartDialogue(IArticyObject aObject)
    {
        Debug.Log("Start dialogue");
        DialogueActive = 1;
        dialogueWidget.SetActive(DialogueActive==1);
        flowPlayer.StartOn = aObject;
    }

    public void CloseDialogueBox()
    {
        DialogueActive = 0;
        dialogueWidget.SetActive(DialogueActive==1);
        flowPlayer.FinishCurrentPausedObject();
    }

    public void OnFlowPlayerPaused(IFlowObject aObject)
    {
        Debug.Log(" OnFlowPaused");
        dialogueText.text = string.Empty;
        dialogueSpeaker.text = string.Empty; 
        
        var objectWithText = aObject as IObjectWithLocalizableText ;
        if (objectWithText != null) dialogueText.text = objectWithText.Text;

        var objectWithSpeaker = aObject as IObjectWithSpeaker;
        if (objectWithSpeaker != null)
        {
            var speakerEntity = objectWithSpeaker.Speaker as Entity;
            if (speakerEntity != null) dialogueSpeaker.text = speakerEntity.DisplayName;
        }
    }

    public void OnBranchesUpdated(IList<Branch> aBranches)
    {
        ClearAllBranches();
        
        bool dialogueIsFinished = true;
        foreach (var branch in aBranches)
        {
            if(branch.Target is IDialogueFragment)
            {
                dialogueIsFinished = false;
                break;
            }
        }

        if (!dialogueIsFinished )
        {
            if( aBranches.Count >1)
            {
                int i = 0;
                foreach (var branch in aBranches)
                {
                    GameObject button = Instantiate(branchPrefab, branchLayoutPanel);
                    button.transform.position += new Vector3(0, i * 30, 0);
                    button.GetComponent<BranchChoices>().AssingBranch(flowPlayer, branch);
                    i++;
                }
            }
        }else DialogueActive = 2;
    }

    public void ContinueDialogue()
    {
        flowPlayer.Play();
    }

    void ClearAllBranches()
    {
        foreach(Transform child in branchLayoutPanel) Destroy(child.gameObject);
    }
}
