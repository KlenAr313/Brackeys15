using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int damage = 5;
    [SerializeField] private float damageInterval = 3;
    [SerializeField] private float attackRange = 1f;

    [SerializeField] private Transform player;
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] private float detectionRange = 10f;

    private float timer = 0;
    [SerializeField] private float UpdateInterval = 0.5f;
    [SerializeField] private bool triggered = true;
    [SerializeField] private bool stayTriggered = true;

    private SlotManager slotManager;

    public GameObject currentSlot;
    private CustomAnimator animator;

    private bool inCombat = false;

    private float damageTimer = 0;
    private Vector3 originalPosition;

    public bool InCombat { get => inCombat; set => inCombat = value; }
    public virtual void Trigger() { triggered = true; }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if(this.currentSlot != null)
        {
            slotManager.ResetSlot(this.currentSlot);
            slotManager.Enemies.Remove(this);
        }
        
        Destroy(gameObject);
    }

    void Start()
    {
        slotManager = FindFirstObjectByType<SlotManager>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponentInChildren<CustomAnimator>();
        damageInterval = Random.Range(2f, 4f);
        slotManager.Enemies.Add(this);
        originalPosition = this.gameObject.transform.position;
    }

    void Update()
    {
        Movement();
        if(damageTimer >= damageInterval)
        {
            damageTimer = 0;
            damageInterval = Random.Range(2f, 4f);
            if (InCombat && inMelleeRange())
            {
                animator.PlayAnimation();
                PlayerControllerScript.Instance.TakeDamage(damage);
            }
        }

        damageTimer += Time.deltaTime;
    }

    void Movement()
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
                inCombat = true;
            }
        }
        else if (distanceToPlayer >= detectionRange && triggered && currentSlot != null)
        {
            agent.SetDestination(transform.position);
            slotManager.ResetSlot(currentSlot);
            currentSlot = null;
            if(!stayTriggered)
                triggered = false;
            inCombat = false;
        }

        if (triggered && currentSlot != null)
        {
            agent.SetDestination(currentSlot.transform.position);
            inCombat = true;
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

    bool inMelleeRange()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        if (Physics.Raycast(gameObject.transform.position, directionToPlayer, out RaycastHit hit, attackRange))
        {
			return true;
        }
        return false;
    }

    public void Reset()
    {
        triggered = true;
        inCombat = false;
        damageTimer = 0;
        this.gameObject.transform.position = originalPosition;
    }
}
