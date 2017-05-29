using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour
{
    // This is where we take care of all the of the logic. Each player movement script sends swipe commands
    // through to this one, which updates both player states and sends them out to the client

    [SyncVar]
    private Player _player1 = new Player(), _player2 = new Player();
    private int p1Start = 10, p2Start = 14;

    void Start ()
    {
        // reset both players
        PlayerReset(1);
        PlayerReset(2);
	}
	
	void Update ()
	{
        // just tests
	    GameObject.Find("p1Swipe").GetComponent<Text>().text = "p1 - " + _player1.GetLastSwipe() + " (" + _player1.GetTimesMoved() + ")";
        GameObject.Find("p2Swipe").GetComponent<Text>().text = "p2 - " + _player2.GetLastSwipe() + " (" + _player2.GetTimesMoved() + ")";

        // this is where the bulk of the gameplay will take place
        // check to see if the players are in the same square

        // if so, check what the state of their attacks are, and act accordingly

        // if not, check for changes in player state and follow through
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

    // this method is really just a test to see things working correctly across the network
    public void Swipe(int playerNumber, string direction)
    {
        if (playerNumber == 1)
        {
            if (direction == "left") _player1.SetLastSwipe(Player.LastSwipe.Left);
            if (direction == "right") _player1.SetLastSwipe(Player.LastSwipe.Right);
            if (direction == "up") _player1.SetLastSwipe(Player.LastSwipe.Up);
            if (direction == "down") _player1.SetLastSwipe(Player.LastSwipe.Down);
            _player1.SetTimesMoved(_player1.GetTimesMoved() + 1);
            Rpc_Swipe(1, _player1.GetLastSwipe(), _player1.GetTimesMoved());
        }
        else
        {
            if (direction == "left") _player2.SetLastSwipe(Player.LastSwipe.Left);
            if (direction == "right") _player2.SetLastSwipe(Player.LastSwipe.Right);
            if (direction == "up") _player2.SetLastSwipe(Player.LastSwipe.Up);
            if (direction == "down") _player2.SetLastSwipe(Player.LastSwipe.Down);
            _player2.SetTimesMoved(_player2.GetTimesMoved() + 1);
            Rpc_Swipe(2, _player2.GetLastSwipe(), _player2.GetTimesMoved());
        }
    }

    [ClientRpc]
    private void Rpc_Swipe(int playerNumber, Player.LastSwipe lastSwipe, int timesMoved)
    {
        if (playerNumber == 1)
        {
            _player1.SetLastSwipe(lastSwipe);
            _player1.SetTimesMoved(timesMoved);
        }
        else
        {
            _player2.SetLastSwipe(lastSwipe);
            _player2.SetTimesMoved(timesMoved);
        }
    }
}
