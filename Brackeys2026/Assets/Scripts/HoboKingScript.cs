using UnityEngine;

public class HoboKingScript : MonoBehaviour
{
    [SerializeField] ThugScript thug1;
    [SerializeField] ThugScript thug2;
    [SerializeField] GameObject door;
    [SerializeField] GameObject nextSceneTrigger;

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
        door.SetActive(false);
        nextSceneTrigger.SetActive(true);
    }
}
