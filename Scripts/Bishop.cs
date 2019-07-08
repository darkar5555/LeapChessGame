using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessMan
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
        ChessMan c;
        int i, j;

        // Arriba Izquierda
        i = CurrentX;
        j = CurretnY;
        while (true)
        {
            i--;
            j++;
            if(i < 0 || j >= 8)
            {
                break;
            }
            c = BoardManager.Instance.Chessmans[i, j];
            if(c == null)
            {
                r[i, j] = true;
            }
            else
            {
                if(isWhite != c.isWhite)
                {
                    r[i, j] = true;
                }
                break;
            }
        }

        // Arriba Derecha
        i = CurrentX;
        j = CurretnY;
        while (true)
        {
            i++;
            j++;
            if (i >= 8 || j >= 8)
            {
                break;
            }
            c = BoardManager.Instance.Chessmans[i, j];
            if (c == null)
            {
                r[i, j] = true;
            }
            else
            {
                if (isWhite != c.isWhite)
                {
                    r[i, j] = true;
                }
                break;
            }
        }

        // Abajo Izquierda
        i = CurrentX;
        j = CurretnY;
        while (true)
        {
            i--;
            j--;
            if (i < 0 || j < 0)
            {
                break;
            }
            c = BoardManager.Instance.Chessmans[i, j];
            if (c == null)
            {
                r[i, j] = true;
            }
            else
            {
                if (isWhite != c.isWhite)
                {
                    r[i, j] = true;
                }
                break;
            }
        }

        // Abajo Derecha
        i = CurrentX;
        j = CurretnY;
        while (true)
        {
            i++;
            j--;
            if (i >= 8 || j < 0)
            {
                break;
            }
            c = BoardManager.Instance.Chessmans[i, j];
            if (c == null)
            {
                r[i, j] = true;
            }
            else
            {
                if (isWhite != c.isWhite)
                {
                    r[i, j] = true;
                }
                break;
            }
        }
        return r;
    }
}
