using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BattlePause : MonoBehaviour {

    public Battle battle;
    public GameObject pauseMenu;

    
    
   

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void Click()
    {
        battle.battleRunning = !battle.battleRunning;

        if (battle.battleRunning)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }

    public void ForfeitButtonClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        
    }

   
}
