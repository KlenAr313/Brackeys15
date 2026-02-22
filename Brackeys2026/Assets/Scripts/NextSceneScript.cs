using System;
using UnityEngine;

public class NextSceneScript : MonoBehaviour
{
    [SerializeField] private Vector3 spawnPosition;
    public void ChangeScene()
    {
        PlayerControllerScript.Instance.transform.position = spawnPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);
        Gizmos.DrawIcon(transform.position, "NextScene.png", true);
    }
}
