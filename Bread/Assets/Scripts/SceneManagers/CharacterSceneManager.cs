using System.Collections;
using System.Collections.Generic;
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
        StaticData.Name = nameText.text;
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
