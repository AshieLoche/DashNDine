using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 targetPos;
    private float speed;
    private Vector3 direction;
    private bool canMove, isMoving;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && Vector3.Distance(transform.position, targetPos) >0.1f)
        {
            transform.position += direction * speed * Time.deltaTime;
            isMoving = true;
        }
        else isMoving = false;
    }
    public bool StartMoving(Vector3 target, float spd)
    {
        targetPos = target;
        speed = spd;
        direction = (targetPos - transform.position).normalized;
        canMove = true;
        return direction.x >= 0 ? true : false;
    }
    public bool CheckMoveAction()
    {
        return isMoving;
    }

    public void ReturnToPosition()
    {

    }
}
