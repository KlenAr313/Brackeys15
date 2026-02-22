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

    [SerializeField] private float ScaleIntensity = 0.3f;
    [SerializeField] private float hueShiftIntensity = 100f;
    [SerializeField]
    private float distiortionSpeed = 5.0f;
    [SerializeField]
    private float maxDistortion = 0.7f;
    [SerializeField]
    private float scalePulse = 0.3f;

    [SerializeField]
    private float swaySpeed = 4.0f;
    [SerializeField]
    private float swayAmount = 6.0f;

    void Start() {
        volume = FindFirstObjectByType<Volume>();
        volume.profile.TryGet(out colorAdjustments);
        volume.profile.TryGet(out lensDistortion);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);
        Gizmos.DrawIcon(transform.position, "nausea.png", true);
    }

    private void OnTriggerEnter(Collider collision)
	{
        if (collision.gameObject.tag.ToLower() == "player")
		{
			TriggerNausea();
            TrigerRat();
		}
	}

    public void TriggerNausea()
    {
        StartCoroutine(NauseaRoutine());
    }

    IEnumerator NauseaRoutine() {
        float elapsed = 0f;

        float hueShift = colorAdjustments.hueShift.value;
        float intensity = lensDistortion.intensity.value;
        float scale = lensDistortion.scale.value;

        GameObject player = GameObject.Find("Main Camera");

        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
        // Cycles the hue from -180 to 180 over time
            float shift = Mathf.PingPong(Time.time * hueShiftIntensity, 360) - 180;
            colorAdjustments.hueShift.value = shift;

            float wave = Mathf.Sin(Time.time * distiortionSpeed) * ScaleIntensity;
            lensDistortion.intensity.value = wave * maxDistortion;
            lensDistortion.scale.value = 1.0f + (wave * scalePulse);

            float tiltZ = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
            player.transform.localRotation = Quaternion.Euler(0, 0, tiltZ);
            yield return null;
        }

        // Reset values after effect ends
        colorAdjustments.hueShift.value = hueShift;
        lensDistortion.intensity.value = intensity;
        lensDistortion.scale.value = scale;
        player.transform.localRotation = Quaternion.identity;
    }

    private void TrigerRat()
    {
        GameObject[] rats = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject rat in rats)
        {
            TurnToPlayer turnToPlayer = rat.GetComponent<TurnToPlayer>();
            if (turnToPlayer != null)
            {
                turnToPlayer.StartGrowUp();
            }
        }
    }
}
