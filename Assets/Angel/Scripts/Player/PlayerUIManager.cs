using DashNDine.ScriptableObjectSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] PlayerSO playerSO;

    [Header("UI")]
    [SerializeField] private Slider hpSlider;
    [SerializeField] private TextMeshProUGUI hpValue;
    [SerializeField] private TextMeshProUGUI reputationValue;


    // Update is called once per frame
    void Update()
    {
        hpSlider.value = playerData.currentHP;
        hpValue.text = hpSlider.value.ToString();
        reputationValue.text = playerSO.ReputationAmount.ToString();
    }
}
