using System.Collections.Generic;
using UnityEngine;

public class MoveUtility
{
    public static Chessboard.BoardCoord[] GetValidMoves(Chessboard board, Chessman piece)
    {
        List<Chessboard.BoardCoord> coord = new List<Chessboard.BoardCoord>();

        Chessboard.BoardCoord[] possibleMoves = GetCoordsForMove(piece);
        return possibleMoves;
        
        if (coord.Count == 0)
        {
            return null;
        }
        
        return coord.ToArray();
    }

    public static Chessboard.BoardCoord[] GetCoordsForMove(Chessman piece)
    {
        switch (piece.rank)
        {
            case ChessmanRank.Pawn:
                return GetPawnMoves(piece);
            case ChessmanRank.Rook:
                return GetRookMoves(piece);
        }

        return null;
    }

    private static Chessboard.BoardCoord[] GetPawnMoves(Chessman pawn)
    {
        if (!pawn.hasMoved)
        {
            return new Chessboard.BoardCoord[]
            {
                pawn.coordinate.Add(Vector2.up * 2, pawn.color),
                pawn.coordinate.Add(Vector2.right, pawn.color)
            };
        }
        else
        {
            return new Chessboard.BoardCoord[]
            {
                pawn.coordinate.Add(Vector2.up, pawn.color)
            };
        }
    }

    private static Chessboard.BoardCoord[] GetRookMoves(Chessman rook)
    {
        List<Chessboard.BoardCoord> coords = new List<Chessboard.BoardCoord>();
        
        //fwd
        for (int f = (int) rook.coordinate.file; f < 8; f++) coords.Add(rook.coordinate.Add(Vector2.up*f, 0));
        
        //bck
        for (int b = (int) rook.coordinate.file; b > 0; b--) coords.Add(rook.coordinate.Add(Vector2.down*b, 0));
        
        //lft
        for (int l = (int) rook.coordinate.rank; l > 0; l--) coords.Add(rook.coordinate.Add(Vector2.left*l, 0));
        
        //rgt
        for (int r = (int) rook.coordinate.rank; r < 8; r++) coords.Add(rook.coordinate.Add(Vector2.right*r, 0));
        
        return coords.ToArray();
    }
}