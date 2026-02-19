using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class CameraEffect : MonoBehaviour
{
    private Volume volume;
    private ColorAdjustments colorAdjustments;
    private LensDistortion lensDistortion;

    [SerializeField]
    private float duration = 3.0f;

    [SerializeField]
    private float distiortionSpeed = 5.0f;
    [SerializeField]
    private float maxDistortion = 0.7f;
    [SerializeField]
    private float scalePulse = 0.0f;

    [SerializeField]
    private float swaySpeed = 4.0f;
    [SerializeField]
    private float swayAmount = 6.0f;

    void Start() {
        volume = FindFirstObjectByType<Volume>();
        volume.profile.TryGet(out colorAdjustments);
        volume.profile.TryGet(out lensDistortion);
    }

    public void TriggerNausea()
    {
        StartCoroutine(NauseaRoutine());
    }

    IEnumerator NauseaRoutine() {
        float elapsed = 0f;

        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
        // Cycles the hue from -180 to 180 over time
            float shift = Mathf.PingPong(Time.time * 500, 360) - 180;
            colorAdjustments.hueShift.value = shift;

            float wave = Mathf.Sin(Time.time * distiortionSpeed);
            lensDistortion.intensity.value = wave * maxDistortion;
            lensDistortion.scale.value = 1.0f + (wave * scalePulse);

            float tiltZ = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
            transform.localRotation = Quaternion.Euler(0, 0, tiltZ);
            yield return null;
        }

        // Reset values after effect ends
        colorAdjustments.hueShift.value = 0;
        lensDistortion.intensity.value = 0;
        lensDistortion.scale.value = 1.0f;
        transform.localRotation = Quaternion.identity;
    }
}
