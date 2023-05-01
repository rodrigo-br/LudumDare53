using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    void FixedUpdate()
    {
        if (Input.anyKeyDown)
        {
            Invoke("RestartGame", 1f);
        }
    }

    void RestartGame() 
    {
        BaseSingleton baseSingleton = FindObjectOfType<BaseSingleton>();
        foreach (Transform child in baseSingleton.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        Destroy(baseSingleton.gameObject);
        SceneManager.LoadScene(0);
    }
}
