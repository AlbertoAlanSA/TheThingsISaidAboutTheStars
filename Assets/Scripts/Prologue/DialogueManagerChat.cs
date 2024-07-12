using System.Collections;
using System.Collections.Generic;
using Articy.Tfg;
using Articy.Tfg.GlobalVariables;
using Articy.Unity;
using Articy.Unity.Interfaces;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Prologue
{
    public class DialogueManagerChat : MonoBehaviour, IArticyFlowPlayerCallbacks
    {
        [FormerlySerializedAs("dialogueWidget")]
        [Header("UI")] 
        [SerializeField] private GameObject canvasChat;
        [SerializeField] private GameObject canvasWeb;

        [SerializeField] private RectTransform ValLayoutPanel;
        [SerializeField] private RectTransform AlexLayoutPanel;
        [SerializeField] private GameObject chat;
        [SerializeField] private GameObject chatBubblePrefab;

        private float lastButtonHeight = -0;
        private int DialogueActive { get; set; } // 0 es false, 1 es true, 2 es acabado
        private ArticyFlowPlayer flowPlayer;

        private void Start()
        {
            flowPlayer = GetComponent<ArticyFlowPlayer>();
            DialogueActive = 0;
            Cursor.visible = false;
        }

        public void StartDialogue(IArticyObject aObject)
        {
            Debug.Log("Start dialogue");
            DialogueActive = 1;
            canvasChat.SetActive(DialogueActive==1);
            flowPlayer.StartOn = aObject;
        }

        public void CloseDialogueBox()
        {
            DialogueActive = 0;
            canvasChat.SetActive(DialogueActive==1);
            flowPlayer.FinishCurrentPausedObject();
        }

        public void OnFlowPlayerPaused(IFlowObject aObject)
        {
          
            Debug.Log(" OnFlowPaused");
        
             var objectWithSpeaker = aObject as IObjectWithSpeaker;
            if (objectWithSpeaker != null)
            {
                var speakerEntity = objectWithSpeaker.Speaker as Entity;
                if (speakerEntity != null)
                {
                    var objectWithText = aObject as IObjectWithLocalizableText ;
                    if (objectWithText != null)
                    {
                        if (objectWithText.Text == "")
                            ContinueDialogue();
                        else
                        {
                           
                            Vector3 v = new Vector3(0, Screen.height, 0);
                            chat.transform.position += new Vector3(v.x, v.normalized.y*lastButtonHeight, v.z);  
                            RectTransform temp = speakerEntity.DisplayName == "Alex" ? AlexLayoutPanel : ValLayoutPanel;
                            var button = Instantiate(chatBubblePrefab, temp.transform.position, new Quaternion(0,0,0,0), chat.transform);
                            button.GetComponentInChildren<TextMeshProUGUI>().text = objectWithText.Text;
                            button.GetComponentInChildren<TextMeshProUGUI>().alignment = speakerEntity.DisplayName == "Alex" ? TextAlignmentOptions.Left : TextAlignmentOptions.Left;
                            if (ArticyGlobalVariables.Default.Test.Link)
                           {
                                button.GetComponent<Utility>().canvasChat = canvasChat;
                                button.GetComponent<Utility>().canvasWeb = canvasWeb;
                                
                                button.GetComponent<Button>().interactable = true;
                                button.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Italic;
                                button.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Underline; 
                                button.GetComponentInChildren<TextMeshProUGUI>().color = Color.blue;
                            }
                            lastButtonHeight= ((button.GetComponent<RectTransform>().rect.height)) ;
                            Debug.Log(chat.transform.position);
                           
                        }
                    }
                }
            }
            
            
        
        }

        public void OnBranchesUpdated(IList<Branch> aBranches)
        {
      
        
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
                // if( aBranches.Count >1) //tiene que estar comentado en el test del prologo, si se desciomenta porner condicion
                //{
                //int i = 0;
                // }
            }
            else
        
                DialogueActive = 2;
        
        }

        public void ContinueDialogue()
        {
            flowPlayer.Play();
        }
        
    }
}

