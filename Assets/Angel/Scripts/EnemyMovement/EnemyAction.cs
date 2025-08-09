using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyAction : MonoBehaviour
{
    private int requiredKeyCount;
    private int keyPressedCount = 0;
    private int PlayerDmg;
    private bool isActive = false, isQTESuccessful = false;
    public bool IsQTESuccessful => isQTESuccessful;


    // Update is called once per frame
    void Update()
    {
        if (keyPressedCount == requiredKeyCount && isActive) 
            isQTESuccessful = true;
    }
    public int QTEKeyInputCheck(int inputtedKey, List<int> qteSequence)
    {
        //Debug.Log(inputtedKey + " " + qteSequence.Count);
        if (inputtedKey == qteSequence[keyPressedCount])
        {
            keyPressedCount++;
        }
        else keyPressedCount = 0;
        return keyPressedCount;
    }
    public List<int> GetQTESequence(Difficulty difficulty)
    {
        List<int> sequence = new List<int>();
        int keyCount = 0;
        int highestInput = 0;
        switch (difficulty)
        {
            case Difficulty.Easy:
                keyCount = 3;
                highestInput = 4;
                break;
            case Difficulty.Normal:
                keyCount = 4;
                highestInput = 5;
                break;
            case Difficulty.Hard:
                highestInput = 6;
                keyCount = 6;
                break;
            default:
                keyCount = 0;
                highestInput = 4;
                break;

        }
        sequence.Clear();
        sequence = RandomizeQTEkey(keyCount, highestInput, sequence);
        isActive = true;
        return sequence;
    }
    private List<int> RandomizeQTEkey(int keyCount, int highestInput, List<int> seq)
    {
        requiredKeyCount = keyCount;
        for (int i = 0; i < keyCount; i++)
        {
            seq.Add(Random.Range(1, highestInput));
        }
        return seq;
    }
    public void SetDmg(int dmg)
    {
        PlayerDmg = dmg;
    }
    public void LookAtPlayer(bool isLookingRight)
    {
        if (isLookingRight && transform.eulerAngles != Vector3.zero) transform.eulerAngles = Vector3.zero;
        else if (!isLookingRight && transform.eulerAngles != new Vector3(0f, 180f, 0f)) transform.eulerAngles = new Vector3(0, 180, 0);
    }
    public void DisableEnemy()
    {
        isQTESuccessful = false;
        isActive = false;
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.name == "Pot")
        {
            collision.gameObject.GetComponent<PotManager>().DamagePot();
        }
        else if (collision.gameObject != null && collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit Player");
            collision.gameObject.GetComponent<PlayerDataManager>().DamagePlayer(PlayerDmg);
        }
    }
}
