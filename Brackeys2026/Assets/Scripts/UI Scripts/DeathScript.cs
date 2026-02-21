using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DeathScript : MonoBehaviour
{
    [SerializeField] private UIDocument deathDocument;
    static InputHandler inputHandler;
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
        inputHandler = GameObject.Find("Player").GetComponent<InputHandler>();
    }

    public static void Die()
    {
        deathRoot.RemoveFromClassList("hidden");
        deathRoot.AddToClassList("visible");

        HealthScript.HideHealth();

        Time.timeScale = 0f;
        inputHandler.Sensitivity = 0f;
        inputHandler.Punch = false;
    }

    void OnRespawnClick(ClickEvent evt)
    {
        deathRoot.RemoveFromClassList("visible");
        deathRoot.AddToClassList("hidden");
        Time.timeScale = 1f;
        inputHandler.Sensitivity = inputHandler.OriginalSensitivity;
        inputHandler.Punch = false;
        HealthScript.ShowHealth();
        PlayerControllerScript.Instance.Respawn();
    }

    void OnExitClick(ClickEvent evt)
    {
        Application.Quit();
    }
}
