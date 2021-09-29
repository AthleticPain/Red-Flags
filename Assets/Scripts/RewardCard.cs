using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardCard : MonoBehaviour
{
    GameController gameController;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        Pawn activePawn = gameController.GetActivePawnFromGameController();
        activePawn.ActivateMultiplierReward();
    }

    public void DestroyCard()
    {
        gameController.UpdatePlayerTurn();
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        DestroyCard();
    }
}
