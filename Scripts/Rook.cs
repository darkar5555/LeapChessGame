using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
        Chessman c;
        int i;

        // Hacia la derecha
        i = (int)(CurrentX * 10);
        while (true)
        {
            i++;
            if (i >= 8)
                break;
            c = BoardManager.Instance.Chessmans[i, (int)(CurrentY * 10)];
            if (c == null)
            {
                r[i, (int)(CurrentY * 10)] = true;
            }
            else
            {
                if (c.isWhite != isWhite)
                {
                    r[i, (int)(CurrentY * 10)] = true;
                }
                break;
            }
        }
        // Hacia la izquierda
        i = (int)(CurrentX * 10);
        while (true)
        {
            i--;
            if (i < 0)
                break;
            c = BoardManager.Instance.Chessmans[i, (int)(CurrentY * 10)];
            if (c == null)
            {
                r[i, (int)(CurrentY * 10)] = true;
            }
            else
            {
                if (c.isWhite != isWhite)
                {
                    r[i, (int)(CurrentY * 10)] = true;
                }
                break;
            }
        }
        // Hacia arriba
        i = (int)(CurrentY * 10);
        while (true)
        {
            i++;
            if (i >= 8)
                break;
            c = BoardManager.Instance.Chessmans[(int)(CurrentX * 10), i];
            if (c == null)
            {
                r[(int)(CurrentX * 10), i] = true;
            }
            else
            {
                if (c.isWhite != isWhite)
                {
                    r[(int)(CurrentX * 10), i] = true;
                }
                break;
            }
        }
        // Hacia abajo
        i = (int)(CurrentY * 10);
        while (true)
        {
            i--;
            if (i < 0)
                break;
            c = BoardManager.Instance.Chessmans[(int)(CurrentX * 10), i];
            if (c == null)
            {
                r[(int)(CurrentX * 10), i] = true;
            }
            else
            {
                if (c.isWhite != isWhite)
                {
                    r[(int)(CurrentX * 10), i] = true;
                }
                break;
            }
        }

        return r;
    }
}
