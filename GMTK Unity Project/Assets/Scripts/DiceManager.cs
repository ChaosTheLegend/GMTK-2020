using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DiceManager : MonoBehaviour
{
    [SerializeField] private float rollTime;
    [SerializeField] private List<DieSide> outcomes;
    [SerializeField] private int RepeatCooldown;
    [SerializeField] private UnityEvent onEndRoll;
    private List<DieSide> history;
    private float tm;
    private bool isRolling;
    public void Roll()
    {
        isRolling = true;
        tm = rollTime;
        DieSide roll = PseudoRandom(outcomes, history, RepeatCooldown);
        history.Add(roll);
        if(history.Count > RepeatCooldown + 2)
        {
            history.RemoveAt(0);
        }
    }

    private DieSide PseudoRandom(List<DieSide> choises, List<DieSide> history, int RepeatCooldown)
    {
        List<DieSide> resticted = new List<DieSide>();
        if(history.Count > 0) resticted = history.GetRange(history.Count - RepeatCooldown - 1, RepeatCooldown);
        foreach (DieSide res in resticted)
        {
            if (choises.Contains(res))
            {
                choises.Remove(res);
            }
        }

        int maxrange = choises.Count;
        int rand = Random.Range(0, maxrange);
        return (choises[rand]);
    }

    void Start()
    {
        history = new List<DieSide>();
    }

    void Update()
    {
        if (!isRolling) return;
        if (tm > 0) tm -= Time.deltaTime;
        if (tm <= 0)
        {
            onEndRoll?.Invoke();
            isRolling = false;
        }
    }
}
