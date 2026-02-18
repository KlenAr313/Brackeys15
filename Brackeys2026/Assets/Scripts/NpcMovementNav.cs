using UnityEngine;

public class NpcMovementNav : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField]
    private float detectionRange = 10f;

    private SlotManager slotManager;
    private bool triggered = false;

    private GameObject currentSlot;

    void Start()
    {
        slotManager = FindFirstObjectByType<SlotManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRange && !triggered)
        {
            currentSlot = slotManager.GetAvailabeClosestSlot(transform.position);
            if (currentSlot != null)
            {
                agent.SetDestination(currentSlot.transform.position);
                triggered = true;
            }
        }
        else if (distanceToPlayer >= detectionRange && triggered)
        {
            agent.SetDestination(transform.position);
            slotManager.ResetSlot(currentSlot);
            currentSlot = null;
            triggered = false;
        }

        if (triggered && currentSlot != null)
        {
            agent.SetDestination(currentSlot.transform.position);
        }
    }
}
