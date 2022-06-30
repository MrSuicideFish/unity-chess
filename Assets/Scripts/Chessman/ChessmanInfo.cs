public class ChessmanInfo
{
    public ChessmanRank rank;
    public ChessmanColor color;
    
    public ChessmanInfo(){}

    public ChessmanInfo(ChessmanRank rank, ChessmanColor color)
    {
        this.rank = rank;
        this.color = color;
    }

    public override int GetHashCode()
    {
        return $"{rank}{color}".GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (obj is ChessmanInfo)
        {
            ChessmanInfo info = (ChessmanInfo) obj;
            return this.rank == info.rank && this.color == info.color;
        }

        return false;
    }
}