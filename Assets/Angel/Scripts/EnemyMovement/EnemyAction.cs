using UnityEngine;
using UnityEngine.Animations;

public class EnemyAction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LookAtPlayer()
    {
        
    }
    public void DisableEnemy()
    {
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.name == "Pot")
        {
            collision.gameObject.GetComponent<PotManager>().DamagePot();
        }
    }
}
