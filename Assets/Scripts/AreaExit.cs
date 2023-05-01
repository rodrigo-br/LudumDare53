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
            Transition();
        }
    }

    public void Transition()
    {
        SceneManagment.Instance.SetTransitionName(sceneTransitionName);
        FadeScreen.Instance.FadeToBlack();
        StartCoroutine(LoadSceneRoutine());
    }

    IEnumerator LoadSceneRoutine()
    {
        yield return new WaitForSecondsRealtime(1f / FadeScreen.Instance.GetFadeSpeed());
        SceneManager.LoadScene(targetScene);
    }
}
