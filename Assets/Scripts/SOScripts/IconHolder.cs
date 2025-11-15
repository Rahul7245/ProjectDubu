using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct IconData
{
    public string id;

    public Sprite icon;
}

[CreateAssetMenu(fileName = "IconHolder", menuName = "ScriptableObjects/IconHolder")]
public class IconHolder : ScriptableObject
{
    public List<IconData> iconDataList;
}
