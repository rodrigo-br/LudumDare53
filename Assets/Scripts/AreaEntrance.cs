using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] string sceneTransitionName;

    void Start()
    {
        if (sceneTransitionName == SceneManagment.Instance.SceneTransitionName)
        {
            FindObjectOfType<PlayerMovement>().transform.position = transform.position;
            CameraController.Instance.SetPlayerCameraFollow();
        }
    }
}
