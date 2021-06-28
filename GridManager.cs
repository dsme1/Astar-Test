using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathing;

/// <summary>
/// GridManager class for instantiating and handling (most) of the grid based events.
/// </summary>
public class GridManager : MonoBehaviour
{  
    //Holds information needed for the class about the grid and size.
    public GameObject[] hexTile;
    [SerializeField]
    public Dictionary<Vector2Int, GameObject> nodes;

    [SerializeField]
    private int mapWidth = 5;
    [SerializeField]
    private int mapHeight = 5;

    //Offset used for Unity units so the tiles are neatly aligned.
    private float tileXOffset = 1.0025f;
    private float tileZOffset = 0.72f;


    private void Start()
    {
        nodes = new Dictionary<Vector2Int, GameObject>();

        CreateHexTileMap();
        SetNeighbours();
    }

    /// <summary>
    /// Function call that creates the actual map and fills it up with tiles based on the width and height given.
    /// </summary>
    private void CreateHexTileMap()
    {

        for (int x = 0; x < mapWidth; x++)
        {
            for (int z = 0; z < mapHeight; z++)
            {
                //Picks a random tile from the hexTile array.
                var randomTile = Random.Range(0, 5);
                GameObject tempTile = Instantiate(hexTile[randomTile]);

                //Checks for uneven line offset.
                if (z % 2 == 0)
                {
                    tempTile.transform.position = new Vector3(x * tileXOffset, 0, z * tileZOffset);
                    nodes.Add(new Vector2Int(x, z), tempTile);

                    switch (randomTile)
                    {
                        case 0:
                            tempTile.GetComponent<Tile>().AddInfo(5.0f, new Vector2Int(x, z));
                            break;
                        case 1:
                            tempTile.GetComponent<Tile>().AddInfo(3.0f, new Vector2Int(x, z));
                            break;
                        case 2:
                            tempTile.GetComponent<Tile>().AddInfo(1.0f, new Vector2Int(x, z));
                            break;
                        case 3:
                            tempTile.GetComponent<Tile>().AddInfo(10.0f, new Vector2Int(x, z));
                            break;
                        case 4:
                            tempTile.GetComponent<Tile>().AddInfo(1000.0f, new Vector2Int(x, z));
                            tempTile.GetComponent<Tile>().walkable = false;
                            break;
                        default:
                            return;
                    }
                }
                else
                {
                    tempTile.transform.position = new Vector3(x * tileXOffset + tileXOffset / 2, 0, z * tileZOffset);
                    nodes.Add(new Vector2Int(x, z), tempTile);

                    switch (randomTile)
                    {
                        case 0:
                            tempTile.GetComponent<Tile>().AddInfo(5.0f, new Vector2Int(x, z));
                            break;
                        case 1:
                            tempTile.GetComponent<Tile>().AddInfo(3.0f, new Vector2Int(x, z));
                            break;
                        case 2:
                            tempTile.GetComponent<Tile>().AddInfo(1.0f, new Vector2Int(x, z));
                            break;
                        case 3:
                            tempTile.GetComponent<Tile>().AddInfo(10.0f, new Vector2Int(x, z));
                            break;
                        case 4:
                            tempTile.GetComponent<Tile>().AddInfo(1000.0f, new Vector2Int(x, z));
                            tempTile.GetComponent<Tile>().walkable = false;
                            break;
                        default:
                            return;
                    }
                }
                TileInfo(tempTile, x, z);
            }
        }
    }

    /// <summary>
    /// Gives coördinates to the tiles in the grid and childs them to the tilegenerator in the editor.
    /// </summary>
    /// <param name="tile"> Tile GameObject </param>
    /// <param name="x"> X Coordinate on the grid </param>
    /// <param name="z"> Z Coordinate on the grid </param>
    private void TileInfo(GameObject tile, int x, int z)
    {
        tile.transform.parent = transform;
        tile.name = "x " + x.ToString() + "," + "z " + z.ToString();
    }

    /// <summary>
    /// This monstrous function (c)/(b)all of if statements sets and links the neighbours for every tile on the map. I'm sure it can be
    /// done better; this is just the fastest way that I could think of to make the funcion do what I wanted it to do.
    /// </summary>
    private void SetNeighbours()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int z = 0; z < mapHeight; z++)
            {
                var offset = z % 2;                

                if (nodes[new Vector2Int(x, z)].GetComponent<Tile>())
                {                    
                    //Is the left neighbour.
                    if (nodes.ContainsKey(new Vector2Int(x - 1, z)))
                    {
                        nodes[new Vector2Int(x, z)].GetComponent<Tile>().AddNeighbour(nodes[new Vector2Int(x - 1, z)].GetComponent<IAStarNode>());
                    }
                    //Is the bottom left neighbour.
                    if (nodes.ContainsKey(new Vector2Int(x - 1 + offset, z - 1)))
                    {
                        nodes[new Vector2Int(x, z)].GetComponent<Tile>().AddNeighbour(nodes[new Vector2Int(x - 1 + offset, z - 1)].GetComponent<IAStarNode>());
                    }
                    //Is the top left neighbour.
                    if (nodes.ContainsKey(new Vector2Int(x - 1 + offset, z + 1)))
                    {
                        nodes[new Vector2Int(x, z)].GetComponent<Tile>().AddNeighbour(nodes[new Vector2Int(x - 1 + offset, z + 1)].GetComponent<IAStarNode>());
                    }
                    //Is the right neighbour.
                    if (nodes.ContainsKey(new Vector2Int(x + 1, z)))
                    {
                        nodes[new Vector2Int(x, z)].GetComponent<Tile>().AddNeighbour(nodes[new Vector2Int(x + 1, z)].GetComponent<IAStarNode>());
                    }
                    //Is the bottom right neighbour.
                    if (nodes.ContainsKey(new Vector2Int(x + offset, z - 1)))
                    {
                        nodes[new Vector2Int(x, z)].GetComponent<Tile>().AddNeighbour(nodes[new Vector2Int(x + offset, z - 1)].GetComponent<IAStarNode>());
                    }
                    //Is the top right neighbour.
                    if (nodes.ContainsKey(new Vector2Int(x + offset, z + 1)))
                    {
                        nodes[new Vector2Int(x, z)].GetComponent<Tile>().AddNeighbour(nodes[new Vector2Int(x + offset, z + 1)].GetComponent<IAStarNode>());
                    }
                }                    
            }
        }
    }
}