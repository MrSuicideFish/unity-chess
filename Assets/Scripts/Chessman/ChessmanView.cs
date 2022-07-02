using DefaultNamespace;
using UnityEngine;

public class ChessmanView : MonoBehaviour
{
    private Chessman _parent;
    public ChessmanRank rank;
    public ChessmanColor color;

    public void Initialize(Chessman parent)
    {
        this.transform.position = ChessboardView.GetWorldPosition(parent.coord);
        _parent = parent;
    }
    
    public void OnMouseDown()
    {
        if (GameManager.PlayerColor == this.color
            && GameManager.TurnColor == this.color)
        {
            
        }
        
        GameManager.SelectChessman(_parent);
    }
    
    public void DoMove(Vector3 moveTo)
    {
        this.transform.position = moveTo;
        this.transform.eulerAngles = new Vector3(-90, 0, -90);
    }

    public void DoBounce()
    {
        
    }

    public void DoDestroy()
    {
        
    }
}