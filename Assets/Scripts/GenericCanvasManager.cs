using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GenericCanvasManager : MonoBehaviour
{
    [SerializeField] private List<string> optionList;
    [SerializeField] private RectTransform buttonLayoutPanel;
    [SerializeField] private GameObject buttonPrefab;


    
    public void CreateButtons()
    {
        
        int i = 0;
        foreach (var obj in optionList)
        {
            Debug.Log(optionList[i]);
            GameObject button = Instantiate(buttonPrefab, buttonLayoutPanel);
            button.transform.position += new Vector3(0, i * 30, 0);
            button.GetComponentInChildren<TextMeshProUGUI>().text = obj;
            i++;
        }
    }

    public void DeleteButtons()
    {
        foreach(Transform child in buttonLayoutPanel) Destroy(child.gameObject);
    }
    
}
    

