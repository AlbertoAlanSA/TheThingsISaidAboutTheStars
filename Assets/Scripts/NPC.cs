using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class NPC : MonoBehaviour
{
    private GameObject PJ;
    private PlayerMovement playerMovement;

    private void Start()
    {
        PJ = GameObject.Find("PJ");
        playerMovement = PJ.GetComponent<PlayerMovement>();

    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(PJ.transform.position, transform.position) <= 2f)
             playerMovement.CanTalkToNpc = true;
        else playerMovement.CanTalkToNpc = false;
    }
}
