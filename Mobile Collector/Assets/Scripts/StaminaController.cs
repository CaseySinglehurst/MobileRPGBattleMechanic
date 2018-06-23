using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StaminaController : MonoBehaviour {


    public Slider staminaBar;

    public Text stamina, timeLeftToNextStaminaPoint;
    
	
	void LateUpdate () {

        if (GameController.instance.currentStamina < GameController.instance.maxStamina)
        {
            timeLeftToNextStaminaPoint.text = (int)GameController.instance.currentSecondsToStamina + " Seconds left to next stamina";
        }
        else
        {
            timeLeftToNextStaminaPoint.text = "";
        }


        stamina.text = GameController.instance.currentStamina + "/" + GameController.instance.maxStamina;
        staminaBar.value = (float)GameController.instance.currentStamina / (float)GameController.instance.maxStamina;
    }
}
