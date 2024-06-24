using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class PlayableDirector : MonoBehaviour
{ 
 
    public void CursorOff()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Debug.LogError("cursor nope");
    }
 
   public void CursorOn()
   {
       Cursor.lockState = CursorLockMode.None;
       Cursor.visible = true;
                Debug.LogError("cursor sipie");

    }
 
}
