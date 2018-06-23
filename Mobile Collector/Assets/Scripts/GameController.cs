using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {


    public static GameController instance = null;

    public GameObject currentEnemy;
    private string currentSystemTime, oldSystemTime;

    private int secondsPerStamina = 1;
    public int currentStamina = 0, maxStamina = 20;
    public float currentSecondsToStamina = 100;

    public int battleStamina = 10;


    public CharacterInfo[] teamMembers = new CharacterInfo[3];

    void Awake(){
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

    }
	// Use this for initialization
	void Start () {
        oldSystemTime = PlayerPrefs.GetString("CurrentSystemTime");
        System.DateTime oldTime = System.DateTime.Parse(oldSystemTime);
        int timeElapsed = (int)(( System.DateTime.Now - oldTime).TotalSeconds);

        
        currentStamina = PlayerPrefs.GetInt("CurrentStamina");
        currentSecondsToStamina = PlayerPrefs.GetFloat("CurrentSecondsToStamina");
        CalculateStaminaGained(timeElapsed);
        PlayerPrefs.SetInt("CurrentStamina", currentStamina);
        PlayerPrefs.SetFloat("CurrentSecondsToStamina", currentSecondsToStamina);
    }
	
	// Update is called once per frame
	void Update () {

        if (currentStamina < maxStamina)
        {
            currentSystemTime = System.DateTime.Now.ToString();
            PlayerPrefs.SetString("CurrentSystemTime", currentSystemTime);

            currentSecondsToStamina -= Time.deltaTime;

            PlayerPrefs.SetFloat("CurrentSecondsToStamina", currentSecondsToStamina);

            if (currentSecondsToStamina <= 0)
            {
                currentStamina += 1;
                PlayerPrefs.SetInt("CurrentStamina", currentStamina);
                currentSecondsToStamina = secondsPerStamina;
            }


        }
        
    }


    void CalculateStaminaGained(int timeElapsed)
    {
        while(timeElapsed >= currentSecondsToStamina && currentStamina < maxStamina)
        {
            timeElapsed -= (int)currentSecondsToStamina;
            currentStamina += 1;
            currentSecondsToStamina = secondsPerStamina;

        }
        currentSecondsToStamina -= timeElapsed;
    }

    public void InitiateBattle()
    {
        currentStamina -= battleStamina;
        PlayerPrefs.SetInt("CurrentStamina", currentStamina);
    }
}
