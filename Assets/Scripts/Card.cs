using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] InstructionCard[] questionCards;
    [SerializeField] Color cardColor;
    [SerializeField] Color textColor;

    [SerializeField] bool isRewardCard = false;
    public InstructionCard GetBlankCardObject()
    {
        if (isRewardCard)
        {
            return questionCards[0];
        }
        else
        {
            int questionIndex = Random.Range(0, questionCards.Length);
            return questionCards[questionIndex];
        }
    }

    public Color GetCardColor()
    {
        //Debug.Log("Card Color is " + cardColor);
        return cardColor;
    }

    public Color GetTextColor()
    {
        return textColor;
    }


    public void DestroyCurrentCard()
    {
        //Debug.Log("Destroy Current Card Back Called");
        Destroy(gameObject);
    }
}
