using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class AudioControl : MonoBehaviour
{
    public AudioSource audioSource;

    private Coroutine loopCoroutine;

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return; // No keyboard connected

        if (keyboard.jKey.wasPressedThisFrame)
        {
            PlayOnce();
        }
        else if (keyboard.kKey.wasPressedThisFrame)
        {
            PlayLoopTimes(2);
        }
        else if (keyboard.lKey.wasPressedThisFrame)
        {
            PlayLoopTimes(3);
        }
        else if (keyboard.mKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    void PlayOnce()
    {
        StopLoopCoroutine();
        audioSource.loop = false;
        audioSource.Play();
    }

    void PlayLoopTimes(int count)
    {
        StopLoopCoroutine();
        loopCoroutine = StartCoroutine(PlayLoopCoroutine(count));
    }

    IEnumerator PlayLoopCoroutine(int loopCount)
    {
        for (int i = 0; i < loopCount; i++)
        {
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
        }
    }

    void TogglePause()
    {
        if (audioSource.isPlaying)
            audioSource.Pause();
        else
            audioSource.UnPause();
    }

    void StopLoopCoroutine()
    {
        if (loopCoroutine != null)
        {
            StopCoroutine(loopCoroutine);
            loopCoroutine = null;
        }
    }
}
