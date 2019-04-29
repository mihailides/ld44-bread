using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Paparazzi : MonoBehaviour
{
    public float lockOnTimeout = 1;
    public float coneAngle = 45;
    public float coneLength = 5f;
    public float maxRotation = 15;
    public bool rotate;
    public Component player;

    private Player playerScript;
    private System.Random rand;
    private float timer;
    private float restartTimer;
    private float originalRotation;
    private GameObject blackScreen;
    private TwitterScript twitterScript;
    private MoveTowardsPlayer moveTowardsPlayer;
    private AudioSource audioSource;
    private AudioClip[] cameraSounds;
    
    private bool tookPicture;

    void Start()
    {
        timer = 0.0f;
        rand = new System.Random();

        playerScript = player.GetComponent<Player>();
        blackScreen = GameObject.Find("BlackScreen");
        twitterScript = GameObject.Find("UI/TwitterScriptObject").GetComponent<TwitterScript>();
        moveTowardsPlayer = GetComponent<MoveTowardsPlayer>();
        originalRotation = transform.eulerAngles.z;

        audioSource = transform.Find("CameraSound").GetComponent<AudioSource>();
        cameraSounds = Resources.LoadAll<AudioClip>("Sounds");
    }

    void FixedUpdate() 
    {
        timer += Time.deltaTime;

        if (rotate && !tookPicture)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time) + originalRotation);
        }

        if (timer > lockOnTimeout && !tookPicture) 
        {
            timer -= lockOnTimeout;

            var lockedOn = rand.NextDouble() <= GetChanceToLockOn();

            if (lockedOn)
            {
                var isInCone = CheckIfPlayerInCone();
                var canSee = CanSeePlayer();

                if (canSee && isInCone)
                {
                    TakePhotoOfPlayer(true);
                }
            }
        }
        else if (tookPicture)
        {
            restartTimer += Time.deltaTime;
        }

        if (restartTimer >= 2)
        {
            restartTimer = 0;
            tookPicture = false;
            timer = 0;
        }
    }
    
    private void TakePhotoOfPlayer(bool sendTweet)
    {
        transform.rotation = Quaternion.AngleAxis(GetAngleOfPlayer(), Vector3.forward);

        if (sendTweet)
        {
            twitterScript.SendMessage("AddPictureTweet");
        }

        if (moveTowardsPlayer != null)
        {
            moveTowardsPlayer.moving = false;
        }
        
        player.SendMessage("LoseDesperationAndMoney");
        
        audioSource.PlayOneShot(cameraSounds[Random.Range(0, cameraSounds.Length)]);
        blackScreen.SendMessage("ScreenFlash");

        tookPicture = true;
    }

    private bool CheckIfPlayerInCone()
    {
        // Get the direction to the player
        var dirToPlayer = player.transform.position - transform.position;

        // Cheap return if we are too far away
        if (dirToPlayer.magnitude > coneLength)
        {
            return false;
        }

        // Find the difference between the direction to the player and our facing direction
        var diff = Mathf.DeltaAngle(Quaternion.FromToRotation(Vector3.up, dirToPlayer).eulerAngles.z,
                                    transform.rotation.eulerAngles.z);

        // Abs for easy compare
        diff = Mathf.Abs(diff);
        return diff < coneAngle / 2;
    }
    
    private bool CanSeePlayer()
    {
        return !Physics2D.Linecast(transform.position, player.transform.position, 5);
    }

    private float GetAngleOfPlayer()
    {
        Vector3 dir = player.transform.position - transform.position;
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
    }
    
    private double GetChanceToLockOn()
    {
        return Math.Max(0.4, playerScript.currentDesperation / 100);
    }
}
