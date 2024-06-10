using System;
using UnityEngine;
using Articy.Unity;
public class PlayerManager : MonoBehaviour
{
    public InputReader inputReader;
    public Rigidbody2D rb;
    public DialogueManager _dialogueManager;
    
    private ArticyObject availableDialogue;

    private MenuManager _menuManager;
    
    private float horizontal;
    private float vertical;
    private float speed = 8f;
    private bool isFacingRight = true;
    private bool isFacingUp = true;

    private void Start()
    {
        _menuManager = GetComponent<MenuManager>();
    }
    private void Update()
    {
        rb.velocity = new Vector3(horizontal * speed,  vertical*speed);
        CheckSpriteOrientation(); //reemplazar con animation tree
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
                this.GetComponent<SpriteRenderer>().enabled = false;
                _dialogueManager.StartDialogue(availableDialogue);
            }
            else if (_dialogueManager.DialogueActive == 2)
            {
                this.GetComponent<SpriteRenderer>().enabled = true;
                _dialogueManager.CloseDialogueBox();
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
       if(!_menuManager.Pause) _menuManager.OpenMenu();
       else _menuManager.CloseMenu();
    }

    private void CheckSpriteOrientation()
    {
        if (!isFacingRight && horizontal > 0f)
            FlipHorizontal(ref isFacingRight);
        else if (isFacingRight && horizontal < 0f)
            FlipHorizontal(ref isFacingRight);

        /*if (!isFacingUp && vertical > 0f)
            FlipVertical(ref isFacingUp);
        else if (isFacingUp && vertical <0f)
            FlipVertical(ref isFacingUp);*/
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
        if(articyReferenceComponent)         availableDialogue = articyReferenceComponent.reference.GetObject();
        Debug.Log(articyReferenceComponent+", "+availableDialogue);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<ArticyReference>() != null) availableDialogue = null;
    }
}
