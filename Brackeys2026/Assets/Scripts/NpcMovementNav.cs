using System.Threading;
using UnityEngine;

public class NpcMovementNav : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField]
    private float detectionRange = 10f;

    private float timer = 0;
    [SerializeField]private float UpdateInterval = 0.5f;

    private SlotManager slotManager;
    private bool triggered = true;

    public GameObject currentSlot;

    public void Trigger()
    {
        triggered = true;
    }

    void Start()
    {
        slotManager = FindFirstObjectByType<SlotManager>();
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        if(timer < UpdateInterval)
        {
            timer += Time.deltaTime;
            return;
        }

        timer = 0;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRange && currentSlot == null && triggered && seesPlayer())
        {

            currentSlot = slotManager.GetAvailabeClosestSlot(transform.position);
            if (currentSlot != null)
            {
                agent.SetDestination(currentSlot.transform.position);
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

    bool seesPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        Ray ray = new Ray(transform.position, directionToPlayer);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, detectionRange, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore))
            return false;

        return true;
    }
}
