using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{
    private float timer;
    public Image image;

    void Start()
    {
        image = GetComponent<Image>();
        timer = 0.0f;
    }

    void Update()
    {
        timer = Time.deltaTime;
        if (timer > 1) 
        {
            ScreenFlash();
            timer -= 1;
        }
    }

    private void ScreenFlash() 
    {   

    }
}
