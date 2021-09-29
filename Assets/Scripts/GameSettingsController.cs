using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsController : MonoBehaviour
{
    const string NUMBER_OF_PLAYERS = "numberOfPlayers";
    public static void SetNumberOfPlayers(int number)
    {
        PlayerPrefs.SetInt(NUMBER_OF_PLAYERS, number);
    }

    public static int GetNumberOfPlayers()
    {
        return PlayerPrefs.GetInt(NUMBER_OF_PLAYERS);
    }
}
