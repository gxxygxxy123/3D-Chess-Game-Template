using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public override List<Vector2Int> MoveToTarget(Vector2Int gridPoint)
    {
        List<Vector2Int> target = new List<Vector2Int>();

        foreach (Vector2Int dir in StraightDirections)
        {
            Vector2Int nextGridPoint = new Vector2Int(gridPoint.x + dir.x, gridPoint.y + dir.y);
            target.Add(nextGridPoint);
        }

        return target;
    }
}
