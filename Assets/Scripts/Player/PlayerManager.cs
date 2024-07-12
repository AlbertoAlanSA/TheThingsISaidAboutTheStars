using System;
using UnityEngine;
using Articy.Unity;
using Unity.VisualScripting;

public class PlayerManager : MonoBehaviour
{
    public InputReader inputReader;
    public Rigidbody2D rb;
    public DialogueManager _dialogueManager;
    
    private ArticyObject availableDialogue;
    private GameObject npcDialogue;


    private MenuManager _menuManager;
    
    private float horizontal;
    private float vertical;
    private float speed = 5f;
    private bool isFacingRight = true;
    private bool talking;

    private void Start()
    {
        _menuManager = GetComponent<MenuManager>();
        talking = false;
    }
    private void Update()
    {
        if (!talking)
            CheckSpriteOrientation(); 
    }

    private void FixedUpdate()
    {
        if (!talking)
        {
            rb.velocity = new Vector2(horizontal * speed, vertical * speed);
            rb.angularVelocity = -rb.angularVelocity;
        }
    }
  private void OnEnable()
    {
        inputReader.EnableGameplayInput();
        inputReader.MoveEvent += OnMove;
        inputReader.InteractuateEvent += OnInteractuate;
        inputReader.OpenInventoryEvent += OnOpenInventory;
        inputReader.CancelEvent += OnCancel;
        inputReader.DiaryEvent += OnDiary;
        inputReader.MP3Event += OnMP3;
        inputReader.OptionsEvent += OnOptions;
    }
    private void OnDisable()
    {
        inputReader.MoveEvent -= OnMove;
        inputReader.InteractuateEvent -= OnInteractuate;
        inputReader.OpenInventoryEvent -= OnOpenInventory;
        inputReader.CancelEvent -= OnCancel;
        inputReader.DiaryEvent -= OnDiary;
        inputReader.MP3Event -= OnMP3;
        inputReader.OptionsEvent -= OnOptions;
    }

   
    public void OnMove(Vector2 movementVector)
    {
        horizontal = movementVector.x;
        vertical = movementVector.y;
    }
    
    public void OnInteractuate ()
    {
        if (availableDialogue )
        {
            Debug.Log("Interactuate");

            if (_dialogueManager.DialogueActive==1) _dialogueManager.ContinueDialogue();
            else if (_dialogueManager.DialogueActive == 0)
            {
                talking = true;
                this.GetComponent<SpriteRenderer>().enabled = false;
                _dialogueManager.StartDialogue(availableDialogue, npcDialogue);
            }
            else if (_dialogueManager.DialogueActive == 2)
            {
                this.GetComponent<SpriteRenderer>().enabled = true;
                _dialogueManager.CloseDialogueBox();
                talking = false;
            }
        }
    }

    public void OnOpenInventory()
    {
        Debug.Log("Open inventory");
    }
    public void OnCancel ()
    {
        Debug.Log("Cancel");
        Application.Quit();
    }
    public void OnDiary ()
    {
        Debug.Log("Diary");
    }
    public void OnMP3 ()
    {
        Debug.Log("MP3");
    }
    public void OnOptions ()
    {
       Debug.Log("Options: "+(_menuManager.Pause));
       if (!_menuManager.Pause)
       {
           talking = true;
           _menuManager.OpenMenu();
       }
       else
       {
           talking = false;
           _menuManager.CloseMenu();
       }
    }

    private void CheckSpriteOrientation()
    {
        if (!isFacingRight && horizontal > 0f)
            FlipHorizontal(ref isFacingRight);
        else if (isFacingRight && horizontal < 0f)
            FlipHorizontal(ref isFacingRight);
    }
    private void FlipHorizontal(ref bool isFacingRight)
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale; 
    }
    private void FlipVertical(ref bool isFacingUp)
    {
        isFacingUp = !isFacingUp;
        Vector3 localScale = transform.localScale;
        localScale.y *= -1f;
        transform.localScale = localScale; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Debug.Log("trigger npc");
        var articyReferenceComponent = other.GetComponent<ArticyReference>();
        if (articyReferenceComponent)
        {
            npcDialogue = other.gameObject;
            availableDialogue = articyReferenceComponent.reference.GetObject();
        }
        Debug.Log(articyReferenceComponent+", "+availableDialogue);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<ArticyReference>() != null) availableDialogue = null;
    }
}
