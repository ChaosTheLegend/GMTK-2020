using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Side",menuName = "Die Side")]
public class DieSide : ScriptableObject
{
    public Sprite SideSprite;
    public int id;
    public string Name;
    [TextArea]
    public string Description;
}
