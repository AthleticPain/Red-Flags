using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionCard : MonoBehaviour
{
    [SerializeField] float multiplierRewardValue = 2.5f;
    GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    //Temporary function. Replace later.
    /*public void GetAnswerTEMP()
    {
        Destroy(gameObject);
        FindObjectOfType<GameController>().UpdatePlayerTurn();
    }*/

    public void BestAnswer()
    {
        Debug.Log("Best Answer");
        gameController.AnswerClicked(1, GetPointsMultiplier());
        Destroy(gameObject);
    }

    public void GoodAnswer()
    {
        Debug.Log("Good Answer");
        gameController.AnswerClicked(2, GetPointsMultiplier());
        Destroy(gameObject);
    }

    public void BadAnswer()
    {
        Debug.Log("Bad Answer");
        gameController.AnswerClicked(3, GetPointsMultiplier());
        Destroy(gameObject);
    }

    private float GetPointsMultiplier()
    {
        Pawn activePawn = FindObjectOfType<GameController>().GetActivePawnFromGameController();
        if (activePawn.IsMultiplierRewardActive())
        {
            activePawn.DeactivateMultiplierReward();
            return multiplierRewardValue;
        }
        else return 1f;
    }
}
