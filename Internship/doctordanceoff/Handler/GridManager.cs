using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public class XY
    {
        public int X;
        public int Y;

        public XY (int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class Tile
    {
        public int TileType;
        public GameObject GO;
        public TileControl TileControl;

        public Tile ()
        {            
            TileType = -1;            
        }

        public Tile (int tileType, GameObject go, TileControl tileControl)
        {
            TileType = tileType;
            GO = go;
            TileControl = tileControl;
        }
    }
    
    public GameObject[] TilePrefabs;
    public int GridWidth;
    public int GridHeight;
    public Tile[,] Grid;
    private ComboHandler comboHandler;
    private int movingTiles;

    void Awake()
    {
        comboHandler = GetComponent<ComboHandler>();
        CreateGrid();
    }

    void CreateGrid()
    {
        Grid = new Tile[GridWidth, GridHeight];        

        for (int x = 0; x < GridWidth; x++)
        {
            for (int y = 0; y < GridHeight; y++)
            {
                int randomTileType = Random.Range(0, TilePrefabs.Length);
                GameObject go = Instantiate(TilePrefabs[randomTileType], new Vector2(x, y), Quaternion.identity) as GameObject;
                TileControl tileControl = go.GetComponent<TileControl>();                
                Grid[x, y] = new Tile(randomTileType, go, tileControl);
                tileControl.gridManager = this;
                tileControl.myXY = new XY(x, y);
                go.name = x + "/" + y;
            }
        }
    }

    public void SwitchTiles(XY firstXY, XY secondXY)
    {
        Tile firstTile = new Tile(Grid[firstXY.X, firstXY.Y].TileType, Grid[firstXY.X, firstXY.Y].GO, Grid[firstXY.X, firstXY.Y].TileControl);
        Tile secondTile = new Tile(Grid[secondXY.X, secondXY.Y].TileType, Grid[secondXY.X, secondXY.Y].GO, Grid[secondXY.X, secondXY.Y].TileControl);

        Grid[firstXY.X, firstXY.Y] = secondTile;
        Grid[secondXY.X, secondXY.Y] = firstTile;        
    }
    public bool CheckCombo(List<XY> tilesToCheck)
    {//first the values are being transformed into a vector3 to compare it to the original
        Vector3 tiles = new Vector3();
        tiles.x = Grid[tilesToCheck[0].X,tilesToCheck[0].Y].TileType;
        tiles.y = Grid[tilesToCheck[1].X, tilesToCheck[1].Y].TileType;
        tiles.z = Grid[tilesToCheck[2].X, tilesToCheck[2].Y].TileType;
        if (comboHandler.comboChecker(tiles))
        {
            DestroyMatches(tilesToCheck);
            return true;
        }
        return false;
    }

    void DestroyMatches (List<XY> tilesToDestroy)
    {
        for (int i = 0; i < tilesToDestroy.Count; i++)
        {            
            Destroy(Grid[tilesToDestroy[i].X, tilesToDestroy[i].Y].GO);
            Grid[tilesToDestroy[i].X, tilesToDestroy[i].Y] = new Tile();
        }
        GravityCheck();
    }

    void GravityCheck()
    {
        for (int x = 0; x < GridWidth; x++)
        {
            int missingTileCount = 0;

            for (int y = 0; y < GridHeight; y++)
            {
                if (Grid[x, y].TileType == -1)
                    missingTileCount++;
                else
                {
                    if (missingTileCount >= 1)
                    {
                        Tile tile = new Tile(Grid[x, y].TileType, Grid[x, y].GO, Grid[x, y].TileControl);
                        Grid[x, y].TileControl.Move(new XY(x, y - missingTileCount));
                        Grid[x, y - missingTileCount] = tile;
                        Grid[x, y] = new Tile();                        
                    }
                }
            }            
        }        
        ReplaceTiles();
    }

    void ReplaceTiles()
    {
        for (int x = 0; x < GridWidth; x++)
        {
            int missingTileCount = 0;

            for (int y = 0; y < GridHeight; y++)
            {
                if (Grid[x, y].TileType == -1)
                    missingTileCount++;
            }

            for (int i = 0; i < missingTileCount; i++)
            {
                int tileY = GridHeight - missingTileCount + i;
                int randomTileType = Random.Range(0, TilePrefabs.Length);
                GameObject go = Instantiate(TilePrefabs[randomTileType], new Vector2(x, GridHeight + i), Quaternion.identity) as GameObject;              
                TileControl tileControl = go.GetComponent<TileControl>();
                tileControl.gridManager = this;
                tileControl.Move(new XY(x, tileY));
                Grid[x, tileY] = new Tile(randomTileType, go, tileControl);
                go.name = x + "/" + tileY;
            }
        }
    }

    public void ReportTileMovement ()
    {
        movingTiles++;
    }

    public void ReportTileStopped ()
    {
        movingTiles--;
    }
}