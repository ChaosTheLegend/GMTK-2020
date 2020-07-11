using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int Damage;
    public float speed;
    public int Pierce;
    private GameObject lasthit;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
            return;
        }
        if (other.gameObject == lasthit) return;
        if (other.gameObject.CompareTag("Enemy"))
        {
            lasthit = other.gameObject;
            Pierce--;
        }
        if (Pierce == 0) Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }
}
