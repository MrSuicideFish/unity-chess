using System.Collections.Generic;
using UnityEngine;

public class MoveUtility
{
    public static BoardCoord[] GetMoves(ChessmanRank rank, BoardCoord coord, ChessmanColor color)
    {
        switch (rank)
        {
            case ChessmanRank.Pawn:
                return GetPawnMoves(coord, color);
            case ChessmanRank.Bishop:
                return GetBishopMoves(coord, color);
            case ChessmanRank.Rook:
                return GetRookMoves(coord, color);
            case ChessmanRank.King:
                return GetKingMoves(coord, color);
            case ChessmanRank.Knight:
                return GetKnightMoves(coord, color);
            case ChessmanRank.Queen:
                return GetQueenMoves(coord, color);
        }

        return null;
    }
    
    private static BoardCoord[] GetPawnMoves(BoardCoord coord, ChessmanColor color)
    {
        List<BoardCoord> coords = new List<BoardCoord>()
        {
            BoardCoord.Add(coord, Vector2.up, color),
            BoardCoord.Add(coord, Vector2.up * 2, color)
        };
        
        return coords.ToArray();
    }

    private static BoardCoord[] GetRookMoves(BoardCoord coord, ChessmanColor color)
    {
        List<BoardCoord> coords = new List<BoardCoord>();
        
        //fwdx
        for (int f = (int) coord.file; f < 8; f++)
        {
            BoardCoord newCoord = BoardCoord.Add(coord, new Vector2(0, f), color);
            coords.Add(newCoord);
        }
        
        //bck
        for (int b = (int) coord.file; b > 0; b--)
        {
            BoardCoord newCoord = BoardCoord.Sub(coord, new Vector2(0, b), color);
            coords.Add(newCoord);
        }
        
        //lft
        for (int l = (int) coord.rank; l > 0; l--)
        {
            BoardCoord newCoord = BoardCoord.Sub(coord, new Vector2(l, 0), color);
            coords.Add(newCoord);
        }
        
        //rgt
        for (int r = (int) coord.rank; r < 8; r++)
        {
            BoardCoord newCoord = BoardCoord.Add(coord, new Vector2(r, 0), color);
            coords.Add(newCoord);
        }
        
        return coords.ToArray();
    }

    private static BoardCoord[] GetBishopMoves(BoardCoord coord, ChessmanColor color)
    {
        
        return null;
    }

    private static BoardCoord[] GetKnightMoves(BoardCoord coord, ChessmanColor color)
    {
        List<BoardCoord> coords = new List<BoardCoord>();
        
        // up
        coords.Add(BoardCoord.Add(coord, new Vector2(1, 2), color));
        coords.Add(BoardCoord.Add(coord, new Vector2(-1, 2), color));
        
        // right
        coords.Add(BoardCoord.Add(coord, new Vector2(2, 1), color));
        coords.Add(BoardCoord.Add(coord, new Vector2(2, -1), color));
        
        // down 
        coords.Add(BoardCoord.Add(coord, new Vector2(1, -2), color));
        coords.Add(BoardCoord.Add(coord, new Vector2(-1, -2), color));
        
        // left
        coords.Add(BoardCoord.Add(coord, new Vector2(-2, 1), color));
        coords.Add(BoardCoord.Add(coord, new Vector2(-2, -1), color));
        
        return coords.ToArray();
    }

    private static BoardCoord[] GetQueenMoves(BoardCoord coord, ChessmanColor color)
    {
        List<BoardCoord> coords = new List<BoardCoord>();

        return coords.ToArray();
    }

    private static BoardCoord[] GetKingMoves(BoardCoord coord, ChessmanColor color)
    {
        List<BoardCoord> coords = new List<BoardCoord>();
        
        // cardinal
        coords.Add(BoardCoord.Add(coord, Vector2.up, color));
        coords.Add(BoardCoord.Add(coord, Vector2.right, color));
        coords.Add(BoardCoord.Add(coord, Vector2.down, color));
        coords.Add(BoardCoord.Add(coord, Vector2.left, color));
        
        // ordinal
        coords.Add(BoardCoord.Add(coord, new Vector2(1,1), color));
        coords.Add(BoardCoord.Add(coord, new Vector2(1,-1), color));
        coords.Add(BoardCoord.Add(coord, new Vector2(-1,-1), color));
        coords.Add(BoardCoord.Add(coord, new Vector2(-1,1), color));
        
        return coords.ToArray();
    }
}