using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMass : MonoBehaviour
{
    [SerializeField] GameObject[] redFlagWaypoints;
    [SerializeField] Pawn playerAssociatedWithThisLandMass;
    [SerializeField] GameObject redFlag;

    [SerializeField] GameObject[] redFlagStack;

    [SerializeField] int removeRedFlagCost = 70;

    private void Start()
    {
        redFlagStack = new GameObject[7];
    }
    public void AddRedFlag()
    {
        for(int i=0; i<7; i++)
        { 
            if(redFlagStack[i]==null)
            {
                redFlagStack[i] = Instantiate(redFlag, redFlagWaypoints[i].transform.position, redFlagWaypoints[i].transform.rotation);
                playerAssociatedWithThisLandMass.AddOneRedFlag();
                break;
            }
        }
    }

    public void DestroyRedFlag()
    {
        if (playerAssociatedWithThisLandMass.GetPoints() > removeRedFlagCost && redFlagStack.Length>0)
        {
            for (int i = 6; i >= 0; i--)
            {
                if (redFlagStack[i] != null)
                {
                    Destroy(redFlagStack[i].gameObject);
                    redFlagStack[i] = null;
                    playerAssociatedWithThisLandMass.RemoveOneRedFlag();
                    Debug.Log("Flag Destroyed");
                    break;
                }
            }
        }
    }

    public void SetPlayerAssociatedWithThisLandMass(Pawn pawn)
    {
        playerAssociatedWithThisLandMass = pawn;
    }
}
