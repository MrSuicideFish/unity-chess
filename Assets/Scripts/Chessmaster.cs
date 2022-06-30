using UnityEngine;

public class Chessmaster
{
    private Chessboard _virtualBoard;
    private Chessboard _gameBoard;

    public Chessmaster(ref Chessboard board)
    {
        _virtualBoard = new Chessboard(board);
        _gameBoard = board;
    }
    
    public void MoveTo(BoardCoord from, BoardCoord to)
    {
        BoardSpace fromBoardSpace = _gameBoard.GetSpace(from);
        BoardSpace toBoardSpace = _gameBoard.GetSpace(to);
        
        Chessman existingFrom = fromBoardSpace.piece;
        Chessman existingTo = toBoardSpace.piece;
        
        if (existingFrom == null) return;
        if (existingTo != null)
        {
            toBoardSpace.piece = null;
            existingTo.Destroy();
        }
        
        existingFrom.MoveToCoord(to);
        
        // clear old space
        fromBoardSpace.piece = null;
    }
    
    public BoardCoord[] GetValidMoves(ChessmanRank rank, BoardCoord coord, ChessmanColor color)
    {
        BoardCoord[] allMoves = MoveUtility.GetMoves(rank, coord, color);
        return allMoves;
    }

    public bool IsCheckmate()
    {
        return false;
    }
}