using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameEndCanvas : MonoBehaviour
{
    public void SetGameEndText(int winnerPlayerNumber)
    {
        GetComponentInChildren<Text>().text = "Game Over\nWinner is Player " + winnerPlayerNumber;
    }

    public void EnableGameEndCanvas()
    {
        gameObject.SetActive(true);
    }
}
