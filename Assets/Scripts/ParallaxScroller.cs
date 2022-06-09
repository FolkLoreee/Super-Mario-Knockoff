using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroller : MonoBehaviour
{

    public Renderer[] layers;
    public float[] speedMultiplier;
    private float prevXPosMario;
    private float prevXPosCamera;
    public Transform mario;
    public Transform mainCamera;
    private float[] offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = new float[layers.Length];
        for (int i = 0; i < layers.Length; i++)
        {
            offset[i] = 0.0f;
        }
        prevXPosMario = mario.transform.position.x;
        prevXPosCamera = mainCamera.transform.position.x;

    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(prevXPosCamera - mainCamera.transform.position.x) > 0.001f)
        {
            for (int i = 0; i < layers.Length; i++)
            {
                if (offset[i] > 1.0f || offset[i] < -1.0f)
                {
                    offset[i] = 0.0f; //reset offset
                }
                float newOffset = mario.transform.position.x - prevXPosMario;
                offset[i] = offset[i] + newOffset * speedMultiplier[i];
                layers[i].material.mainTextureOffset = new Vector2(offset[i], 0);
            }
        }

        //update previous pos
        prevXPosMario = mario.transform.position.x;
        prevXPosCamera = mainCamera.transform.position.x;
    }
}
