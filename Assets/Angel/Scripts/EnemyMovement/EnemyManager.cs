using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum enemyType
{
    Ambush, Raider, Defense
}

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [Header("Basic Enemy Information")]
    [SerializeField] private enemyType enemyType;
    [SerializeField] private int hp;
    [SerializeField] private float speed;
    [SerializeField] private GameObject target;
    [SerializeField] private List<int> qteSequence;
    private bool isQteSuccessful = false;
    private Animator enemyAnim;

    [Header("Raider Enemy Information")]
    [SerializeField] private Vector3 startingPosition;
    [SerializeField] private float range, deadTimer, deathTime = 15f;
    [SerializeField] private float playerDistance, spawnPtDistance, moveDistanceThreshold;
    private bool isLookingRight, isMoving, isDead=false;

    [Header("Player Data")]
    [SerializeField] PlayerDataManager playerDataManager;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI qteText;

    private EnemyMovement enemyMovement;
    private EnemyAction enemyAction;
    private EnemyUI enemyUI;

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAction = GetComponent<EnemyAction>();
        enemyUI = GetComponent<EnemyUI>();

        if (qteText == null)
        {
            Debug.LogWarning($"{name}: No QTE Text assigned or found in scene!");
        }
    }

    private void Start()
    {
        InitializeEnemy();
        playerDataManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDataManager>();
        if(enemyType == enemyType.Raider)
        {
            hp = enemyData.maxHp;
        }
    }

    private void OnEnable()
    {
        InitializeEnemy();
    }

    private void Update()
    {
        if (enemyType == enemyType.Raider)
        {
            qteText.gameObject.SetActive(false);
            playerDistance = Vector3.Distance(target.transform.position, transform.position);
            spawnPtDistance = Vector3.Distance(transform.position, startingPosition);

            if (playerDistance < range && spawnPtDistance < moveDistanceThreshold)
            {
                isMoving = true;
                isLookingRight = enemyMovement.StartMoving(target.transform.position, speed);
            }
            else
            {
                isMoving = true;
                isLookingRight = enemyMovement.StartMoving(startingPosition, speed);
            }

            if (isDead)
            {
                deadTimer += Time.deltaTime;
                if(deadTimer> deathTime)
                {
                    gameObject.SetActive(true);
                }
            }
        }
        else
        {
            isQteSuccessful = enemyAction.IsQTESuccessful;
            if (isQteSuccessful) Die();
        }

        isMoving = enemyMovement.CheckMoveAction();
        if (isMoving) enemyAction.LookAtPlayer(isLookingRight);


    }
    public void SetEnemyType(enemyType type)
    {
        enemyType = type;
    }

    private void InitializeEnemy()
    {
        speed = enemyData.baseSpeed;
        enemyAnim = GetComponent<Animator>();
        enemyAnim.runtimeAnimatorController = enemyData.controller;
        enemyAction.SetDmg(enemyData.baseDmg);

        GetComponent<SpriteRenderer>().sprite = enemyData.image;

        if (enemyType == enemyType.Raider)
        {
            startingPosition = transform.position;
            target = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            qteSequence = enemyAction.GetQTESequence(enemyData.difficulty);
            enemyUI.SetSequence(qteText, qteSequence);
        }
                    
    }

    public void CheckInput(int inputtedKey)
    {
        if (enemyType == enemyType.Raider) return;

        if (qteSequence == null || qteSequence.Count == 0)
        {
            Debug.LogWarning($"{name} QTE sequence not initialized yet!");
            return;
        }

        int correctInputs = enemyAction.QTEKeyInputCheck(inputtedKey, qteSequence);
        enemyUI.IndicateCorrectInput(correctInputs);
    }
    public void BurnEnemy(int dmg)
    {
        hp -= dmg;
        if(hp <=0)Die();
    }
    public void SlowEnemy(float divider)
    {
        StartCoroutine(Slowed(divider));
    }

    public void SetDifficulty(Difficulty difficulty)
    {
        enemyData.difficulty = difficulty;
    }

    public void MoveEnemy(GameObject trgt)
    {
        isMoving = true;
        target = trgt;
        isLookingRight = enemyMovement.StartMoving(target.transform.position, speed);
    }

    public void Die()
    {
        playerDataManager.EnemyKilled();
        enemyMovement.ReturnToPosition();
        enemyAction.DisableEnemy();
        if (enemyType == enemyType.Raider)
        {
            deadTimer = 0;
            isDead = true;
        }
    }

    public void ResetEnemy()
    {
        enemyMovement.ReturnToPosition();
    }
    IEnumerator Slowed(float div)
    {
        var s = speed;
        speed = speed * div;
        yield return new WaitForSeconds(3);
        speed = s;
    }
}
