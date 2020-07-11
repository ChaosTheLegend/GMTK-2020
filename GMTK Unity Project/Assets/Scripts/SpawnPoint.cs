using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private float DetectionRadius;
    public bool canSpawn = true;
    private Transform localEnemy;
    // Start is called before the first frame update
    public void Awake()
    {
        canSpawn = true;
    }
    public void Spawn(GameObject enemy)
    {
        localEnemy = Instantiate(enemy, transform.position, Quaternion.identity).transform;
        canSpawn = false;
    }


    private void Update()
    {
        if (!localEnemy) return;
        Vector2 dis = transform.position - localEnemy.position;
        if (dis.sqrMagnitude > DetectionRadius * DetectionRadius) canSpawn = true;
    }
}
