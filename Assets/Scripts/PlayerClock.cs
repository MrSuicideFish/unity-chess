using System;
using UnityEngine;

public class PlayerClock
{
    private float _time;

    public ChessmanColor Color { get; private set; }

    public PlayerClock(ChessmanColor ownerColor, float duration)
    {
        _time = duration;
        Color = ownerColor;
    }

    public float Step()
    {
        _time -= UnityEngine.Time.deltaTime;
        return _time;
    }
}