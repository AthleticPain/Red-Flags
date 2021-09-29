using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoDisplay : MonoBehaviour
{
    [SerializeField] Text pointsText;
    [SerializeField] Text redFlagsText;
    public void UpdateText(int points, int redFlags)
    {
        pointsText.text = "Points: " + points;
        redFlagsText.text = "Red Flags: " + redFlags;
    }

    public void EnableButton()
    {
        /*foreach(PlayerInfoDisplay display in FindObjectsOfType<PlayerInfoDisplay>())
        {
            GetComponentInChildren<Button>().interactable = false;
        }*/
        Button thisButton = GetComponentInChildren<Button>();
        thisButton.interactable = true;
    }

    public void DisableButton()
    {
        GetComponentInChildren<Button>().interactable = false;
    }
}
