using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Pawn : Chessman
{
    bool[,] global = new bool[8,8];    
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
        Chessman c, c2;
        //Debug.Log(CurrentX + "y el y" + CurrentY);
        //White team nmove
        if (isWhite)
        {
            //Diagonal Left
            if ((int)(CurrentX*10) != 0 && (int)(CurrentY*10) != 7)
            {
                c = BoardManager.Instance.Chessmans[(int)((CurrentX - 0.1f)*10),(int)( (CurrentY + 0.1f)*10)];
                if (c!= null && !c.isWhite)
                {
                    r[(int)((CurrentX - 0.1f)*10), (int)((CurrentY + 0.1f)*10)] = true;
                }
            }
            //Diagonal Right
            if ((int)(CurrentX * 10) != 7 && (int)(CurrentY * 10) != 7)
            {
                c = BoardManager.Instance.Chessmans[(int)(CurrentX - 0.1f), (int)(CurrentY + 0.1f)];
                if (c != null && !c.isWhite)
                {
                    r[(int)(CurrentX + 0.1f), (int)(CurrentY + 0.1f)] = true;
                }
            }
            //Middle
            if ((int)(CurrentY*10) != 7 )
            {
                c = BoardManager.Instance.Chessmans[(int)(CurrentX*10),(int)( CurrentY*10) + 1];
                if (c == null)
                {
                    r[(int)(CurrentX*10), (int)(CurrentY*10) + 1] = true;
                }
            }
            //Middle en first move
            //Debug.Log((int)(CurrentY * 10));
            if ((int)(CurrentY*10) == 1)
            {
                c = BoardManager.Instance.Chessmans[(int)(CurrentX*10), (int)(CurrentY*10) + 1];
                c2 = BoardManager.Instance.Chessmans[(int)(CurrentX*10), (int)(CurrentY*10) + 2];
                if(c == null & c2 == null)
                {
                    r[(int)(CurrentX*10), (int)(CurrentY*10) + 2] = true;
                }
            }
        }
        else
        {
            //Diagonal Left
            if (CurrentX != 0 && CurrentY != 0)
            {
                c = BoardManager.Instance.Chessmans[(int)(CurrentX - 0.1f), (int)(CurrentY - 0.1f)];
                if (c != null && c.isWhite)
                {
                    r[(int)(CurrentX - 0.1f), (int)(CurrentY - 0.1f)] = true;
                }
            }
            //Diagonal Right
            if (CurrentX != 7 && CurrentY != 0)
            {
                c = BoardManager.Instance.Chessmans[(int)(CurrentX - 0.1f), (int)(CurrentY - 0.1f)];
                if (c != null && c.isWhite)
                {
                    r[(int)(CurrentX + 0.1f), (int)(CurrentY - 0.1f)] = true;
                }
            }
            //Middle
            if (CurrentY != 0)
            {
                c = BoardManager.Instance.Chessmans[(int)(CurrentX), (int)(CurrentY - 0.1f)];
                if (c == null)
                {
                    r[(int)(CurrentX), (int)(CurrentY - 1)] = true;
                }
            }
            //Middle en first move
            if (CurrentY == 6)
            {
                c = BoardManager.Instance.Chessmans[(int)CurrentX, (int)CurrentY - 1];
                c2 = BoardManager.Instance.Chessmans[(int)CurrentX, (int)CurrentY - 2];
                if (c == null & c2 == null)
                {
                    r[(int)(CurrentX*10.0f), (int)(CurrentY*10.0f) - 2] = true;
                }
            }
        }
        //r[3, 3] = true;
        return r;
    }

    public override void PosicionGrasped()
    {
        pieceGraspedX = (int)(this.transform.position.x * 10);
        pieceGraspedY = (int)(this.transform.position.z * 10);
        grasped = true;
        Debug.Log("Se supone que aqui tiene eso " + grasped);
    }

}
