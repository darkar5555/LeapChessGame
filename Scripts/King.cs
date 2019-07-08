using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : ChessMan
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        ChessMan c;
        int i, j;

        // Arriba
        i = CurrentX - 1;
        j = CurretnY + 1;
        if(CurretnY != 7)
        {
            for(int k = 0; k < 3; k++)
            {
                if (i >= 0 || i < 8)
                {
                    c = BoardManager.Instance.Chessmans[i, j];
                    if(c == null)
                    {
                        r[i, j] = true;
                    }
                    else if (isWhite != c.isWhite)
                    {
                        r[i, j] = true;
                    }
                }
                i++;
            }
        }

        // Abajo al lado
        i = CurrentX - 1;
        j = CurretnY - 1;
        if (CurretnY != 0)
        {
            for (int k = 0; k < 3; k++)
            {
                if (i >= 0 || i < 8)
                {
                    c = BoardManager.Instance.Chessmans[i, j];
                    if (c == null)
                    {
                        r[i, j] = true;
                    }
                    else if (isWhite != c.isWhite)
                    {
                        r[i, j] = true;
                    }
                }
                i++;
            }
        }

        //Medio izquierda
        if(CurrentX != 0)
        {
            c = BoardManager.Instance.Chessmans[CurrentX - 1, CurretnY];
            if( c == null)
            {
                r[CurrentX - 1, CurretnY] = true;
            }
            else if (isWhite != c.isWhite)
            {
                r[CurrentX - 1, CurretnY] = true;
            }
        }

        //Medio derecha
        if (CurrentX != 7)
        {
            c = BoardManager.Instance.Chessmans[CurrentX - 1, CurretnY];
            if (c == null)
            {
                r[CurrentX + 1, CurretnY] = true;
            }
            else if (isWhite != c.isWhite)
            {
                r[CurrentX + 1, CurretnY] = true;
            }
        }

        return r;
    }
}
