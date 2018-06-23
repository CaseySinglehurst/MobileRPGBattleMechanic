using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
[System.Serializable]
public class Enemy : ScriptableObject {

    public GameObject enemyModel;

    public int health;
    

}
