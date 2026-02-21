using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TurnToPlayer : MonoBehaviour
{
    public Transform player;

    [SerializeField]
    private Sprite bigRatSprite;
    [SerializeField]
    private Sprite smallRatSprite;
    [SerializeField]
    private float growUpTime = 1.5f;
    
    private bool isRat = false;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        isRat = gameObject.name.Contains("RAT");
    }

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0; 
        transform.rotation = Quaternion.LookRotation(direction);
    }
    
    public void StartGrowUp()
    {
        if (isRat)
        {
            StartCoroutine(growUpChange());
        }
    }
    
    private void growUp()
    {
        NavMeshAgent navMesh = GetComponent<NavMeshAgent>();
        navMesh.baseOffset = 0.6f;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = bigRatSprite;
    }

    private void growDown()
    {
        NavMeshAgent navMesh = GetComponent<NavMeshAgent>();
        navMesh.baseOffset = 0.2f;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = smallRatSprite;
    }

    private IEnumerator growUpChange()
    {
        yield return new WaitForSeconds(0.5f);
        bool isSmall = false;
        float i = 0f;
        while(i < growUpTime)
        {
            if (isSmall)
            {
                growUp();
            }
            else
            {
                growDown();
            }
            isSmall = !isSmall;
            yield return new WaitForSeconds(0.12f);
            i += 0.12f;
        }
        growUp();
    }

}
