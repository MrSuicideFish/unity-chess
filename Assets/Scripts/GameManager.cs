using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerController playerController { get; private set; }
    public ChessmanColor playerColor { get; private set; }
    
    public Chessman BlackKing { get; private set; }
    public Chessman WhiteKing { get; private set; }
    
    public Chessboard board;
    public Chessman[] chessmen;

    public ChessmanColor turnColor { get; private set; } = ChessmanColor.White;

    private bool hasMoved = false;

    private void OnEnable()
    {
        Instance = this;
    }

    private void Start()
    {
        playerController = this.GetComponent<PlayerController>();
        playerColor = UnityEngine.Random.Range(0, 100) > 50 ? ChessmanColor.Black : ChessmanColor.White;
        Setup(ChessmanColor.Black);
        Setup(ChessmanColor.White);
        StartCoroutine(BeginTurn(turnColor = ChessmanColor.White));
    }

    private void Setup(ChessmanColor color)
    {
        ChessmanCollection collection = chessmen.Where(
            x => x.color == color).ToChessmanCollection();

        uint upperClassRank = color == ChessmanColor.Black ? (uint) 1 : (uint) 8;
        uint lowerClassRank = color == ChessmanColor.Black ? (uint) 2 : (uint) 7;
        
        collection[ChessmanRank.Rook][0].DoMove(board, 'A', upperClassRank);
        collection[ChessmanRank.Knight][0].DoMove(board, 'B', upperClassRank);
        collection[ChessmanRank.Bishop][0].DoMove(board, 'C', upperClassRank);
        collection[ChessmanRank.King][0].DoMove(board, 'D', upperClassRank);
        collection[ChessmanRank.Queen][0].DoMove(board, 'E', upperClassRank);
        collection[ChessmanRank.Bishop][1].DoMove(board, 'F', upperClassRank);
        collection[ChessmanRank.Knight][1].DoMove(board, 'G', upperClassRank);
        collection[ChessmanRank.Rook][1].DoMove(board, 'H', upperClassRank);

        if (color == ChessmanColor.Black)
        {
            BlackKing = collection[ChessmanRank.King][0];
        }
        else
        {
            WhiteKing = collection[ChessmanRank.King][0];
        }

        List<Chessman> pawns = collection[ChessmanRank.Pawn]; 
        for (int p = 0; p < pawns.Count; p++)
        {
            pawns[p].DoMove(board, new Chessboard.BoardCoord((uint) p + 1, lowerClassRank));
        }
    }

    private IEnumerator BeginTurn(ChessmanColor color)
    {
        turnColor = color;
        while (!hasMoved)
        {
            yield return null;
        }

        if (IsCheckmate())
        {
            yield return EndGame(turnColor);
        }

        yield return null;
    }

    private IEnumerator EndGame(ChessmanColor winner)
    {
        yield return null;
    }

    public void Select(Chessman piece)
    {
        Debug.Log("Selecting: " + piece);
        /*
        if (IsInCheck(out Chessman[] checkedBy))
        {
            Chessman allyKing = turnColor == ChessmanColor.White ? WhiteKing : BlackKing;
            allyKing.DoBounce();
            return;
        }*/
        
        // show preview moves
        Chessboard.BoardCoord[] validMoves = MoveUtility.GetValidMoves(board, piece);
        if (validMoves != null)
        {
            foreach (var coord in validMoves)
            {
                var cube =GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                cube.transform.position = board.GetWorldPosition(coord);
            }
        }
    }

    public void MoveTo(Chessman piece, Chessboard.BoardCoord toCoord)
    {
        Chessman existing = board.GetSpace(toCoord).piece;
        if (existing == null)
        {
            existing.gameObject.SetActive(false);
        }
        
        board.GetSpace(piece.coordinate).piece = null;
        board.GetSpace(toCoord).piece = piece;

        piece.DoMove(board, toCoord);
        piece.hasMoved = true;
        hasMoved = true;
    }

    public bool IsInCheck(out Chessman[] checkedBy)
    {
        Chessman allyKing = turnColor == ChessmanColor.White ? WhiteKing : BlackKing;
        checkedBy = null;
        return false;
    }

    private bool IsCheckmate()
    {
        Chessman enemyKing = turnColor == ChessmanColor.White ? BlackKing : WhiteKing;
        return false;
    }

    private void OnGUI()
    {
        
    }
}