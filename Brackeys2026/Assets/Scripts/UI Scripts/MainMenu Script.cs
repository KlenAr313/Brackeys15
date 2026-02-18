using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UIDocument document;
    private Button startButton;
    private Button optionsButton;
    private Button exitButton;
    void Awake()
    {
        if (document != null)
        {
            startButton = document.rootVisualElement.Q<Button>("StartButton");
            startButton.RegisterCallback<ClickEvent>(OnStartClick);
            optionsButton = document.rootVisualElement.Q<Button>("OptionsButton");
            optionsButton.RegisterCallback<ClickEvent>(OnOptionsClick);
            exitButton = document.rootVisualElement.Q<Button>("ExitButton");
            exitButton.RegisterCallback<ClickEvent>(OnExitClick);
        }
    }

    void OnStartClick(ClickEvent evt)
    {
        SceneManager.LoadScene("Mező");
    }

    void OnOptionsClick(ClickEvent evt)
    {
        
    }

    void OnExitClick(ClickEvent evt)
    {
        Application.Quit();
    }
}
