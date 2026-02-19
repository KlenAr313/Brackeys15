using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int damage = 10;

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
}
