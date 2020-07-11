using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    [SerializeField] private List<DieSide> outcomes;
    [SerializeField] private int RepeatCooldown;
    private List<DieSide> history;
    
    public void Roll()
    {
        DieSide roll = PseudoRandom(outcomes, history, RepeatCooldown);
        history.Add(roll);

        if(history.Count > RepeatCooldown + 2)
        {
            history.RemoveAt(0);
        }
    }

    private DieSide PseudoRandom(List<DieSide> choises, List<DieSide> history, int RepeatCooldown)
    {
        List<DieSide> resticted = history.GetRange(history.Count - RepeatCooldown - 1, RepeatCooldown);
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
        
    }

    void Update()
    {
        
    }
}
