using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImagesAlphaChanger : MonoBehaviour
{

    public Image[] images;

    public float transparentCol = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        images = GetComponentsInChildren<Image>();
        foreach (Image img in images)
        {
            Color col = img.color;
            col.a = transparentCol;
            img.color = col;
        }
    }

    public void KeyPicked(KeyData data)
    {
        foreach (Image img in images)
        {
            if (img.gameObject.name == data.KEY_TYPE.ToString())
            {
                Color col = img.color;
                col.a = 1f;
                img.color = col;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
