using UnityEngine;

public class Chessmaster
{
    private Chessboard _virtualBoard;
    private Chessboard _gameBoard;
    private ChessEngine _engine;
    
    public bool HasMoved { get; private set; }

    public Chessmaster(ref Chessboard board)
    {
        _virtualBoard = new Chessboard(board);
        _gameBoard = board;
        _engine = new ChessEngine();
    }

    public void StartTurn(ChessmanColor turnColor)
    {
        HasMoved = false;
    }

    public void DoMove(string command)
    {
        HasMoved = true;
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
        HasMoved = true;
    }
    
    public BoardCoord[] GetValidMoves(Chessman chessman)
    {
        BoardCoord[] allMoves = MoveUtility.GetMoves(chessman.rank, chessman.coord, chessman.color);
        return allMoves;
    }

    public bool IsCheckmate(out GameEndResult result)
    {
        result = GameEndResult.NotApplicable;
        return false;
    }
}