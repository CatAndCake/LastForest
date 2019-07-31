using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpeed : MonoBehaviour
{
    EnemyTarget target;
    
    EnemyPathfindin pathfindin;
    public float speed;
    public float timer;

    private void Awake()
    {
        target = GetComponent<EnemyTarget>();
        pathfindin = GetComponent<EnemyPathfindin>();
    }

    private void Update()
    {
        CalculateSpeed();
        pathfindin.normalSpeed = speed;
        
    }

    void CalculateSpeed()
    {
        if (pathfindin.reachedAttackPosition == false)
        {
            // AI still didn't reach attack position and has to go to the target

            if (target.distance >= 6f)
            {
                if (speed < 4f)
                {
                    speed = speed + 0.01f;
                }
                if (speed >= 4f)
                {
                    speed = speed - 0.01f;
                }
                if(speed == 4f)
                {
                    speed = 4f;
                }
            }
            if (target.distance < 6f)
            {
                if (speed > 2f)
                {
                    speed = speed - 0.07f;
                }
                if (speed <= 2f)
                {
                    speed = speed + 0.01f;
                }
                if(speed == 2f)
                {
                    speed = 2f;
                }

            }

        }
        else if(pathfindin.reachedAttackPosition == true)
        {
            //AI reached attack position and should attack right now
            speed = 0f;

            //Jesli znajdujesz sie przed targettem



            //Jesli musisz sie przekrecic w lewo
            //speed 0f
            //float r = time.deltatiem * 80f;


            //Jesli musisz sie przekrecic w prawo
            //speed = 0f;
            //float r = time.deltatiem * -80f;

            //Jedna opcja jest taka ze target znaduje sie kompletnie poza naszym katem
            //Druga opcja jest taka ze target znaduje sie pod lekkim katem
        }



    }
    
}
