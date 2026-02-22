using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DeathScript : MonoBehaviour
{
    [SerializeField] private UIDocument document;
    private static UIDocument deathDocument;
    static InputHandler inputHandler;
    static PlayerControllerScript playerControllerScript;
    static VisualElement deathRoot;
    Button respawnButton;
    Button exitButton;

    void OnEnable()
    {
        deathDocument = document;
        deathRoot = deathDocument.rootVisualElement.Q<VisualElement>("DeathRoot");
        deathRoot.AddToClassList("hidden");
        deathRoot.style.display = DisplayStyle.None;
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
        deathRoot.style.display = DisplayStyle.Flex;

        HealthScript.HideHealth();

        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        deathDocument.rootVisualElement.pickingMode = PickingMode.Position;

        Time.timeScale = 0f;
        inputHandler.Sensitivity = 0f;
        PlayerControllerScript.Instance.canPunch = false;
    }

    void OnRespawnClick(ClickEvent evt)
    {
        deathRoot.RemoveFromClassList("visible");
        deathRoot.AddToClassList("hidden");
        deathRoot.style.display = DisplayStyle.None;

        HealthScript.ShowHealth();

        Time.timeScale = 1f;
        inputHandler.Sensitivity = inputHandler.OriginalSensitivity;
        PlayerControllerScript.Instance.canPunch = true;
        PlayerControllerScript.Instance.Respawn();
    }

    void OnExitClick(ClickEvent evt)
    {
        Application.Quit();
    }
}
