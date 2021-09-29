using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] Canvas playerNumberCanvas;
    [SerializeField] Text numberOfPlayersText;
    [SerializeField] Slider slider;
    [SerializeField] Canvas landMassSelectionCanvas;
    [SerializeField] Text playerNumberTextForLandMassMenu;
    [SerializeField] Canvas selectPlayerFlagColorCanvas;
    [SerializeField] Text playerNumberTextForFlagColorMenu;
    [SerializeField] Canvas startGameCanvas;
    [SerializeField] Pawn[] playerPrefabs;
    [SerializeField] LandMass NorthAmericaPrefab;
    [SerializeField] LandMass SouthAmericaPrefab;
    [SerializeField] LandMass GreenlandPrefab;
    [SerializeField] LandMass AfricaPrefab;
    [SerializeField] LandMass EurasiaPrefab;



    int numberOfPlayers;
    int counterForLandMassMenu = 0;
    int counterForFlagColorMenu = 0;

    private void Start()
    {
        playerNumberCanvas.gameObject.SetActive(true);
        landMassSelectionCanvas.gameObject.SetActive(false);
        startGameCanvas.gameObject.SetActive(false);
        selectPlayerFlagColorCanvas.gameObject.SetActive(false);
        counterForLandMassMenu = 0;
        counterForFlagColorMenu = 0;
        UpdateNumberOfPlayersText();
    }

    public void SetNumberOfPlayers()
    {
        numberOfPlayers = ((int)slider.GetComponent<Slider>().value);
        GameSettingsController.SetNumberOfPlayers(numberOfPlayers);
        Debug.Log("Number of Players = " + numberOfPlayers);
        //FindObjectOfType<LevelLoader>().LoadNextScene();
        for(int i=0; i<playerPrefabs.Length; i++)
        {
            playerPrefabs[i].AssignNoLandMassToPlayer();
        }
        playerNumberCanvas.gameObject.SetActive(false);
        selectPlayerFlagColorCanvas.gameObject.SetActive(true);
        UpdatePlayerNumberText(playerNumberTextForLandMassMenu, 0);
        UpdatePlayerNumberText(playerNumberTextForFlagColorMenu, 0);
    }

    public void UpdateNumberOfPlayersText()
    {
        numberOfPlayersText.text = slider.value.ToString();
    }

    public void SetNorthAmerica()
    {
        AssignLandMassToPlayerAndUpdateMenu(NorthAmericaPrefab);
    }
    public void SetSouthAmerica()
    {
        AssignLandMassToPlayerAndUpdateMenu(SouthAmericaPrefab);
    }
    public void SetGreenland()
    {
        AssignLandMassToPlayerAndUpdateMenu(GreenlandPrefab);
    }
    public void SetAfrica()
    {
        AssignLandMassToPlayerAndUpdateMenu(AfricaPrefab);
    }
    public void SetEurasia()
    {
        AssignLandMassToPlayerAndUpdateMenu(EurasiaPrefab);
    }

    private void AssignLandMassToPlayerAndUpdateMenu(LandMass landMassPrefab)
    {
        Debug.Log("Counter = " + counterForLandMassMenu);
        playerPrefabs[counterForLandMassMenu].AssignLandMassToPlayer(landMassPrefab);
        counterForLandMassMenu++;
        UpdatePlayerNumberText(playerNumberTextForLandMassMenu, counterForLandMassMenu);
        if(counterForLandMassMenu >= numberOfPlayers)
        {
            landMassSelectionCanvas.gameObject.SetActive(false);
            startGameCanvas.gameObject.SetActive(true);
        }
    }

    public void AssignFlagSpriteToPlayerAndUpdateMenu(Sprite newFlagSprite)
    {
        playerPrefabs[counterForFlagColorMenu].AssignFlagSpriteToPlayer(newFlagSprite);
        counterForFlagColorMenu++;
        Debug.Log("Flag Sprite Assigned");
        UpdatePlayerNumberText(playerNumberTextForFlagColorMenu, counterForFlagColorMenu);
        if(counterForFlagColorMenu >= numberOfPlayers)
        {
            selectPlayerFlagColorCanvas.gameObject.SetActive(false);
            landMassSelectionCanvas.gameObject.SetActive(true);
        }
    }

    private void UpdatePlayerNumberText(Text textToBeUpdated, int playerNumber)
    {
        textToBeUpdated.text = "Player " + (playerNumber + 1)+": ";
    }

}
