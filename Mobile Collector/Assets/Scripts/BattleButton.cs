using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BattleButton : MonoBehaviour {

    Button button;

	// Use this for initialization
	void Start () {
        button = GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if (GameController.instance.currentStamina >= GameController.instance.battleStamina)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
	}

    public void BattleButtonClicked()
    {

        GameController.instance.InitiateBattle();
        SceneManager.LoadScene(1);
    }
}
