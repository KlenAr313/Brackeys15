using UnityEngine;

public class StartMusicScript : MonoBehaviour
{
    private bool triggered = false;
    private void OnTriggerEnter(Collider collision)
	{
        if (collision.gameObject.tag.ToLower() == "player" && !triggered)
		{
            triggered = true;
			TriggerMusic();
		}
	}

    private void TriggerMusic()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
