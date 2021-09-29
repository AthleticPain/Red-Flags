using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField] public Sprite[] spriteArray;
    [SerializeField] public Waypoint[] waypoints;
    [SerializeField] int startingWaypointIndex = 65;

    private int destinationWaypointIndex = 0;

    SpriteRenderer spriteRenderer;
    Pawn activePawn;

    private void Start()
    {
        SetAllPawnsToStartSpace();
    }

    /*private void MoveTillWaypointIsReached()
    {
        pawn.MovePawn(waypoints[nextWaypointIndex]);
        if (pawn.transform.position == waypoints[nextWaypointIndex].transform.position)
        {
            nextWaypointIndex++;
            //Debug.Log("New WaypointIndex is: " + nextWaypointIndex);
        }
        if(nextWaypointIndex>=destinationWaypointIndex+1)
        {
            moveAllowed = false;
        }   

    }*/


    public void RollDie()
    {
        StartCoroutine(GenerateRollNumber());
    }

    IEnumerator GenerateRollNumber()
    {
        int randomSide = 0;
        for (int i = 0; i < 18; i++)
        {
            randomSide = Random.Range(0, 6);
            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = spriteArray[randomSide];
            yield return new WaitForSeconds(0.05f);
        }
        //Debug.Log("Dice Number: " + randomSide);
        //FindDestinationWaypoint(randomSide);
    }

    public void FindDestinationWaypoint(int randomSide)
    {
        int currentWaypointIndex = activePawn.GetPawnCurrentWaypointIndex();
        destinationWaypointIndex = currentWaypointIndex + randomSide + 1;
        if(destinationWaypointIndex > waypoints.Length)
        {
            destinationWaypointIndex = waypoints.Length - 1;
        }
        
        activePawn.SetPawnCurrentWaypointIndex(destinationWaypointIndex);
        Waypoint[] waypointMovementArray = new Waypoint[destinationWaypointIndex - currentWaypointIndex];
        //Debug.Log("Number of spaces to move is " + waypointMovementArray.Length);
        for (int i = 0; i < waypointMovementArray.Length; i++, currentWaypointIndex++)
        {
            waypointMovementArray[i] = waypoints[currentWaypointIndex + 1];
        }
        activePawn.MovePawn(waypointMovementArray);
    }

    public void SetAllPawnsToStartSpace()
    {
        Pawn[] pawns = FindObjectsOfType<Pawn>();
        foreach(Pawn pawn in pawns)
        {
            pawn.transform.position = waypoints[startingWaypointIndex].transform.position;
            pawn.SetPawnCurrentWaypointIndex(startingWaypointIndex);
        }
    }

    public void SetActivePawn(Pawn pawn)
    {
        activePawn = pawn;
    }
}
