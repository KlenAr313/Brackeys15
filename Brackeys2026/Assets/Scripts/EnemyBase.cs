using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int damage = 5;
    [SerializeField] private float damageInterval = 3;

    private bool inCombat = false;

    private float damageTimer = 0;

    public bool InCombat { get => inCombat; set => inCombat = value; }

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
        SlotManager.Instance.ResetSlot(this.gameObject.GetComponent<NpcMovementNav>().currentSlot);
        Destroy(gameObject);
    }

    void Update()
    {
        if(damageTimer >= damageInterval)
        {
            damageTimer = 0;
            if (InCombat)
            {
                MovementScript.Instance.TakeDamage(damage);
            }
        }

        damageTimer += Time.deltaTime;
    }
}
