using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UIDocument document;
    private VisualElement mainRoot;
    private VisualElement optionsRoot;
    private Button startButton;
    private Button optionsButton;
    private Button exitButton;
    private Button backButton;
    private Slider musicSlider;
    private Slider sfxSlider;
    private Slider sensSlider;

    private bool options = false;
    void Start()
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

            musicSlider = document.rootVisualElement.Q<Slider>("MusicSlider");
            musicSlider.RegisterValueChangedCallback(evt => { AudioManager.Instance.SetMusic(evt.newValue); });
            musicSlider.value = AudioManager.Instance.GetMusic();
        
            sfxSlider = document.rootVisualElement.Q<Slider>("SFXSlider");
            sfxSlider.RegisterValueChangedCallback(evt => { AudioManager.Instance.SetSFX(evt.newValue); });
            sfxSlider.value = AudioManager.Instance.GetSFX();

            sensSlider = document.rootVisualElement.Q<Slider>("SensitivitySlider");
            sensSlider.RegisterValueChangedCallback(evt => { AudioManager.Instance.SetSens(evt.newValue); });
            sensSlider.value   = AudioManager.Instance.GetSens();
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
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
