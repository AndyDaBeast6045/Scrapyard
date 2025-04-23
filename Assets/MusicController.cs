using System.Collections;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioClip startclip;
    [SerializeField] AudioClip loopclip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicSource.clip = startclip;
        StartCoroutine(StartLoop());
    }
    IEnumerator StartLoop()
    {
        yield return new WaitForSeconds(startclip.length);
        musicSource.loop = true;
        musicSource.clip = loopclip;
        musicSource.Play();
    }
}
