using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezireFollowAdv : MonoBehaviour
{
    public BezierCurvePathAdv route;
    public float Speed = 1f;

    private Vector2 position;

    public bool folowing = false;

    public enum EndActions {Stop = 0,Restart=1,GoBack=2}
    public EndActions onEnd;
    [HideInInspector]
    public float tParam;
    [HideInInspector]
    public int part;
    private bool allowCorutine;

    //[HideInInspector]
    // Start is called before the first frame update
    void Start()
    {
        tParam = 0f;
        allowCorutine = true;
        part = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (allowCorutine && folowing)
        {
            if (Speed > 0)
            {
                part = 0;
                tParam = 0f;
                StartCoroutine(FollowPathForward(onEnd));
            }
            if (Speed < 0)
            {
                part = (route.controllPoints.Count / 3)-1;
                tParam = 1f;
                StartCoroutine(FollowPathBackward(onEnd));
            }
        }
    }
    private IEnumerator FollowPathBackward(EndActions onEnd) {
        allowCorutine = false;
        while (part >= 0)
        {
            Vector2 p1 = route.controllPoints[part * 3];
            Vector2 p2 = route.controllPoints[part * 3 + 1];
            Vector2 p3 = route.controllPoints[part * 3 + 2];
            Vector2 p4 = route.controllPoints[part * 3 + 3];
            while (tParam > 0)
            {
                tParam += Time.deltaTime * Speed * (route.controllPoints.Count / 3);
                position = Mathf.Pow(1 - tParam, 3) * p1 +
                    3 * Mathf.Pow(1 - tParam, 2) * tParam * p2 +
                    3 * Mathf.Pow(tParam, 2) * (1 - tParam) * p3 +
                    Mathf.Pow(tParam, 3) * p4;
                transform.position = position;
                yield return new WaitForEndOfFrame();
            }
            tParam = 1f;
            part--;
            yield return new WaitForEndOfFrame();
        }
        allowCorutine = true;
        if ((int)onEnd == 2) {
            folowing = true;
            Speed = -Speed;
        }
        if ((int)onEnd == 1)
        {
            folowing = true;
        }
        if ((int)onEnd == 0) folowing = false;
    }
    private IEnumerator FollowPathForward(EndActions onEnd) {
        allowCorutine = false;
        while (part < (route.controllPoints.Count / 3))
        {
            Vector2 p1 = route.controllPoints[part * 3];
            Vector2 p2 = route.controllPoints[part * 3 + 1];
            Vector2 p3 = route.controllPoints[part * 3 + 2];
            Vector2 p4 = route.controllPoints[part * 3 + 3];
            while (tParam < 1)
            {
                tParam += Time.deltaTime * Speed * (route.controllPoints.Count / 3);
                position = Mathf.Pow(1 - tParam, 3) * p1 +
                    3 * Mathf.Pow(1 - tParam, 2) * tParam * p2 +
                    3 * Mathf.Pow(tParam, 2) * (1 - tParam) * p3 +
                    Mathf.Pow(tParam, 3) * p4;
                transform.position = position;
                yield return new WaitForEndOfFrame();
            }
            tParam = 0f;
            part++;
            yield return new WaitForEndOfFrame();
        }
        tParam = 0f;
        allowCorutine = true;
        if ((int)onEnd == 2)
        {
            folowing = true;
            Speed = -Speed;
        }
        if ((int)onEnd == 1)
        {
            folowing = true;
            part = 0;
        }
        if ((int)onEnd == 0) folowing = false;
    }

}

