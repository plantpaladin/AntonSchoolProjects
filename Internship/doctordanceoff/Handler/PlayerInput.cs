using UnityEngine;
using System.Collections.Generic;

public class PlayerInput : MonoBehaviour
{
    private GridManager gridManager;
    public LayerMask Tiles;
    public GameObject spritePrefab;
    private GameObject activeTile;
    private GameObject otherActiveTile;
    void Awake()
    {
        gridManager = GetComponent<GridManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (activeTile == null)
            {
                SelectTile(ref activeTile);
            }
            else if(otherActiveTile==null)
            {
                SelectTile(ref otherActiveTile);
            }
            else
                AttemptMove();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (activeTile != null)
            {
                DeSelectTile(ref activeTile);
            }
            if (otherActiveTile != null)
            {
                DeSelectTile(ref otherActiveTile);
            }
        }
    }

    void SelectTile(ref GameObject tileToSelect)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 50f, Tiles);
        if (hit)
        {
            tileToSelect = hit.collider.gameObject;
            tileToSelect.GetComponent<TileControl>().Select();
        }
        if(activeTile == otherActiveTile)
        {//prevents the player from selecting the same tile twice
            DeSelectTile(ref activeTile);
            DeSelectTile(ref otherActiveTile);
        }
    }
    void DeSelectTile(ref GameObject tileToDeSelect)
    {
        if (tileToDeSelect != null)
        {
            tileToDeSelect.GetComponent<TileControl>().DeSelect();
            tileToDeSelect = null;
        }
    }
    void AttemptMove()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 50f, Tiles);
        if (hit)
        {
            if ((hit.collider.gameObject != activeTile) && (hit.collider.gameObject != otherActiveTile))
            {
                TileControl activeControl = activeTile.GetComponent<TileControl>();
                TileControl otherActiveControl = otherActiveTile.GetComponent<TileControl>();
                TileControl hitControl = hit.collider.gameObject.GetComponent<TileControl>();

                List<GridManager.XY> tilesToDestroy = new List<GridManager.XY>();

                tilesToDestroy.Add(activeControl.myXY);
                tilesToDestroy.Add(otherActiveControl.myXY);
                tilesToDestroy.Add(hitControl.myXY);


                if (gridManager.CheckCombo(tilesToDestroy))
                { };
                DeSelectTile(ref otherActiveTile);
                DeSelectTile(ref activeTile);

            }
        }
    }    
}
