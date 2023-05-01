using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] string targetScene;
    [SerializeField] string sceneTransitionName;
    [SerializeField] bool isStair;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isStair && other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(targetScene);
            SceneManagment.Instance.SetTransitionName(sceneTransitionName);
        }
    }

    public void Transition()
    {
        SceneManager.LoadScene(targetScene);
        SceneManagment.Instance.SetTransitionName(sceneTransitionName);
    }
}
