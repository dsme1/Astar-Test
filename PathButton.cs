using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathing;

/// <summary>
/// Button Class for handling button events, in this case getting the path from start to goal.
/// </summary>
public class PathButton : MonoBehaviour
{
    public MouseHandler handler;
    public IList<IAStarNode> path;

    /// <summary>
    /// Function that fires when clicking the pathfinding button in the game. Calculates the optimal path using A* algorithm.
    /// </summary>
    public void ButtonClick()
    {       
        //Grabs start and goal variables from mousehandler script.
        IAStarNode start = handler.start;
        IAStarNode goal = handler.goal;

        if (start == null || goal == null)
            return;

        //Fires the A* algorithm using start and goal as parameters.
        path = AStar.GetPath(start, goal);
        handler.SetPath(path);
        HighLightPath(path);      
    }

    /// <summary>
    /// Function that highlights the path when the start and goal nodes are selected using the list that returns from the Astar GetPath function.
    /// </summary>
    /// <param name="nodes"> IList of IAStarNodes </param>
    public void HighLightPath(IList<IAStarNode> nodes)
    {
        List<Tile> tiles = new List<Tile>();
        
        foreach (var node in nodes)
        {
            Tile tile = (Tile)node;
            tiles.Add(tile);
            Renderer renderer = tile.GetComponent<Renderer>();
            Material material = renderer.material;
            material.color = Color.red;
            renderer.material = material;
        }
    }

    /// <summary>
    /// Nulls the path Ilist in this class when clearing with ClearSelection().
    /// </summary>
    public void SetPathToNull()
    {
        path = null;
    }
}
