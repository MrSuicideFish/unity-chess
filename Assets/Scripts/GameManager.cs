using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance;
    public static ChessmanColor PlayerColor { get; private set; }
    public static ChessmanColor TurnColor { get; private set; }

    private Chessmaster _chessmaster;
    private Chessboard _chessboard;
    private PlayerController _playerController;

    private void Start()
    {
        Instance = this;
        _playerController = this.GetComponent<PlayerController>();
        PlayerColor = UnityEngine.Random.Range(0, 100) > 50 ? ChessmanColor.Black : ChessmanColor.White;
        
        _chessboard = SetupGameboard();
        _chessmaster = new Chessmaster(ref _chessboard);
    }

    private Chessboard SetupGameboard()
    {
        ChessmanInfoCollection infoCollection = new ChessmanInfoCollection();

        var allViews = FindObjectsOfType<ChessmanView>();
        foreach(var view in allViews)
        {
            infoCollection.Add(new ChessmanInfo(view.rank, view.color), view);
        }
        Chessboard newBoard = new Chessboard(infoCollection);
        newBoard.SetView(FindObjectOfType<ChessboardView>());
        
        return newBoard;
    }

    private IEnumerator EndGame(ChessmanColor winner)
    {
        yield return null;
    }

    public static void Select(Chessman piece)
    {
        // show preview moves
        BoardCoord[] validMoves = Instance._chessmaster.GetValidMoves(piece.rank, piece.coord, piece.color);
        if (validMoves != null)
        {
            foreach (var coord in validMoves)
            {
                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                cube.transform.position = ChessboardView.GetWorldPosition(coord);
            }
        }
    }
}