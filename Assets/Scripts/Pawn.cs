using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] int points = 0;
    [SerializeField] int redFlags = 0;

    [SerializeField] PlayerInfoDisplay textDisplay;

    [SerializeField] GameObject rewardPowerUp;

    [SerializeField] bool multiplierReward;

    [SerializeField] GameObject flagSprite;

    [SerializeField] bool isDisabled = false;

    int currentWaypointIndex;

    [SerializeField] LandMass thisPlayersLandMassPrefab;
    [SerializeField] LandMass thisPlayerLandMassInstance;

    private void Start()
    {
        Debug.Log("Object instantiated");
        redFlags = 0;
        //textDisplay.UpdateText(points, redFlags);
        thisPlayerLandMassInstance = Instantiate(thisPlayersLandMassPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        thisPlayerLandMassInstance.SetPlayerAssociatedWithThisLandMass(gameObject.GetComponent<Pawn>());
    }

    public LandMass GetPlayersLandmass()
    {
        Debug.Log(thisPlayerLandMassInstance);
        return thisPlayerLandMassInstance;
    }

    IEnumerator MovePawnCoroutine(Waypoint[] waypoints)
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            //Debug.Log("The next space is " + waypoints[i]);
            while (!gameObject.transform.position.Equals(waypoints[i].transform.position))
            {
                gameObject.transform.position = Vector2.MoveTowards(transform.position, waypoints[i].transform.position, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
        waypoints[waypoints.Length - 1].GetCard();
    }

    public void MovePawn(Waypoint[] waypoints)
    {
        StartCoroutine(MovePawnCoroutine(waypoints));
    }

    public int GetPawnCurrentWaypointIndex()
    {
        return currentWaypointIndex;
    }

    public void SetPawnCurrentWaypointIndex(int index)
    {
        currentWaypointIndex = index;
    }

    public void AddPoints(int pointsToBeAdded)
    {
        points += pointsToBeAdded;
        textDisplay.UpdateText(points, redFlags);
    }

    public int GetPoints()
    {
        return points;
    }

    public int GetRedFlags()
    {
        return redFlags;
    }

    public void AssignTextDisplayToPawn(PlayerInfoDisplay textDisplayToBeAssigned)
    {
        textDisplay = textDisplayToBeAssigned;
    }

    public void AssignLandMassToPlayer(LandMass landMass)
    {
        thisPlayersLandMassPrefab = landMass;
    }

    public void AssignFlagSpriteToPlayer(Sprite spriteToBeAssigned)
    {
        flagSprite.GetComponent<SpriteRenderer>().sprite = spriteToBeAssigned;
    }

    public void AssignNoLandMassToPlayer()
    {
        thisPlayersLandMassPrefab = null;
    }

    public void AddOneRedFlag()
    {
        if (redFlags < 7)
        {
            redFlags++;
            textDisplay.UpdateText(points, redFlags);
        }
        if(redFlags >= 7)
        {
            isDisabled = true;
            Debug.Log("Player Loses");
            flagSprite.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
            Debug.Log("New color set for " + flagSprite);
        }
    }

    public void RemoveOneRedFlag()
    {
            redFlags--;
            points -= 70;
            textDisplay.UpdateText(points, redFlags);
    }

    public void EnableRemoveRedFlagButton()
    {
        textDisplay.EnableButton();
    }

    public void DisableRemoveRedFlagButton()
    {
        textDisplay.DisableButton();
    }

    public void ActivateMultiplierReward()
    {
        multiplierReward = true;
    }
    public void DeactivateMultiplierReward()
    {
        multiplierReward = false;
    }

    public bool IsMultiplierRewardActive()
    {
        return multiplierReward;
    }

    public bool IsPawnDisabled()
    {
        return isDisabled;
    }
}
