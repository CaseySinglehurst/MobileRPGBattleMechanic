using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour {

    public GameObject currentTile;
    public Vector2 currentPosition;

    public RectTransform UICanvas;

    public Image healthBar;

    public int health, currentHealth;
    public int attackDamage;
    public bool isFire = false, isRooted = false, isRegen = false;

    // Use this for initialization
    void Start () {
        UICanvas = GetComponentInChildren<RectTransform>();


	}
	
	// Update is called once per frame
	void Update () {


        // move towards this character's position on the grid
        transform.position = Vector3.Lerp(transform.position, new Vector3(currentTile.transform.position.x, currentTile.transform.position.y + 1f, currentTile.transform.position.z), 10 * Time.deltaTime);

        healthBar.fillAmount = 1.0f * currentHealth / health;
        
        //Get the health bar to rotate upwards towards camera so we can always see it
        Vector3 v = Camera.main.transform.position - transform.position;
        v.z = v.y = 0.0f;
        UICanvas.LookAt(Camera.main.transform.position - v);
        UICanvas.Rotate(0, 180, 0);
    }
}
