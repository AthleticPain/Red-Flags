using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBoardController : MonoBehaviour
{
    [SerializeField] GameObject redFlag;

    [SerializeField] GameObject[] NorthAmericaWaypoints;
    [SerializeField] GameObject[] SouthAmericaWaypoints;
    [SerializeField] GameObject[] GreenlandWaypoints;
    [SerializeField] GameObject[] AfricaWaypoints;
    [SerializeField] GameObject[] EurasiaWaypoints;

    int NorthAmericaIndex = 0;
    int SouthAmericaIndex = 1;
    int GreenlandIndex = 2;
    int AfricaIndex = 3;
    int EurasiaIndex = 4;

    [SerializeField] bool isNorthAmericaActive = false;
    [SerializeField] bool isSouthAmericaActive = false;
    [SerializeField] bool isGreenlandActive = false;
    [SerializeField] bool isAfricaActive = false;
    [SerializeField] bool isEurasiaActive = false;

    [SerializeField] bool[] NorthAmericaFlagStack = { false, false, false, false, false, false, false };
    [SerializeField] bool[] SouthAmericaFlagStack = { false, false, false, false, false, false, false };
    [SerializeField] bool[] GreenlandFlagStack = { false, false, false, false, false, false, false };
    [SerializeField] bool[] AfricaFlagStack = { false, false, false, false, false, false, false };
    [SerializeField] bool[] EurasiaFlagStack = { false, false, false, false, false, false, false };

    [SerializeField] Vector3[,] redFlagWaypointPositions;

    Pawn NorthAmericaPlayer;
    Pawn SouthAmericaPlayer;
    Pawn GreenlandPlayer;
    Pawn AfricaPlayer;
    Pawn EurasiaPlayer;

    private void Start()
    {
        if(FindObjectsOfType<EarthBoardController>().Length>1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void InitializeLandMasses(int numberOfPlayers)
    {
        redFlagWaypointPositions = new Vector3[numberOfPlayers, 7];
        int counter = 0;
        if (counter < numberOfPlayers)
        {
            InitializeNorthAmericaFlagPositionArray();
            counter++;
        }
        if (counter < numberOfPlayers)
        {
            InitializeSouthAmericaFlagPositionArray();
            counter++;
        }
        if (counter < numberOfPlayers)
        {
            InitializeGreenlandFlagPositionArray();
            counter++;
        }
        if (counter < numberOfPlayers)
        {
            InitializeAfricaFlagPositionArray();
            counter++;
        }
        if (counter < numberOfPlayers)
        {
            InitializeEurasiaFlagPositionArray();
            counter++;
        }
        
    }

    //InitializeLandMasses Overloads
    public void SetNorthAmericaIndex(int index)
    {
        NorthAmericaIndex = index;
        isNorthAmericaActive = true;
    }
    public void SetSouthAmericaIndex(int index)
    {
        SouthAmericaIndex = index;
        isSouthAmericaActive = true;
    }
    public void SetGreenlandIndex(int index)
    {
        GreenlandIndex = index;
        isGreenlandActive = true;
    }
    public void SetAfricaIndex(int index)
    {
        AfricaIndex = index;
        isAfricaActive = true;
    }
    public void SetEurasiaIndex(int index)
    {
        isEurasiaActive = true;
        EurasiaIndex = index;
    }

    public void InitializeNorthAmericaFlagPositionArray()
    {
        for (int i = 0; i < 7; i++)
        {
            redFlagWaypointPositions[NorthAmericaIndex, i] = NorthAmericaWaypoints[i].transform.position;
        }
    }
    public void InitializeSouthAmericaFlagPositionArray()
    {
        for (int i = 0; i < 7; i++)
        {
            redFlagWaypointPositions[SouthAmericaIndex, i] = SouthAmericaWaypoints[i].transform.position;
        }
    }
    public void InitializeGreenlandFlagPositionArray()
    {

        for (int i = 0; i < 7; i++)
        {
            redFlagWaypointPositions[GreenlandIndex, i] = GreenlandWaypoints[i].transform.position;
        }
    }
    public void InitializeAfricaFlagPositionArray()
    {

        for (int i = 0; i < 7; i++)
        {
            redFlagWaypointPositions[AfricaIndex, i] = AfricaWaypoints[i].transform.position;
        }
    }
    public void InitializeEurasiaFlagPositionArray()
    {

        for (int i = 0; i < 7; i++)
        {
            redFlagWaypointPositions[EurasiaIndex, i] = EurasiaWaypoints[i].transform.position;
        }
    }

    public void UpdateRedFlags(int roundNumber)
    {
        if(roundNumber%2 == 0)
        {
            if (isNorthAmericaActive == true)
            {
                AddRedFlagToLandMass(NorthAmericaFlagStack, NorthAmericaIndex);
            }
            if (isSouthAmericaActive == true)
            {
                AddRedFlagToLandMass(SouthAmericaFlagStack, SouthAmericaIndex);
            }
            if (isGreenlandActive == true)
            {
                AddRedFlagToLandMass(GreenlandFlagStack, GreenlandIndex);
            }
            if(isAfricaActive == true)
            {
                AddRedFlagToLandMass(AfricaFlagStack, AfricaIndex);
            }
            if(isEurasiaActive == true)
            {
                AddRedFlagToLandMass(EurasiaFlagStack, EurasiaIndex);
            }
        }
    }

    private void AddRedFlagToLandMass(bool[] landMassStack, int landMassWaypointArrayIndex)
    {
        for (int i = 0; i < 7; i++)
        {
            if (landMassStack[i] == false)
            {
                landMassStack[i] = true;
                Instantiate(redFlag, redFlagWaypointPositions[landMassWaypointArrayIndex, i], new Quaternion(0,0,0,0));
                break;
            }
        }
    }
}
