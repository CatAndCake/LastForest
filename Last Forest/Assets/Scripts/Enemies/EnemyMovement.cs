using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyBattle enemyBattle;
    public GameObject target;

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

    public UnityEngine.AI.NavMeshAgent navMesh;
    float timer;
    public float speedAnimation = 0f;
    public float distance;
    Animator anim;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enddar = GameObject.FindGameObjectWithTag("Enddar");

        navMesh = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = this.gameObject.GetComponent<Animator>();
        enemyBattle = this.gameObject.GetComponent<EnemyBattle>();
        distance = 100;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        distancePlayer = Vector3.Distance(player.transform.position, transform.position);
        distanceEnddar = Vector3.Distance(enddar.transform.position, transform.position);
        if(closestTree() != null)
        {
            distanceTree = Vector3.Distance(closestTree().transform.position, transform.position);
        }
        
        CompareDistances(distancePlayer, distanceEnddar, distanceTree);
        //enemyBattle.inFront = InFront();
        // gdy porównamy dystense i dowiemy się który obiekt jest najbliżej musimy sprawdzić 
        // czy jest konieczność obrócenia się czy jej nie ma
        //TurnOrAttack();
        Attack();







    }

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
                target = closestTree();

            }
            else
            {
                if (distancePlayer - distanceEnddar <= 5)
                {
                    target = player;
                    distance = distancePlayer;

                }

                if (distancePlayer - distanceEnddar >= 5)
                {
                    target = enddar;
                    distance = distanceEnddar;
                }
            }
        }

        if(closestTree() == null)
        {
            if (distancePlayer - distanceEnddar <= 5)
            {
                distance = distancePlayer;
                target = player;

            }

            if (distancePlayer - distanceEnddar >= 5)
            {
                distance = distanceEnddar;
                target = enddar;
            }
        }
    }

    public void Tk()
    {
        Vector3 targetDir = target.transform.position - transform.position;
        Vector3 forward = transform.forward;
        float angle = Vector3.SignedAngle(targetDir, forward, Vector3.up);
        

        if (angle >= -90 && angle <= 90)
        {
            //anim.SetFloat("Movement", 0);
            // GO to the target
            Debug.DrawLine(transform.position, target.transform.position, Color.white);
            anim.SetFloat("Movement", 0);
            Attack();
            //anim.SetFloat("Move", 0);
        }
        else
        {
            
            anim.SetFloat("Movement", 1);

            if (angle >= -180 && angle < -90)
            {
                //right
                Debug.DrawLine(transform.position, target.transform.position, Color.blue);
                anim.SetFloat("Turn", 1);
                float r = Time.deltaTime * 50f;

                transform.Rotate(0, r, 0, Space.Self);
            }
            if (angle > 90 && angle <= 180)
            {
                //left
                Debug.DrawLine(transform.position, target.transform.position, Color.green);
                anim.SetFloat("Turn", 0);

                float r = Time.deltaTime * -50f;
                

                transform.Rotate(0, r, 0, Space.Self);
            }
        }
    }

    public void Attack()
    {
        if(MoveOrTurn() == 0)
        {
            

            if(distance > 1.5f)
            {
                navMesh.SetDestination(target.transform.position);
                anim.SetFloat("Movement", 0);
                navMesh.speed = speedAnimation * 2;
                //navMesh.speed = speedAnimation * 1.5f;

                if (SmoothAnimation())
                {
                    if (distance > 6f)
                    {
                        if (speedAnimation < 1)
                        {
                            speedAnimation = speedAnimation + 0.1f;
                        }
                        if (speedAnimation >= 1)
                        {
                            speedAnimation = speedAnimation + 0.1f;
                        }
                        if (speedAnimation >= 2)
                        {
                            speedAnimation = 2;
                        }
                        anim.SetFloat("Move", speedAnimation);
                        timer = 0f;
                    }

                    if (distance < 6f && distance > 1.5f)
                    {

                        if (speedAnimation > 1)
                        {
                            speedAnimation = speedAnimation - 0.07f;

                            if (speedAnimation <= 1f)
                            {
                                speedAnimation = 1f;
                            }
                            anim.SetFloat("Move", speedAnimation);
                            timer = 0f;

                        }
                        if (speedAnimation < 1)
                        {
                            speedAnimation = speedAnimation + 0.01f;
                            if (speedAnimation >= 1f)
                            {
                                speedAnimation = 1f;
                            }
                            anim.SetFloat("Move", speedAnimation);
                            timer = 0f;
                        }

                        if (speedAnimation == 1)
                        {
                            speedAnimation = 1f;

                            anim.SetFloat("Move", speedAnimation);
                            timer = 0f;
                        }

                    }
                }
            } 

            if(distance <= 1.5f)
            {
                if(InFront() == 0)
                {
                    navMesh.SetDestination(transform.position);
                    //transform.LookAt(target.transform.position);
                    anim.SetFloat("Movement", 0);
                    anim.SetFloat("Move", 0);
                    timer = 0f;
                }
                if(InFront() == 1)
                {
                    navMesh.SetDestination(transform.position);

                    anim.SetFloat("Turn", 1);
                    float r = Time.deltaTime * 80f;

                    transform.Rotate(0, r, 0, Space.Self);
                    timer = 0f;
                }

                if(InFront() == 2)
                {
                    navMesh.SetDestination(transform.position);

                    anim.SetFloat("Turn", 0);
                    float r = Time.deltaTime * -80f;

                    transform.Rotate(0, r, 0, Space.Self);
                    timer = 0f;
                }
            }
            
        }

        if(MoveOrTurn() == 1 || MoveOrTurn() == 2)
        {
            anim.SetFloat("Movement", 1);
            navMesh.SetDestination(transform.position);

            if(MoveOrTurn() == 1)
            {
                anim.SetFloat("Turn", 1);
                float r = Time.deltaTime * 80f;

                transform.Rotate(0, r, 0, Space.Self);
            }
            if (MoveOrTurn() == 2)
            {
                anim.SetFloat("Turn", 0);
                float r = Time.deltaTime * -80f;

                transform.Rotate(0, r, 0, Space.Self);
            }

        }

        
    }

   public float MoveOrTurn()
    {
        //Tutaj sprawdzamy czy musimy sie odwrocic do celu

        Vector3 targetDir = target.transform.position - transform.position;
        Vector3 forward = transform.forward;
        float angle = Vector3.SignedAngle(targetDir, forward, Vector3.up);
        
        if (angle >= -90 && angle <= 90)
        {
            return 0;
            
        }
        else
        {
            if (angle >= -180 && angle < -90)
            {
                return 1;
            }
            else
            {
                return 2;  
            }
        } 
    }

    public float InFront()
    {
        //Tutaj sprawdzamy czy nasz cel znaduje się dokładnie przed nami

        Vector3 targetDir = target.transform.position - transform.position;
        Vector3 forward = transform.forward;
        float angle = Vector3.SignedAngle(targetDir, forward, Vector3.up);
        

        if (angle >= -10 && angle <= 10)
        {
            return 0;
        }
        if(angle < -10 && angle >= -180)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }
   

    private bool SmoothAnimation()
    {
        if (timer >= 0.001f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
    


    


