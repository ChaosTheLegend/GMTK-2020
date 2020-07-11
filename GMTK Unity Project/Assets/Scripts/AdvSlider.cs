using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdvSlider : MonoBehaviour
{
    [SerializeField] private float showtime;
    [SerializeField] private float decreaseRate;
    [SerializeField] private bool proportionalDecreaseRate;
    [SerializeField] private PlayerController player;
    private float tm;

    [SerializeField] private Slider MainSlider;
    [SerializeField] private Slider underSlider;

    private float trueValue;

    // Start is called before the first frame update
    public void onChange()
    {
        tm = showtime;
    }


    // Update is called once per frame
    void Update()
    {
        MainSlider.value = player.hp;
        trueValue = player.hp;
        if (tm > 0) tm -= Time.deltaTime;
        else if (underSlider.value != trueValue)
        {
            if (proportionalDecreaseRate) underSlider.value = Mathf.Max(trueValue, underSlider.value - Time.deltaTime * (underSlider.value - trueValue) * decreaseRate);
            else underSlider.value = Mathf.Max(trueValue, underSlider.value - Time.deltaTime * decreaseRate);
        }
    }
}
