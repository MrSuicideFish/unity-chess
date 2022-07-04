using System;
using UnityEngine;

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
        
        public static BoardCoord operator +(BoardCoord coordA, Vector2 dir)
        {
            BoardCoord newCoord = new BoardCoord();

            newCoord.file = (uint) Mathf.Clamp(coordA.file + dir.y, 0, 8);
            newCoord.rank = (uint) Mathf.Clamp(coordA.rank + dir.x, 0, 8);

            return newCoord;
        }
        
        public static BoardCoord operator -(BoardCoord coordA, Vector2 dir)
        {
            BoardCoord newCoord = new BoardCoord();

            newCoord.file = (uint) Mathf.Clamp(coordA.file - dir.y, 0, 8);
            newCoord.rank = (uint) Mathf.Clamp(coordA.rank - dir.x, 0, 8);

            return newCoord;
        }

        public BoardCoord Add(Vector2 dir, ChessmanColor color)
        {
            if (color == ChessmanColor.White)
            {
                this.rank += (uint) dir.x;
                this.file += (uint) dir.y;
                return this;
            }

            this.rank -= (uint) dir.x;
            this.file -= (uint) dir.y;
            return this;
        }

        public BoardCoord Sub(Vector2 dir, ChessmanColor color)
        {
            if (color == ChessmanColor.White)
            {
                this.rank -= (uint) dir.x;
                this.file -= (uint) dir.y;
                return this;
            }

            this.rank += (uint) dir.x;
            this.file += (uint) dir.y;
            return this;
        }

        public static BoardCoord Add(BoardCoord coord, Vector2 dir, ChessmanColor color)
        {
            BoardCoord newCoord = new BoardCoord(coord.rank, coord.file);
            if (color == ChessmanColor.White)
            {
                newCoord.rank += (uint) dir.x;
                newCoord.file += (uint) dir.y;
                return newCoord;
            }

            newCoord.rank -= (uint) dir.x;
            newCoord.file -= (uint) dir.y;
            return newCoord;
        }
        
        public static BoardCoord Sub(BoardCoord coord, Vector2 dir, ChessmanColor color)
        {
            BoardCoord newCoord = new BoardCoord(coord.rank, coord.file);
            if (color == ChessmanColor.White)
            {
                newCoord.rank -= (uint) dir.x;
                newCoord.file -= (uint) dir.y;
                return newCoord;
            }

            newCoord.rank += (uint) dir.x;
            newCoord.file += (uint) dir.y;
            return newCoord;
        }

        public override string ToString()
        {
            uint num = rank + 47;
            return $"{Convert.ToChar(num + 17)}{file}";
        }
    }