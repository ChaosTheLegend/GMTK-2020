using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurvePathAdv : MonoBehaviour
{

    public bool edit = true;
    public bool showPath = true;
    //[HideInInspector]
    public List<Vector2> controllPoints = new List<Vector2>{Vector2.zero,Vector2.zero+Vector2.left*1f, Vector2.zero + Vector2.left * 2f, Vector2.zero + Vector2.left * 3f };

    public void AddSegment()
    {
        controllPoints.Add(controllPoints[controllPoints.Count - 1] * 2 - controllPoints[controllPoints.Count - 2]);
        controllPoints.Add((controllPoints[controllPoints.Count - 1]) * 0.5f);
        controllPoints.Add(Vector2.zero);

    }

    public void MovePoint(int index,Vector2 pos)
    {
        Vector2 deltaMove = pos - controllPoints[index];
        controllPoints[index] = pos;
        if(index % 3 == 0)
        {
            if (index + 1 < controllPoints.Count) controllPoints[index + 1] += deltaMove;
            if (index - 1 > 0) controllPoints[index - 1] += deltaMove;
        }
        if(index % 3 == 1)
        {
            if (index - 2 > 0) controllPoints[index - 2] = controllPoints[index - 1] * 2f - controllPoints[index];
        }
        if (index % 3 == 2)
        {
            if (index + 2 < controllPoints.Count) controllPoints[index + 2] = controllPoints[index + 1] * 2f - controllPoints[index];
        }
    }
}
