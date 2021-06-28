using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathing;

/// <summary>
/// Class for the hextiles that hold all the relevant information like position in the grid, cost and a list of neighbours.
/// </summary>
public class Tile : MonoBehaviour, IAStarNode
{
    public List<IAStarNode> neighbours;
    [SerializeField]
    private float cost;
    [SerializeField]
    private Vector2Int position;
    [SerializeField]
    public bool walkable = true;


    public void Awake()
    {
        neighbours = new List<IAStarNode>();
    }

    //public void Start()
    //{
    //    var test = neighbours.Count.ToString();
    //    Debug.Log(test);
    //}

    /// <summary>
    /// Returns a list of all the neighbours of this tile when called.
    /// </summary>
    public IEnumerable<IAStarNode> Neighbours
    {
        get
        {
            return this.neighbours;
        }
    }

    /// <summary>
    /// Returns the cost of this tile when called.
    /// </summary>
    /// <param name="neighbour"> IAStarNode of this tile. </param>
    /// <returns></returns>
    public float CostTo(IAStarNode neighbour)
    {
        return cost;
    }

    /// <summary>
    /// Calculates the heuristic value from this tile to the goal tile.
    /// </summary>
    /// <param name="goal"> IAStarNode of the goal tile. </param>
    /// <returns></returns>
    public float EstimatedCostTo(IAStarNode goal)
    {
        var target = (Tile)goal;
        var targetCubePosition = OffsetToCube(target.position);
        var selfCubePosition = OffsetToCube(position);
        return (Mathf.Abs(selfCubePosition.x - targetCubePosition.x)
               + Mathf.Abs(selfCubePosition.y - targetCubePosition.y)
               + Mathf.Abs(selfCubePosition.z - targetCubePosition.z)) / 2;
    }

    /// <summary>
    /// Links this tile with all of it's neighbouring tiles.
    /// </summary>
    /// <param name="neighbour"> IAStarNode of the neighbouring tile. </param>
    public void AddNeighbour(IAStarNode neighbour)
    {
        if (walkable == false)
            return;

        neighbours.Add(neighbour);        
    }

    /// <summary>
    /// Adds all relevant information for this tile.
    /// </summary>
    /// <param name="cost"> Travelling cost float of this tile. </param>
    /// <param name="position"> X and Z coordinates of this tile. </param>
    public void AddInfo(float cost, Vector2Int position)
    {
        this.cost = cost;
        this.position = position;
    }

    /// <summary>
    /// Converts this tile's axial-coordinates to cube-coordinates used for calculating the heuristic distance to goal node.
    /// </summary>
    /// <param name="position"> X and Z coordinates of this tile. </param>
    /// <returns></returns>
    private Vector3Int OffsetToCube(Vector2Int position)
    {
        var vec = new Vector3Int
        {
            x = position.y - (position.x - ((position.x & 1) == 1 ? 1 : 0)),
            y = position.x,

        };
        vec.z = -vec.x - vec.y;
        return vec;
    }
}
