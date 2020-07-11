using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static Object PseudoRandom(List<Object> choises, List<Object> history, int RepeatCooldown)
    {
        List<Object> resticted = history.GetRange(history.Count-RepeatCooldown-1,RepeatCooldown);
        foreach(Object res in resticted)
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
