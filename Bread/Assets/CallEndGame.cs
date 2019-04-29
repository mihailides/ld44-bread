using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallEndGame : MonoBehaviour
{
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        StaticData.NetWorth = player.GetComponent<Player>().currentMoney;
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
}
