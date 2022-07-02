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
        TurnColor = ChessmanColor.White;

        while (!_chessmaster.IsCheckmate())
        {
            
            yield return null;
        }
        
        yield return EndGame(GameEndResult.BlackChessmate);
    }

    private IEnumerator EndGame(GameEndResult result)
    {
        Debug.Log("Game Over! Result: " + result);
        yield return null;
    }

    public static void Move(string from, string to)
    {
        
    }

    private static List<GameObject> previewSquares = new List<GameObject>();
    public static void SelectChessman(Chessman piece)
    {
        if (previewSquares.Count > 0)
        {
            for (int i = 0; i < previewSquares.Count; i++)
            {
                GameObject.Destroy(previewSquares[i]);
            }
        }
        
        // show preview moves
        BoardCoord[] validMoves = Instance._chessmaster.GetValidMoves(piece);
        if (validMoves != null)
        {
            foreach (var coord in validMoves)
            {
                if (Instance._chessboard.GetSpace(coord) != null)
                {
                    var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                    cube.transform.position = ChessboardView.GetWorldPosition(coord);
                    
                    previewSquares.Add(cube);
                }
            }
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 200, 24), $"Player Color: {PlayerColor}");
        GUI.Label(new Rect(0, 24, 200, 24), $"Turn: {TurnColor}");
    }
}