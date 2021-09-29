using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardText : MonoBehaviour
{
    public void SetTextColor(Color textColor)
    {
        Text[] textObjects = GetComponentsInChildren<Text>();
        foreach(Text text in textObjects)
        {
            text.color = textColor;
        }
    }
}
