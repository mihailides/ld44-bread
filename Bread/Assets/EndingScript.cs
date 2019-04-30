using System;
using TMPro;
using UnityEngine;

public class EndingScript : MonoBehaviour
{
    public GameObject messageText;
    public GameObject message;
    public GameObject player;
    public GameObject camera;
    private float atCoffee;
    private float atMessage;
    private TextMeshProUGUI messageTextTMP;
    
    // Start is called before the first frame update
    void Start()
    {
        atCoffee = 0;
        messageTextTMP = messageText.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x < 13.8)
        {
            player.transform.position += Vector3.right * 3 * Time.deltaTime;
        }
        else
        {
            if (atCoffee >= 0)
            {
                atCoffee += Time.deltaTime;
            }
        }

        if (atCoffee > 0.5)
        {
            player.transform.Rotate(new Vector3(0, 0, 90));
            atCoffee = -1;
        }

        if (Math.Abs(atCoffee - -1) < 0.1)
        {
            message.SetActive(true);
            atCoffee = -2;

            if (StaticData.NetWorth <= 3)
            {
                messageTextTMP.text = "Sorry... but you don't have enough money to buy this bread.";
            }
            else if (StaticData.NetWorth >= 1000000)
            {
                messageTextTMP.text = "Here's your rye bread!";
            }
            else
            {
                messageTextTMP.text = "Here's your bread!";
            }
        }
        
        if (atCoffee <= -2 && atMessage >= 0)
        {
            atMessage += Time.deltaTime;
        }

        if (atMessage >= 5)
        {
            message.SetActive(false);
            player.transform.Rotate(new Vector3(0, 0, -90));
            atMessage = -1;
        }
        
        if (Math.Abs(atMessage - -1) < 0.1)
        {
            player.transform.position += Vector3.right * 3 * Time.deltaTime;
            camera.GetComponent<FollowPlayer>().player = null;
        }

        if (player.transform.position.x > 25)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
