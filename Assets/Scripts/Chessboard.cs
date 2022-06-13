using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class Chessboard : MonoBehaviour
{
    private const float SpaceSize = 0.6f;
    private const float BoardSizeX = 1;
    private const float BoardSizeZ = 1;
    private const float BoardEdgeBuffer = 0.4f;

    public class BoardCoord
    {
        public uint rank;
        public uint file;

        public BoardCoord(){}

        public BoardCoord(uint rank, uint file)
        {
            this.rank = ChessmanUtility.ClampRank(rank);
            this.file = file;
        }
    }
    
    public class Space
    {
        public Chessman piece = null;
    }
    
    public static Chessboard Instance { get; private set; }

    private Space[,] _data;

    void OnEnable()
    {
        Instance = this;
        _data = new Space[8, 8];
    }

    public Space GetSpace(BoardCoord coord)
    {
        return GetSpace(coord.rank, coord.file);
    }

    public Space GetSpace(uint rank, uint file)
    {
        if (rank > 8 || file > 8)
        {
            return null;
        }

        rank -= 1;
        file -= 1;

        if (_data[file, rank] == null)
        {
            _data[file, rank] = new Space();
        }
        
        return _data[file, rank];
    }

    public Vector3 GetWorldPosition(BoardCoord coord)
    {
        return GetWorldPosition(coord.rank, coord.file);
    }

    public Vector3 GetWorldPosition(uint rank, uint file)
    {
        rank = ChessmanUtility.ClampRank(rank);
        file--;
        
        Vector3 pos;
        pos.x = BoardEdgeBuffer + (rank * SpaceSize) - (SpaceSize / 2.0f);
        pos.y = 0;
        pos.z = BoardEdgeBuffer + (file * SpaceSize) + (SpaceSize / 2.0f);
        return pos;
    }
}
