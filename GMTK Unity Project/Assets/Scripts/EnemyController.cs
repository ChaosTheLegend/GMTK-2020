using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float AttackRange;
    private GameObject player;
    private Transform trans;

    [Header("Collison")]
    public List<float> Offset;
    public List<float> Length;

    private enum Side { UP = 0, RIGHT = 1, DOWN = 2, LEFT = 3 }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        trans = transform;
    }

    private bool CheckColision(int side)
    {
        Vector2 begin = new Vector2(), end = new Vector2();

        switch (side)
        {
            case (int)Side.DOWN:
                begin = new Vector2(trans.position.x - Length[2], trans.position.y - Offset[2]);
                end = new Vector2(trans.position.x + Length[2], trans.position.y - Offset[2]);
                break;
            case (int)Side.UP:
                begin = new Vector2(transform.position.x - Length[0], transform.position.y + Offset[0]);
                end = new Vector2(transform.position.x + Length[0], transform.position.y + Offset[0]);
                break;
            case (int)Side.LEFT:
                begin = new Vector2(transform.position.x - Offset[3], transform.position.y - Length[3]);
                end = new Vector2(transform.position.x - Offset[3], transform.position.y + Length[3]);
                break;
            case (int)Side.RIGHT:
                begin = new Vector2(transform.position.x + Offset[1], transform.position.y - Length[1]);
                end = new Vector2(transform.position.x + Offset[1], transform.position.y + Length[1]);
                break;
        }

        RaycastHit2D cast = Physics2D.Linecast(begin, end, LayerMask.GetMask("Solid"));
        return cast;
    }
    // Update is called once per frame
    private void Move()
    {
        if (!player) return;
        Vector3 dest = player.transform.position;
        Vector3 dir = dest - trans.position;
        if (dir.sqrMagnitude < AttackRange*AttackRange) return;
        float hor = dir.x;
        if (CheckColision((int)Side.LEFT)) hor = Mathf.Max(0, hor);
        if (CheckColision((int)Side.RIGHT)) hor = Mathf.Min(0, hor);

        float ver = dir.y;
        if (CheckColision((int)Side.DOWN)) ver = Mathf.Max(0, ver);
        if (CheckColision((int)Side.UP)) ver = Mathf.Min(0, ver);

        Vector3 moveVector = new Vector3(hor, ver).normalized;

        trans.position += moveVector * Time.deltaTime * speed;

    }

    void Update()
    {
        Move();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);

        Vector2 begin, end;
        Gizmos.color = Color.blue;

        begin = new Vector2(transform.position.x - Length[0], transform.position.y + Offset[0]);
        end = new Vector2(transform.position.x + Length[0], transform.position.y + Offset[0]);
        Gizmos.DrawLine(begin, end);
        begin = new Vector2(transform.position.x + Offset[1], transform.position.y - Length[1]);
        end = new Vector2(transform.position.x + Offset[1], transform.position.y + Length[1]);
        Gizmos.DrawLine(begin, end);
        begin = new Vector2(transform.position.x - Length[2], transform.position.y - Offset[2]);
        end = new Vector2(transform.position.x + Length[2], transform.position.y - Offset[2]);
        Gizmos.DrawLine(begin, end);
        begin = new Vector2(transform.position.x - Offset[3], transform.position.y - Length[3]);
        end = new Vector2(transform.position.x - Offset[3], transform.position.y + Length[3]);
        Gizmos.DrawLine(begin, end);

    }
}
