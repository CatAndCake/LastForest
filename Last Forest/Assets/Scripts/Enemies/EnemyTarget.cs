using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyTarget : MonoBehaviour
{
    public EnemyPathfindin pathfinding;
    //public AIPath path;
    public Vector3 closestTarget;

    //Player
    GameObject player;

    //Enddar
    GameObject enddar;

    //Trees
    GameObject[] trees;

    //Disances
    float distancePlayer;
    float distanceEnddar;
    float distanceTree;

    public float distance;

    //PathUtilities utilities;
    new List<Vector3> aroundTarget = new List<Vector3>();

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enddar = GameObject.FindGameObjectWithTag("Enddar");
        pathfinding = GetComponent<EnemyPathfindin>();
        //path = GetComponent<AIPath>();
        distance = 100;
    }

    private void Update()
    {
        distancePlayer = Vector3.Distance(player.transform.position, transform.position);
        distanceEnddar = Vector3.Distance(enddar.transform.position, transform.position);
        if (closestTree() != null)
        {
            distanceTree = Vector3.Distance(closestTree().transform.position, transform.position);
        }

        CompareDistances(distancePlayer, distanceEnddar, distanceTree);

        pathfinding.target = closestTarget;
        //path.target = closestTarget;
        
    }

    //NavGraph grap;

    

    
    public GameObject closestTree()
    {
        GameObject[] trees;
        trees = GameObject.FindGameObjectsWithTag("Tree");
        GameObject closestTree = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject tree in trees)
        {
            Vector3 difference = tree.transform.position - position;
            float curDistance = difference.sqrMagnitude;
            if (curDistance < distance)
            {
                closestTree = tree;
                distance = curDistance;
            }
        }
        return closestTree;
    }

    void CompareDistances(float distancePlayer, float distanceEnddar, float distanceTree)
    {

        if (closestTree() != null)
        {
            if (distancePlayer - distanceTree >= 4f && distanceEnddar - distanceTree >= 4f)
            {
                distance = distanceTree;
                closestTarget = closestTree().transform.position;

            }
            else
            {
                if (distancePlayer - distanceEnddar <= 5)
                {
                    closestTarget = player.transform.position;
                    distance = distancePlayer;

                }

                if (distancePlayer - distanceEnddar >= 5)
                {
                    closestTarget = enddar.transform.position;
                    distance = distanceEnddar;
                }
            }
        }

        if (closestTree() == null)
        {
            if (distancePlayer - distanceEnddar <= 5)
            {
                distance = distancePlayer;
                closestTarget = player.transform.position;

            }

            if (distancePlayer - distanceEnddar >= 5)
            {
                distance = distanceEnddar;
                closestTarget = enddar.transform.position;
            }
        }
    }
}
