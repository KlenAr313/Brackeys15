using Unity.VisualScripting;
using UnityEngine;

public class ThugScript : EnemyBase
{

    [SerializeField] private Sprite triggeredSprite;
    [SerializeField] private HoboKingScript hoboKingScript;

    new void Start()
    {
        base.Start();
        canInteract = false;
    }


    public override void Trigger()
    {
        if (!canInteract)
        {
            return;
        }

        base.Trigger();
        this.GetComponent<SpriteRenderer>().sprite = triggeredSprite;
    }

    protected override void Die()
    {
        hoboKingScript.QuestComplete();
        base.Die();
    }
}
