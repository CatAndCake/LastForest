using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerBattle : MonoBehaviour {

    //UI
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    // Held Sword
    int swordEfficiency;
    int swordCurrentEfficiecny;
    int swordSharpness;
    int swordWeight;
    int swordWidth;
    int savedSouls;

    //Buttons
    GameObject attackButton;

    // Player Scripts
    PlayerStatistics playerStatistics;
    // location
    Vector3 forwardVector;

    //Collision
    bool weaponCollision;
    public GameObject collidedEnemy;

    //Animator
    Animator anim;

    //Timer
    float TimerAttack;

    private void Awake()
    {
        attackButton = GameObject.FindGameObjectWithTag("CanvasTouch").transform.Find("PanelBattle").transform.Find("Attack").
            transform.Find("AttackButton").transform.gameObject;

        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();

        anim = this.gameObject.GetComponent<Animator>();
        playerStatistics = this.gameObject.GetComponentInParent<PlayerStatistics>();
    }

    private void Update()
    {
        TimerAttack += Time.deltaTime;
        forwardVector = this.gameObject.transform.TransformDirection(Vector3.forward);
        Buttons();
        //AttackAnimations(a);
    }

    void Buttons()
    {
        if(Input.touchCount > 0)
        {
            //It will not work if there is already one finger on the screen!
            Touch touch;
            touch = Input.GetTouch(0);

            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);

            eventDataCurrentPosition.position = new Vector2(touch.position.x, touch.position.y);

            List<RaycastResult> results = new List<RaycastResult>();
            
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

            foreach (RaycastResult result in results)
            {
                if(result.gameObject == attackButton)
                {
                    AttackAnimations();
                }
            }
            
        }
    }

    bool CanAttack()
    {
        if (TimerAttack > 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AttackAnimations()
    {
        if (CanAttack())
        {
            anim.SetTrigger("Attack");
            TimerAttack = 0f;
        }
    }

    public void WeaponCollision(GameObject collEnemy)
    {
        weaponCollision = true;
        collidedEnemy = collEnemy;
    }

    public void EndWeaponCollision()
    {
        weaponCollision = false;
        collidedEnemy = null;
    }

    void Hit(int i = 0)
    {
        if(i == 1)
        {
            if (weaponCollision)
            {
                AttackEffect();
            }
        }
    }

    void AttackEffect()
    {
        if(collidedEnemy != null)
        {
            collidedEnemy.GetComponent<Rigidbody>().AddForce(forwardVector * 100);
            collidedEnemy.GetComponent<EnemyBattle>().GetHit(playerStatistics.strength);
        }
    }
}
