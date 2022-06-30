public static class ChessmanUtility
{
    public static uint ClampRank(uint c)
    {
        uint result = c;
        if (c >= 'A' && c <= 'Z') result -= 64;
        else if (c >= 'a' && c <= 'z') result -= 96;
        return result;
    }

    public static BoardCoord ToBoardCoord(this string str)
    {
        BoardCoord coord = new BoardCoord();
        if (str.Length <= 2)
        {
            coord.rank = ClampRank(str[0]);
            coord.file = uint.Parse(str[1].ToString());
        }
        return coord;
    }
}