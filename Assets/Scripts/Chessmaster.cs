using UnityEngine;

public class Chessmaster
{
    private Chessboard _virtualBoard;
    private Chessboard _gameBoard;

    private ChessEngine _engine;

    public Chessmaster(ref Chessboard board)
    {
        _virtualBoard = new Chessboard(board);
        _gameBoard = board;
        _engine = new ChessEngine();
    }

    public void DoMove(string command)
    {
        
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
    
    public BoardCoord[] GetValidMoves(Chessman chessman)
    {
        BoardCoord[] allMoves = MoveUtility.GetMoves(chessman.rank, chessman.coord, chessman.color);
        return allMoves;
    }

    public bool IsCheckmate()
    {
        return false;
    }
}