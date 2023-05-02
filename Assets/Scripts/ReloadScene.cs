using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    void Start()
    {
        BaseSingleton baseSingleton = FindObjectOfType<BaseSingleton>();
        if (baseSingleton != null)
        {
            foreach (Transform child in baseSingleton.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            Destroy(baseSingleton.gameObject);
        }
    }

    void FixedUpdate()
    {
        if (Input.anyKeyDown)
        {
            Invoke("RestartGame", 1f);
        }
    }

    void RestartGame() 
    {
        SceneManager.LoadScene("Scene 1");
    }
}
