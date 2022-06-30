using System.Collections.Generic;

public class ChessmanInfoCollection
{
    public class ChessmanInfoCollectionEntry
    {
        public ChessmanInfo info { get; private set; }
        public Stack<ChessmanView> views;

        public ChessmanInfoCollectionEntry(ChessmanInfo chessmanInfo)
        {
            info = chessmanInfo;
            views = new Stack<ChessmanView>();
        }

        public void Push(ChessmanView newView)
        {
            views.Push(newView);
        }

        public ChessmanView Pop()
        {
            return views.Pop();
        }
    }

    private List<ChessmanInfoCollectionEntry> _collection;

    public ChessmanInfoCollection()
    {
        _collection = new List<ChessmanInfoCollectionEntry>();
    }

    public bool Contains(ChessmanInfo info)
    {
        for (int i = 0; i < _collection.Count; i++)
        {
            if (_collection[i].info.Equals(info))
            {
                return true;
            }
        }
        
        return false;
    }

    public ChessmanInfoCollectionEntry Get(ChessmanInfo info)
    {
        for (int i = 0; i < _collection.Count; i++)
        {
            if (_collection[i].info.Equals(info))
            {
                return _collection[i];
            }
        }

        return null;
    }

    public void Add(ChessmanInfo info, ChessmanView view)
    {
        ChessmanInfoCollectionEntry entry = this.Get(info);
        if (entry == null)
        {
            entry = new ChessmanInfoCollectionEntry(info);
            _collection.Add(entry);
        }

        entry.Push(view);
    }
    
    public ChessmanView Pop(ChessmanInfo info)
    {
        ChessmanInfoCollectionEntry entry = this.Get(info);
        if (entry != null)
        {
            return entry.Pop();
        }

        return null;
    }
}