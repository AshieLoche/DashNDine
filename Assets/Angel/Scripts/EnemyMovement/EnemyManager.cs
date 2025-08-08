using System.Runtime.Serialization;
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
    [SerializeField] private float speed;
    [SerializeField] private GameObject target;
    private Animator enemyAnim;

    [Header("Raider Enemy Information")]
    [SerializeField] private Vector3 startingPosition;
    [SerializeField] private float range;
    [SerializeField] private float playerDistance, spawnPtDistance, moveDistanceThreshold;
    private bool isLookingRight, isMoving;

    private EnemyMovement enemyMovement;
    private EnemyAction enemyAction;

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAction = GetComponent<EnemyAction>();
    }
    public void Start()
    {
        speed = enemyData.baseSpeed;
        enemyAnim = GetComponent<Animator>();
        enemyAnim.runtimeAnimatorController = enemyData.controller;

        gameObject.GetComponent<SpriteRenderer>().sprite = enemyData.image;
        if (enemyType == enemyType.Raider)
        {
            startingPosition = transform.position;
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }
    public void Update()
    {
        if(enemyType == enemyType.Raider )
        {
            playerDistance = Vector3.Distance(target.transform.position, gameObject.transform.position);
            spawnPtDistance = Vector3.Distance(gameObject.transform.position, startingPosition);
            // If in range and within allowable move distance;
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
        }
        isMoving = enemyMovement.CheckMoveAction();
        if (isLookingRight && transform.eulerAngles != Vector3.zero && isMoving) transform.eulerAngles = Vector3.zero;
        else if (!isLookingRight && transform.eulerAngles != new Vector3(0f, 180f, 0f) && isMoving) transform.eulerAngles = new Vector3(0,180,0);
    }

    public void MoveEnemy(GameObject trgt)
    {
        isMoving = true;
        target = trgt;
        isLookingRight = enemyMovement.StartMoving(target.transform.position, speed);
    }

    public void Die()
    {
        enemyMovement.ReturnToPosition();
        enemyAction.DisableEnemy();
    }

    public void ResetEnemy()
    {
        enemyMovement.ReturnToPosition();
    }
}
