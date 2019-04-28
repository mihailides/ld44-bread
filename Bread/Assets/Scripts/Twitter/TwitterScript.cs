using System;
using System.Collections.Generic;
using TMPro;
using Twitter;
using UnityEngine;
using Random = System.Random;

public class TwitterScript : MonoBehaviour
{
    private GameObject feed;
    private GameObject originalTweet;

    private readonly PersonNameGenerator nameGenerator = new PersonNameGenerator();

    private readonly ShuffleBag<string> desperationBag = new ShuffleBag<string>(
        "anyone seen {0}? heard some juicy stuff",
        "looking for {0}"
    );
    
    private readonly ShuffleBag<string> pictureBag = new ShuffleBag<string>(
        "you won't believe this picture we just took\n\n#{0}",
        "omfg look at this picture i just took of {0}",
        "HAHAHA {0} OMG"
    );

    private readonly ShuffleBag<string> startBag = new ShuffleBag<string>(
        "so i just saw {0} they look like a total trainwreck",
        "I just saw {0} and I am totally shocked...",
        "omfg you won't believe who i just saw. dead",
        "hot tip: just saw {0} leave",
        "{0} is so cancelled right now",
        "can't believe how different {0} looks",
        "{0} SPOTTED"
    );

    private readonly ShuffleBag<string> nameBag = new ShuffleBag<string>(
        "spotted3left1", "bakerygang", "Kat1029314", "nirmalmalick",
        "happypenguinjei", "spotted_flower", "umium", "Grim_Chimp",
        "wrojzaa", "Archaeous_Up", "lozo33", "{0}_news",
        "extra_sayonara", "fallen_4ng3l_113", "thesefosters", "murpphhhhh",
        "paperbox", "Shapeway4Life", "ShapewayTwoPlease", "pixi662318",
        "nick110013", "cholula_noice", "taemanetteta", "start_i_zen",
        "{0}131", "{0}aaa", "{0}Fan", "{0}OfficialNews", "{0}Lads",
        "{0}_sucks", "{0}News", "Real{0}News", "Based{0}",
        "{0}77512", "stripey_candie", "lightie1", "toucano",
        "top_down_right_up", "{0}Mafia", "{0}GlobalNEWS", "REALLY_NOW3",
        "ShreddyTheTiger", "_its{0}", "bunnyjungle1997", "molly_h3ernandez",
        "gtfogabby", "hceahands", "NameTaken10204", "phaseslittle"
    );

    void Start()
    {
        feed = GameObject.Find("TwitterView");
        originalTweet = GameObject.Find("Tweet");

        AddTweet(GenerateTwitterItem(startBag, false));
    }

    void Update()
    {
        
    }

    public void AddDesperationTweet()
    {
        AddTweet(GenerateTwitterItem(desperationBag, false));
    }

    public void AddPictureTweet()
    {
        AddTweet(GenerateTwitterItem(pictureBag, true));
    }

    private void AddTweet(TwitterItem item)
    {
        var newTweet = Instantiate(originalTweet);

        var tweetName = newTweet.transform.Find("TweetHolder/HandleHolder/ActualName");
        tweetName.GetComponent<TMP_Text>().text = item.Name;

        var handle = newTweet.transform.Find("TweetHolder/HandleHolder/Handle");
        handle.GetComponent<TMP_Text>().text = "@" + item.Username;

        var contents = newTweet.transform.Find("TweetHolder/TweetContents");
        contents.GetComponent<TMP_Text>().text = item.Tweet;

        newTweet.transform.SetParent(feed.transform, false);
    }
    
    private TwitterItem GenerateTwitterItem(ShuffleBag<string> bag, bool withImage)
    {
        string famousPerson = "FamousPerson";
        
        return new TwitterItem
        {
            Username = String.Format(nameBag.Next(), famousPerson),
            Name = nameGenerator.GenerateRandomFirstAndLastName(),
            Tweet = String.Format(bag.Next(), famousPerson),
            Timestamp = DateTime.Now,
            ImageSeed = withImage ? GetImageSeed : (int?)null
        };
    }
    
    private int GetImageSeed => new Random().Next(0, int.MaxValue);
}
