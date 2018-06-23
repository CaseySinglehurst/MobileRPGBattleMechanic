using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {
    public RectTransform UICanvas;
    public Image healthBar;
    public GameObject battleManager;
    public Battle battle;

    private float attackTimer = 5;
    private bool isAttacking = false;

    public float health , currentHealth;
    
    // Use this for initialization
    void Start () {
        UICanvas = GetComponentInChildren<RectTransform>();
        battleManager = GameObject.FindGameObjectWithTag("BattleController");
        battle = battleManager.GetComponent<Battle>();
        health = 120;
        currentHealth = 120;
    }
	
	// Update is called once per frame
	void Update () {

        if (battle.battleRunning)
        {
            UpdateHealthBar();
            CheckIfAttacking();
        }
        
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = 1.0f * currentHealth / health;
        //Get the health bar to rotate upwards towards camera so we can always see it
        Vector3 v = Camera.main.transform.position - transform.position;
        v.z = v.y = 0.0f;
        UICanvas.LookAt(Camera.main.transform.position - v);
        UICanvas.Rotate(0, 180, 0);
    }

    void CheckIfAttacking()
    {
        if (!isAttacking) { attackTimer -= Time.deltaTime; }

        if (attackTimer <= 0)
        {
            attackTimer = 5;
            isAttacking = true;

            ChooseAttack();

        }
    }

    void ChooseAttack()
    {
        int choice = Random.Range(1, 4);
        switch (choice)
        {
            case 1: crossAttack(); break;
            case 2: meshAttack(); break;
            case 3: StartCoroutine("Fade"); break;
            default: break;
        }
    }

    void crossAttack()
    {
        battle.grid[0, 0].GetComponent<GridPointController>().StartAttack(3, 20);
        battle.grid[0, 4].GetComponent<GridPointController>().StartAttack(3, 20);
        battle.grid[4, 0].GetComponent<GridPointController>().StartAttack(3, 20);
        battle.grid[4, 4].GetComponent<GridPointController>().StartAttack(3, 20);

        battle.grid[2, 0].GetComponent<GridPointController>().StartAttack(3, 20);
        battle.grid[2, 1].GetComponent<GridPointController>().StartAttack(3, 20);
        battle.grid[2, 2].GetComponent<GridPointController>().StartAttack(3, 20);
        battle.grid[2, 3].GetComponent<GridPointController>().StartAttack(3, 20);
        battle.grid[2, 4].GetComponent<GridPointController>().StartAttack(3, 20);

        battle.grid[0, 2].GetComponent<GridPointController>().StartAttack(3, 20);
        battle.grid[1, 2].GetComponent<GridPointController>().StartAttack(3, 20);
        battle.grid[3, 2].GetComponent<GridPointController>().StartAttack(3, 20);
        battle.grid[4, 2].GetComponent<GridPointController>().StartAttack(3, 20);
        isAttacking = false;
    }

    void meshAttack()
    {
        battle.grid[0, 0].GetComponent<GridPointController>().StartAttack(3, 20);
        battle.grid[0, 2].GetComponent<GridPointController>().StartAttack(3, 20);
        battle.grid[0, 4].GetComponent<GridPointController>().StartAttack(3, 20);

        battle.grid[1, 1].GetComponent<GridPointController>().StartAttack(3, 20);
        battle.grid[1, 3].GetComponent<GridPointController>().StartAttack(3, 20);

        battle.grid[2, 0].GetComponent<GridPointController>().StartAttack(3, 20);
        battle.grid[2, 2].GetComponent<GridPointController>().StartAttack(3, 20);
        battle.grid[2, 4].GetComponent<GridPointController>().StartAttack(3, 20);

        battle.grid[3, 1].GetComponent<GridPointController>().StartAttack(3, 20);
        battle.grid[3, 3].GetComponent<GridPointController>().StartAttack(3, 20);

        battle.grid[4, 0].GetComponent<GridPointController>().StartAttack(3, 20);
        battle.grid[4, 2].GetComponent<GridPointController>().StartAttack(3, 20);
        battle.grid[4, 4].GetComponent<GridPointController>().StartAttack(3, 20);
        isAttacking = false;
    }

    IEnumerator Fade()
    {
        for (int x = 0; x <= 4; x += 1)
        {
            for (int y = 0; y <= 4; y += 1)
            {
                battle.grid[x,y].GetComponent<GridPointController>().StartAttack(3, 20);
                if (y == 4 && x == 4)
                {
                    isAttacking = false;
                    
                }
                yield return new WaitForSeconds(0.4f);
                
            }
        }
        
        
    }
}
