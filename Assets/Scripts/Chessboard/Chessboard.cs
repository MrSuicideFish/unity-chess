using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class Chessboard
{
    public Chessman _blackKing { get; private set; }
    public Chessman _whiteKing { get; private set; }
    
    private BoardSpace[,] _data;
    private ChessboardView _view;

    public Chessboard(Chessboard board)
    {
        _data = board._data;
    }

    public Chessboard(ChessmanInfoCollection chessmanViews)
    {
        if (_data == null)
        {
            _data = new BoardSpace[8, 8];
            for (int x = 0; x < _data.GetLength(0); x++)
            {
                for(int y = 0; y < _data.GetLength(1); y++)
                {
                    _data[x, y] = new BoardSpace();
                }
            }
            
            // setup white
            SetupChessmen(ChessmanColor.White, ref chessmanViews);
            SetupChessmen(ChessmanColor.Black, ref chessmanViews);
        }
    }

    private void SetupChessmen(ChessmanColor color, ref ChessmanInfoCollection chessmanViews)
    {
        ChessmanInfo info = new ChessmanInfo();
        info.color = color;
        uint rank = color == ChessmanColor.White ? (uint)1 : (uint)8;
            
        info.rank = ChessmanRank.Rook;
        SetSpace(new BoardCoord('a', rank), info, ref chessmanViews);

        info.rank = ChessmanRank.Knight;
        SetSpace(new BoardCoord('b', rank), info, ref chessmanViews);
            
        info.rank = ChessmanRank.Bishop;
        SetSpace(new BoardCoord('c', rank), info, ref chessmanViews);
            
        info.rank = ChessmanRank.Queen;
        SetSpace(new BoardCoord('d', rank), info, ref chessmanViews);
            
        info.rank = ChessmanRank.King;
        SetSpace(new BoardCoord('e', rank), info, ref chessmanViews);
            
        info.rank = ChessmanRank.Bishop;
        SetSpace(new BoardCoord('f', rank), info, ref chessmanViews);
            
        info.rank = ChessmanRank.Knight;
        SetSpace(new BoardCoord('g', rank), info, ref chessmanViews);
            
        info.rank = ChessmanRank.Rook;
        SetSpace(new BoardCoord('h', rank), info, ref chessmanViews);

        rank = color == ChessmanColor.White ? (uint) 2 : (uint) 7;
        info.rank = ChessmanRank.Pawn;
        SetSpace(new BoardCoord('a', rank), info, ref chessmanViews);
        SetSpace(new BoardCoord('b', rank), info, ref chessmanViews);
        SetSpace(new BoardCoord('c', rank), info, ref chessmanViews);
        SetSpace(new BoardCoord('d', rank), info, ref chessmanViews);
        SetSpace(new BoardCoord('e', rank), info, ref chessmanViews);
        SetSpace(new BoardCoord('f', rank), info, ref chessmanViews);
        SetSpace(new BoardCoord('g', rank), info, ref chessmanViews);
        SetSpace(new BoardCoord('h', rank), info, ref chessmanViews);
    }
    
    public void SetView(ChessboardView view)
    {
        _view = view;
    }
    
    public BoardSpace GetSpace(BoardCoord coord)
    {
        return GetSpace(coord.rank, coord.file);
    }

    public BoardSpace GetSpace(uint rank, uint file)
    {
        if (rank > 8 || file > 8)
        {
            return null;
        }

        if (rank == 0 || file == 0)
        {
            return null;
        }

        rank -= 1;
        file -= 1;
        
        return _data[file, rank];
    }

    private void SetSpace(BoardCoord coord, ChessmanInfo info, ref ChessmanInfoCollection chessmanViews)
    {
        Chessman chessman = new Chessman(info);
        chessman.color = info.color;
        chessman.coord = coord;
        chessman.SetView(chessmanViews?.Pop(info));
        GetSpace(coord).piece = chessman;
    }

    public bool IsOccupied(BoardCoord coord)
    {
        BoardSpace spc = GetSpace(coord);
        if (spc != null)
        {
            return spc.piece != null;   
        }

        return false;
    }
}
