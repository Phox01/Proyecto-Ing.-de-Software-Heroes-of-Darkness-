using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootItem
{
    public GameObject itemPrefab; 
    public float dropProbability;
}

public class LootDropper : MonoBehaviour
{
    public List<LootItem> lootItems;
    public float dropRadius = 1f;

    public void DropLoot(Vector3 position)
    {
        foreach (var lootItem in lootItems)
        {
            if (lootItem.itemPrefab == null)
            {
                Debug.LogError("Loot item prefab is not assigned.");
                continue;
            }

            if (Random.value <= lootItem.dropProbability)
            {
                    Vector3 randomOffset = new Vector3(
                        Random.Range(-dropRadius, dropRadius),
                        Random.Range(-dropRadius, dropRadius),
                        0
                    );

                    Vector3 dropPosition = position + randomOffset;
                    Instantiate(lootItem.itemPrefab, dropPosition, Quaternion.identity);
            }
        }
    }
}
