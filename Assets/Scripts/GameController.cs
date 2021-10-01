using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] [Range(1,5)] int numberOfPlayers = 1;
    [SerializeField] Pawn[] pawnPrefabs;
    [SerializeField] PlayerInfoDisplay[] textDisplays;
    [SerializeField] RoundInfoDisplay roundInfoDisplay;
    [SerializeField] LandMass[] landMasses;
    [SerializeField] int numberOfRoundsInGame;
    [SerializeField] GameEndCanvas gameEndCanvas;


    Pawn[] pawnsInGame;
    Pawn activePawn;

    BoardController boardController;
    int playerTurn = -1;
    int round = 0;

    private void Start()
    { 
        StartGame();
    }

    public void StartGame()
    {
        numberOfPlayers = GameSettingsController.GetNumberOfPlayers(); 
        pawnsInGame = new Pawn[numberOfPlayers];
        boardController = FindObjectOfType<BoardController>();
        InstantiateAllPawns();
        //FindObjectOfType<EarthBoardController>().InitializeLandMasses(numberOfPlayers);
        UpdatePlayerTurn();
        
    }

    private void InstantiateAllPawns()
    {
        for(int i = 0; i < numberOfPlayers; i++)
        {
            textDisplays[i].gameObject.SetActive(true);
            Pawn newPawn = Instantiate(pawnPrefabs[i], transform.position, transform.rotation);
            newPawn.AssignTextDisplayToPawn(textDisplays[i]);
            newPawn.DisableRemoveRedFlagButton();
            pawnsInGame[i] = newPawn;
            landMasses[i] = newPawn.GetPlayersLandmass();
        }
        boardController.SetAllPawnsToStartSpace();
        activePawn = pawnsInGame[0];
    }

    public void UpdatePlayerTurn()
    {
        FindObjectOfType<MainCanvas>().EnableRollButton();
        activePawn.DisableRemoveRedFlagButton();

        if (round >= numberOfRoundsInGame || areAllPawnsDisabled())
        {
            EndGame();
            gameObject.SetActive(false);
            return;
        }
        else
        {
            playerTurn = (playerTurn + 1) % pawnsInGame.Length;
            if (playerTurn == 0)
            {
                round++;
                roundInfoDisplay.SetRoundinfoText(round);
                if (round % 2 == 0)
                {
                    UpdateAllPlayersRedFlags();
                }
            }
            activePawn = pawnsInGame[playerTurn];
            if (activePawn.IsPawnDisabled())
            {
                UpdatePlayerTurn();
                return;
            }
            boardController.SetActivePawn(activePawn);
            activePawn.EnableRemoveRedFlagButton();
            Debug.Log("It is Player " + (playerTurn + 1) + " turn. Round " + round + "\nPlayer points: " + activePawn.GetPoints());
        }
    }

    public void AnswerClicked(int optionChosen, float multiplier)
    {
        switch (optionChosen)
        {
            case 1:
                activePawn.AddPoints(((int)(30 * multiplier)));
                break;
            case 2:
                activePawn.AddPoints(((int)(20 * multiplier)));
                break;
            case 3:
                activePawn.AddPoints(((int)(10 * multiplier)));
                break;
            default:
                Debug.LogError("Error in AnswerClicked()");
                break;
        }
        UpdatePlayerTurn();
    }

    public void SetNumberOfPlayers(int number)
    {
        numberOfPlayers = number;
    }

    private void UpdateAllPlayersRedFlags()
    {
        for(int i=0; i<numberOfPlayers; i++)
        {
            pawnsInGame[i].GetPlayersLandmass().AddRedFlag();
        }
    }

    public Pawn GetActivePawnFromGameController()
    {
        return activePawn;
    }

    private bool areAllPawnsDisabled()
    {
        foreach(Pawn pawn in pawnsInGame)
        {
            if (pawn.IsPawnDisabled() == false)
                return false;
        }
        return true;
    }

    private void EndGame()
    {
        Pawn winner;
        int winnerIndex = 0;
        int leastRedFlags = 8;
        int mostPoints = 0;

        if(areAllPawnsDisabled())
        {
            Debug.Log("All players lose");
            gameEndCanvas.SetGameEndText();
            gameEndCanvas.EnableGameEndCanvas();
        }
        else
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Debug.Log("Checking for Player " + (i + 1) + "\nRed Flags: " + pawnsInGame[i].GetRedFlags() + "\nPoints: " + pawnsInGame[i].GetPoints());
                if (pawnsInGame[i].GetRedFlags() < leastRedFlags)
                {
                    winner = pawnsInGame[i];
                    leastRedFlags = pawnsInGame[i].GetRedFlags();
                    mostPoints = pawnsInGame[i].GetPoints();
                    winnerIndex = i;
                }
                else if (pawnsInGame[i].GetRedFlags() == leastRedFlags && pawnsInGame[i].GetPoints() > mostPoints)
                {
                    winner = pawnsInGame[i];
                    leastRedFlags = pawnsInGame[i].GetRedFlags();
                    mostPoints = pawnsInGame[i].GetPoints();
                    winnerIndex = i;
                }
            }
            gameEndCanvas.SetGameEndText(winnerIndex + 1);
            gameEndCanvas.EnableGameEndCanvas();
            Debug.Log("Game Over\nWinner is Player " + (winnerIndex + 1));
        }
    }
}
