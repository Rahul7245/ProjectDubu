using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

     public List<IconData> FetchIcons(int count)
    {
        if (iconDataList == null || iconDataList.Count == 0)
        {
            Debug.LogWarning("Data list is empty!");
            return new List<IconData>();
        }

        List<IconData> result = new List<IconData>();
        int maxUnique = iconDataList.Count;

        if (count <= maxUnique)
        {
            // Shuffle and take 'count' unique elements
            List<IconData> shuffled = iconDataList.OrderBy(x => Random.value).ToList();
            result = shuffled.Take(count).ToList();
        }
        else
        {
            // All unique elements (shuffled)
            List<IconData> shuffled = iconDataList.OrderBy(x => Random.value).ToList();
            result.AddRange(shuffled);

            // Fill remaining slots with random elements (can repeat)
            int remaining = count - maxUnique;
            for (int i = 0; i < remaining; i++)
            {
                result.Add(iconDataList[Random.Range(0, iconDataList.Count)]);
            }
        }

        return result;
    }
}
