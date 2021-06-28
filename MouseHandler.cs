using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathing;

/// <summary>
/// Mouse handler class for handling mouse events. Functions for selecting start and goal tiles and highlighting them.
/// </summary>
public class MouseHandler : MonoBehaviour
{
    //Holds information for the two chosen tiles in GameObjects or IAStarNodes.
    [SerializeField]
    public GameObject selectedObjectOne;
    [SerializeField]
    public GameObject selectedObjectTwo;
    [SerializeField]    
    public IAStarNode start;
    public IAStarNode goal;
    private IList<IAStarNode> path;
    private int selectedOne = 0;

    private void Start()
    {        
        //Making sure the cursor is visible.
        Cursor.visible = true;
    }

    private void Update()
    {
        MouseSelect();               
    }

    /// <summary>
    /// Gets the path list from the PathButton class and stores it in local path IList.
    /// </summary>
    /// <param name="path"> IList of IAStarNodes. </param>
    public void SetPath(IList<IAStarNode> path)
    {
        this.path = path;
    }

    /// <summary>
    /// Function for clicking on two tiles in the game and getting their information. 
    /// </summary>
    private void MouseSelect()
    {
        //Gets mouse input according to position in cameraview.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hitInfo) && selectedOne == 0)
        {         
            //The actual information I want to get.
            GameObject hitObject = hitInfo.collider.transform.gameObject;

            if (hitObject.GetComponent<Tile>().walkable == false)
                return;

            IAStarNode node = hitInfo.transform.GetComponent<IAStarNode>();
            start = node;

            //For highlighting selected objects green.
            SelectObject(hitObject);            
            selectedOne++;
        } 
        else if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hitInfo) && selectedOne == 1)
        {
            //The actual information I want to get.
            GameObject hitObject = hitInfo.collider.transform.gameObject;

            if (hitObject.GetComponent<Tile>().walkable == false)
                return;

            IAStarNode node = hitInfo.transform.GetComponent<IAStarNode>();
            goal = node;

            //For highlighting selected objects green.
            SelectObject(hitObject);
            selectedOne++;
        }

        if (Input.GetMouseButton(1))
        {
            //For clearing selected tiles and the path.
            ClearSelection();
        }
    }

    /// <summary>
    /// Function for highlighting selected objects with the MouseSelect() function green.
    /// </summary>
    /// <param name="obj"> Is the stored GameObject found with MouseSelect() function. </param>
    private void SelectObject(GameObject obj)
    {
        if (selectedObjectOne != null)
        {
            if (obj == selectedObjectOne || obj == selectedObjectTwo)
                return;
        }

        if (selectedOne == 0)
        {
            selectedObjectOne = obj;
            //takes the renderer information of the selected object and turns it green for highlighting purposes
            Renderer[] renderers = selectedObjectOne.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                Material material = renderer.material;
                material.color = Color.green;
                renderer.material = material;
            }
        }
        else if (selectedOne == 1)
        {
            selectedObjectTwo = obj;
            //takes the renderer information of the selected object and turns it green for highlighting purposes
            Renderer[] renderers = selectedObjectTwo.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                Material material = renderer.material;
                material.color = Color.green;
                renderer.material = material;
            }
        }                   
    }

    /// <summary>
    /// Funtion for clearing the selection made with MouseSelect() and removing highlights made with SelectObject().
    /// </summary>
    private void ClearSelection()
    {
        if (selectedObjectOne == null || selectedObjectTwo == null)
            return;
        
        foreach (var node in path)
        {
            Tile tile = (Tile)node;
            Renderer renderer = tile.GetComponent<Renderer>();
            Material material = renderer.material;
            material.color = Color.white;
            renderer.material = material;
        }

        path = null;
        selectedObjectOne = null;
        selectedObjectTwo = null;
        start = null;
        goal = null;
        selectedOne = 0;
    }
}
