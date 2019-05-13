using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class PositionPieceGrasp : MonoBehaviour
{
    public float positionPieceX = -1.0f;
    public float positionPieceY = -1.0f;

    public void PieceGrasp()
    {

        positionPieceX = this.transform.position.x;
        positionPieceY = this.transform.position.z;
        Debug.Log(positionPieceX +" " + positionPieceY);
    }


}
