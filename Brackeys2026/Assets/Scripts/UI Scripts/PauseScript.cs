using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private UIDocument pauseDocument;
    //This will be the main UI, that will disappear/reappear based on pausing
    //[SerializeField] private UIDocument mainDocument;
    [SerializeField] private KeyCode interactionKey = KeyCode.Escape;
    private VisualElement pauseRoot;
    private VisualElement mainRoot;
    private VisualElement optionsRoot;
    private Button startButton;
    private Button optionsButton;
    private Button backButton;
    private Button exitButton;
    private Slider musicSlider;
    private Slider sfxSlider;
    private bool paused = false;
    private bool options = false;

    void Start()
    {
        if (pauseDocument != null)
        {
            pauseRoot = pauseDocument.rootVisualElement.Q<VisualElement>("PauseRoot");
            pauseRoot.AddToClassList("hidden");

            mainRoot = pauseDocument.rootVisualElement.Q<VisualElement>("MainRoot");

            optionsRoot = pauseDocument.rootVisualElement.Q<VisualElement>("OptionsRoot");
            optionsRoot.AddToClassList("hidden");

            startButton = pauseDocument.rootVisualElement.Q<Button>("ResumeButton");
            startButton.RegisterCallback<ClickEvent>(OnResumeClick);
            optionsButton = pauseDocument.rootVisualElement.Q<Button>("OptionsButton");
            optionsButton.RegisterCallback<ClickEvent>(OnOptionsClick);
            backButton = pauseDocument.rootVisualElement.Q<Button>("BackButton");
            backButton.RegisterCallback<ClickEvent>(OnOptionsClick);
            exitButton = pauseDocument.rootVisualElement.Q<Button>("ExitButton");
            exitButton.RegisterCallback<ClickEvent>(OnExitClick);

            if (AudioManager.Instance != null)
            {
                musicSlider = pauseDocument.rootVisualElement.Q<Slider>("MusicSlider");
                musicSlider.RegisterValueChangedCallback(evt => { AudioManager.Instance.SetMusic(evt.newValue); });
                musicSlider.value = AudioManager.Instance.GetMusic();

                sfxSlider = pauseDocument.rootVisualElement.Q<Slider>("SFXSlider");
                sfxSlider.RegisterValueChangedCallback(evt => { AudioManager.Instance.SetSFX(evt.newValue); });
                sfxSlider.value   = AudioManager.Instance.GetSFX();
            }
        
        }
    }

    void Update()
    {
        if (Keyboard.current != null)
        {
            Key parsedKey;
            if (System.Enum.TryParse<Key>(interactionKey.ToString(), out parsedKey))
            {
                KeyControl keyControl = Keyboard.current[parsedKey];
                if (keyControl != null && keyControl.wasPressedThisFrame)
                    if (!options)
                    {
                        TogglePause();
                        //DeathScript.Die();
                        //HealthScript.SetHealth(70f, 100f);
                        //DeathScript.Die();
                        //HealthScript.SetHealth(70f, 100f);
                    }
                    else
                    {
                        ToggleOptions();
                    }
                    
            }
        }
    }

    void TogglePause()
    {
        paused = !paused;
        InputHandler playerControllerScript = GameObject.Find("Player").GetComponent<InputHandler>();

        if (paused)
        {
            pauseRoot.RemoveFromClassList("hidden");
            pauseRoot.AddToClassList("visible");

            HealthScript.HideHealth();

            HealthScript.HideHealth();

            Time.timeScale = 0f;
            playerControllerScript.Sensitivity = 0f;
            playerControllerScript.Punch = false;
        }
        else
        {
            pauseRoot.RemoveFromClassList("visible");
            pauseRoot.AddToClassList("hidden");

            HealthScript.ShowHealth();

            HealthScript.ShowHealth();

            Time.timeScale = 1f;
            playerControllerScript.Sensitivity = playerControllerScript.OriginalSensitivity;
            playerControllerScript.Punch = false;
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

    void OnResumeClick(ClickEvent evt)
    {
        TogglePause();
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
