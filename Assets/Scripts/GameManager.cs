using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ChessmanColor playerColor;
    public Chessboard board;
    public Chessman[] chessmen;

    private IEnumerator Start()
    {
        Setup(ChessmanColor.Black);
        Setup(ChessmanColor.White);
        
        yield return null;
    }

    private void Setup(ChessmanColor color)
    {
        ChessmanCollection collection = chessmen.Where(
            x => x.color == color).ToChessmanCollection();

        uint upperClassRank = color == ChessmanColor.Black ? (uint) 1 : (uint) 8;
        uint lowerClassRank = color == ChessmanColor.Black ? (uint) 2 : (uint) 7;
        
        collection[ChessmanRank.Rook][0].MoveTo(board, 'A', upperClassRank);
        collection[ChessmanRank.Knight][0].MoveTo(board, 'B', upperClassRank);
        collection[ChessmanRank.Bishop][0].MoveTo(board, 'C', upperClassRank);
        collection[ChessmanRank.King][0].MoveTo(board, 'D', upperClassRank);
        collection[ChessmanRank.Queen][0].MoveTo(board, 'E', upperClassRank);
        collection[ChessmanRank.Bishop][1].MoveTo(board, 'F', upperClassRank);
        collection[ChessmanRank.Knight][1].MoveTo(board, 'G', upperClassRank);
        collection[ChessmanRank.Rook][1].MoveTo(board, 'H', upperClassRank);

        List<Chessman> pawns = collection[ChessmanRank.Pawn]; 
        for (int p = 0; p < pawns.Count; p++)
        {
            pawns[p].MoveTo(board, new Chessboard.BoardCoord((uint) p + 1, lowerClassRank));
        }
    }
    
    private void StartGame()
    {
        
    }

    private void OnGUI()
    {
        
    }
}