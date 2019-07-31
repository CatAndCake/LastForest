using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //Game Objects
    public GameObject gem;

    public GameObject treeFruits;
    public GameObject treeFighting;
    public GameObject treeDefending;

    public GameObject weapon;

    GameObject[] trees;

    //Player
    GameObject player;

    //Searching Trees
    int round = 2;
    Vector3 nTreePosition;

    //Floats
    float randomEvent;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        trees = GameObject.FindGameObjectsWithTag("Tree");
        
        
    }


    public void AfterDeathEvent(float x, float y, float z, int experience)
    {
        DevelopTrees(experience);

        float positionX = x;
        float positionY = y;
        float positionZ = z;

        randomEvent = Random.Range(0, 100);

        if (randomEvent <= 50)
        {
            Debug.Log("Nothing");
        }
        if (randomEvent > 50 && randomEvent <= 85)
        {
            Debug.Log("Gem");
            SpawnLifeGem(positionX, positionY, positionZ);
        }
        if (randomEvent > 85 && randomEvent <= 90)
        {
            Debug.Log("Weapon");
            SpawnWeapon(positionX, positionY, positionZ);
        }
        if (randomEvent > 90 && randomEvent <= 100)
        {
            Debug.Log("Tree");

            float distancePlayerToClosestTree = 
                Vector3.Distance(player.transform.position,
                closestTreeForPlayer().transform.position);

            if(distancePlayerToClosestTree >= 5f)
            {
                SpawnTree(positionX, positionY, positionZ);
            }
            if(distancePlayerToClosestTree < 5f)
            {
                FindLocationForNewTree();
            }
        }
    }

    void SpawnLifeGem(float x, float y, float z)
    {
        Instantiate(gem, new Vector3(x, y, z),
        Quaternion.identity);
    }

    void SpawnWeapon(float x, float y, float z)
    {
        Instantiate(weapon, new Vector3(x, y, z),
        Quaternion.identity);
    }

    void FindLocationForNewTree()
    {
        Vector3 nLocation;

        nLocation = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

        //Check left vector
        Vector3 nLocationLeft;
        Vector3 nLocationUp;
        Vector3 nLocationRight;
        Vector3 nLocationDown;
        Vector3 nLocationLeft2;
        Vector3 nLocationUp2;

        //Should I search longer?
        bool keepLooking = true;

        if (keepLooking == true)
        {
            for (float f = 0; f <= round / 2; f = f + 1)
            {
                nLocationLeft = nLocation + new Vector3(-f, 0f, 0f);

                nTreePosition = new Vector3(nLocationLeft.x, nLocationLeft.y, nLocationLeft.z);

                float treeDistance = Vector3.Distance(nLocationLeft, closestTree().transform.position);

                if (keepLooking == true)
                {
                    if (treeDistance >= 3f)
                    {
                        Debug.Log("Found Position Left");
                        Debug.Log(round);
                        keepLooking = false;
                        SpawnTreeAndFindLocation(nLocationLeft.x, nLocationLeft.y, nLocationLeft.z);
                        round = 2;
                    }
                }
            }
        }

        nLocationLeft = nLocation + new Vector3(-round / 2f, 0f, 0f);

        if (keepLooking == true)
        {
            for (float f = 0; f <= round / 2; f = f + 1)
            {
                nLocationUp = nLocationLeft + new Vector3(0f, 0f, f);

                nTreePosition = new Vector3(nLocationUp.x, nLocationUp.y, nLocationUp.z);

                float treeDistance = Vector3.Distance(nLocationUp, closestTree().transform.position);

                if (keepLooking == true)
                {
                    if (treeDistance >= 3f)
                    {
                        Debug.Log("Found Position Up");
                        Debug.Log(round);
                        keepLooking = false;
                        SpawnTreeAndFindLocation(nLocationUp.x, nLocationUp.y, nLocationUp.z);
                        round = 2;
                    }
                }
            }
        }

        nLocationUp = nLocationLeft + new Vector3(0f, 0f, round / 2f);

        if (keepLooking == true)
        {
            for (float f = 0; f <= round; f = f + 1)
            {
                nLocationRight = nLocationUp + new Vector3(f, 0f, 0f);
                nTreePosition = new Vector3(nLocationRight.x, nLocationRight.y, nLocationRight.z);

                float treeDistance = Vector3.Distance(nLocationRight, closestTree().transform.position);

                if (keepLooking == true)
                {
                    if (treeDistance >= 3f)
                    {
                        Debug.Log("Found Position Right");
                        Debug.Log(round);
                        keepLooking = false;
                        SpawnTreeAndFindLocation(nLocationRight.x, nLocationRight.y, nLocationRight.z);
                        round = 2;
                    }
                }
            }
        }

        nLocationRight = nLocationUp + new Vector3(round, 0f, 0f);

        if (keepLooking == true)
        {
            for (float f = 0; f <= round; f = f + 1)
            {
                nLocationDown = nLocationRight - new Vector3(0f, 0f, f);

                nTreePosition = new Vector3(nLocationDown.x, nLocationDown.y, nLocationDown.z);

                float treeDistance = Vector3.Distance(nLocationDown, closestTree().transform.position);

                if (keepLooking == true)
                {
                    if (treeDistance >= 3f)
                    {
                        Debug.Log("Found Position Down");
                        Debug.Log(round);
                        keepLooking = false;
                        SpawnTreeAndFindLocation(nLocationDown.x, nLocationDown.y, nLocationDown.z);
                        round = 2;
                    }
                }
            }
        }

        nLocationDown = nLocationRight - new Vector3(0f, 0f, round);

        if (keepLooking == true)
        {
            for (float f = 0; f <= round; f = f + 1)
            {
                nLocationLeft2 = nLocationDown - new Vector3(f, 0f, 0f);

                nTreePosition = new Vector3(nLocationLeft2.x, nLocationLeft2.y, nLocationLeft2.z);

                float treeDistance = Vector3.Distance(nLocationLeft2, closestTree().transform.position);

                if (keepLooking == true)
                {
                    if (treeDistance >= 3f)
                    {
                        Debug.Log("Found Position Left2");
                        Debug.Log(round);
                        keepLooking = false;
                        SpawnTreeAndFindLocation(nLocationLeft2.x, nLocationLeft2.y, nLocationLeft2.z);
                        round = 2;
                    }
                }
            }
        }

        nLocationLeft2 = nLocationDown - new Vector3(round, 0f, 0f);

        if (keepLooking == true)
        {
            for (float f = 0; f <= round; f = f + 1)
            {
                nLocationUp2 = nLocationLeft2 + new Vector3(0f, 0f, f);

                nTreePosition = new Vector3(nLocationUp2.x, nLocationUp2.y, nLocationUp2.z);

                float treeDistance = Vector3.Distance(nLocationUp2, closestTree().transform.position);

                if (keepLooking == true)
                {
                    if (treeDistance >= 3f)
                    {
                        Debug.Log("Found Position Left2");
                        Debug.Log(round);
                        keepLooking = false;
                        SpawnTreeAndFindLocation(nLocationUp2.x, nLocationUp2.y, nLocationUp2.z);
                        round = 2;
                    }
                }
            }
        }

        if (keepLooking == true)
        {
            round = round + 2;
            FindLocationForNewTree();
        }

    }

    void SpawnTree(float x, float y, float z)
    {

        int treeType = Random.Range(0, 100);
        if (treeType <= 50)
        {
            Debug.Log("Fruits Tree");
            Instantiate(treeFruits, new Vector3(x, y, z),
            Quaternion.identity);
        }

        if(treeType > 50 && treeType <= 80)
        {
            Debug.Log("Fighting Tree");
            Instantiate(treeFighting, new Vector3(x, y, z),
            Quaternion.identity);
        }

        if(treeType > 80 && treeType <= 95)
        {
            Debug.Log("Defending Tree");

            //Defending tree
            Instantiate(treeDefending, new Vector3(x, y, z),
            Quaternion.identity);
        }

        if(treeType > 95 && treeType <= 100)
        {
            Debug.Log("Ent");
            //Ent
            Instantiate(treeFighting, new Vector3(x, y, z),
            Quaternion.identity);
        }

    }

    void SpawnTreeAndFindLocation(float x, float y, float z)
    {
        int treeType = Random.Range(0, 100);
        if (treeType <= 50)
        {
            Debug.Log("Fruits Tree");
            Instantiate(treeFruits, new Vector3(x, y, z),
            Quaternion.identity);
        }

        if (treeType > 50 && treeType <= 80)
        {
            Debug.Log("Fighting Tree");
            Instantiate(treeFighting, new Vector3(x, y, z),
            Quaternion.identity);
        }

        if (treeType > 80 && treeType <= 95)
        {
            Debug.Log("Defending Tree");

            //Defending tree
            Instantiate(treeDefending, new Vector3(x, y, z),
            Quaternion.identity);
        }

        if (treeType > 95 && treeType <= 100)
        {
            Debug.Log("Ent");
            //Ent
            Instantiate(treeFighting, new Vector3(x, y, z),
            Quaternion.identity);
        }
    }

    void DevelopTrees(int experience)
    {
        foreach (GameObject tree in trees)
        {
            if(tree.transform.parent.transform.tag == "TreeFruit")
            {
                tree.transform.parent.GetComponent<TreeFruits>().exp =
                    tree.transform.parent.GetComponent<TreeFruits>().exp + experience;
            }

            if (tree.transform.parent.transform.tag == "TreeFighting")
            {
                tree.transform.parent.GetComponent<TreeFighting>().exp =
                    tree.transform.parent.GetComponent<TreeFighting>().exp + experience;
            }

            if(tree.transform.parent.tag == "TreeDefending")
            {
                tree.transform.parent.GetComponent<TreeDefending>().exp =
                    tree.transform.parent.GetComponent<TreeDefending>().exp + experience;
            }
        }
    }

    public GameObject closestTree()
    {
        GameObject[] trees;
        trees = GameObject.FindGameObjectsWithTag("Tree");
        GameObject closestTree = null;
        float distance = Mathf.Infinity;
        Vector3 position = nTreePosition;
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

    public GameObject closestTreeForPlayer()
    {
        GameObject[] trees;
        trees = GameObject.FindGameObjectsWithTag("Tree");
        GameObject closestTree = null;
        float distance = Mathf.Infinity;
        Vector3 position = player.transform.position;
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

    
}
