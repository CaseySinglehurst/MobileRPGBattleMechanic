using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
[System.Serializable]
public class CharacterInfo : ScriptableObject {


    [HideInInspector]
    public string characterName;
    public bool owned;
    public int level, baseAttack, baseHealth, baseArmour;
    public float AttackModifier, healthModifier, armourModifier;
    public float currentAttack, currentHealth, currentArmour;

    public GameObject model;
    public float modelHeight;

    public Accessory headGear, chestGear, legGear, weapon, ring, amulet;


    
}
