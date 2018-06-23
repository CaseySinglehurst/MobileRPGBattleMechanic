using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBattleInformation : MonoBehaviour {

    private CharacterInfo character;
    public int teamMember = -1;
    public GameObject characterModel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if (teamMember != -1 && character == null)
        {
            character = GameController.instance.teamMembers[teamMember];
            
            CreateCharacterModel();
        }

	}

    void CreateCharacterModel()
    {
        characterModel = Instantiate(character.model, this.gameObject.transform);
    }
}
