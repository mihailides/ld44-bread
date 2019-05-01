using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public float speed;
    public Animator animator;
    float timer = 0;
    
    /** Desperation values */
    public float startingDesperation = 0;
    public float currentDesperation;
    public Slider desperationSlider;
    public float periodicDesperationRate = 1;
    public float periodicDesperationTimeOut = 1;
    public float desperationLossPerPhoto = 10;
    public float maxDesperation = 100;

    /** Money values */
    public float startingMoney = 100000;
    public float currentMoney;
    public float startingLossPercentage = 10;
    public GameObject moneyTextObject;
    private TMPro.TextMeshProUGUI moneyText;
    
    private TwitterScript twitterScript;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentDesperation = startingDesperation;
        currentMoney = startingMoney;
        moneyText = moneyTextObject.GetComponent<TMPro.TextMeshProUGUI>();
        moneyText.text = "$" + startingMoney.ToString();
        
        twitterScript = GameObject.Find("UI/TwitterScriptObject").GetComponent<TwitterScript>();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > periodicDesperationTimeOut) 
        {
            PeriodicallyIncreaseDesperation();
            timer -= periodicDesperationTimeOut;
        }
    }

    private void MoveCharacter() 
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
        Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0);
        movement = Vector3.ClampMagnitude(movement, 1);
        
        var moving = (Math.Abs(moveHorizontal) > 0.1 || Math.Abs(moveVertical) > 0.1);
        if (moving)
        {
            var angle = Mathf.Atan2(moveHorizontal, moveVertical) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
        }

        animator.SetBool("moving", moving);
        
        transform.position += movement * speed * Time.deltaTime;
    }

    /** Increase desperation over time */
    private void PeriodicallyIncreaseDesperation() {
        float possibleDesperation = currentDesperation + periodicDesperationRate;
        if (currentDesperation == maxDesperation || possibleDesperation >= maxDesperation) 
        {
            if (currentDesperation != maxDesperation) 
            {
                currentDesperation = maxDesperation;
            }
        } 
        else 
        {
            currentDesperation += periodicDesperationRate;
        }

        if (currentDesperation % 10 == 0)
        {
            twitterScript.SendMessage("AddDesperationTweet");
        }
        
        desperationSlider.value = currentDesperation;
    }

    /** Use this function when a photo is taken. Desperation decreases, but so does money. */
    public void LoseDesperationAndMoney() 
    {
        if (currentMoney == 0) 
        {
            return;
        }
        else if (currentMoney < 100) 
        {
            float possibleMoneyLoss = currentMoney - 6.62f;
            if (possibleMoneyLoss <  0) {
                currentMoney = 0;
            } else {
                currentMoney -= 6.62f;
            }
        } 
        // Maybe take in whether it was a chaser or a static mob? And change val based on that.
        else if (currentMoney < 1000) 
        {
            float possibleMoneyLoss = currentMoney - 100;
            if (possibleMoneyLoss <  0) {
                currentMoney = 0;
            } else {
                currentMoney -= 100;
            }
        }
        else 
        {
            currentMoney = currentMoney / startingLossPercentage;
        }

        // Update desperation
        if (currentDesperation <= desperationLossPerPhoto) 
        {   
            currentDesperation = 0;
        }
        else 
        {
            currentDesperation -= desperationLossPerPhoto;
        }
        SetMoneyText(currentMoney);
        desperationSlider.value = currentDesperation;
    }

    public void SetMoneyText(float currentMoney) 
    {
        moneyText.text = "$" + currentMoney.ToString();
    }
}
