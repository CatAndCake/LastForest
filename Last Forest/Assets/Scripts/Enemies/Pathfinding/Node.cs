using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node : IHeapItem<Node>
{
    public int GridX; // Pozycja X danego noda
    public int GridY; // Pozycja Y danego noda

    public int movementPenalty;

    
    public bool nonWalkableStatic; // Like tree for instance
    public bool nonWalkableDynamic; // Like AI for instance

    public GameObject AI;
    public float time; // Czas w jakim pojawi się tutaj dany AI
    public Vector3 Position; // Pozycja 

    public Node ParentNode;

    public int GCost; // Jaka jest odległość od początkowej lokalizacji
    public int HCost; // Odległość do naszego celu

    public int FCost { get { return GCost + HCost; } } // Suma licząca jaka droga będzie najlepsza

    int HipIndex;

    public Node(bool _nonWalkableStatic, Vector3 position, int gridX, int gridY)
    {
        this.nonWalkableStatic = _nonWalkableStatic;
        this.Position = position;
        this.GridX = gridX;
        this.GridY = gridY;
    }

    public int HeapIndex
    {
        get { return HipIndex; }
        set { HipIndex = value; }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = FCost.CompareTo(nodeToCompare.FCost);
        if (compare == 0)
        {
            compare = HCost.CompareTo(nodeToCompare.HCost);
        }

        return -compare;
    }

}
