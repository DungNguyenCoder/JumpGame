using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Map", menuName = "MapData", order = 0)]
public class MapData : ScriptableObject
{
    public Sprite mapIcon;
    public Sprite wall;
    public Sprite backGround;
    public Sprite smallNormalGround;
    public Sprite smallCrashGround;
    public Sprite bigNormalGround;
    public Sprite bigCrashGround;
}