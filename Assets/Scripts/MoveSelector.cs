using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelector : MonoBehaviour
{
    public GameObject hintHighlight;
    public GameObject movableHighlight;

    public GameObject attackableHighlight;

    private GameObject cursorHighlight;
    private GameObject movingPiece;
    private List<Vector2Int> movableLocations; /* may contain friend chess */
    private List<GameObject> locationHighlights; /* movable + attackable */
    void Start()
    {
        this.enabled = false;
        cursorHighlight = Instantiate(hintHighlight, Geometry.PointFromGrid(new Vector2Int(0, 0)), Quaternion.identity, gameObject.transform);
        cursorHighlight.SetActive(false);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 point = hit.point;
            Vector2Int gridPoint = Geometry.GridFromPoint(point);

            cursorHighlight.SetActive(true);
            cursorHighlight.transform.position = Geometry.PointFromGrid(gridPoint);
            if (Input.GetMouseButtonDown(0))
            {
                if (GameManager.instance.FriendlyPieceAt(gridPoint))
                {
                    SelectOtherFriendChessToMove(gridPoint);
                }
                else if (!movableLocations.Contains(gridPoint))
                {
                    return;
                }

                else if (GameManager.instance.PieceAtGrid(gridPoint) == null)
                {
                    GameManager.instance.Move(movingPiece, gridPoint);
                    ExitState();
                }

                else
                {
                    GameManager.instance.CapturePieceAt(gridPoint);
                    GameManager.instance.Move(movingPiece, gridPoint);
                    ExitState();
                }

            }
        }
        else
        {
            cursorHighlight.SetActive(false);
        }
    }
    private void SelectOtherFriendChessToMove(Vector2Int NewGridPoint)
    {
        /* Change Material */
        GameManager.instance.DeselectPiece(movingPiece);
        /* Destroy current movable and attackable highlights */
        foreach (GameObject highlight in locationHighlights)
        {
            Destroy(highlight);
        }
        /* Change Material */
        GameObject selectedPiece = GameManager.instance.PieceAtGrid(NewGridPoint);
        GameManager.instance.SelectPiece(selectedPiece);

        /* New moving piece */
        movingPiece = selectedPiece;
        /* Find next step of new piece */
        movableLocations = GameManager.instance.MovesForPiece(movingPiece);
        movableLocations.RemoveAll(gp => GameManager.instance.FriendlyPieceAt(gp));
        foreach (Vector2Int loc in movableLocations)
        {
            GameObject highlight;
            if (GameManager.instance.PieceAtGrid(loc))
            {
                highlight = Instantiate(attackableHighlight, Geometry.PointFromGrid(loc), Quaternion.identity, gameObject.transform);
            }
            else
            {
                highlight = Instantiate(movableHighlight, Geometry.PointFromGrid(loc), Quaternion.identity, gameObject.transform);
            }
            locationHighlights.Add(highlight);
        }
    }
    private void CancelMove()
    {
        this.enabled = false;

        foreach (GameObject highlight in locationHighlights)
        {
            Destroy(highlight);
        }

        GameManager.instance.DeselectPiece(movingPiece);
        TileSelector selector = GetComponent<TileSelector>();
        selector.EnterState();
    }

    public void EnterState(GameObject piece)
    {
        movingPiece = piece;
        this.enabled = true;

        movableLocations = GameManager.instance.MovesForPiece(movingPiece);

        movableLocations.RemoveAll(gp => GameManager.instance.FriendlyPieceAt(gp));
        locationHighlights = new List<GameObject>();

        if (movableLocations.Count == 0)
        {
            CancelMove();
        }

        foreach (Vector2Int loc in movableLocations)
        {
            GameObject highlight;
            if (GameManager.instance.PieceAtGrid(loc))
            {
                highlight = Instantiate(attackableHighlight, Geometry.PointFromGrid(loc), Quaternion.identity, gameObject.transform);
            }
            else
            {
                highlight = Instantiate(movableHighlight, Geometry.PointFromGrid(loc), Quaternion.identity, gameObject.transform);
            }
            locationHighlights.Add(highlight);
        }
    }

    private void ExitState()
    {
        this.enabled = false;
        TileSelector selector = GetComponent<TileSelector>();
        cursorHighlight.SetActive(false);
        GameManager.instance.DeselectPiece(movingPiece);
        movingPiece = null;
        GameManager.instance.NextPlayer();
        selector.EnterState();
        foreach (GameObject highlight in locationHighlights)
        {
            Destroy(highlight);
        }
    }
}
