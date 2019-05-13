using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knigth : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        // Arriba Izquierda
        KnightMove((int)(CurrentX*10.0f) - 1, (int)(CurrentY*10.0f) + 2, ref r);

        // Arriba Derecha
        KnightMove((int)(CurrentX * 10.0f) + 1, (int)(CurrentY * 10.0f) + 2, ref r);

        // Derecha arriba
        KnightMove((int)(CurrentX * 10.0f) + 2, (int)(CurrentY * 10.0f) + 1, ref r);

        // Derecha abajo
        KnightMove((int)(CurrentX * 10.0f) + 2, (int)(CurrentY * 10.0f) - 1, ref r);

        // Abajo Izquierda
        KnightMove((int)(CurrentX * 10.0f) - 1, (int)(CurrentY * 10.0f) - 2, ref r);

        // Abajo Derecha
        KnightMove((int)(CurrentX * 10.0f) + 1, (int)(CurrentY * 10.0f) - 2, ref r);

        // Izquierda Arriba
        KnightMove((int)(CurrentX * 10.0f) - 2, (int)(CurrentY * 10.0f) + 1, ref r);

        // Izquierda Abajo
        KnightMove((int)(CurrentX * 10.0f) - 2, (int)(CurrentY * 10.0f) - 1, ref r);

        return r;
    }

    public void KnightMove(int x, int y, ref bool[,] r)
    {
        Chessman c;
        if (x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            c = BoardManager.Instance.Chessmans[x, y];
            if (c == null)
            {
                r[x, y] = true;
            }
            else if (isWhite != c.isWhite)
            {
                r[x, y] = true;
            }
        }
    }
}
