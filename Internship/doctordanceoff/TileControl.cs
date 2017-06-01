using UnityEngine;
using System.Collections;

public class TileControl : MonoBehaviour
{
    public GridManager gridManager;
    public GridManager.XY myXY;

    private Behaviour selectEffect;
    void Awake()
    {
        selectEffect = (Behaviour)GetComponent("Halo");
        selectEffect.enabled = false;
    }

    public void Select()
    {
        selectEffect.enabled = true;
    }

    public void DeSelect()
    {
        selectEffect.enabled = false;
    }

    public void Move(GridManager.XY xy)
    {
        StartCoroutine(Moving(xy));
    }

    IEnumerator Moving (GridManager.XY xy)
    {
        gridManager.ReportTileMovement();

        Vector2 destination = new Vector2(xy.X, xy.Y);
        bool moving = true;

        while (moving)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, 15f * Time.deltaTime);

            if (Vector2.Distance(transform.position, destination) <= 0.1f)
            {
                transform.position = destination;
                moving = false;
            }
            yield return null;
        }

        myXY = xy;
        gameObject.name = xy.X + "/" + xy.Y;
        gridManager.ReportTileStopped();        
    }
}