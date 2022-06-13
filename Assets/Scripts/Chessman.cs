using System.Collections.Generic;
using System.Text;
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

public class ChessmanCollection : Dictionary<ChessmanRank, List<Chessman>>
{
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var v in this[ChessmanRank.King]) sb.AppendLine(v.rank.ToString());
        foreach (var v in this[ChessmanRank.Queen]) sb.AppendLine(v.rank.ToString());
        foreach (var v in this[ChessmanRank.Bishop]) sb.AppendLine(v.rank.ToString());
        foreach (var v in this[ChessmanRank.Knight]) sb.AppendLine(v.rank.ToString());
        foreach (var v in this[ChessmanRank.Rook]) sb.AppendLine(v.rank.ToString());
        foreach (var v in this[ChessmanRank.Pawn]) sb.AppendLine(v.rank.ToString());
        return sb.ToString();
    }
}

public static class ChessmanUtility
{
    public static ChessmanCollection ToChessmanCollection(this IEnumerable<Chessman> query)
    {
        ChessmanCollection newCollection = new ChessmanCollection();
        
        newCollection.Add(ChessmanRank.King, new List<Chessman>());
        newCollection.Add(ChessmanRank.Queen, new List<Chessman>());
        newCollection.Add(ChessmanRank.Bishop, new List<Chessman>());
        newCollection.Add(ChessmanRank.Knight, new List<Chessman>());
        newCollection.Add(ChessmanRank.Rook, new List<Chessman>());
        newCollection.Add(ChessmanRank.Pawn, new List<Chessman>());

        foreach (Chessman chessman in query)
        {
            if (!newCollection[chessman.rank].Contains(chessman))
            {
                newCollection[chessman.rank].Add(chessman);
            }
        }
        return newCollection;
    }

    public static uint ClampRank(uint c)
    {
        uint result = c;
        if (c >= 'A' && c <= 'Z') result -= 64;
        else if (c >= 'a' && c <= 'z') result -= 96;
        return result;
    }

    public static Chessboard.BoardCoord ToBoardCoord(this string str)
    {
        Chessboard.BoardCoord coord = new Chessboard.BoardCoord();
        if (str.Length <= 2)
        {
            coord.rank = ClampRank(str[0]);
            coord.file = uint.Parse(str[1].ToString());
        }
        return coord;
    }
}

public class Chessman : MonoBehaviour
{
    public ChessmanRank rank;
    public ChessmanColor color;
    public Chessboard.BoardCoord coordinate;

    public void MoveTo(Chessboard board, uint rank, uint file)
    {
        MoveTo(board, new Chessboard.BoardCoord(rank, file));
    }

    public void MoveTo(Chessboard board, Chessboard.BoardCoord coord)
    {
        board.GetSpace(coord).piece = this;
        this.transform.position = board.GetWorldPosition(coord);
        coordinate = coord;
    }
    
    public virtual void Select(){}
    public virtual void ConfirmMove(){}
}