using System.Collections.Generic;
using UnityEngine;

public class Soldier : Piece
{
    public override List<Vector2Int> MoveToTarget(Vector2Int gridPoint)
    {
        List<Vector2Int> target = new List<Vector2Int>();
        List<Vector2Int> directions = new List<Vector2Int>(DiagonalDirections);
        directions.AddRange(StraightDirections);
        foreach(Vector2Int dir in DiagonalDirections)
        {
            Vector2Int nextGridPoint = new Vector2Int(gridPoint.x + dir.x, gridPoint.y + dir.y);
            target.Add(nextGridPoint);
        }
        foreach (Vector2Int dir in StraightDirections)
        {
            for (int i = 1; i <= 2; i++)
            {
                Vector2Int nextGridPoint = new Vector2Int(gridPoint.x + i * dir.x, gridPoint.y + i * dir.y);
                target.Add(nextGridPoint);
                if (GameManager.instance.PieceAtGrid(nextGridPoint))
                {
                    break;
                }
            }
        }
        return target;
    }
}
