using UnityEngine;

public class HoboKingScript : MonoBehaviour
{
    [SerializeField] ThugScript thug1;
    [SerializeField] ThugScript thug2;

    private NPCInteractScript npcInteractScript;

    void Start()
    {
        npcInteractScript = GetComponent<NPCInteractScript>();
    }

    void Update()
    {
        if (npcInteractScript != null)
        {
            if (npcInteractScript.didInteract)
            {
                thug1.canInteract = true;
                thug2.canInteract = true;
            }
        }
    }

    public void QuestComplete()
    {
        
    }
}
