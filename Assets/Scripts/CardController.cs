using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    [SerializeField] Card windCard;
    [SerializeField] Card landCard;
    [SerializeField] Card waterCard;
    [SerializeField] Card rewardsCard;
    [SerializeField] Card mixedCard;

    [SerializeField] float moveSpeed = 2f;
    [SerializeField] Vector3 desiredScaleVector = new Vector3(3f, 3f, 0);
    [Tooltip("Speed of rotation in degrees")]
    [SerializeField] float flipSpeed = 30f;

    Card currentCard;
    Vector3 scaleChange;

    Vector2 centre = new Vector2(0, 0);

    float timeCounter = 0f;
    float timeTakenToReachCentre;

    public void FindCurrentCard(Waypoint waypoint)
    {
        if (waypoint.GetComponent<WindSpace>())
        { 
            currentCard = Instantiate(windCard, windCard.transform.position, windCard.transform.rotation);
            Debug.Log("The current card is WIND");
        } 
        else if (waypoint.GetComponent<LandSpace>())
        { 
            currentCard = Instantiate(landCard, landCard.transform.position, landCard.transform.rotation);
            Debug.Log("The current card is LAND");
        }
        else if (waypoint.GetComponent<WaterSpace>())
        { 
            currentCard = Instantiate(waterCard, waterCard.transform.position, waterCard.transform.rotation);
            Debug.Log("The current card is WATER");
        }
        else if (waypoint.GetComponent<RewardSpace>())
        { 
            currentCard = Instantiate(rewardsCard, rewardsCard.transform.position, rewardsCard.transform.rotation);
            Debug.Log("The current card is REWARDS");
        }
        else if (waypoint.GetComponent<MixedSpace>())
        {
            currentCard = Instantiate(mixedCard, mixedCard.transform.position, mixedCard.transform.rotation);
            Debug.Log("The current card is MIXED");
        }
        else if (waypoint.GetComponent<PlusTenSpace>())
        {
            FindObjectOfType<GameController>().GetActivePawnFromGameController().AddPoints(10);
            Debug.Log("Player Gets Ten Points");
            FindObjectOfType<GameController>().UpdatePlayerTurn();
            return;
        }
        else if(waypoint.GetComponent<CatastropheSpace>())
        {
            FindObjectOfType<GameController>().GetActivePawnFromGameController().GetPlayersLandmass().AddRedFlag();
            Debug.Log("Catastrophe Space");
            FindObjectOfType<GameController>().UpdatePlayerTurn();
            return;
        }
        else
        {
            FindObjectOfType<GameController>().UpdatePlayerTurn();
            return; 
        }
        CalculateRequiredTime();
        MoveCard();
    }

    private void CalculateRequiredTime()
    {
        timeTakenToReachCentre = ((new Vector2(currentCard.transform.position.x, currentCard.transform.position.y) - centre).magnitude) / moveSpeed;
    }

    public void MoveCard()
    {
        StartCoroutine(MoveToCentre());
    }

    IEnumerator MoveToCentre()
    {
        //Debug.Log("Coroutine Started");
        if (currentCard == null)
        {
            yield break;
        }
        else while (!currentCard.gameObject.transform.position.Equals(centre))
            {
                currentCard.transform.position = Vector2.MoveTowards(currentCard.transform.position, centre, moveSpeed * Time.deltaTime);
                timeCounter += Time.deltaTime;
                ChangeScale();
                yield return null;
            }
        //Debug.Log("Coroutine Ended");
        //Debug.Log("TimeTaken = " + timeCounter);
        StartCoroutine(FlipCard());
    }

    private void ChangeScale()
    {

        currentCard.transform.localScale += ((desiredScaleVector - new Vector3(1,1,0)) / timeTakenToReachCentre) * Time.deltaTime;
    }

    IEnumerator FlipCard()
    {
        while(currentCard.transform.rotation.eulerAngles.y <= 90)
        {
            currentCard.transform.Rotate(new Vector3(0, flipSpeed * Time.deltaTime, 0));
            yield return null;
        }
        InstructionCard newCard = Instantiate(currentCard.GetBlankCardObject(), currentCard.transform.position, currentCard.transform.rotation);
        newCard.SetColors(currentCard.GetCardColor(), currentCard.GetTextColor());
        newCard.SetScale(desiredScaleVector);
        //Debug.Break();
        //newCard.RevealCard();
        currentCard.DestroyCurrentCard();
    }


    public float GetFlipSpeed()
    {
        return flipSpeed;
    }

    public Vector3 GetScale()
    {
        return desiredScaleVector;
    }

}
