using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit : MonoBehaviour
{
    GridWorld grid;
    public Transform target;
    float speed = 5f;
    public Vector3[] path;
    int targetIndex;

    int gridX, gridY;

    public List<Node> currentNodes;

    private void Awake()
    {
        grid = GameObject.FindGameObjectWithTag("PathfindingManager").GetComponent<GridWorld>();
        currentNodes = new List<Node>();
        
    }
    private void Start()
    {
        //MakeCurrentPositionNonWalkable();
        //PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    private void Update()
    {
        MakeCurrentPositionNonWalkable();
    }
    void MakeCurrentPositionNonWalkable()
    {
        Node currentNode = grid.NodeFromWorldPoint(transform.position);

        gridX = currentNode.GridX;
        gridY = currentNode.GridY;

        if (!currentNodes.Contains(currentNode))
        {
            currentNodes.Add(currentNode);
        }

        foreach (Node walkedNode in currentNodes)
        {
            if (walkedNode.GridX == gridX && walkedNode.GridY == gridY)
            {
                walkedNode.nonWalkableDynamic = true;
            }
            else
            {
                walkedNode.nonWalkableDynamic = false;
            }
        }

        for (int i = 0; i < currentNodes.Count; i++)
        {
            if (currentNodes[i].nonWalkableDynamic == false)
            {
                currentNodes.Remove(currentNodes[i]);
            }
        }
    }
    public void OnPathFound(Vector3[] newPath, bool pathSuccesfull) // tutaj prosimy o wartość drogi
    {
        if (pathSuccesfull)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath() //tutaj podążamy drogą 
    {
        Vector3 currentWaypoint = path[0];

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }

    

    
    
}
