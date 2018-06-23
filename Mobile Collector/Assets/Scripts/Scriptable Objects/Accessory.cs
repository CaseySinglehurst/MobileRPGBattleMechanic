using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Accessory", menuName = "Accessory")]
[System.Serializable]
public class Accessory : ScriptableObject {

    public Sprite image;
    public string accName, description;
    public int extraHealth, extraArmour, extraDamage, extraXp;


	
}
