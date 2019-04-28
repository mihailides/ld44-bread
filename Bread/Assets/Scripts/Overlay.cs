using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{
    public Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (Input.GetKeyDown("q")) 
        {
            StartCoroutine(ScreenFlash());
        }
    }

    IEnumerator ScreenFlash() 
    {  
        var originalAlpha = image.color.a;
        var illuminatedView = image.color;

        illuminatedView.a = 0f;
        image.color = illuminatedView;

        yield return new WaitForSeconds(0.1f);

        illuminatedView.a = originalAlpha;
        image.color = illuminatedView;
    }
}
