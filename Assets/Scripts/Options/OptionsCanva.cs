using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OptionsCanva : MonoBehaviour
{

    public InputReader inputReader;
    
    private void OnEnable()
    {
        inputReader.EnableMenuInput();
    }

    private void OnDisable()
    {
       inputReader.EnableGameplayInput();
    }
}
