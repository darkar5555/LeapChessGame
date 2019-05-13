using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Leap;
using Leap.Unity;


public class ControlMove : MonoBehaviour
{
    Controller controller;
    float HandPalmPitch;
    float HandPalmYam;
    float HandPalmRoll;
    float HandPristRot;
    public GameObject player;
    private Vector3 positionPlayer;

    public GameObject manolas;
    
    Finger finger;
    //InteractionBox interactionBox = frame.InteractionBox;
    //Vector3 fwd = transform.TransformDirection(Vector3.forward);
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        controller = new Controller();
        Frame frame = controller.Frame(); 
        List<Hand> hands = frame.Hands;
        positionPlayer = player.transform.position;
        manolas = GameObject.Find("Interaction Hand (Left)/Palm Transform");
        if (positionPlayer.z >= -0.3)
        {       
            if (hands[0].IsLeft)
            {
                finger = hands[0].Fingers[1];
            
                Debug.DrawRay(manolas.transform.position, hands[1].Direction.ToVector3()*-1, Color.red,0.01f* Time.deltaTime, true);

            }
            if (hands[1].IsLeft)
            {

                finger = hands[0].Fingers[1];
                Debug.DrawRay(manolas.transform.position, hands[1].Direction.ToVector3()*-1, Color.red, 0.01f * Time.deltaTime, true);
                //Debug.Log("Este es la palma" + hands[0].GetPalmPose());
            }
            return;
            
        }
        else
        {
            
            if (frame.Hands.Count > 0)
            {
                Hand firstHand = hands[0];
            }
            HandPalmPitch = hands[0].PalmNormal.Pitch;
            HandPalmRoll = hands[0].PalmNormal.Roll;
            HandPalmYam = hands[0].PalmNormal.Yaw;
            HandPristRot = hands[0].WristPosition.Pitch;

            if (HandPalmYam > -2f && HandPalmYam < 3.5f)
            {

                transform.Translate(new Vector3(0, 0, 0.5f * Time.deltaTime));
                
            }
            
            else if (HandPalmYam < -2.2f)
            {
                transform.Translate(new Vector3(0, 0, -0.5f * Time.deltaTime));
                
            }

            

        }

    }


}
