using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using LeapInternal;
using Leap.Unity.Interaction;
using Leap.Unity.Animation;

public class RotateBoard : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject blockOne;
    public bool spin;
    Controller controller;
    Finger pulgar;
    public GameObject moverBoard;
    public float turnSpeed = 50f;

void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        controller = new Controller();
        Frame frame = controller.Frame();
        List<Hand> hands = frame.Hands;
        if (hands[0].IsLeft)
        {
            pulgar = hands[0].Fingers[0];
            Vector3 temp =new Vector3 (0.0f, 0.0f, 0.0f);
            //Debug.Log(pulgar.Direction);
            if (pulgar.Direction.y > 0.5)
            {
                Debug.Log("mover el board");
                moverBoard = GameObject.Find("Board");
                if(moverBoard.transform.rotation.x == 0.0f || moverBoard.transform.rotation.x*100 > -29.0f)
                {
                    moverBoard.transform.Rotate(new Vector3(-0.01f, 0.0f, 0.0f),5.0f* Time.deltaTime);
                    //Debug.Log(moverBoard.transform.rotation.x);
                    //transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime, Space.World);
                }

            }
            if (pulgar.Direction.y < -0.4)
            {
                    moverBoard.transform.rotation = Quaternion.identity;

            }
        }
    }
}
