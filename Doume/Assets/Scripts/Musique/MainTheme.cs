using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTheme : MonoBehaviour
{
    private int themeIndex;
    public AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager.Play("Main Theme");
        themeIndex = 0;
    }

    private void Update()
    {
        if(!audioManager.CurrentSound.source.isPlaying)
        {
            themeIndex++;
            if (themeIndex > audioManager.sounds.Length)
                themeIndex = 0;
            audioManager.Play(themeIndex);
        }
    }
}
