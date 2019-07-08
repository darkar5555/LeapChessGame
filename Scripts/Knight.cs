using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ChessMan
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        // Arriba Izquierda
        KnightMove(CurrentX - 1, CurretnY + 2, ref r);

        // Arriba Derecha
        KnightMove(CurrentX + 1, CurretnY + 2, ref r);

        // Derecha arriba
        KnightMove(CurrentX + 2, CurretnY + 1, ref r);

        // Derecha abajo
        KnightMove(CurrentX + 2, CurretnY - 1, ref r);

        // Abajo Izquierda
        KnightMove(CurrentX - 1, CurretnY - 2, ref r);

        // Abajo Derecha
        KnightMove(CurrentX + 1, CurretnY - 2, ref r);

        // Izquierda Arriba
        KnightMove(CurrentX - 2, CurretnY + 1, ref r);

        // Izquierda Abajo
        KnightMove(CurrentX - 2, CurretnY - 1, ref r);

        return r;
    }

    public void KnightMove(int x, int y, ref bool [,] r)
    {
        ChessMan c;
        if( x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            c = BoardManager.Instance.Chessmans[x, y];
            if(c == null)
            {
                r[x, y] = true;
            }
            else if(isWhite != c.isWhite)
            {
                r[x, y] = true;
            }
        }
    }
}
