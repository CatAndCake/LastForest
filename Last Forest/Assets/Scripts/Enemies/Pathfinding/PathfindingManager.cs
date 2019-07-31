using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class PathfindingManager : MonoBehaviour
{
    PathRequestManager requestManager;
    GridWorld grid;
    public float timer;

    private void Awake()
    {
        //start = transform;
        requestManager = GetComponent<PathRequestManager>();
        grid = GetComponent<GridWorld>();
    }

    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        //StartCoroutine(FindPath(startPos, targetPos));
    }

    public void FindPath(PathRequest request, Action<PathResult> callback)    //public IEnumerator FindPath(Vector3 startPosition, Vector3 targetPosition) // zmieniamy z ienumerator na void
    {
        

        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        Node startNode = grid.NodeFromWorldPoint(request.pathStart); // tu był vector3
        Node targetNode = grid.NodeFromWorldPoint(request.pathEnd); //tu był vector3

        GameObject enemy = request.enemy;
        
        //We have find the closest node to the target node which is walkable

        Heap<Node> openSet = new Heap<Node>(grid.MaxSize);

        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {

            Node currentNode = openSet.RemoveFirst();

            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                GetFinalPath(startNode, targetNode);
                pathSuccess = true;
                break;
            }

            foreach (Node neighbour in grid.GetNeighboringNodes(currentNode))
            {
                

                if (neighbour.nonWalkableStatic || closedSet.Contains(neighbour)  || neighbour.nonWalkableDynamic)
                {
                    continue;
                }

                int newMovementCost = currentNode.GCost + GetDistance(currentNode, neighbour);

                if(neighbour.AI != enemy)
                {
                    newMovementCost = newMovementCost + 50;
                }

                if (newMovementCost < neighbour.GCost || !openSet.Contains(neighbour))
                {
                    neighbour.GCost = newMovementCost;
                    neighbour.HCost = GetDistance(neighbour, targetNode);
                    neighbour.ParentNode = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                    else
                    {
                        openSet.UpdateItem(neighbour);
                    }
                }
            }
        }

        
        if (pathSuccess)
        {
            waypoints = RetracePath(startNode, targetNode);
            pathSuccess = waypoints.Length > 0;
        }
        callback(new PathResult(waypoints, pathSuccess, request.callback));
    }

    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.ParentNode;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    Vector3[] GetFinalPath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();//List to hold the path sequentially 
        Node currentNode = endNode;//Node to store the current node being checked

        while (currentNode != startNode)//While loop to work through each node going through the parents to the beginning of the path
        {
            path.Add(currentNode);//Add that node to the final path
            currentNode = currentNode.ParentNode;//Move onto its parent node
        }


        Vector3[] waypoints = SimplifyPath(path);
        grid.FinalPath = path; //Set the final path
        Array.Reverse(waypoints);  //path.Reverse();//Reverse the path to get the correct order
        return waypoints;
    }


    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 0; i < path.Count; i++)
        {
            waypoints.Add(path[i].Position);
        }
        return waypoints.ToArray();
    }

    public int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
        int distanceY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }
        else
        {
            return 14 * distanceX + 10 * (distanceY - distanceX);
        }
    }
}
