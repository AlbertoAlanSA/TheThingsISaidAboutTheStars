using System.Collections;
using System.Collections.Generic;
using Articy.Tfg.GlobalVariables;
using UnityEngine;
using Articy.Unity;
using Articy.Unity.Interfaces;
using TMPro;
using UnityEngine.UI;
public class BranchChoices : MonoBehaviour
{
    private Branch branch;
    private ArticyFlowPlayer flowPlayer;
    [SerializeField] private TextMeshProUGUI buttonText;
    public void AssingBranch(ArticyFlowPlayer flowPlayer, Branch aBranch)
    {

        branch = aBranch;
        this.flowPlayer = flowPlayer;
        IFlowObject target = aBranch.Target;
        buttonText.text = string.Empty;

        var objectWithMenuText = target as IObjectWithMenuText;
        if (objectWithMenuText != null && objectWithMenuText.MenuText.ToString() != "") buttonText.text = objectWithMenuText.MenuText;
        else
        {
            var objectWithText = target as IObjectWithLocalizableText;
            if (objectWithText != null) buttonText.text = objectWithText.Text;
        }
    }

    public void OnBranchSelected()
    {
        flowPlayer.Play(branch);
    }

 
    
}
