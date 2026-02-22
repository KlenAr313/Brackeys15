using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class HealthScript : MonoBehaviour
{
    [SerializeField] private UIDocument mainDocument;
    static VisualElement fullHealthRoot;
    static VisualElement missingHealthRoot;
    static float height;
    void Start()
    {
        
        fullHealthRoot = mainDocument.rootVisualElement.Q<VisualElement>("FullHealthRoot");
        missingHealthRoot = mainDocument.rootVisualElement.Q<VisualElement>("MissingHealthRoot");
        missingHealthRoot.RegisterCallback<GeometryChangedEvent>(evt =>
        {
            height = evt.newRect.height;
        });
    }



    public static void SetHealth(float current, float max = 100)
    {
        float percent = current / max;

        float newHeight = percent * height;

        fullHealthRoot.experimental.animation.Start(new StyleValues {
            height = newHeight,
        }, 1000);
    }

    public static void HideHealth()
    {
        fullHealthRoot.RemoveFromClassList("visible");
        fullHealthRoot.AddToClassList("hidden");
        missingHealthRoot.RemoveFromClassList("visible");
        missingHealthRoot.AddToClassList("hidden");
    }

    public static void ShowHealth()
    {
        fullHealthRoot.RemoveFromClassList("hidden");
        fullHealthRoot.AddToClassList("visible");
        missingHealthRoot.RemoveFromClassList("hidden");
        missingHealthRoot.AddToClassList("visible");
    }
}
