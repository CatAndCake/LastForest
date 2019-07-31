using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasBars : MonoBehaviour
{
    PlayerStatistics playerStats;
    EnddarStatistics enddarStats;

    public GameObject playerHealth;
    public GameObject playerStamina;
    public GameObject playerHunger;

    public GameObject experience;
    public GameObject playerLevel;

    public GameObject clock;
    public GameObject nextBattle;
    public GameObject sure;
    public GameObject sureYes;
    public GameObject sureNo;

    public GameObject enddarLevel;
    public GameObject enddarHealth;
    public GameObject roots;

    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();
        enddarStats = GameObject.FindGameObjectWithTag("Enddar").GetComponent<EnddarStatistics>();

        playerHealth = this.gameObject.transform.Find("Player").transform.Find("Bars").
            transform.Find("PlayerHealth").transform.Find("Border").gameObject;
        playerStamina = this.transform.Find("Player").transform.Find("Bars").
            transform.Find("PlayerStamina").transform.Find("Border").gameObject;
        playerHunger = this.transform.Find("Player").transform.Find("Bars").
            transform.Find("PlayerHunger").transform.Find("Border").gameObject;

        experience = this.transform.Find("Player").transform.Find("Stats").
            transform.Find("Experience").transform.Find("Background").transform.Find("Button").gameObject;
        playerLevel = this.transform.Find("Player").transform.Find("Stats").
            transform.Find("Level").transform.Find("Image").gameObject;

        clock = this.transform.Find("Player").transform.Find("BattleTimer").
            transform.Find("Clock").transform.Find("Background").transform.Find("Text").gameObject;
        nextBattle = this.transform.Find("Player").transform.Find("BattleTimer").
            transform.Find("NextBattle").transform.Find("Panel").transform.Find("Text").gameObject;
        sure = this.transform.Find("Player").transform.Find("BattleTimer").
            transform.Find("AreYouSure").gameObject;
        sureYes = this.transform.Find("Player").transform.Find("BattleTimer").transform.Find("AreYouSure").
            transform.Find("Background").transform.Find("Buttons").transform.Find("Yes").gameObject;
        sureNo = this.transform.Find("Player").transform.Find("BattleTimer").transform.Find("AreYouSure").
            transform.Find("Background").transform.Find("Buttons").transform.Find("No").gameObject;

        enddarLevel = this.transform.Find("Enddar").transform.Find("Stats").
            transform.Find("Level").transform.Find("Image").gameObject;
        enddarHealth = this.transform.Find("Enddar").transform.Find("Stats").
            transform.Find("Health").transform.Find("Image").gameObject;
        roots = this.transform.Find("Enddar").transform.Find("Stats").
            transform.Find("Roots").transform.Find("Image").gameObject;

    }

    private void Start()
    {
        sure.SetActive(false);
    }

    private void Update()
    {
        InteractingWithButtons();
    }
    void InteractingWithButtons()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
                eventDataCurrentPosition.position = new Vector2(touch.position.x, touch.position.y);
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

                foreach (RaycastResult result in results)
                {
                    // czy jest włączony jakis inny canvas? jestli tak to nic sie nie wyswietli

                    if(result.gameObject == clock)
                    {
                        CanvasInfoText.SetText("After this time next battle will start", 5);
                    }
                    if (result.gameObject == nextBattle)
                    {
                        sure.SetActive(true);
                    }
                    if(result.gameObject == sureYes)
                    {
                        BattleManager.timerBattle = 0;
                    }
                    if(result.gameObject == sureNo)
                    {
                        sure.SetActive(false);
                    }
                    if (result.gameObject == experience)
                    {
                        CanvasInfoText.SetText("Player's experience: " + playerStats.experiance + "\n" +
                            (playerStats.experianceToUpgrade - playerStats.experiance) + " exp left to next level", 5);
                    }
                    if (result.gameObject == playerLevel)
                    {
                        CanvasInfoText.SetText("Player's level: " + playerStats.level, 3);
                    }
                    if (result.gameObject == playerHealth)
                    {
                        CanvasInfoText.SetText("Player's health: " + playerStats.healthCurrent, 3);
                    }
                    if (result.gameObject == playerStamina)
                    {
                        CanvasInfoText.SetText("Player's stamina: " + playerStats.staminaCurrent, 3);
                    }
                    if (result.gameObject == playerHunger)
                    {
                        CanvasInfoText.SetText("Player's hunger: " + playerStats.hungerCurrent, 3);
                    }
                    
                    if (result.gameObject == enddarLevel)
                    {
                        CanvasInfoText.SetText("Enddar's level: " + enddarStats.level, 3);
                    }
                    if (result.gameObject == enddarHealth)
                    {
                        CanvasInfoText.SetText("Enddar's health: " + enddarStats.healthCurrent, 3);
                    }
                    if (result.gameObject == roots)
                    {
                        CanvasInfoText.SetText("Enddar's roots: " + enddarStats.roots, 3);
                    }
                }
            }
        }
    }
}
