using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;
public class DeathController : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    private TMP_Text textfield;

    [FormerlySerializedAs("DeathPercent")]
    private float PseudoPercent;

    private float TruePercent;

    //Exponential Growth described by this graph: https://www.desmos.com/calculator/mqdtxvtdwe
    private float GetPseudoPercent(float dmg)
    {
        float max = 50f;
        float bias = 8.5f;
        float k = 0.5f;
        float denum = 1f + Mathf.Exp(-k * (dmg - bias));
        return max / denum;
    }
    // Start is called before the first frame update
    void Start()
    {
        textfield = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        PseudoPercent = GetPseudoPercent((float)player.DamageTaken);
        textfield.text = $"{(int)PseudoPercent}%";
    }
}
