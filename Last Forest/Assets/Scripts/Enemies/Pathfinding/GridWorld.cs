using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridWorld : MonoBehaviour
{
    public bool displayGridGizmos;
    public Transform StartPosition; // To w tym miescu nasz program rozpocznie szukanie celu
    public LayerMask WallMask; // Jeśli program natrafi na przeszkodę to uzna to miejsce za niedostępne
    public Vector2 GridWorldSize; // Jak duża będzie przestrzeń w które będzie szukać naszego celu
    public float NodeRadius; // Jak dużą przestrzeń będzie zajmować każde miejsce badane przez program
    public float DistanceBetweenNodes; // Dystans pomiędzy badanymi przestrzeniami

    public Node[,] nodeArray; // 
    public List<Node> openList;
    public HashSet<Node> closedList;
    public List<Node> FinalPath; // Znaleziona przez program droga do celu, w której każdy krok posiada dane

    float nodeDiameter; //
    public int gridSizeX, gridSizeY; //Wielkość gridów

    

    public bool alreadyChecked = false;


    private void Awake()
    {
        displayGridGizmos = true;
        nodeDiameter = NodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(GridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(GridWorldSize.y / nodeDiameter);

        CreateGrid();


    }

    private void Update()
    {
        
    }

    public int MaxSize
    {
        get { return gridSizeX * gridSizeY; }
    }



    void CreateGrid()
    {
        nodeArray = new Node[gridSizeX, gridSizeY];//Declare the array of nodes.

        Vector3 bottomLeft = transform.position - Vector3.right * GridWorldSize.x / 2 - Vector3.forward * GridWorldSize.y / 2;//Get the real world position of the bottom left of the grid.

        for (int x = 0; x < gridSizeX; x++)//Loop through the array of nodes.
        {
            for (int y = 0; y < gridSizeY; y++)//Loop through the array of nodes
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + NodeRadius) + Vector3.forward * (y * nodeDiameter + NodeRadius);//Get the world co ordinates of the bottom left of the graph
                bool nonWalkableStatic = false;//Make the node a wall

                //If the node is not being obstructed
                //Quick collision check against the current node and anything in the world at its position. If it is colliding with an object with a WallMask,
                //The if statement will return false.
                if (Physics.CheckSphere(worldPoint, NodeRadius, WallMask))
                {
                    nonWalkableStatic = true;//Object is not a wall
                }

                nodeArray[x, y] = new Node(nonWalkableStatic, worldPoint, x, y);//Create a new node in the array.
                //allPath.Add(new Node(Wall, worldPoint, x, y));
            }
        }

    }

    void BlurPenaltyMap(int blurSize)
    {
        int kernelSize = blurSize * 2 + 1;
    }

    //Function that gets the neighboring nodes of the given node.
    public List<Node> GetNeighboringNodes(Node neighborNode)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = neighborNode.GridX + x;
                int checkY = neighborNode.GridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(nodeArray[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    public List<Node> GetWalkableNeigboringNodes(Node node, int distance)
    {
        List<Node> neighbours = new List<Node>();
        
        //Node neighbourNode;
        for (int x = -distance; x <= distance; x++)
        {
            for (int y = -distance; y <= distance; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }
                
                int checkX = node.GridX + x;
                int checkY = node.GridY + y;

                Node neighboringNode;
                neighboringNode = nodeArray[checkX, checkY];

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    //neighbours.Add(nodeArray[checkX, checkY]);
                    if (!neighboringNode.nonWalkableDynamic && !neighboringNode.nonWalkableStatic)
                    {
                        neighbours.Add(neighboringNode);
                    }
                }
            }
        }

        return neighbours;
    }

    //Tutaj sprawdzamy ktore kwadraciki sa najblize miejscom ktore szukamy
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float xPos = ((worldPosition.x + GridWorldSize.x / 2) / GridWorldSize.x);
        float yPos = ((worldPosition.z + GridWorldSize.y / 2) / GridWorldSize.y);

        xPos = Mathf.Clamp01(xPos);
        yPos = Mathf.Clamp01(yPos);

        int ix = Mathf.RoundToInt((gridSizeX - 1) * xPos);
        int iy = Mathf.RoundToInt((gridSizeY - 1) * yPos);

        return nodeArray[ix, iy];
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(GridWorldSize.x, 1, GridWorldSize.y));

        if (nodeArray != null && displayGridGizmos)
        {

            foreach (Node n in nodeArray)
            {
                Gizmos.color = Color.white;

                if (n.nonWalkableDynamic == true)
                {
                    Gizmos.color = Color.yellow;
                }

                Gizmos.DrawCube(n.Position, Vector3.one * (nodeDiameter - DistanceBetweenNodes) / 2);
            }
        }
    }

}
