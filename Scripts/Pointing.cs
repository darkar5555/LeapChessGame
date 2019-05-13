using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class Pointing : MonoBehaviour
{
    Hand hand;
    HandModel hand_model;
    // Start is called before the first frame update
    void Start()
    {
        hand_model = GetComponent<HandModel>();
        hand = hand_model.GetLeapHand();
        if(hand == null)
        {
            Debug.Log("No leap_hand founded");
        }

    }

    // Update is called once per frame
    void Update()
    {
        for(int i =0; i < HandModel.NUM_FINGERS; i++)
        {
            FingerModel finger = hand_model.fingers[i];
            Debug.DrawRay(finger.GetTipPosition(), finger.GetRay().direction, Color.red);
        }
    }
}
