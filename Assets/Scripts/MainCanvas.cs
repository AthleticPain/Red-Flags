using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    public void DisableRollButton()
    {
        GetComponentInChildren<Button>().interactable = false;
    }

    public void EnableRollButton()
    {
        GetComponentInChildren<Button>().interactable = true;
    }
}
