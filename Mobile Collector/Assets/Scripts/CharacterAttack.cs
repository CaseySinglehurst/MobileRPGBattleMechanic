using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAttack : MonoBehaviour {

    public Image attackBar;

    public float attackTimer = 5, currentAttackTimer = 0;

    private float attackDamage = 24;

    public GameObject enemy;
	// Use this for initialization
	void Start () {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
	}
	
	// Update is called once per frame
	void Update () {

        
        
        if (currentAttackTimer < attackTimer)
        {
            currentAttackTimer += Time.deltaTime;
        }

        attackBar.fillAmount = currentAttackTimer / attackTimer;

    }

    public void Attack()
    {

        if (currentAttackTimer >= attackTimer)
        {
            EnemyController EC = enemy.GetComponent<EnemyController>();
            EC.currentHealth -= attackDamage;
            currentAttackTimer = 0;
        }
    }
}
