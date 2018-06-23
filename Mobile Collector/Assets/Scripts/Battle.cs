using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle : MonoBehaviour {

    public GameObject gridPoint;
    public GameObject[,] grid;
    private float gridBuffer = 0.1f;

    private Vector2 startPosition, endPosition, startGrid;

    public GameObject character;
    public GameObject[,] characterGrid;

    public GameObject enemyPlatform;

    public GameObject enemy1;
    public GameObject gameController;

    public Text winLoseText;

    private GameObject gridHit, gridEnd;

    public bool battleRunning = true;

    public LayerMask gridLayer;


    // Use this for initialization
    void Start () {
        
        LoadBoard();
    }
	
    
    void LoadBoard()
    {
        enemy1 = GameController.instance.currentEnemy;
        Instantiate(enemyPlatform, new Vector3(7.25f, 0f, 2f), Quaternion.identity);
        Instantiate(enemy1, new Vector3(7.25f, 1f, 2f), Quaternion.identity);
        
        grid = new GameObject[5, 5];
        characterGrid = new GameObject[5, 5];
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                grid[x, y] = Instantiate(gridPoint, new Vector3(x + (gridBuffer * x), 0, y + (gridBuffer * y)), Quaternion.identity);
                grid[x, y].GetComponent<GridPointController>().gridPosition = new Vector2(x, y);
            }
        }
        // Temporary instantiation of test characters. This would be handled from the main screen where the player could choose their characters
        characterGrid[2, 3] = Instantiate(character, new Vector3(grid[2, 3].transform.position.x, grid[2, 3].transform.position.y + 2, grid[2, 3].transform.position.z), Quaternion.identity);
        characterGrid[2, 3].GetComponent<CharacterBattleInformation>().teamMember = 0;
        characterGrid[2, 3].GetComponent<CharacterMovement>().currentTile = grid[2, 3];
        characterGrid[2, 3].GetComponent<CharacterMovement>().currentPosition = new Vector3(2, 3);
        characterGrid[2, 3].GetComponent<CharacterMovement>().health = 50;
        characterGrid[2, 3].GetComponent<CharacterMovement>().currentHealth = 50;

        characterGrid[4, 3] = Instantiate(character, new Vector3(grid[4, 3].transform.position.x, grid[4, 3].transform.position.y + 2, grid[4, 3].transform.position.z), Quaternion.identity);
        characterGrid[4, 3].GetComponent<CharacterBattleInformation>().teamMember = 1;
        characterGrid[4, 3].GetComponent<CharacterMovement>().currentTile = grid[4, 3];
        characterGrid[4, 3].GetComponent<CharacterMovement>().currentPosition = new Vector3(4, 3);
        characterGrid[4, 3].GetComponent<CharacterMovement>().health = 50;
        characterGrid[4, 3].GetComponent<CharacterMovement>().currentHealth = 50;

        characterGrid[4, 1] = Instantiate(character, new Vector3(grid[4, 1].transform.position.x, grid[2, 3].transform.position.y + 2, grid[2, 3].transform.position.z), Quaternion.identity);
        characterGrid[4, 1].GetComponent<CharacterBattleInformation>().teamMember = 2;
        characterGrid[4, 1].GetComponent<CharacterMovement>().currentTile = grid[4, 1];
        characterGrid[4, 1].GetComponent<CharacterMovement>().currentPosition = new Vector3(4, 1);
        characterGrid[4, 1].GetComponent<CharacterMovement>().health = 50;
        characterGrid[4, 1].GetComponent<CharacterMovement>().currentHealth = 50;
    }

    // Update is called once per frame
    void Update () {
        if (battleRunning)
        {
            CheckForWinLoss();
            UpdateInput();
        }
    }

    void CheckForWinLoss()
    {
        float characters = 0;
        foreach (GameObject cc in characterGrid)
        {
            if (cc != null)
            {
                if (cc.GetComponent<CharacterMovement>().currentHealth <= 0)
                {
                    Destroy(cc.gameObject);
                }
                else
                {
                    characters += 1;
                }
            }
        }

        if (characters == 0 )
        {
            
        }
    }

    void UpdateInput()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                // raycast to see if we hit a grid block
                Camera theCamera = Camera.main;
                Ray ray = theCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, 100f, gridLayer))
                {
                    gridHit = hitInfo.transform.gameObject;
                    startGrid = gridHit.GetComponent<GridPointController>().gridPosition;
                    startPosition = theCamera.ScreenToViewportPoint(Input.mousePosition);
                }
            }

            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                Camera theCamera = Camera.main;
                Ray ray = theCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    gridEnd = hitInfo.transform.gameObject;
                    if (gridEnd == gridHit)
                    {
                        CharacterAttack(startGrid);
                    }
                    else
                    {
                        endPosition = theCamera.ScreenToViewportPoint(Input.mousePosition);
                        // check if we're to the left, right, up or down from where we started the click when we end the click
                        if (Mathf.Abs(endPosition.x - startPosition.x) >= Mathf.Abs(endPosition.y - startPosition.y))
                        {
                            if ((endPosition.x - startPosition.x) <= 0)
                            {
                                MoveCharacter(startGrid, new Vector2(-1, 0));
                            }
                            else
                            {
                                MoveCharacter(startGrid, new Vector2(1, 0));
                            }
                        }
                        else
                        {
                            if ((endPosition.y - startPosition.y) <= 0)
                            {
                                MoveCharacter(startGrid, new Vector2(0, -1));
                            }
                            else
                            {
                                MoveCharacter(startGrid, new Vector2(0, 1));
                            }
                        }
                    }
                }
            }
        }
    }

    void MoveCharacter(Vector2 whatToMove, Vector2 movement)
    {
        if (characterGrid[Mathf.RoundToInt(whatToMove.x) ,Mathf.RoundToInt(whatToMove.y)] != null)
        {
            Vector2 newPosition = whatToMove + movement;
            if (newPosition.x >= 0 && newPosition.x <= 4 && newPosition.y >= 0 && newPosition.y <= 4)
            {
                if ((grid[Mathf.RoundToInt(newPosition.x), Mathf.RoundToInt(newPosition.y)] != null) && characterGrid[Mathf.RoundToInt(newPosition.x), Mathf.RoundToInt(newPosition.y)] == null)
                {
                    characterGrid[Mathf.RoundToInt(newPosition.x), Mathf.RoundToInt(newPosition.y)] = characterGrid[Mathf.RoundToInt(whatToMove.x), Mathf.RoundToInt(whatToMove.y)];
                    characterGrid[Mathf.RoundToInt(whatToMove.x), Mathf.RoundToInt(whatToMove.y)] = null;
                    characterGrid[Mathf.RoundToInt(newPosition.x), Mathf.RoundToInt(newPosition.y)].GetComponent<CharacterMovement>().currentTile = grid[Mathf.RoundToInt(newPosition.x), Mathf.RoundToInt(newPosition.y)];
                    characterGrid[Mathf.RoundToInt(newPosition.x), Mathf.RoundToInt(newPosition.y)].GetComponent<CharacterMovement>().currentPosition = newPosition;

                }
            }

        }
    }

    void CharacterAttack(Vector2 whoAttacks)
    {
        GameObject attacker = characterGrid[Mathf.RoundToInt(whoAttacks.x),Mathf.RoundToInt(whoAttacks.y)];
        
        if (attacker != null)
        {
            CharacterAttack atk = attacker.GetComponent<CharacterAttack>();
            atk.Attack();
        }
    }
}
