using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerMovement : NetworkBehaviour
{
    // some touch control code taken from http://blog.trsquarelab.com/2015/02/detecting-swipe-from-unity3d-c-scripts.html

    private CharacterController controller;
    public float speed;
    private bool touchingScreen;
    private Vector2 startTouchPos;
    [SerializeField]
    public int playerNumber;

    private readonly Vector2 mXAxis = new Vector2(1, 0);
    private readonly Vector2 mYAxis = new Vector2(0, 1);

    private readonly string[] mMessage = 
    {
        "",
        "Swipe Left",
        "Swipe Right",
        "Swipe Top",
        "Swipe Bottom"
    };

    private int mMessageIndex = 0;

    // The angle range for detecting swipe
    private const float mAngleRange = 30;

    // To recognize as swipe user should at lease swipe for this many pixels
    private const float mMinSwipeDist = 50.0f;

    // To recognize as a swipe the velocity of the swipe
    // should be at least mMinVelocity
    // Reduce or increase to control the swipe speed
    private const float mMinVelocity = 500.0f;

    private Vector2 mStartPosition;
    private float mSwipeStartTime;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        GameObject.Find("IpAddress").GetComponent<Text>().text = Network.player.ipAddress;
        GameObject.Find("GameManager").GetComponent<GameManager>().OnPlayerConnected();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 1) playerNumber = 1;
        else playerNumber = 2;
        if (playerNumber == 1) gameObject.GetComponent<MeshRenderer>().material.color = Color.cyan;
        if (playerNumber == 2) gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        KeyboardControl();
        TouchControl();
        DisplayGesture();
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
        // Mouse button down, possible chance for a swipe
        if (Input.GetMouseButtonDown(0))
        {
            // Record start time and position
            mStartPosition = new Vector2(Input.mousePosition.x,
                Input.mousePosition.y);
            mSwipeStartTime = Time.time;
        }

        // Mouse button up, possible chance for a swipe
        if (Input.GetMouseButtonUp(0))
        {
            float deltaTime = Time.time - mSwipeStartTime;

            Vector2 endPosition = new Vector2(Input.mousePosition.x,
                Input.mousePosition.y);
            Vector2 swipeVector = endPosition - mStartPosition;

            float velocity = swipeVector.magnitude/deltaTime;

            if (velocity > mMinVelocity &&
                swipeVector.magnitude > mMinSwipeDist)
            {
                // if the swipe has enough velocity and enough distance

                swipeVector.Normalize();

                float angleOfSwipe = Vector2.Dot(swipeVector, mXAxis);
                angleOfSwipe = Mathf.Acos(angleOfSwipe)*Mathf.Rad2Deg;

                // Detect left and right swipe
                if (angleOfSwipe < mAngleRange)
                {
                    OnSwipeRight();
                }
                else if ((180.0f - angleOfSwipe) < mAngleRange)
                {
                    OnSwipeLeft();
                }
                else
                {
                    // Detect top and bottom swipe
                    angleOfSwipe = Vector2.Dot(swipeVector, mYAxis);
                    angleOfSwipe = Mathf.Acos(angleOfSwipe)*Mathf.Rad2Deg;
                    if (angleOfSwipe < mAngleRange)
                    {
                        OnSwipeTop();
                    }
                    else if ((180.0f - angleOfSwipe) < mAngleRange)
                    {
                        OnSwipeBottom();
                    }
                    else
                    {
                        mMessageIndex = 0;
                    }
                }
            }
        }
    }

    private void OnSwipeLeft()
    {
        mMessageIndex = 1;
        Debug.Log(mMessage[mMessageIndex]);
        GameObject.Find("GameManager").GetComponent<GameManager>().SwipeLeft(playerNumber);
    }

    private void OnSwipeRight()
    {
        mMessageIndex = 2;
        Debug.Log(mMessage[mMessageIndex]);
    }

    private void OnSwipeTop()
    {
        mMessageIndex = 3;
        Debug.Log(mMessage[mMessageIndex]);
    }

    private void OnSwipeBottom()
    {
        mMessageIndex = 4;
        Debug.Log(mMessage[mMessageIndex]);
    }

    private void DisplayGesture()
    {
        GameObject.Find("Gesture").GetComponent<Text>().text = mMessage[mMessageIndex];
    }
}
