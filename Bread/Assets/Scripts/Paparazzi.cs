using UnityEngine;

public class Paparazzi : MonoBehaviour
{
    public float lockOnTimeout = 1;
    public float coneAngle = 45;
    public float maxRotation = 15;
    public bool rotate = true;
    public Component player;

    private System.Random rand;
    private float timer;
    private float originalRotation;
    private GameObject blackScreen;
    private GameObject hud;
    private AudioSource audioSource;
    private AudioClip[] cameraSounds;
    
    private bool tookPicture;

    void Start()
    {
        timer = 0.0f;
        rand = new System.Random();
        blackScreen = GameObject.Find("BlackScreen");
        originalRotation = transform.eulerAngles.z;

        audioSource = transform.Find("CameraSound").GetComponent<AudioSource>();
        cameraSounds = Resources.LoadAll<AudioClip>("Sounds");
    }

    void FixedUpdate() 
    {
        timer += Time.deltaTime;

        if (rotate)
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
    }
    
    private void TakePhotoOfPlayer(bool sendTweet)
    {
        transform.rotation = Quaternion.AngleAxis(GetAngleOfPlayer(), Vector3.forward);

        if (sendTweet)
        {
            //SendMessage("AddPictureTweet");
        }

        player.SendMessage("LoseDesperationAndMoney");
        
        audioSource.PlayOneShot(cameraSounds[Random.Range(0, cameraSounds.Length)]);
        blackScreen.SendMessage("ScreenFlash");

        tookPicture = true;
    }

    private bool CheckIfPlayerInCone()
    {
        return true;
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
        // TODO: weight this depending on desperation
        return 0.5;
    }
}
