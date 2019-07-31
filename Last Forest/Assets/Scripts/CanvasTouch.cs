using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasTouch : MonoBehaviour
{
    // Hides the keyboard if the device is facing down
    // and resumes input if the device is facing up.
    //Canvas
    CanvasMenu canvasMenu;
    public bool battle = false;
    public GameObject enemy;

    // Player
    PlayerBattle playerBattle;
    PlayerMovement playerMovement;

    //No Battle
    GameObject panelNoBattle;
    GameObject development;
    GameObject interaction;
    GameObject speed;

    //Battle
    GameObject panelBattle;
    public GameObject attack;
    public GameObject speedBattle;
    public GameObject jump;
    public GameObject defence;
    public GameObject attackType;

    public bool run = false;
 

    

    bool developSword;
    int swordPower;

    private void Awake()
    {
        canvasMenu = GameObject.FindGameObjectWithTag("CanvasMenu").GetComponent<CanvasMenu>();
        playerBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBattle>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        // No battle
        panelNoBattle = this.gameObject.transform.Find("PanelNoBattle").gameObject;

        development = this.gameObject.transform.Find("PanelNoBattle").transform.Find("PlayerDevelopment").gameObject;
        interaction = this.gameObject.transform.Find("PanelNoBattle").transform.Find("Interaction").gameObject;
        speed = this.gameObject.transform.Find("PanelNoBattle").transform.Find("Speed").gameObject;

        // Battle

        panelBattle = this.gameObject.transform.Find("PanelBattle").gameObject;

        attack = this.gameObject.transform.Find("PanelBattle").transform.Find("Attack").gameObject;
        speedBattle = this.gameObject.transform.Find("PanelBattle").transform.Find("Speed").gameObject;
        jump = this.gameObject.transform.Find("PanelBattle").transform.Find("Jump").gameObject;
        defence = this.gameObject.transform.Find("PanelBattle").transform.Find("Defence").gameObject;
        attackType = this.gameObject.transform.Find("PanelBattle").transform.Find("AttackType").gameObject;
    }

    private void Update()
    {
        InteractingWithButtons();
        //PanelBattle();
        //PanelNoBattle();

        if (Input.GetKeyDown(KeyCode.L))
        {
            Enemy();
        }
    }


    public void PanelBattle()
    {
        if(battle == true)
        {
            panelNoBattle.GetComponent<RectTransform>().offsetMin =  new Vector2(699, 250);
            
            panelBattle.SetActive(true);
            interaction.SetActive(false);
            speed.SetActive(false);
        }
    }

    public void PanelNoBattle()
    {
        if(battle == false)
        {
            panelNoBattle.GetComponent<RectTransform>().offsetMin = new Vector2(699, 120);

            panelBattle.SetActive(false);
            interaction.SetActive(true);
            speed.SetActive(true);
        }
    }

    void InteractingWithButtons()
    {
        if(Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                if (touch.phase == TouchPhase.Began)
                {
                    PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
                    eventDataCurrentPosition.position = new Vector2(touch.position.x, touch.position.y);
                    List<RaycastResult> results = new List<RaycastResult>();
                    EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

                    foreach (RaycastResult result in results)
                    {
                        if (result.gameObject.name == "Menu")
                        {
                            Menu();
                        }
                        if (result.gameObject.name == "Battle")
                        {
                            BattleOrNot();
                        }

                        if (result.gameObject.name == "Enemy")
                        {
                            Debug.Log("Enemy");
                        }
                        // no battle

                        //battle
                        if (result.gameObject == attack)
                        {
                            Attack();
                        }
                        if (result.gameObject == speedBattle)
                        {
                            Debug.Log("Speed1");
                            Speed();
                        }
                        if (result.gameObject == defence)
                        {

                        }
                        if (result.gameObject == attackType)
                        {

                        }
                    }
                }
            }
            
        }
    }

    void Menu()
    {
        // naciskając ten canvas gracz nie moze sie poruszac oraz zamykamy wszystkie inne canvasy
        
        canvasMenu.Enabled();
    }

    void BattleOrNot()
    {
        battle = !battle;

        if(battle == true)
        {
            panelNoBattle.GetComponent<RectTransform>().offsetMin = new Vector2(699, 250);

            panelBattle.SetActive(true);
            interaction.SetActive(false);
            speed.SetActive(false);
        }

        if(battle == false)
        {
            panelNoBattle.GetComponent<RectTransform>().offsetMin = new Vector2(699, 120);

            panelBattle.SetActive(false);
            interaction.SetActive(true);
            speed.SetActive(true);
        }
    }

    void Enemy()
    {
        GameObject nEnemy = Instantiate(enemy);

        nEnemy.transform.position = new Vector3(0, 0, 0);
    }

    void Attack()
    {
        playerBattle.AttackAnimations();
    }

    void Speed()
    {
        run = !run;
        
        if (run == true)
        {
            playerMovement.verticalSpeed = 10f;
            playerMovement.horizontalSpeed = 10f;
        }

        if(run == false)
        {
            playerMovement.verticalSpeed = 5f;
            playerMovement.horizontalSpeed = 5f;
        }
    }

}
