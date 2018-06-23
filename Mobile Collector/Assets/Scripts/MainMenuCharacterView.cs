using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCharacterView : MonoBehaviour {

    public Transform pedestal1, pedestal2, pedestal3;


	// Use this for initialization
	void Start () {
        CreateCharacter();
	}
	
    void CreateCharacter()
    {
        Instantiate(GameController.instance.teamMembers[0].model, new Vector3(pedestal1.transform.position.x, pedestal1.transform.position.y + (GameController.instance.teamMembers[0].modelHeight/2), pedestal1.transform.position.z), Quaternion.identity);
        Instantiate(GameController.instance.teamMembers[1].model, new Vector3(pedestal2.transform.position.x, pedestal2.transform.position.y + (GameController.instance.teamMembers[1].modelHeight/2), pedestal2.transform.position.z), Quaternion.identity);
        Instantiate(GameController.instance.teamMembers[2].model, new Vector3(pedestal3.transform.position.x, pedestal3.transform.position.y + (GameController.instance.teamMembers[2].modelHeight/2), pedestal3.transform.position.z), Quaternion.identity);
    }
}
