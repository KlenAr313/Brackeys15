using UnityEngine;

public class StartMusicScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
	{
        if (collision.gameObject.tag.ToLower() == "player")
		{
			TriggerMusic();
		}
	}

    private void TriggerMusic()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
