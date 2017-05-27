using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerMovement : NetworkBehaviour
{
    private CharacterController controller;
    public float speed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        GameObject.Find("IpAddress").GetComponent<Text>().text = Network.player.ipAddress;
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        KeyboardControl();
        TouchControl();
    }

    void KeyboardControl()
    {
        if (Input.GetKey(KeyCode.D))
        {
            controller.Move(Vector3.right*speed*Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            controller.Move(Vector3.left*speed*Time.deltaTime);
        }
    }

    void TouchControl()
    {
        // cycles though all the fingers touching the screen
        for (int i = 0; i < Input.touchCount; i++)
        {
            // if finger is touching the left side of the screen
            if (Input.GetTouch(i).position.x < Screen.width/2)
            {
                controller.Move(Vector3.left * speed * Time.deltaTime);
            }
            // if finger is touching the right side of the screen
            else
            {
                controller.Move(Vector3.right * speed * Time.deltaTime);
            }
        }
    }
}
