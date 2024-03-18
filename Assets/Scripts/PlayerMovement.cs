using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    private bool _canTalkToNpc = false;
    public bool CanTalkToNpc
    {
        get { return _canTalkToNpc;}
        set { _canTalkToNpc = value; }
    }
    
    private float horizontal;
    private float vertical;
    private float speed = 8f;
    private bool isFacingRight = true;
    private bool isFacingUp = true;
    

    private void Update()
    {
        rb.velocity = new Vector3(horizontal * speed,  vertical*speed);
        CheckSpriteOrientation(); //reemplazar con animation tree
    }

    private void CheckSpriteOrientation()
    {
        if (!isFacingRight && horizontal > 0f)
            FlipHorizontal(ref isFacingRight);
        else if (isFacingRight && horizontal < 0f)
            FlipHorizontal(ref isFacingRight);

        if (!isFacingUp && vertical > 0f)
            FlipVertical(ref isFacingUp);
        else if (isFacingUp && vertical <0f)
            FlipVertical(ref isFacingUp);
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

    
    #region ACTIONS
    
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }
    
    public void Interactuate (InputAction.CallbackContext context)
    {
        if(context.performed && _canTalkToNpc) Debug.Log("Interactuate");
    }

    public void OpenInventory(InputAction.CallbackContext context)
    {
        if(context.performed) Debug.Log("Open inventory");
    }
    public void Cancel (InputAction.CallbackContext context)
    {
        if(context.performed) Debug.Log("Cancel");
    }
    public void Diary (InputAction.CallbackContext context)
    {
        if(context.performed) Debug.Log("Diary");
    }
    public void MP3 (InputAction.CallbackContext context)
    {
        if(context.performed) Debug.Log("MP3");
    }
    public void Options (InputAction.CallbackContext context)
    {
        if(context.performed) Debug.Log("Options");
    }
    
    #endregion
}
