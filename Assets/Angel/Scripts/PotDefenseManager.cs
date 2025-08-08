using UnityEngine;

public class PotDefenseManager : MonoBehaviour
{
    [SerializeField] private GameObject pot;
    [SerializeField] private Vector2 potSpawnPt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(pot, potSpawnPt, Quaternion.identity);
        pot.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SummonPot()
    {
        pot.SetActive(true);
    }
    public void PotDestroyed()
    {
        Debug.Log("Pot has been destroyed. Defense failed...");
    }
}
