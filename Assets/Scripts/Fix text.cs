using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Fixtext : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI _textMeshProUGUI;

    private void Start()
    {
        Debug.Log(_textMeshProUGUI.text);
        GetComponent<TextMeshProUGUI>().text = _textMeshProUGUI.text;
    }
}
