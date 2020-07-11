using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Wave", menuName = "Wave")]
public class Wave : ScriptableObject
{
    public List<GameObject> enemies;
    public List<int> ammount;
    public float spawnDelay;
    public int maxSpawns;
}
