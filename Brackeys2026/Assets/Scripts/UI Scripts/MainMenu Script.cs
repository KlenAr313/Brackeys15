using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UIDocument document;
    private VisualElement mainRoot;
    private VisualElement optionsRoot;
    private Button startButton;
    private Button optionsButton;
    private Button exitButton;
    private Button backButton;
    private bool options = false;
    void Awake()
    {
        if (document != null)
        {
            mainRoot = document.rootVisualElement.Q<VisualElement>("MainRoot");

            optionsRoot = document.rootVisualElement.Q<VisualElement>("OptionsRoot");
            optionsRoot.AddToClassList("hidden");

            startButton = document.rootVisualElement.Q<Button>("StartButton");
            startButton.RegisterCallback<ClickEvent>(OnStartClick);
            optionsButton = document.rootVisualElement.Q<Button>("OptionsButton");
            optionsButton.RegisterCallback<ClickEvent>(OnOptionsClick);
            backButton = document.rootVisualElement.Q<Button>("BackButton");
            backButton.RegisterCallback<ClickEvent>(OnOptionsClick);
            exitButton = document.rootVisualElement.Q<Button>("ExitButton");
            exitButton.RegisterCallback<ClickEvent>(OnExitClick);
        }
    }

    void ToggleOptions()
    {
        options = !options;

        if (options)
        {
            optionsRoot.RemoveFromClassList("hidden");
            optionsRoot.AddToClassList("visible");
            mainRoot.RemoveFromClassList("visible");
            mainRoot.AddToClassList("hidden");
        }
        else
        {
            optionsRoot.RemoveFromClassList("visible");
            optionsRoot.AddToClassList("hidden");
            mainRoot.RemoveFromClassList("hidden");
            mainRoot.AddToClassList("visible");
        }
    }

    void OnStartClick(ClickEvent evt)
    {
        SceneManager.LoadScene("Mező");
    }

    void OnOptionsClick(ClickEvent evt)
    {
        ToggleOptions();
    }

    void OnExitClick(ClickEvent evt)
    {
        Application.Quit();
    }
}
