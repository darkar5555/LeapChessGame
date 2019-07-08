using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessMan
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
        ChessMan c;
        int i;

        // Hacia la derecha
        i = CurrentX;
        while (true)
        {
            i++;
            if (i >= 8)
                break;
            c = BoardManager.Instance.Chessmans[i, CurretnY];
            if(c == null)
            {
                r[i, CurretnY] = true;
            }
            else
            {
                if(c.isWhite != isWhite)
                {
                    r[i, CurretnY] = true;
                }
                break;
            }
        }
        // Hacia la izquierda
        i = CurrentX;
        while (true)
        {
            i--;
            if (i < 0)
                break;
            c = BoardManager.Instance.Chessmans[i, CurretnY];
            if (c == null)
            {
                r[i, CurretnY] = true;
            }
            else
            {
                if (c.isWhite != isWhite)
                {
                    r[i, CurretnY] = true;
                }
                break;
            }
        }
        // Hacia arriba
        i = CurretnY;
        while (true)
        {
            i++;
            if (i >= 8)
                break;
            c = BoardManager.Instance.Chessmans[CurrentX, i];
            if (c == null)
            {
                r[CurrentX, i] = true;
            }
            else
            {
                if (c.isWhite != isWhite)
                {
                    r[CurrentX, i] = true;
                }
                break;
            }
        }
        // Hacia abajo
        i = CurretnY;
        while (true)
        {
            i--;
            if (i < 0)
                break;
            c = BoardManager.Instance.Chessmans[CurrentX, i];
            if (c == null)
            {
                r[CurrentX, i] = true;
            }
            else
            {
                if (c.isWhite != isWhite)
                {
                    r[CurrentX, i] = true;
                }
                break;
            }
        }

        return r;
    }
}
