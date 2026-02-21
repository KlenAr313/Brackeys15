using UnityEngine;
using System.Collections.Generic;

public class SlotManager : MonoBehaviour
{
    [SerializeField]
    private int numberOfSlots = 5;
    [SerializeField]
    private float spaceToPlayer = 2f;

    private static SlotManager instance;

    private Dictionary<GameObject, bool> slotStatus;

    public static SlotManager Instance { get => instance; set => instance = value; }

    public List<EnemyBase> Enemies;

    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            return;
        }


        slotStatus = new Dictionary<GameObject, bool>();
        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject slot = new GameObject("Slot" + i);
            slot.transform.parent = transform;
            slot.transform.localPosition = new Vector3(
                Mathf.Sin(i * 2 * Mathf.PI / numberOfSlots) * spaceToPlayer,
                0, 
                Mathf.Cos(i * 2 * Mathf.PI / numberOfSlots) * spaceToPlayer 
            );
            slotStatus[slot] = true;
        }
    }
    
    public GameObject GetAvailabeClosestSlot(Vector3 npcPosition)
    {
        GameObject closestSlot = null;
        float closestDistance = Mathf.Infinity;

        foreach (var slot in slotStatus)
        {
            if (slot.Value) // If the slot is available
            {
                float distance = Vector3.Distance(npcPosition, slot.Key.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestSlot = slot.Key;
                }
            }
        }

        if (closestSlot != null)
            slotStatus[closestSlot] = false;
        return closestSlot;
    }

    public void ResetSlot(GameObject slot)
    {
        if (slotStatus.ContainsKey(slot))
            slotStatus[slot] = true;
    }

}
