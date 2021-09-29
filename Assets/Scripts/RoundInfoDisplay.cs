using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundInfoDisplay : MonoBehaviour
{

    public void SetRoundinfoText(int round)
    {
        GetComponent<Text>().text = ("Round: " + round);
    }
}
