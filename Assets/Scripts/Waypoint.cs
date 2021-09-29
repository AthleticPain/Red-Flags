using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public void GetCard()
    {
        CardController cardController = FindObjectOfType<CardController>();
        cardController.FindCurrentCard(gameObject.GetComponent<Waypoint>());
    }
}
