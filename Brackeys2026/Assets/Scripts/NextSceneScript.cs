using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneScript : MonoBehaviour
{
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private Quaternion spawnRotation;

    [SerializeField] private bool isRestart = false;
    public void ChangeScene()
    {
        PlayerControllerScript.Instance.transform.position = spawnPosition;
        PlayerControllerScript.Instance.transform.rotation = spawnRotation;

        if (isRestart)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);
        Gizmos.DrawIcon(transform.position, "NextScene.png", true);
    }
}
