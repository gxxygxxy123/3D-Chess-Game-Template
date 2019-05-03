using System.Collections.Generic;
using UnityEngine;

public class Horse : Piece
{
    public override List<Vector2Int> MoveToTarget(Vector2Int gridPoint)
    {
        List<Vector2Int> target = new List<Vector2Int>();

        target.Add(new Vector2Int(gridPoint.x - 1, gridPoint.y + 2));
        target.Add(new Vector2Int(gridPoint.x + 1, gridPoint.y + 2));

        target.Add(new Vector2Int(gridPoint.x + 2, gridPoint.y + 1));
        target.Add(new Vector2Int(gridPoint.x - 2, gridPoint.y + 1));

        target.Add(new Vector2Int(gridPoint.x + 2, gridPoint.y - 1));
        target.Add(new Vector2Int(gridPoint.x - 2, gridPoint.y - 1));

        target.Add(new Vector2Int(gridPoint.x + 1, gridPoint.y - 2));
        target.Add(new Vector2Int(gridPoint.x - 1, gridPoint.y - 2));

        return target;
    }
}