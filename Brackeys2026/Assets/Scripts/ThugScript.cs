using Unity.VisualScripting;
using UnityEngine;

public class ThugScript : EnemyBase
{

    [SerializeField] private Sprite triggeredSprite;

    public override void Trigger()
    {
        base.Trigger();
        this.GetComponent<SpriteRenderer>().sprite = triggeredSprite;
    }
}
