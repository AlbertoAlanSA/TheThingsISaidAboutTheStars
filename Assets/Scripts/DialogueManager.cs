using System;
using System.Collections;
using System.Collections.Generic;
using Articy.Tfg;
using Articy.Tfg.GlobalVariables;
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
     [SerializeField] private GameObject canvasRec;
     [SerializeField] private TextMeshProUGUI dialogueText;
     [SerializeField] private TextMeshProUGUI dialogueSpeaker;
     [SerializeField] private RectTransform branchLayoutPanel;
     [SerializeField] private GameObject branchPrefab;
     [SerializeField] private Camera dialogueCamera;
     [SerializeField] private GameObject roll;
     [SerializeField] private GameObject pasiveRoll;
     
    public int DialogueActive { get; set; } // 0 es false, 1 es true, 2 es acabado
    private ArticyFlowPlayer flowPlayer;
    
    private void Start()
    {
        dialogueCamera.enabled = false;
        flowPlayer = GetComponent<ArticyFlowPlayer>();
        DialogueActive = 0;
    }

    public void StartDialogue(IArticyObject aObject)
    {
        Debug.Log("Start dialogue");
        dialogueCamera.enabled = true;
        DialogueActive = 1;
        dialogueWidget.SetActive(DialogueActive==1);
        canvasRec.SetActive(DialogueActive==1);
        flowPlayer.StartOn = aObject;
    }

    public void CloseDialogueBox()
    {
        DialogueActive = 0;
        dialogueWidget.SetActive(DialogueActive==1);
        canvasRec.SetActive(DialogueActive==1);
        dialogueCamera.enabled = false;
        flowPlayer.FinishCurrentPausedObject();
    }

    public void OnFlowPlayerPaused(IFlowObject aObject)
    {
        Debug.Log(" OnFlowPaused");
        
            dialogueText.text = string.Empty;
            dialogueSpeaker.text = string.Empty;
        switch (ArticyGlobalVariables.Default.Dados.Roll)
        {
            case 1:
                roll.SetActive(true);
                StartCoroutine(Roll());
                break;
            case 2:
                pasiveRoll.SetActive(true);
                StartCoroutine(RollPasive());
                break;
            case 0:
                roll.SetActive(false);
                pasiveRoll.SetActive(false);
                break;
        }

        string temp = "";

        var objectWithText = aObject as IObjectWithLocalizableText;
        if (objectWithText != null)
        {
            if(objectWithText.Text == "") flowPlayer.Play();
            else
            {
                var objectWithSpeaker = aObject as IObjectWithSpeaker;
                if (objectWithSpeaker != null)
                { 
                    var speakerEntity = objectWithSpeaker.Speaker as Entity;
                    if (speakerEntity != null)
                        temp = "<i>" + speakerEntity.DisplayName + "</i >" +
                               ": "; //dialogueSpeaker.text = speakerEntity.DisplayName; 
                }
                dialogueText.text = temp + objectWithText.Text;
            }
                 
        }
    }

    public void OnBranchesUpdated(IList<Branch> aBranches)
    {
        Debug.Log("branch");
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
                    button.transform.position += new Vector3(i * 30, 0, 0);
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

    
   IEnumerator Roll()
    {
        Transform modificador = roll.transform.GetChild(1);
        Transform dado = roll.transform.GetChild(3);
        Transform dadoImagen = roll.transform.GetChild(2);
        Transform resultado = roll.transform.GetChild(4);
        
        modificador.gameObject.SetActive(true);
        modificador.GetComponent<TextMeshProUGUI>().text = ArticyGlobalVariables.Default.Dados.Tipo +": + "+ ArticyGlobalVariables.Default.Dados.Modificador; //dado modificador
        
        Debug.LogError("roll");
        dado.GetComponent<TextMeshProUGUI>().text = ArticyGlobalVariables.Default.Dados.Dado.ToString(); //dado numero
        
        resultado.GetComponent<TextMeshProUGUI>().text =
            ArticyGlobalVariables.Default.Dados.Resultado > ArticyGlobalVariables.Default.Dados.Superar
                ? "Acierto"
                : "Fallo";
        //animacion aparecer
         //animacion entrar
         roll.GetComponent<Animator>().Play("Base Layer.RollPanelIn");
         float animationLength = roll.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
         yield return new WaitForSecondsRealtime(animationLength);
         
         dadoImagen.gameObject.GetComponent<Animator>().Play("Base Layer.DadoImagen");
         animationLength = dadoImagen.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
         yield return new WaitForSecondsRealtime(animationLength);

         dado.gameObject.GetComponent<Animator>().Play("Base Layer.DiceNumberAppears");
         resultado.gameObject.GetComponent<Animator>().Play("Base Layer.DiceResultadoAppears");
         animationLength = resultado.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;

         yield return new WaitForSecondsRealtime(animationLength + 1);
         
        //animacion salir
        roll.GetComponent<Animator>().Play("Base Layer.RollPanelOut");

        //dado.gameObject.SetActive(false);
        //resultado.gameObject.SetActive(false);
    }

    IEnumerator RollPasive()
    {
        //animacion entrar
        pasiveRoll.GetComponent<Animator>().Play("Base Layer.PasiveRollPanelIn");
     

        string temp  ="Tirada de " + ArticyGlobalVariables.Default.Dados.Tipo.ToLower() + ": ";
        temp +=    ArticyGlobalVariables.Default.Dados.Resultado > ArticyGlobalVariables.Default.Dados.Superar
                ? "Acierto"
                : "Fallo";
        pasiveRoll.GetComponentInChildren<TextMeshProUGUI>().text = temp;
   yield return new WaitForSeconds(3);
        //animacion entrar
        pasiveRoll.GetComponent<Animator>().Play("Base Layer.PasiveRollPanelOut",0);

    }
}
