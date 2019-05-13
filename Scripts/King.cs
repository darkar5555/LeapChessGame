using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        Chessman c;
        int i, j;

        // Arriba
        i = (int)(CurrentX*10.0f) - 1;
        j = (int)(CurrentY*10.0f) + 1;
        if ((int)(CurrentY * 10.0f) != 7)
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

        // Abajo al lado
        i = (int)(CurrentX * 10.0f) - 1;
        j = (int)(CurrentY * 10.0f) - 1;
        if ((int)(CurrentY * 10.0f) != 0)
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
        if (CurrentX != 0)
        {
            c = BoardManager.Instance.Chessmans[(int)(CurrentX * 10.0f) - 1, (int)(CurrentY * 10.0f)];
            if (c == null)
            {
                r[(int)(CurrentX * 10.0f) - 1, (int)(CurrentY * 10.0f)] = true;
            }
            else if (isWhite != c.isWhite)
            {
                r[(int)(CurrentX * 10.0f) - 1, (int)(CurrentY * 10.0f)] = true;
            }
        }

        //Medio derecha
        if (CurrentX != 7)
        {
            c = BoardManager.Instance.Chessmans[(int)(CurrentX * 10.0f) - 1, (int)(CurrentY * 10.0f)];
            if (c == null)
            {
                r[(int)(CurrentX * 10.0f) + 1, (int)(CurrentY * 10.0f)] = true;
            }
            else if (isWhite != c.isWhite)
            {
                r[(int)(CurrentX * 10.0f) + 1, (int)(CurrentY * 10.0f)] = true;
            }
        }

        return r;
    }
}
