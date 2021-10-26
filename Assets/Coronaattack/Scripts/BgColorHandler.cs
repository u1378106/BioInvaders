using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgColorHandler : MonoBehaviour
{
    MeshRenderer bgSpriteRenderer;

    [SerializeField] [Range(0f, 1f)] float lerpTime;

    [SerializeField] Color[] myColors;

    int colorIndex = 0;

    float t = 0f;

    int len;

    Color alphaColor;

    // Start is called before the first frame update
    void Start()
    {
        bgSpriteRenderer = GetComponent<MeshRenderer>();
        
        len = myColors.Length;
    }

    // Update is called once per frame
    void Update()
    {
        bgSpriteRenderer.material.color = Color.Lerp(bgSpriteRenderer.material.color, myColors[colorIndex], lerpTime * Time.deltaTime);

        //alphaColor = bgSpriteRenderer.material.color;
        //alphaColor.a = 255;
        //bgSpriteRenderer.material.color = alphaColor;

        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);

        if (t > 0.9f)
        {
            t = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= len) ? 0 : colorIndex;
        }
    }
}
