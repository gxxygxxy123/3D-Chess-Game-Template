using System.Collections.Generic;
using UnityEngine;

public enum PieceType { King, Others};

public abstract class Piece : MonoBehaviour
{
    public PieceType type;

    protected Vector2Int[] StraightDirections = {new Vector2Int(0,1), new Vector2Int(1, 0), new Vector2Int(0, -1), new Vector2Int(-1, 0)};
    protected Vector2Int[] DiagonalDirections = {new Vector2Int(1,1), new Vector2Int(1, -1), new Vector2Int(-1, -1), new Vector2Int(-1, 1)};

    public abstract List<Vector2Int> MoveToTarget(Vector2Int gridPoint);
}