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
    
    // game control
    public static bool GameHasStarted { get; private set; }
    public static bool GameHasEnded { get; private set; }

    private Chessmaster _chessmaster;
    private Chessboard _chessboard;
    private PlayerController _playerController;

    private void Start()
    {
        Instance = this;
        
        // setup clocks
        _blackClock = new PlayerClock(ChessmanColor.Black, 5);
        _whiteClock = new PlayerClock(ChessmanColor.White, 5);
        
        // human player controls
        _playerController = this.GetComponent<PlayerController>();
        PlayerColor = ChessmanColor.White; // human always white
        
        // game logic
        _chessboard = SetupGameboard();
        _chessmaster = new Chessmaster(ref _chessboard);
        
        // start game
        StartCoroutine(PlayGame());
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

    private IEnumerator PlayGame()
    {
        TurnColor = ChessmanColor.Black;
        GameEndResult result = GameEndResult.NotApplicable;
        GameHasStarted = true;
        GameHasEnded = false;
        
        while (!_chessmaster.IsCheckmate(out result))
        {
            // swap turns
            TurnColor = TurnColor == ChessmanColor.White ? ChessmanColor.Black : ChessmanColor.White;
            _chessmaster.StartTurn(TurnColor);
            HideMovePreview();g
            
            // wait for turn to move
            while (!_chessmaster.HasMoved)
            {
                if (GetTurnClock().Step() <= 0.0f)
                {
                    yield return EndGame(TurnColor == ChessmanColor.White
                        ? GameEndResult.WhiteTimeout
                        : GameEndResult.BlackTimeout);
                }

                yield return null;
            }
            yield return null;
        }
        
        yield return EndGame(result);
    }

    private IEnumerator EndGame(GameEndResult result)
    {
        Debug.Log("Game Over! Result: " + result);
        GameHasEnded = true;
        HideMovePreview();
        StopAllCoroutines();
        yield return null;
    }

    private PlayerClock _whiteClock;
    private PlayerClock _blackClock;
    private PlayerClock GetTurnClock()
    {
        if (TurnColor == ChessmanColor.Black)
        {
            return _blackClock;
        }

        return _whiteClock;
    }

    private static List<GameObject> previewSquares = new List<GameObject>();

    private static void HideMovePreview()
    {
        if (previewSquares.Count > 0)
        {
            for (int i = 0; i < previewSquares.Count; i++)
            {
                GameObject.Destroy(previewSquares[i]);
            }
        }
    }

    private static void ShowMovePreview(Chessman piece, BoardCoord[] coords)
    {
        if (coords != null)
        {
            foreach (var coord in coords)
            {
                if (Instance._chessboard.GetSpace(coord) != null)
                {
                    var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                    cube.transform.position = ChessboardView.GetWorldPosition(coord);
                    cube.AddComponent<MovePreviewActor>().SetMove(piece, coord);
                    
                    previewSquares.Add(cube);
                }
            }
        }
    }
    
    public static void SelectChessman(Chessman piece)
    {
        if (GameHasEnded) return;
        HideMovePreview();

        // show preview moves
        BoardCoord[] validMoves = Instance._chessmaster.GetValidMoves(piece);
        ShowMovePreview(piece, validMoves);
    }

    public static void SelectMove(Chessman piece, BoardCoord toCoord)
    {
        Debug.Log($"Do Move: {piece.rank} to {toCoord.ToString()}");
        Instance._chessmaster.MoveTo(piece.coord, toCoord);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 200, 24), $"Player Color: {PlayerColor}");
        GUI.Label(new Rect(0, 24, 200, 24), $"Turn: {TurnColor}");
        GUI.Label(new Rect(0, 40, 200, 24), $"Clock: {GetTurnClock().Time}");
    }
}