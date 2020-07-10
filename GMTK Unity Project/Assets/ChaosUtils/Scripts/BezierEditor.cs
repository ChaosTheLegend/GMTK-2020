using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(BezierCurvePathAdv))]
public class BezierEditor : Editor
{
    BezierCurvePathAdv path;
    private void OnEnable()
    {
        path = (BezierCurvePathAdv)target;  
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Add Segment"))
        {
            path.AddSegment();
        }
        if (GUILayout.Button("Remove Segment"))
        {
            if (path.controllPoints.Count <= 4) return;
            for (int i = 0; i < 3; i++)
            {
                path.controllPoints.RemoveAt(path.controllPoints.Count - 1);
            }
        }
    }

    public void OnSceneGUI()
    {
        //Drawing paths
        if (!path.showPath) return;

        Vector2 lastpoint = path.controllPoints[0];
        Vector2 newpoint = path.controllPoints[1];
        Handles.color = Color.yellow;
        for (int i = 0; i < path.controllPoints.Count - 3; i += 3)
        {
            for (float t = 0.05f; t <= 1.01f; t += 0.05f)
            {
                newpoint = Mathf.Pow(1 - t, 3) * path.controllPoints[i] +
                3 * Mathf.Pow(1 - t, 2) * t * path.controllPoints[i + 1] +
                3 * Mathf.Pow(t, 2) * (1 - t) * path.controllPoints[i + 2] +
                Mathf.Pow(t, 3) * path.controllPoints[i + 3];
                Handles.DrawLine(lastpoint, newpoint);
                lastpoint = newpoint;
            }
        }
 

        if (!path.edit) return;
        //Drawing controll points 
        for (int i=0;i<path.controllPoints.Count; i++)
        {
            Handles.color = Color.blue;
            if (i % 3 == 0)
            {
                if(i+1 != path.controllPoints.Count) Handles.DrawLine(path.controllPoints[i], path.controllPoints[i + 1]);
                if (i - 1 > 0) Handles.DrawLine(path.controllPoints[i], path.controllPoints[i - 1]);
                Handles.color = Color.red;
            }
            Vector2 point = path.controllPoints[i];
            
            point = Handles.FreeMoveHandle(point, Quaternion.identity, HandleUtility.GetHandleSize(point)*0.1f, Vector3.one, Handles.SphereHandleCap);
            if (point != path.controllPoints[i]) path.MovePoint(i,point);            
            
        }

        

    }
}
