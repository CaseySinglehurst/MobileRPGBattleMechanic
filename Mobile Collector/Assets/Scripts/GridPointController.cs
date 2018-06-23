using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridPointController : MonoBehaviour {
    BoxCollider thisCollider;

    public Vector2 gridPosition;

    private Image attackMarker;
    private ParticleSystem explosion;


    public GameObject battleController;
    public Battle battle;


    private bool isAttacking;
    private float timeToAttack = 10, currentTimer;
    private int attackDamage;


    // Use this for initialization
    void Start () {
        thisCollider = GetComponent<BoxCollider>();
        battleController = GameObject.FindGameObjectWithTag("BattleController");
        battle = battleController.GetComponent<Battle>();
        attackMarker = GetComponentInChildren<Image>();
        explosion = GetComponentInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {

        if (isAttacking)
        {
            attackMarker.fillAmount = 1 - (currentTimer / timeToAttack);
            currentTimer -= Time.deltaTime;
            {
                if ( currentTimer <= 0)
                {
                    Attack();
                }
            }
        }
       
	}

   public void StartAttack(float time, int damage)
    {
        if (!isAttacking)
        {
            isAttacking = true;
            attackDamage = damage;
            timeToAttack = time;
            currentTimer = time;
        }
    }

    void Attack()
    {
        isAttacking = false;
        attackMarker.fillAmount = 0;

        if (battle.characterGrid[Mathf.RoundToInt(gridPosition.x), Mathf.RoundToInt(gridPosition.y)] != null)
        {
            battle.characterGrid[Mathf.RoundToInt(gridPosition.x), Mathf.RoundToInt(gridPosition.y)].GetComponent<CharacterMovement>().currentHealth -= attackDamage;
        }
        explosion.Play();
    }
}
