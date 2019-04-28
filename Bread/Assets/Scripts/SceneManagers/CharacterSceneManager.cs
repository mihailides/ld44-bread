using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class CharacterSceneManager : MonoBehaviour
{
    public TextMeshProUGUI nameText; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OKClicked()
    {
        if (nameText.text.Length == 0) return;
        
        var text = nameText.text.Substring(0, Math.Min(nameText.text.Length, 25));
        text = string.Concat(text.Where(c => char.IsWhiteSpace(c) || char.IsLetterOrDigit(c)));

        StaticData.Name = text;
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
}
