using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject bg1, bg2, bg3;
    public GameObject[] bgAudios = new GameObject[2];
    public GameObject  groceryAudio, sanitizerAudio, startAudio,  gameOverAudio, startScreenAudio;

    // Start is called before the first frame update
    void Start()
    {
        bgAudios[0] = bg1;
        bgAudios[1] = bg2;
        bgAudios[2] = bg3;
    }

}
