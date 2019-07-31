using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarsManager : MonoBehaviour
{

    GameObject canvasBars;

    // Next Battle
    
    GameObject clock;
    GameObject nextBattle;

    //Player

    PlayerStatistics playerStatistics;

    GameObject experience;
    int exp;
    int expToUpgrade;

    
    GameObject playerLevel;

    GameObject goPlayerHealth;
    Transform playerHealth;
    public float playerHealthValue;

    GameObject goPlayerStamina;
    Transform playerStamina;
    float playerStaminaValue;

    GameObject goPlayerHunger;
    Transform playerHunger;
    float playerHungerValue;


    // Enddar
    EnddarStatistics enddarStatistics;

    GameObject enddarLevel;

    GameObject enddarHealth;

    GameObject enddarRoots;
    //Enddar Mana

    private void Awake()
    {
        canvasBars = GameObject.FindGameObjectWithTag("CanvasBars");
        playerStatistics = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();

        //Battle timer
        clock = canvasBars.transform.Find("Player").transform.Find("BattleTimer").
            transform.Find("Clock").transform.Find("Background").transform.Find("Text").gameObject;
        nextBattle = canvasBars.transform.Find("Player").transform.Find("BattleTimer").
            transform.Find("NextBattle").gameObject;

        //Player stats
        experience = canvasBars.transform.Find("Player").transform.Find("Stats").
            transform.Find("Experience").transform.Find("Background").transform.Find("Button").
            transform.Find("Text").gameObject;

        playerLevel = canvasBars.transform.Find("Player").transform.Find("Stats").
            transform.Find("Level").transform.Find("Image").transform.Find("Text").gameObject;

        //Player Bars

        playerHealth = canvasBars.transform.Find("Player").transform.Find("Bars").
            transform.Find("PlayerHealth").transform.Find("Background").transform.Find("BarValue").gameObject.transform;

        playerStamina = canvasBars.transform.Find("Player").transform.Find("Bars").
            transform.Find("PlayerStamina").transform.Find("Background").transform.Find("BarValue").gameObject.transform;

        playerHunger = canvasBars.transform.Find("Player").transform.Find("Bars").
            transform.Find("PlayerHunger").transform.Find("Background").transform.Find("BarValue").gameObject.transform;

        // Enddar

        enddarStatistics = GameObject.FindGameObjectWithTag("Enddar").GetComponent<EnddarStatistics>();

        enddarLevel = canvasBars.transform.Find("Enddar").transform.Find("Stats").
            transform.Find("Level").transform.Find("Image").transform.Find("Text").gameObject;

        enddarHealth = canvasBars.transform.Find("Enddar").transform.Find("Stats").
            transform.Find("Health").transform.Find("Image").transform.Find("Text").gameObject;

        enddarRoots = canvasBars.transform.Find("Enddar").transform.Find("Stats").
            transform.Find("Roots").transform.Find("Image").transform.Find("Text").gameObject;

    }
    private void Start()
    {

        /*canvasBars = GameObject.FindGameObjectWithTag("CanvasBars");

        playerHealth = canvasBars.transform.Find("PlayerHealth").transform.
            Find("Background").transform.Find("BarValue").gameObject.transform;

        playerStamina = canvasBars.transform.Find("PlayerStamina").transform.
            Find("Background").transform.Find("BarValue").gameObject.transform;

        playerHunger = canvasBars.transform.Find("PlayerHunger").transform.
            Find("Background").transform.Find("BarValue").gameObject.transform;

        playerStatistics = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();

        enddarHealth = canvasBars.transform.Find("EnddarHealth").transform.
            Find("Background").transform.Find("BarValue").gameObject.transform;
        enddarStatistics = GameObject.FindGameObjectWithTag("Enddar").GetComponent<EnddarStatistics>();*/
    }

    private void Update()
    {
        //PlayerBars();
        //EnddarBars();
        BattleTimer();
        PlayerStats();
        PlayerBars();
        EnddarStats();
    }

    void BattleTimer()
    {
        clock.GetComponent<Text>().text = BattleManager.timerBattle.ToString("###");
    }

    void PlayerStats()
    {
        experience.GetComponent<Text>().text = "EXP: " + playerStatistics.experiance + " / " + playerStatistics.experianceToUpgrade;
        playerLevel.GetComponent<Text>().text = playerStatistics.level.ToString();
    }

    void PlayerBars()
    {
        playerHealthValue = (playerStatistics.healthCurrent * 100f) / playerStatistics.healthMax;
        playerHealth.localScale = new Vector3(playerHealthValue / 100f, 1f, 1f);

        playerStaminaValue = (playerStatistics.staminaCurrent * 100f) / playerStatistics.staminaMax;
        playerStamina.localScale = new Vector3(playerStaminaValue / 100f, 1f, 1f);

        playerHungerValue = (playerStatistics.hungerCurrent * 100f) / playerStatistics.hungerMax;
        playerHunger.localScale = new Vector3(playerHungerValue / 100f, 1f, 1f);
    }

    void EnddarStats()
    {
        enddarLevel.GetComponent<Text>().text = enddarStatistics.level.ToString();
        enddarHealth.GetComponent<Text>().text = enddarStatistics.healthCurrent.ToString();
        enddarRoots.GetComponent<Text>().text = enddarStatistics.roots.ToString();
    }
}
