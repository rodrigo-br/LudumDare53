using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YukiFlash : MonoBehaviour
{
    [SerializeField] AudioClip whisper;
    SpriteRenderer girl;

    void Awake()
    {
        girl = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        girl.enabled = false;
    }

    void FixedUpdate()
    {
        FlashGirl();
    }

    void FlashGirl()
    {
        if (Random.Range(0, 2000) % 1999 == 0)
        {
            AudioSource.PlayClipAtPoint(whisper, Camera.main.transform.position);
            StartCoroutine(FlashingCoroutine());
        }
    }

    IEnumerator FlashingCoroutine()
    {
        float timeFlashing = 0.5f;
        while (timeFlashing > 0)
        {
            girl.enabled = !girl.enabled;
            timeFlashing -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        girl.enabled = false;
    }
}
