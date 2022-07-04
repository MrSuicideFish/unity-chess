using System;
using UnityEngine;

public class MovePreviewActor : MonoBehaviour
{
    private Chessman _piece;
    private BoardCoord _targetCoord;
    
    public void SetMove(Chessman piece, BoardCoord targetCoord)
    {
        _piece = piece;
        _targetCoord = targetCoord;
    }

    public void OnMouseDown()
    {
        GameManager.SelectMove(_piece, _targetCoord);
    }
}