using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessMan
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
        ChessMan c, c2;
        int[] e = BoardManager.Instance.EnPassantMove;

        // Movimiento de las blancas
        if (isWhite)
        {
            // Movimiento Diagonal Izquierda
            if(CurrentX != 0 && CurretnY != 7)
            {
                if(e[0] == CurrentX -1 && e[1] == CurretnY + 1)
                {
                    r[CurrentX - 1, CurretnY + 1] = true;
                }
                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurretnY + 1];
                if(c!= null && !c.isWhite)
                {
                    r[CurrentX - 1, CurretnY + 1] = true;
                }
            }
            // Movimiento Diagonal Derecha
            if (CurrentX != 7 && CurretnY != 7)
            {
                if (e[0] == CurrentX + 1 && e[1] == CurretnY + 1)
                {
                    r[CurrentX + 1, CurretnY + 1] = true;
                }

                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurretnY + 1];
                if (c != null && !c.isWhite)
                {
                    r[CurrentX + 1, CurretnY + 1] = true;
                }
            }
            // Movimiento Al Medio
            if(CurretnY != 7)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurretnY + 1];
                if(c == null)
                {
                    r[CurrentX, CurretnY + 1] = true;
                }
            }

            // Movimiendo Doble Al Empezar
            if(CurretnY == 1)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurretnY + 1];
                c2 = BoardManager.Instance.Chessmans[CurrentX, CurretnY + 2];
                if(c == null && c2 == null)
                {
                    r[CurrentX, CurretnY + 2] = true;
                }
            }
        }
        //r[3, 3] = true;
        // Movimiendo de las negras
        else
        {
            // Movimiento Diagonal Izquierda
            if (CurrentX != 0 && CurretnY != 0)
            {
                if (e[0] == CurrentX - 1 && e[1] == CurretnY - 1)
                {
                    r[CurrentX - 1, CurretnY - 1] = true;
                }

                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurretnY - 1];
                if (c != null && c.isWhite)
                {
                    r[CurrentX - 1, CurretnY - 1] = true;
                }
            }
            // Movimiento Diagonal Derecha
            if (CurrentX != 7 && CurretnY != 0)
            {
                if (e[0] == CurrentX + 1 && e[1] == CurretnY - 1)
                {
                    r[CurrentX + 1, CurretnY - 1] = true;
                }

                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurretnY - 1];
                if (c != null && c.isWhite)
                {
                    r[CurrentX + 1, CurretnY - 1] = true;
                }
            }
            // Movimiento Al Medio
            if (CurretnY != 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurretnY - 1];
                if (c == null)
                {
                    r[CurrentX, CurretnY - 1] = true;
                }
            }

            // Movimiendo Doble Al Empezar
            if (CurretnY == 6)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurretnY - 1];
                c2 = BoardManager.Instance.Chessmans[CurrentX, CurretnY - 2];
                if (c == null && c2 == null)
                {
                    r[CurrentX, CurretnY - 2] = true;
                }
            }
        }
        return r;
    }
}
