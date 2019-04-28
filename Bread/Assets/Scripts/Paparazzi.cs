using UnityEngine;
using Random = System.Random;

public class Paparazzi : MonoBehaviour
{
    public float lockOnTimeout = 1;
    private bool tookPicture;
    private Random rand;
    private float timer;
    
    private GameObject blackScreen;

    void Start()
    {
        timer = 0.0f;
        rand = new Random();
        
        blackScreen = GameObject.Find("BlackScreen");
            }

    void FixedUpdate() 
    {        
        timer += Time.deltaTime;
        if (timer > lockOnTimeout && !tookPicture) 
        {
            timer -= lockOnTimeout;

            var lockedOn = rand.NextDouble() <= GetChanceToLockOn();

            if (lockedOn)
            {
                Debug.Log("Locked on...");

                var canSee = CanSeePlayer();

                Debug.Log("Looking for player: " + canSee);

                if (canSee)
                {
                    TakePhotoOfPlayer(true);
                }
            }
        }      
    }

    private void TakePhotoOfPlayer(bool sendTweet)
    {
        transform.rotation = Quaternion.AngleAxis(GetAngleOfPlayer(), Vector3.forward);

        if (sendTweet)
        {
            //SendMessage("AddPictureTweet");
        }

        blackScreen.SendMessage("ScreenFlash");

        tookPicture = true;
    }

    private bool CanSeePlayer()
    {
        var player = GameObject.Find("Player");
        return !Physics2D.Linecast(transform.position, player.transform.position, 5);
    }

    private float GetAngleOfPlayer()
    {
        var player = GameObject.Find("Player");
        Vector3 dir = player.transform.position - transform.position;
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
    }
    
    private double GetChanceToLockOn()
    {
        // TODO: weight this depending on desperation
        return 0.5;
    }
}
