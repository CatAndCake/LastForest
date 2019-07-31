using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfindin : MonoBehaviour
{ 

    PathfindingManager pathfindingManager;
    GridWorld gridWorld;

    public List<Node> currentNodes = new List<Node>();
    public List<Node> neigbourNodesToTarget = new List<Node>();

    const float minPathUpdateTime = .2f;
    const float pathUpdateMoveThreshold = .5f;

    Vector3 tPosition = new Vector3();
    public Vector3 target;
    public Vector3 currentTarget;
    public Vector3 attackPosition;
    public bool reachedAttackPosition = false;

    public float normalSpeed; //This is correct speed which AI should has if there are no other AI in front of it
    public float speed; // Current speed, if there are other AI in front of it's ai it will be smaller than normal
    public float turnSpeed = 3;
    public float turnDst = 5;
    public float stoppingDst = 10;

    Path path;
    public Vector3[] currentPath;

    private void Awake()
    {
        gridWorld = GameObject.FindGameObjectWithTag("PathfindingManager").GetComponent<GridWorld>();
        pathfindingManager = GameObject.FindGameObjectWithTag("PathfindingManager").GetComponent<PathfindingManager>();
    }
    void Start()
    {
        StartCoroutine(UpdatePath());
    }

    private void Update()
    {
        FindCurrentTarget();
        MakeCurrentPositionNonWalkable();
        FindBestPositionToAttack();
        tPosition = transform.position;
        
    }

    void FindCurrentTarget()
    {
        // If the AI target gotten from Enemy Target is different from current target
        // then current target becomes the target from Enemy target class
        if (target != null)
        {
            if (currentTarget != target)
            {
                currentTarget = target;
                FindNeighbourNodesOfTarget();
            }
        }
    }

    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = new Path(waypoints, transform.position, turnDst, stoppingDst);
            currentPath = waypoints;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator UpdatePath()
    {

        if (Time.timeSinceLevelLoad < .3f)
        {
            yield return new WaitForSeconds(.3f);
        }

        FindNeighbourNodesOfTarget();
        PathRequestManager.RequestPath(new PathRequest(transform.position, attackPosition, OnPathFound, this.gameObject));

        float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector3 targetPosOld = attackPosition;

        while (true)
        {
            yield return new WaitForSeconds(minPathUpdateTime);
            
            if ((attackPosition - targetPosOld).sqrMagnitude > sqrMoveThreshold)
            {
                FindNeighbourNodesOfTarget();
                PathRequestManager.RequestPath(new PathRequest(transform.position, attackPosition, OnPathFound, this.gameObject));
                targetPosOld = attackPosition;
            }
        }
    }

    IEnumerator FollowPath()
    {

        bool followingPath = true;
        int pathIndex = 0;
        transform.LookAt(path.lookPoints[0]);

        float speedPercent = 1;

        while (followingPath)
        {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
            while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {
                if (pathIndex == path.finishLineIndex)
                {
                    followingPath = false;
                    break;
                }
                else
                {
                    pathIndex++;
                }
            }

            if (followingPath)
            {

                if (pathIndex >= path.slowDownIndex && stoppingDst > 0)
                {
                    speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D) / stoppingDst);
                    if (speedPercent < 0.01f)
                    {
                        followingPath = false;
                    }
                }

                Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                //transform.Translate((Vector3.forward + MoveToTarget()) * Time.deltaTime * speed * speedPercent, Space.Self);
                transform.position = Vector3.MoveTowards(transform.position, path.lookPoints[pathIndex], normalSpeed * Time.deltaTime) + CalculatedSeparation();
            }

            yield return null;

        }
    }

    void MakeCurrentPositionNonWalkable()
    {
        Node currentNode = gridWorld.NodeFromWorldPoint(transform.position);
        
        int gridX = currentNode.GridX;
        int gridY = currentNode.GridY;

        

        if (!currentNodes.Contains(currentNode))
        {
            currentNodes.Add(currentNode);
        }

        foreach (Node walkedNode in currentNodes)
        {
            if (walkedNode.GridX == gridX && walkedNode.GridY == gridY)
            {
                walkedNode.nonWalkableDynamic = true;
                if(walkedNode.AI == null)
                {
                    walkedNode.AI = this.gameObject;
                }
            }
            else
            {
                walkedNode.nonWalkableDynamic = false;
                if(walkedNode.AI == this.gameObject)
                {
                    walkedNode.AI = null;
                }
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

    void FindBestPositionToAttack()
    {
        //While AI is walking, on path it might find other ai 
        //So it has to find walkable nodes

        Node attackPositionNode = gridWorld.NodeFromWorldPoint(attackPosition);
        Node currentNode = gridWorld.NodeFromWorldPoint(transform.position);

        //If our possition is equal to attack position 

        if(transform.position == attackPositionNode.Position)
        {
            reachedAttackPosition = true;
            transform.LookAt(currentTarget);
        }
        else
        {
            reachedAttackPosition = false;
        }

        if (currentNode == attackPositionNode)
        {
            if(transform.position != attackPosition)
            {
                Debug.Log(this.gameObject.name);
            }
        }
        
        if(attackPositionNode.AI != null)
        {
            if (attackPositionNode.AI != this.gameObject)
            {
                FindNeighbourNodesOfTarget();
                PathRequestManager.RequestPath(new PathRequest(transform.position, attackPosition, OnPathFound, this.gameObject));
            }
        }
        //Powinniśmy również obliczyć odległość naszego AI 
    }

    

    public List<GameObject> closeAI = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            if (!closeAI.Contains(other.gameObject))
            {
                closeAI.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            closeAI.Remove(other.gameObject);
        }
    }

    public Vector3 Separation()
    {
        Vector3 separateVector = new Vector3();

        if (closeAI.Count == 0)
        {
            return separateVector;
        }

        foreach (GameObject AI in closeAI)
        {
            Vector3 movingTowards = this.transform.position - AI.transform.position;
            if (movingTowards.magnitude > 0)
            {
                separateVector += movingTowards.normalized / movingTowards.magnitude;
            }

        }

        return separateVector.normalized;
    }

    Vector3 CalculatedSeparation()
    {
        Node currentNode = gridWorld.NodeFromWorldPoint(transform.position);
        Node attackNode = gridWorld.NodeFromWorldPoint(attackPosition);

        if(currentNode != attackNode)
        {
            Vector3 acceleration = Separation();
            acceleration = Vector3.ClampMagnitude(acceleration, 1) * Time.deltaTime;
            return acceleration;
        }
        else
        {
            return new Vector3(0, 0, 0);
        }
        
    }
    

   

    void FindNeighbourNodesOfTarget()
    {
        // Jesli wszystkie nody sa zajete to trzeba znaleźć nastepne
        Node targetNode = gridWorld.NodeFromWorldPoint(currentTarget);
        Node currentNode = gridWorld.NodeFromWorldPoint(transform.position);
        neigbourNodesToTarget = new List<Node>();

        bool findNeighboringNodes = true;
        int distance = 1;

        while(findNeighboringNodes)
        {
            neigbourNodesToTarget = gridWorld.GetWalkableNeigboringNodes(targetNode, distance);

            if(neigbourNodesToTarget.Count > 0)
            {
                findNeighboringNodes = false;
            }
            distance++;
        }

        
        foreach (Node neighbour in neigbourNodesToTarget)
        {

            //odległość od gracza do noda
            int newMovementCost = currentNode.GCost + pathfindingManager.GetDistance(currentNode, neighbour);

            neighbour.GCost = newMovementCost;
            neighbour.HCost = pathfindingManager.GetDistance(neighbour, targetNode);
        }


        float neighbourNodeFCost = Mathf.Infinity;
        Vector3 newAttackPosition = new Vector3();

        for (int i = 0; i < neigbourNodesToTarget.Count; i++)
        {
            if (neigbourNodesToTarget[i].FCost < neighbourNodeFCost)
            {
                neighbourNodeFCost = neigbourNodesToTarget[i].FCost;
                newAttackPosition = neigbourNodesToTarget[i].Position;
            }

        }
        attackPosition = newAttackPosition;
        neigbourNodesToTarget = new List<Node>();
    }

    public void OnDrawGizmos()
    {
        if(neigbourNodesToTarget.Count > 0)
        {
            Gizmos.color = Color.blue;
            foreach(Node n in neigbourNodesToTarget)
            {
                Gizmos.DrawCube(n.Position, Vector3.one);
            }
            
        }
    }
}

