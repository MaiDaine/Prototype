using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RuntimeSet/Unit")]
public class UnitSet : RuntimeSet<Unit>
{
    public void ClearSelf()
    {
        for (int i = items.Count - 1; i > -1; i--)
            if (items[i].GetComponent<UnitHealth>().alive == false)
            {
                Destroy(items[i].gameObject);
                items.RemoveAt(i);
            }
    }
}