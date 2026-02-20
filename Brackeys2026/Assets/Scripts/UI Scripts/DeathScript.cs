using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DeathScript : MonoBehaviour
{
    [SerializeField] private UIDocument deathDocument;
    static PlayerControllerScript playerControllerScript;
    static VisualElement deathRoot;
    Button respawnButton;
    Button exitButton;

    void Start()
    {
        deathRoot = deathDocument.rootVisualElement.Q<VisualElement>("DeathRoot");
        deathRoot.AddToClassList("hidden");
        respawnButton = deathDocument.rootVisualElement.Q<Button>("RespawnButton");
        respawnButton.RegisterCallback<ClickEvent>(OnRespawnClick);
        exitButton = deathDocument.rootVisualElement.Q<Button>("ExitButton");
        exitButton.RegisterCallback<ClickEvent>(OnExitClick);
    }

    public static void Die()
    {
        deathRoot.RemoveFromClassList("hidden");
        deathRoot.AddToClassList("visible");

        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerScript>();
        Time.timeScale = 0f;
        playerControllerScript.Sensitivity = 0f;
        playerControllerScript.Punch = false;
    }

    void OnRespawnClick(ClickEvent evt)
    {
        Time.timeScale = 1f;
        playerControllerScript.Sensitivity = playerControllerScript.OriginalSensitivity;
        playerControllerScript.Punch = false;
    }

    void OnExitClick(ClickEvent evt)
    {
        Application.Quit();
    }
}
