using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    private CharacterController controller;
    public float speed;

	void Start ()
	{
	    controller = GetComponent<CharacterController>();
	}
	
	void Update ()
	{
	    if (!isLocalPlayer)
	    {
	        return;
	    }

	    if (Input.GetKey(KeyCode.D))
	    {
	        controller.Move(Vector3.right * speed * Time.deltaTime);
	    }
        if (Input.GetKey(KeyCode.A))
        {
            controller.Move(Vector3.left * speed * Time.deltaTime);
        }
    }
}
