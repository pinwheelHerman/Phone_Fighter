using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{
    // This is where we take care of all the of the logic. Each player movement script sends swipe commands
    // through to this one, which updates both player states and sends them out to the client

    [SyncVar]
    private Player _player1 = new Player(), _player2 = new Player();
    private int p1Start = 10, p2Start = 14;
    [SerializeField]
    private int playerCount = 0;

    public void OnPlayerConnected()
    {
        Cmd_OnPlayerConnected();
    }

    [Command]
    private void Cmd_OnPlayerConnected()
    {
        playerCount++;
    }

    public int GetPlayerCount()
    {
        return playerCount;
    }

    void Start ()
    {
        // reset both players
        PlayerReset(1);
        PlayerReset(2);
	}
	
	void Update ()
    {
		
	}

    private void PlayerReset(int playerNum)
    {
        if (playerNum == 1)
        {
            _player1.SetFeet(Player.Feet.Ground);
            _player1.SetAttack(Player.Attack.None);
            _player1.SetCanAttack(true);
            _player1.SetSide(Player.Side.Left);
            _player1.SetPosition(p1Start);
        }
        if (playerNum == 2)
        {
            _player2.SetFeet(Player.Feet.Ground);
            _player2.SetAttack(Player.Attack.None);
            _player2.SetCanAttack(true);
            _player2.SetSide(Player.Side.Right);
            _player2.SetPosition(p2Start);
        }
    }

    public void SwipeLeft(int playerNumber)
    {
        Cmd_SwipeLeft(playerNumber);
    }

    [Command]
    private void Cmd_SwipeLeft(int playerNumber)
    {
        Debug.Log("Player " + playerNumber + " swiped Left");
    }
}
