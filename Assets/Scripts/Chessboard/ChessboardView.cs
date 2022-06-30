using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class ChessboardView : MonoBehaviour
    {
        private const float SpaceSize = 0.6f;
        private const float BoardSizeX = 1;
        private const float BoardSizeZ = 1;
        private const float BoardEdgeBuffer = 0.4f;

        public static Vector3 GetWorldPosition(BoardCoord coord)
        {
            return GetWorldPosition(coord.rank, coord.file);
        }

        public static Vector3 GetWorldPosition(uint rank, uint file)
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
}