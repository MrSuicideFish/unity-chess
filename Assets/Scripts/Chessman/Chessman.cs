using System;
using System.Collections.Generic;
using System.Text;
using DefaultNamespace;
using UnityEngine;


public enum ChessmanRank
{
    Pawn,
    Rook,
    Knight,
    Bishop,
    Queen,
    King
}

public enum ChessmanColor
{
    Black,
    White
}

public class Chessman
{
    public ChessmanRank rank;
    public ChessmanColor color;
    
    public BoardCoord coord;
    public bool hasMoved = false;

    private ChessmanView _view;

    public Chessman(ChessmanInfo info)
    {
        this.rank = info.rank;
        this.color = info.color;
    }

    public Chessman(ChessmanInfo info, ChessmanView view)
        :this(info)
    {
        _view = view;
    }

    public void SetView(ChessmanView view)
    {
        this._view = view;
        this._view.Initialize(this);
    }

    public void MoveToCoord(BoardCoord coord)
    {
        this.coord = coord;
        if (_view != null)
        {
            _view.DoMove(ChessboardView.GetWorldPosition(coord));
        }
    }

    public virtual void Destroy()
    {
        _view.DoDestroy();
    }

    public virtual void Select(){}
    public virtual void ConfirmMove(){}

    public void OnMouseDown()
    {
        if (GameManager.PlayerColor == this.color
        && GameManager.TurnColor == this.color)
        {
            
        }
        
        GameManager.Select(this);
    }
}