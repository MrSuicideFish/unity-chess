using UnityEngine;


public enum ChessmanRank
{
    Pawn,
    Rook,
    Knight,
    Bishop,
    Queen,
    King
}

public enum ChessmanColor
{
    Black,
    White
}

public class Chessman : MonoBehaviour
{
    public ChessmanRank rank;
    public ChessmanColor color;
    public Chessboard.BoardCoord coordinate;
    
    public virtual void Select(){}
    public virtual void ConfirmMove(){}
}