using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Leap;
using Leap.Unity;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { set; get; }
    private bool[,] allowedMoves { set; get; }


    public ChessMan[,] Chessmans { set; get; }
    private ChessMan selectedChessman;

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    private int selectionX = -1;
    private int selectionY = -1;

    public List<GameObject> chessmanPrefabs;
    private List<GameObject> activeChessman;

    private Material previousMat;
    public Material selectedMat;

    public int[] EnPassantMove { set; get; }

    private Quaternion orientation = Quaternion.Euler(0, 180, 0);

    public bool isWhiteTurn = true;




    private float piecePositionX = -1;
    public float piecePositionY = -1;
    public float pieceLeftPositionX = -1;
    public float pieceLeftPositionY = -1;
    public int piecePositionWithoutX = -1;
    public int piecePositionWithoutY = -1;
    public bool grasped = false;
    Controller controller;
    Frame frame;
    List<Hand> hands;
    public GameObject mano;
    public GameObject manoLeft;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        SpawnAllChessmans();
    }

    private void Update()
    {
        UpdateSelection();
        DrawChessBoard();


        controller = new Controller();
        Frame frame = controller.Frame();
        List<Hand> hands = frame.Hands;
        UpdateHand(hands);
        UpdateHandLeft(hands);



        NetworkManager nM = NetworkManager.instance.GetComponent<NetworkManager>();
        Reloj reloj = Reloj.instance.GetComponent<Reloj>();
        RelojBalck relojBalck = RelojBalck.instance.GetComponent<RelojBalck>();
        if (nM.secPawn.z == 1)
        {
            SelectChessman((int)nM.secPawn.x, (int)nM.secPawn.y);
            //Debug.Log("entre aqui por favor");
            nM.secPawn.z = 0;
            reloj.estaPausado = false;
            relojBalck.estaPausado = true;
        }
        if ( nM.movPawn.z == 1)
        {
            MoveChessman((int)nM.movPawn.x , (int)nM.movPawn.y);
            nM.movPawn.z = 0;
            reloj.estaPausado = true;
            relojBalck.estaPausado = false;
            nM.clockTurn.y = 0;
        }
        //Debug.Log("Posicion x " + piecePositionWithoutX + " " + "posicion Y " + piecePositionWithoutY);
        //Debug.Log("El angulo de grab " + hands[0].GrabAngle);
        if (selectedChessman == null && hands[0].GrabAngle > 2.5) 
        {
            SelectChessman(piecePositionWithoutX, piecePositionWithoutY);
            nM.CommandSelectPawn(new Vector3(piecePositionWithoutX, piecePositionWithoutY, 1));
        }
        if (selectedChessman != null && Input.GetMouseButtonDown(0))
        {
            //SelectChessman(piecePositionWithoutX, piecePositionWithoutY);
            nM.CommandMovePawn(new Vector3(pieceLeftPositionX, pieceLeftPositionY, 1));
        }
        Debug.Log(selectedChessman);
        if (Input.GetMouseButtonDown(0))
        {
            if(selectionX >= 0 && selectionY >= 0)
            {
                if(selectedChessman == null )
                {
                    nM.CommandSelectPawn(new Vector3(selectionX, selectionY, 1));
                    //int tempx = (int)nM.secPawn.x;
                    //int tempy = (int)nM.secPawn.y;
                }
                else if (selectedChessman != null)
                {
                    nM.CommandMovePawn(new Vector3(selectionX, selectionY, 1));
                }
            }
        }
    }

    private void UpdateHand(List<Hand> a)
    {
        RaycastHit hit;
        Vector3 temp = a[0].PalmPosition.ToVector3();
        mano = GameObject.Find("Interaction Hand (Right)/Palm Transform");
        piecePositionWithoutX = (int)(mano.transform.position.x);
        piecePositionWithoutY = (int)(mano.transform.position.z);
        

        //Este es el ray cast que puedo descomentar por si se necesita
        /*if(Physics.Raycast(mano.transform.position, a[0].PalmNormal.ToVector3() , out hit, 2.5f, LayerMask.GetMask("ChessPlane")))
        {
            piecePositionX = (float)Math.Round(hit.point.x, 1);
            piecePositionY = (float)Math.Round(hit.point.z, 1);
            if (piecePositionY > hit.point.z && piecePositionX > hit.point.x)
            {
                piecePositionX = piecePositionX - 0.1f;
                piecePositionY = piecePositionY - 0.1f;
            }
            if (piecePositionY < hit.point.z && piecePositionX > hit.point.x)
            {
                piecePositionX = piecePositionX - 0.1f;
            }
            if (piecePositionY > hit.point.z && piecePositionX < hit.point.x)
            {
                piecePositionY = piecePositionY - 0.1f;
            }
            else
            {
                piecePositionX = -1.0f;
                piecePositionY = -1.0f;
            }

        }*/
        //Debug.Log(piecePositionX + "La posicion y" + piecePositionY);
    }

    void UpdateHandLeft(List<Hand> a)
    {
        RaycastHit hit;
        //Vector3 temp = hands[1].PalmPosition.ToVector3();
        manoLeft = GameObject.Find("Interaction Hand (Left)/Palm Transform");
        //if (a[1].IsLeft)
            //Debug.Log("si es la isquiera");
        Debug.DrawLine(manoLeft.transform.position, a[1].PalmNormal.ToVector3(), Color.black);
        if (Physics.Raycast(manoLeft.transform.position, a[1].PalmNormal.ToVector3(), out hit, 2.5f, LayerMask.GetMask("ChessPlane")))
        {
            Debug.Log(hit.point);
            pieceLeftPositionX = (int)(hit.point.x);
            pieceLeftPositionY = (int)(hit.point.z);
            Debug.DrawLine(
                Vector3.forward * pieceLeftPositionY + Vector3.right * pieceLeftPositionX,
                Vector3.forward * (pieceLeftPositionY + 1) + Vector3.right * (pieceLeftPositionX + 1), Color.red);
            Debug.DrawLine(
                Vector3.forward * (pieceLeftPositionY + 1) + Vector3.right * pieceLeftPositionX,
                Vector3.forward * pieceLeftPositionY + Vector3.right * (pieceLeftPositionX + 1), Color.red);
        }
        else
        {
            pieceLeftPositionX = -1.0f;
            pieceLeftPositionY = -1.0f;
        }
        //Debug.Log(pieceLeftPositionX + "La posicion y" + pieceLeftPositionY);
    }

    private void SelectChessman(int x, int y)
    {
        if(Chessmans[x, y] == null)
        {
            return;
        }
        if(Chessmans[x, y].isWhite != isWhiteTurn)
        {
            return;
        }
        bool hasAtleastOneMove = false;

        allowedMoves = Chessmans[x, y].PossibleMove();

        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                if (allowedMoves[i, j])
                    hasAtleastOneMove = true;   
            }
        }

        selectedChessman = Chessmans[x, y];
        previousMat = selectedChessman.GetComponent<MeshRenderer>().material;
        selectedMat.mainTexture = previousMat.mainTexture;
        selectedChessman.GetComponent<MeshRenderer>().material = selectedMat;
        BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);
    }

    private void MoveChessman(int x, int y)
    {
        if(allowedMoves[x,y])
        {
            ChessMan c = Chessmans[x, y];

            if(c != null)
            {
                // Comer una pieza

                // Si es el rey
                if (c.GetType() == typeof(King))
                {
                    // Fin de juego y retornar
                    EndGame();
                    return;
                }

                activeChessman.Remove(c.gameObject);
                Destroy(c.gameObject);
            }

            if (x == EnPassantMove[0] && y == EnPassantMove[1])
            {
                if (isWhiteTurn)
                {
                    c = Chessmans[x, y - 1];
                }
                else
                {
                    c = Chessmans[x, y + 1];
                }
                activeChessman.Remove(c.gameObject);
                Destroy(c.gameObject);

            }
            EnPassantMove[0] = -1;
            EnPassantMove[1] = -1;
            if(selectedChessman.GetType() == typeof(Pawn))
            {
                if ( y == 7 )
                {
                    activeChessman.Remove(selectedChessman.gameObject);
                    Destroy(selectedChessman.gameObject);
                    SpawnChessman(1, x, y);
                    selectedChessman = Chessmans[x, y];
                    
                }
                else if (y == 0)
                {
                    activeChessman.Remove(selectedChessman.gameObject);
                    Destroy(selectedChessman.gameObject);
                    SpawnChessman(7, x, y);
                    selectedChessman = Chessmans[x, y];
                }
                if (selectedChessman.CurretnY == 1 && y == 3)
                {
                    EnPassantMove[0] = x;
                    EnPassantMove[1] = y - 1;
                }
                else if(selectedChessman.CurretnY == 6 && y == 4)
                {
                    EnPassantMove[0] = x;
                    EnPassantMove[1] = y + 1;
                }
            }

            Chessmans[selectedChessman.CurrentX, selectedChessman.CurretnY] = null;
            selectedChessman.transform.position = GetTileCenter(x, y);
            selectedChessman.SetPosition(x, y);
            Chessmans[x, y] = selectedChessman;
            isWhiteTurn = !isWhiteTurn;

        }

        selectedChessman.GetComponent<MeshRenderer>().material = previousMat;
        BoardHighlights.Instance.Hidehighlights(); 
        selectedChessman = null;
    }

    private void UpdateSelection()
    {
        if (!Camera.main)
        {
            return;
        }
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("ChessPlane")))
        {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
            //Debug.Log(hit.point);
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }

    private void SpawnChessman(int index, int x, int y)
    {
        GameObject go = Instantiate(chessmanPrefabs[index], GetTileCenter(x,y), orientation) as GameObject;
        go.transform.SetParent(transform);
        Chessmans[x, y] = go.GetComponent<ChessMan>();
        Chessmans[x, y].SetPosition(x, y);
        activeChessman.Add(go);
    }

    private void SpawnChessmanWhites(int index, int x, int y)
    {
        GameObject go = Instantiate(chessmanPrefabs[index], GetTileCenter(x,y), Quaternion.identity) as GameObject;
        go.transform.SetParent(transform);
        Chessmans[x, y] = go.GetComponent<ChessMan>();
        Chessmans[x, y].SetPosition(x, y);
        activeChessman.Add(go);
    }

    private void SpawnAllChessmans()
    {
        activeChessman = new List<GameObject>();
        Chessmans = new ChessMan[8,8];
        EnPassantMove = new int[2] { -1, -1 };

        // Primero las blancas
        // Rey Blanco
        SpawnChessman(0, 4, 0);

        // Reina Blanca
        SpawnChessman(1, 3, 0);

        // Torres blancas
        SpawnChessman(2, 0, 0);
        SpawnChessman(2, 7, 0);

        // Alfiles Blancos
        SpawnChessmanWhites(3, 2, 0);
        SpawnChessmanWhites(3, 5, 0);

        // Caballos Blancos
        SpawnChessmanWhites(4, 1, 0);
        SpawnChessmanWhites(4, 6, 0);

        // Peones Blancos
        for(int i = 0; i < 8; i++)
        {
            SpawnChessman(5, i, 1);
        }

        // Segundo las negras
        // Rey Negro
        SpawnChessman(6, 4, 7);

        // Reina Negra
        SpawnChessman(7, 3, 7);

        // Torres Negras
        SpawnChessman(8, 0, 7);
        SpawnChessman(8, 7, 7);

        // Alfiles Negros
        SpawnChessman(9, 2, 7);
        SpawnChessman(9, 5, 7);

        // Caballos Negros
        SpawnChessman(10, 1, 7);
        SpawnChessman(10, 6, 7);

        // Peones Negros
        for (int i = 0; i < 8; i++)
        {
            SpawnChessman(11, i, 6);
        }

    }

    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        return origin;

    }

    private void DrawChessBoard()
    {
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heigthLine = Vector3.forward * 8;

        for(int i = 0; i <= 8; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);
            for(int j = 0; j <= 8; j++)
            {
                start = Vector3.right * i;
                Debug.DrawLine(start, start + heigthLine);
            }
        }

        if(selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(
            Vector3.forward * selectionY + Vector3.right * selectionX,
            Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));

            Debug.DrawLine(
            Vector3.forward * (selectionY+1) + Vector3.right * selectionX,
            Vector3.forward * selectionY + Vector3.right * (selectionX + 1));
        }
    }

    private void EndGame()
    {
        if (isWhiteTurn)
        {
            Debug.Log("White team wins");
        }
        else
        {
            Debug.Log("Black team wins");
        }
        foreach(GameObject go in activeChessman)
        {
            Destroy(go);
        }
        isWhiteTurn = true;
        BoardHighlights.Instance.Hidehighlights();
        SpawnAllChessmans();
    }


}
