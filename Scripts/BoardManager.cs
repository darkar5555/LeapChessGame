using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Leap;
using Leap.Unity;
public class BoardManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static BoardManager Instance { set; get; }
    private bool[,] allowedMoves { set; get; }
    public Chessman[,] Chessmans { set; get; }
    private Chessman selectedChessman;
    public Chessman selecionado;
    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.05f;

    private float piecePositionX = -1;
    private float piecePositionY = -1;

    private float pieceLeftPositionX = -1;
    private float pieceLeftPositionY = -1;

    private float piecePositionWithoutX = -1;
    private float piecePositionWithoutY = -1;
    private bool grasped = false;

    Controller controller;
    Frame frame;
    List<Hand> hands;


    private float selectionX = -1.0f;
    private float selectionY = -1.0f;

    public GameObject mano;
    public GameObject manoLeft;

    public List<GameObject> chessmanPrefabs;
    public List<GameObject> activeChessman;

    public bool isWhiteTurn = true;

    //public List<GameObject> activeChessman { get => activeChessman; set => activeChessman = value; }

    private void Start()
    {
        Instance = this;
        SpawnAllChessmans();

    }

    private void Update()
    {
        UpdateSelection();
        DrawChessboard();

        controller = new Controller();
        Frame frame = controller.Frame();
        List<Hand> hands = frame.Hands;
        UpdateHand(hands);
        UpdateHandLeft(hands);
        //selectedChessman = Chessmans[4, 1];
        //sDebug.Log(selectedChessman);
        //Debug.Log(hands[0].GrabAngle);
        //Debug.Log("ahora que paso");
        if ( Input.GetMouseButtonDown(0))
        {
            Debug.Log("logre entrarn aqui");
            if (piecePositionWithoutX >= 0.0f && piecePositionWithoutY >= 0.0f)
            {

                if (selectedChessman == null)
                {
                    Debug.Log("podemos mover");
                    Debug.Log((int)(piecePositionWithoutX*10.0f) + " " + (int)(piecePositionWithoutY*10.0f));
                    SelectChessman((int)(piecePositionWithoutX * 10.0f), (int)(piecePositionWithoutY * 10.0f));
                    //SelectChessman((int)(piecePositionWithoutX * 10.0f), (int)(piecePositionWithoutY * 10.0f));
                    selectedChessman.transform.position = mano.transform.position;
                    Debug.Log(selectedChessman);
                }
                /*if(selectedChessman != null && hands[0].GrabAngle < 1.4f)
                {
                    MoveChessman(4, 4);
                    Debug.Log("Entre en el else");
                }*/
                else
                {
                    // Mueve la ficha
                    //MoveChessman((int)(piecePositionX * 10.0f), (int)(piecePositionY * 10.0f));
                    MoveChessman(4, 4);
                    Debug.Log("Entre en el else");
                    //Debug.Log(selectedChessman.pieceGraspedX + "Por favor la posicion y " + selectedChessman.pieceGraspedY);

                }
            }
        }
        //if (hands[0].GrabAngle < 1.5 && Input.GetMouseButtonDown(0))
        //{
         //   MoveChessman(4, 4);
          //  Debug.Log("moviendo el chessman");
        //}
        /*if (Input.GetMouseButtonDown(0))
        {
            if (selectionX >= 0.0f && selectionY >= 0.0f)
            {

                if (selectedChessman == null)
                {
                    // Selecciona una pieza
                    //SelectChessman(piecePositionX, piecePositionY);
                    //SelectChessmanHand();
                    //Debug.Log("Este es el seleccionado " +selecionado);
                    Debug.Log((int)(selectionX * 10.0f) + " " + (int)(selectionY * 10.0f));
                    SelectChessman((int)(selectionX * 10.0f), (int)(selectionY * 10.0f));
                    //Debug.Log()
                    Debug.Log("podemos mover");
                    //selectedChessman = Chessmans[4, 1];
                    Debug.Log(selectedChessman);
                    //Debug.Log(selectedChessman.pieceGraspedX + "Por favor la posicion y " + selectedChessman.pieceGraspedY);
                    //Debug.Log(selectedChessman.grasped);
                }
                else
                {
                    // Mueve la ficha
                    MoveChessman((int)(selectionX * 10.0f), (int)(selectionY * 10.0f));
                    //Debug.Log(selectedChessman.pieceGraspedX + "Por favor la posicion y " + selectedChessman.pieceGraspedY);

                }
            }
        }*/
    }

    // Aqui se vera los movimientos para la fichas aqui podemos sacar o modificar para el ongrasp event

    private void SelectChessman(int x, int y)
    {
        if (Chessmans[x, y] == null)
        {
            return;
        }
        if (Chessmans[x, y].isWhite != isWhiteTurn)
        {
            return;
        }
        //bool[,] temp;

        allowedMoves = Chessmans[x, y].PossibleMove();
        selectedChessman = Chessmans[x, y];
        BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);

    }


    private void MoveChessman(float x, float y)
    {
        if (allowedMoves[(int)x, (int)y])
        {
            int tempx = (int)(selectedChessman.CurrentX * 10.0f);
            int tempy = (int)(selectedChessman.CurrentY * 10.0f);
            Chessmans[tempx, tempy] = null;
            selectedChessman.transform.position = GetTileCenter(x / 10.0f, y / 10.0f);
            selectedChessman.SetPosition(x, y);
            //Debug.Log((int)(x * 10.0f));
            //Debug.Log((int)(y * 10.0f));
            Chessmans[(int)x, (int)y] = selectedChessman;
            isWhiteTurn = !isWhiteTurn;
        }
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
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 2.5f, LayerMask.GetMask("ChessPlane")))
        {
            //Debug.Log(hit.point);
            selectionX = (float)Math.Round(hit.point.x, 1);
            selectionY = (float)Math.Round(hit.point.z, 1);
            if (selectionY > hit.point.z && selectionX > hit.point.x)
            {
                selectionX = selectionX - 0.1f;
                selectionY = selectionY - 0.1f;
                // Debug.Log(selectionX);
            }
            if (selectionY < hit.point.z && selectionX > hit.point.x)
            {
                selectionX = selectionX - 0.1f;
                // Debug.Log(selectionX);
            }
            if (selectionY > hit.point.z && selectionX < hit.point.x)
            {
                selectionY = selectionY - 0.1f;
                // Debug.Log(selectionY);
            }
            /*(if (selectionY < hit.point.z && selectionX < hit.point.x)
            {
                selectionX = selectionX - 0.1f;
                selectionY = selectionY - 0.1f;
            }*/
            //Debug.Log(Math.Round(hit.point.x, 1));
            //Debug.Log(hit.point);
        }
        else
        {
            selectionX = -1.0f;
            selectionY = -1.0f;
        }


    }

    private void UpdateHand(List<Hand> a)
    {
        //controller = new Controller();
        //Frame frame = controller.Frame();
        //List<Hand> hands = frame.Hands;
        //if (!Camera.main)
        //{
        //    return;
        //}
        RaycastHit hit;
        Vector3 temp = a[0].PalmPosition.ToVector3();
        mano = GameObject.Find("Interaction Hand (Right)/Palm Transform");
        piecePositionWithoutX = (float)Math.Round(mano.transform.position.x, 1);
        piecePositionWithoutY = (float)Math.Round(mano.transform.position.z, 1);
        //Debug.Log("Posicion x" + piecePositionWithoutX + " " + "posicion Y" + piecePositionWithoutY);

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
        if (a[1].IsLeft)
            Debug.Log("si es la isquiera");
        
        if (Physics.Raycast(manoLeft.transform.position, a[1].PalmNormal.ToVector3(), out hit, 2.5f, LayerMask.GetMask("ChessPlane")))
        {
            Debug.Log(hit.point);
            pieceLeftPositionX = (float)Math.Round(hit.point.x, 1);
            pieceLeftPositionY = (float)Math.Round(hit.point.z, 1);
            if (pieceLeftPositionY > hit.point.z && piecePositionX > hit.point.x)
            {
                pieceLeftPositionX = pieceLeftPositionX - 0.1f;
                pieceLeftPositionY = pieceLeftPositionY - 0.1f;
            }
            if (pieceLeftPositionY < hit.point.z && pieceLeftPositionX > hit.point.x)
            {
                pieceLeftPositionX = pieceLeftPositionX - 0.1f;
            }
            if (pieceLeftPositionY > hit.point.z && pieceLeftPositionX < hit.point.x)
            {
                pieceLeftPositionY = pieceLeftPositionY - 0.1f;
            }
            else
            {
                pieceLeftPositionX = -1.0f;
                pieceLeftPositionY = -1.0f;
            }

        }
        //Debug.Log(pieceLeftPositionX + "La posicion y" + pieceLeftPositionY);
    }

   

    private void SpawnChessman(int index, float x2, float y2)
    {
        GameObject go = Instantiate(chessmanPrefabs[index], GetTileCenter(x2, y2), Quaternion.identity) as GameObject;
        go.transform.SetParent(transform);
        Chessmans[(int)(x2 * 10.0f), (int)(y2 * 10.0f)] = go.GetComponent<Chessman>();
        Chessmans[(int)(x2 * 10.0f), (int)(y2 * 10.0f)].SetPosition(x2, y2);
        //Debug.Log((int)(x2 * 10.0f));
        //Debug.Log((int)(y2 * 10.0f));
        activeChessman.Add(go);
    }

    public Vector3 GetTileCenter(float x, float y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        return origin;
    }

    private void SpawnAllChessmans()
    {
        activeChessman = new List<GameObject>();
        Chessmans = new Chessman[8, 8];
        // Dibujar las blancas
        // Rey
        SpawnChessman(0, 0.4f, 0.0f);

        //Reina
        SpawnChessman(1, 0.3f, 0.0f);

        //Torres
        SpawnChessman(2, 0.4f, 0.3f);
        SpawnChessman(2, 0.7f, 0.0f);

        // Alfiles
        SpawnChessman(3, 0.2f, 0.0f);
        SpawnChessman(3, 0.5f, 0.0f);

        //Caballos
        SpawnChessman(4, 0.1f, 0.0f);
        SpawnChessman(4, 0.6f, 0.0f);

        //Peones
        for (float i = 0.0f; i < 0.8f; i = i + 0.1f)
        {
            SpawnChessman(5, i, 0.1f);

        }

        // Dibujar las negras
        // Rey
        SpawnChessman(6, 0.4f, 0.7f);

        //Reina
        SpawnChessman(7, 0.3f, 0.7f);

        //Torres
        SpawnChessman(8, 0.0f, 0.7f);
        SpawnChessman(8, 0.7f, 0.7f);

        // Alfiles
        SpawnChessman(9, 0.2f, 0.7f);
        SpawnChessman(9, 0.5f, 0.7f);

        //Caballos
        SpawnChessman(10, 0.1f, 0.7f);
        SpawnChessman(10, 0.6f, 0.7f);

        //Peones
        for (float i = 0.0f; i < 0.8f; i = i + 0.1f)
        {
            SpawnChessman(11, i, 0.6f);

        }



    }

    private void DrawChessboard()
    {
        Vector3 widthLine = Vector3.right * 0.8f;
        Vector3 heigthLine = Vector3.forward * 0.8f;
        for (float i = 0.0f; i <= 0.9f; i = i + 0.1f)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);
            for (float j = 0.0f; j <= 0.9f; j = j + 0.1f)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heigthLine);
                // Debug.Log(j);
            }
        }

        // Dibujar la seleccion
        if (selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(
                Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 0.1f) + Vector3.right * (selectionX + 0.1f)
                );
            Debug.DrawLine(
                Vector3.forward * (selectionY + 0.1f) + Vector3.right * selectionX,
                Vector3.forward * selectionY + Vector3.right * (selectionX + 0.1f)
                );
        }
        //Dibujar la seleccion con la mano
        if (piecePositionX >= 0 && piecePositionY >= 0)
        {
            Debug.DrawLine(
                Vector3.forward * piecePositionY + Vector3.right * piecePositionX,
                Vector3.forward * (piecePositionY + 0.1f) + Vector3.right * (piecePositionX + 0.1f), Color.red
                );
            Debug.DrawLine(
                Vector3.forward * (piecePositionY + 0.1f) + Vector3.right * piecePositionX,
                Vector3.forward * piecePositionY + Vector3.right * (piecePositionX + 0.1f), Color.red
                );
        }

        //Dibujar el lugar de movimiento
        if (pieceLeftPositionX >= 0 && pieceLeftPositionY >= 0)
        {
            Debug.DrawLine(
                Vector3.forward * pieceLeftPositionY + Vector3.right * pieceLeftPositionX,
                Vector3.forward * (pieceLeftPositionY + 0.1f) + Vector3.right * (pieceLeftPositionX + 0.1f), Color.green
                );
            Debug.DrawLine(
                Vector3.forward * (pieceLeftPositionY + 0.1f) + Vector3.right * pieceLeftPositionX,
                Vector3.forward * pieceLeftPositionY + Vector3.right * (pieceLeftPositionX + 0.1f), Color.green
                );
        }

        //Dibujar la seleccion de la mano sin raycast
        if (piecePositionWithoutX >= 0 && piecePositionWithoutY >= 0)
        {
            Debug.DrawLine(
                Vector3.forward * piecePositionWithoutY + Vector3.right * piecePositionWithoutX,
                Vector3.forward * (piecePositionWithoutY + 0.1f) + Vector3.right * (piecePositionWithoutX + 0.1f), Color.blue
                );
            Debug.DrawLine(
                Vector3.forward * (piecePositionWithoutY + 0.1f) + Vector3.right * piecePositionWithoutX,
                Vector3.forward * piecePositionWithoutY + Vector3.right * (piecePositionWithoutX + 0.1f), Color.blue
                );
        }
    }

    /*public void PiecePosition()
    {
        piecePositionX = this.transform.position.x;
        piecePositionY = this.transform.position.z;
        grasped = true;
        Debug.Log((int)(piecePositionX * 10.0f) + " " + (int)(piecePositionY * 10.0f));
        selectedChessman = Chessmans[(int)(piecePositionX*10.0f), (int)(piecePositionY*10.0f)];
        Debug.Log(selectedChessman);
    }*/

    /*public void PieceDeGrasp()
    {
        piecePositionX = -1;
        piecePositionY = -1;
        grasped = false;
    }*/
      

}
