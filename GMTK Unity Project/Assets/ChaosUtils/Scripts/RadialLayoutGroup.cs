using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RadialLayoutGroup : MonoBehaviour
{
    [Header("Manual Distribution")]
    public float initAngle;
    public float Spacing;
    public float centerDistance;
    [Header("Auto Distribution")]
    public bool autoDistribute;
    public float endAngle;
    [Header("Children")]
    public bool rotateChildren;
    public float childAngle;
    [Header("Auto Size")]
    public bool autoSize;
    public float childWidth;
    public float childHeight;

    public int unlocked = 1;
    public RectTransform rt;
    public RectTransform[] Children;
    // Start is called before the first frame update
    private void OnEnable()
    {
        rt = GetComponent<RectTransform>();
        LayoutUpdate();
        //Children = rt.GetComponentsInChildren<RectTransform>();
        
    }

    private void LayoutUpdate()
    {
        Children = new RectTransform[transform.childCount];
        int i = 0;
        foreach (Transform child in transform)
        {
            Children[i] = child.GetComponent<RectTransform>();
            i++;
        }
    }
    public void Unlock()
    {
        unlocked += 1;
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 center = rt.position;
        if (autoDistribute)
        {
            Spacing = (endAngle - initAngle) / (unlocked + 1);    
        }
        for (int i = 1; i <= unlocked; i++)
        {
            Children[i-1].gameObject.SetActive(true);
            float childx = centerDistance * Mathf.Cos((initAngle + Spacing * i) * Mathf.Deg2Rad);
            float childy = centerDistance * Mathf.Sin((initAngle + Spacing * i) * Mathf.Deg2Rad);
            Vector2 childpos = center + new Vector2(childx, childy);
            Children[i-1].position = childpos;
            if (rotateChildren)
            {
                Children[i-1].rotation = Quaternion.Euler(0f, 0f, initAngle + Spacing * i + childAngle - 90);
            }
            if (autoSize)
            {
                //Children[i-1].sizeDelta = new Vector2(childWidth, childHeight);
                Children[i - 1].transform.localScale = new Vector2(childWidth, childHeight);
            }
        }
        for (int i = unlocked; i < Children.Length; i++)
        {
            Children[i].gameObject.SetActive(false);
        }
    }
}
