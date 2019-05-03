using UnityEngine;

public class Geometry
{
    static public Vector3 PointFromGrid(Vector2Int gridPoint)
    {
        // To instantiate the object into the coordinate system by grid point([0,1,...,15, 0,1,...,15])
        float x = 1.0f * gridPoint.x;
        float z = 1.0f * gridPoint.y;
        // Assume the pivot y-coordinate is 0
        return new Vector3(x, 0, z);
    }

    static public Vector2Int GridPoint(int col, int row)
    {
        // col : 0 ~ 15 (int type)
        // row : 0 ~ 15 (int type)
        return new Vector2Int(col, row);
    }

    static public Vector2Int GridFromPoint(Vector3 point)
    {
        // To compute which grid the point belongs to
        int col = Mathf.FloorToInt(0.5f + point.x);
        int row = Mathf.FloorToInt(0.5f + point.z);
        return new Vector2Int(col, row);
    }
}
