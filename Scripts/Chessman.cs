using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    bool[,] global = new bool[8, 8];


    public int pieceGraspedX { set; get; }
    public int pieceGraspedY { set; get; }
    public bool grasped { set; get; }

    public float CurrentX { set; get; }
    public float CurrentY { set; get; }
    public bool isWhite;

    public void SetPosition(float x, float y)
    {
        CurrentX = x;
        CurrentY = y;
    }

    public virtual void PosicionGrasped()
    {
        
    }

    public virtual bool[,] PossibleMove()
    {

        return new bool [8,8];
    }

    public virtual void Encapsulado()
    {

    }
}
