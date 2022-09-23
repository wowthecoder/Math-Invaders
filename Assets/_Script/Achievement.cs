using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Achievement : MonoBehaviour
{
    public RectTransform achievementcontent;
    public Scrollbar achievementscrollbar;
    public GameObject[] content1;
    public GameObject[] content2;
    public GameObject[] PartA;
    public Text[] timertext;
    public Button[] claimrwd;
    public GameObject smallreddot;
    public Text cointext;
    List<Text> progresstext = new List<Text>();
    List<Slider> progressSlider = new List<Slider>();
    private int asteroiddestroyed, enplanedestroyed, correctquestion, totalscore8000, totalscore15000, threekfor3games, fivekfor3games;
    private float totaltime;
    private bool rwdclaimed1, rwdclaimed2, rwdclaimed3, rwdclaimed4, rwdclaimed5, rwdclaimed6, rwdclaimed7, rwdclaimed8, rwdclaimed9;
    public static bool breakhighscore;
    private DateTime initialtime1, initialtime2, initialtime3, initialtime4, initialtime5, initialtime6, initialtime7, initialtime8, initialtime9;
    private int goldamount, goldtobeadded, addgoldhowmanytimes;
    private bool isclaimingrwd;//if claiming in process, stop another claiming until the first one has been done
    public Animator coinvibrate;
    public AudioSource coinaddsound;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Startingposition", 0.05f);
        for (int i = 0; i < PartA.Length; i++)
        {
            progresstext.Add(PartA[i].transform.Find("progress").gameObject.GetComponent<Text>());
            progressSlider.Add(PartA[i].transform.Find("progressSlider").gameObject.GetComponent<Slider>());
        }
        rwdclaimed1 = (PlayerPrefs.GetInt("rwdoneclaimed", 0) == 1) ? true: false;
        rwdclaimed2 = (PlayerPrefs.GetInt("rwdtwoclaimed", 0) == 1) ? true : false;
        rwdclaimed3 = (PlayerPrefs.GetInt("rwdthreeclaimed", 0) == 1) ? true : false;
        rwdclaimed4 = (PlayerPrefs.GetInt("rwdfourclaimed", 0) == 1) ? true : false;
        rwdclaimed5 = (PlayerPrefs.GetInt("rwdfiveclaimed", 0) == 1) ? true : false;
        rwdclaimed6 = (PlayerPrefs.GetInt("rwdsixclaimed", 0) == 1) ? true : false;
        rwdclaimed7 = (PlayerPrefs.GetInt("rwdsevenclaimed", 0) == 1) ? true : false;
        rwdclaimed8 = (PlayerPrefs.GetInt("rwdeightclaimed", 0) == 1) ? true : false;
        rwdclaimed9 = (PlayerPrefs.GetInt("rwdnineclaimed", 0) == 1) ? true : false;

        string timeclaim1 = PlayerPrefs.GetString("inittime1", DateTime.MinValue.ToString());
        initialtime1 = Convert.ToDateTime(timeclaim1);
        string timeclaim2 = PlayerPrefs.GetString("inittime2", DateTime.MinValue.ToString());
        initialtime2 = DateTime.Parse(timeclaim2);
        string timeclaim3 = PlayerPrefs.GetString("inittime3", DateTime.MinValue.ToString());
        initialtime3 = DateTime.Parse(timeclaim3);
        string timeclaim4 = PlayerPrefs.GetString("inittime4", DateTime.MinValue.ToString());
        initialtime4 = DateTime.Parse(timeclaim4);
        string timeclaim5 = PlayerPrefs.GetString("inittime5", DateTime.MinValue.ToString());
        initialtime5 = DateTime.Parse(timeclaim5);
        string timeclaim6 = PlayerPrefs.GetString("inittime6", DateTime.MinValue.ToString());
        initialtime6 = DateTime.Parse(timeclaim6);
        string timeclaim7 = PlayerPrefs.GetString("inittime7", DateTime.MinValue.ToString());
        initialtime7 = DateTime.Parse(timeclaim7);
        string timeclaim8 = PlayerPrefs.GetString("inittime8", DateTime.MinValue.ToString());
        initialtime8 = DateTime.Parse(timeclaim8);
        string timeclaim9 = PlayerPrefs.GetString("inittime9", DateTime.MinValue.ToString());
        initialtime9 = DateTime.Parse(timeclaim9);
    }

    // Update is called once per frame
    void Update()
    {
        if (achievementcontent.anchoredPosition.x != 0)
            achievementcontent.anchoredPosition = new Vector2(0, achievementcontent.anchoredPosition.y);
        if (achievementcontent.anchoredPosition.y < -6.1f)
            achievementcontent.anchoredPosition = new Vector2(achievementcontent.anchoredPosition.x, -6.1f);
        if (achievementcontent.anchoredPosition.y > 550)
            achievementcontent.anchoredPosition = new Vector2(achievementcontent.anchoredPosition.x, 550);
        achievementscrollbar.value = (achievementcontent.anchoredPosition.y + 6) / 511;

        cointext.text = PlayerPrefs.GetInt("Gold", 0).ToString();
        asteroiddestroyed = PlayerPrefs.GetInt("asteroiddestroy", 0);
        enplanedestroyed = PlayerPrefs.GetInt("enplanedestroy", 0);
        correctquestion = PlayerPrefs.GetInt("corques", 0);
        totalscore8000 = PlayerPrefs.GetInt("Totalscore8000", 0);
        totalscore15000 = PlayerPrefs.GetInt("Totalscore15000", 0);
        totaltime = PlayerPrefs.GetFloat("Totaltime", 0);
        breakhighscore = (PlayerPrefs.GetInt("highscorebreaked", 0) == 1) ? true : false;
        threekfor3games = PlayerPrefs.GetInt("3k3games", 0);
        fivekfor3games = PlayerPrefs.GetInt("5k3games", 0);
        goldamount = PlayerPrefs.GetInt("Gold", 1000);
        if ((asteroiddestroyed >= 70 && !rwdclaimed1)||(enplanedestroyed >= 50 && !rwdclaimed2)||(correctquestion >= 30 && !rwdclaimed3)||(totalscore8000 >= 8000 && !rwdclaimed4)
            ||(totalscore15000 >= 15000 && !rwdclaimed5)||(totaltime >= 300 && !rwdclaimed6)||(breakhighscore && !rwdclaimed7)||(threekfor3games >= 3 && !rwdclaimed8)||(fivekfor3games >= 3 && !rwdclaimed9))//one of the achievements not claimed, activate small red dot
        {
            smallreddot.SetActive(true);
        }
        else
        {
            smallreddot.SetActive(false);
        }
        //1st achievement
        if (asteroiddestroyed < 70 && !rwdclaimed1)
        {
            Haventcomplete(0);
            progresstext[0].text = asteroiddestroyed.ToString() + " / 70";
            float A = asteroiddestroyed;
            progressSlider[0].value = (A / 70) * 100;
        }
        if (asteroiddestroyed >= 70 && !rwdclaimed1)
        {
            Completehaventclaim(0);
        }
        if (rwdclaimed1)
        {
            asteroiddestroyed = 0;
            PlayerPrefs.SetInt("asteroiddestroy", asteroiddestroyed);
            Activatetimer(0);
            //timer code
            TimeSpan timereset1 = System.DateTime.Now - initialtime1;
            int totalsecondspassed = Mathf.RoundToInt((float)timereset1.TotalSeconds);
            if (totalsecondspassed < 43200)//lesser than 12 hours
            {
                float totalsec1 = totalsecondspassed;
                int hour = 11 - (int)Mathf.Floor(totalsec1 / 3600);
                int min = 59 - (int)Mathf.Floor((totalsec1 % 3600) / 60);
                int second = 59 - (int)Mathf.Floor((totalsec1 % 3600) % 60);
                timertext[0].text = hour + " : " + min + " : " + second;
                if (second < 10 || min < 10 || hour < 10)
                {
                    SetTimerAddZero(0, second, min, hour);
                }
            }
            else if (totalsecondspassed >= 43200)// >= 12 hours, mission reset
            {
                Haventcomplete(0);
                asteroiddestroyed = 0;
                PlayerPrefs.SetInt("asteroiddestroy", asteroiddestroyed);
                rwdclaimed1 = false;
                PlayerPrefs.SetInt("rwdoneclaimed", 0);
                initialtime1 = DateTime.MinValue;
                PlayerPrefs.SetString("inittime1", initialtime1.ToString());
            }
        }
        //2nd achievement
        if (enplanedestroyed < 50 && !rwdclaimed2)
        {
            Haventcomplete(1);
            progresstext[1].text = enplanedestroyed.ToString() + " / 50";
            float A = enplanedestroyed;
            progressSlider[1].value = (A / 50) * 100;
        }
        if (enplanedestroyed >= 50 && !rwdclaimed2)
        {
            Completehaventclaim(1);
        }
        if (rwdclaimed2)
        {
            enplanedestroyed = 0;
            PlayerPrefs.SetInt("enplanedestroy", enplanedestroyed);
            Activatetimer(1);
            //timer code
            TimeSpan timereset2 = System.DateTime.Now - initialtime2;
            int totalsecondspassed = Mathf.RoundToInt((float)timereset2.TotalSeconds);
            if (totalsecondspassed < 43200)//lesser than 12 hours
            {
                float totalsec2 = totalsecondspassed;
                int hour = 11 - (int)Mathf.Floor(totalsec2 / 3600);
                int min = 59 - (int)Mathf.Floor((totalsec2 % 3600) / 60);
                int second = 59 - (int)Mathf.Floor((totalsec2 % 3600) % 60);
                timertext[1].text = hour + " : " + min + " : " + second;
                if (second < 10 || min < 10 || hour < 10)
                {
                    SetTimerAddZero(1, second, min, hour);
                }
            }
            else if (totalsecondspassed >= 43200)// >= 12 hours, mission reset
            {
                Haventcomplete(1);
                enplanedestroyed = 0;
                PlayerPrefs.SetInt("enplanedestroy", enplanedestroyed);
                rwdclaimed2 = false;
                PlayerPrefs.SetInt("rwdtwoclaimed", 0);
                initialtime2 = DateTime.MinValue;
                PlayerPrefs.SetString("inittime2", initialtime2.ToString());
            }
        }
        //3rd achievement
        if (correctquestion < 30 && !rwdclaimed3)
        {
            Haventcomplete(2);
            progresstext[2].text = correctquestion.ToString() + " / 30";
            float A = correctquestion;
            progressSlider[2].value = (A / 30) * 100;
        }
        if (correctquestion >= 30 && !rwdclaimed3)
        {
            Completehaventclaim(2);
        }
        if (rwdclaimed3)
        {
            correctquestion = 0;
            PlayerPrefs.SetInt("corques", correctquestion);
            Activatetimer(2);
            //timer code
            TimeSpan timereset3 = System.DateTime.Now - initialtime3;
            int totalsecondspassed = Mathf.RoundToInt((float)timereset3.TotalSeconds);
            if (totalsecondspassed < 43200)//lesser than 12 hours
            {
                float totalsec3 = totalsecondspassed;
                int hour = 11 - (int)Mathf.Floor(totalsec3 / 3600);
                int min = 59 - (int)Mathf.Floor((totalsec3 % 3600) / 60);
                int second = 59 - (int)Mathf.Floor((totalsec3 % 3600) % 60);
                timertext[2].text = hour + " : " + min + " : " + second;
                if (second < 10 || min < 10 || hour < 10)
                {
                    SetTimerAddZero(2, second, min, hour);
                }
            }
            else if (totalsecondspassed >= 43200)// >= 12 hours, mission reset
            {
                Haventcomplete(2);
                correctquestion = 0;
                PlayerPrefs.SetInt("corques", correctquestion);
                rwdclaimed3 = false;
                PlayerPrefs.SetInt("rwdthreeclaimed", 0);
                initialtime3 = DateTime.MinValue;
                PlayerPrefs.SetString("inittime3", initialtime3.ToString());
            }
        }
        //4th achievement
        if (totalscore8000 < 8000 && !rwdclaimed4)
        {
            Haventcomplete(3);
            progresstext[3].text = totalscore8000.ToString() + " / 8000";
            float A = totalscore8000;
            progressSlider[3].value = (A / 8000) * 100;
        }
        if (totalscore8000 >= 8000 && !rwdclaimed4)
        {
            Completehaventclaim(3);
        }
        if (rwdclaimed4)
        {
            Activatetimer(3);
            totalscore8000 = 0;
            PlayerPrefs.SetInt("Totalscore8000", totalscore8000);
            //timer code
            TimeSpan timereset4 = System.DateTime.Now - initialtime4;
            int totalsecondspassed = Mathf.RoundToInt((float)timereset4.TotalSeconds);
            if (totalsecondspassed < 43200)//lesser than 12 hours
            {
                float totalsec4 = totalsecondspassed;
                int hour = 11 - (int)Mathf.Floor(totalsec4 / 3600);
                int min = 59 - (int)Mathf.Floor((totalsec4 % 3600) / 60);
                int second = 59 - (int)Mathf.Floor((totalsec4 % 3600) % 60);
                timertext[3].text = hour + " : " + min + " : " + second;
                if (second < 10 || min < 10 || hour < 10)
                {
                    SetTimerAddZero(3, second, min, hour);
                }
            }
            else if (totalsecondspassed >= 43200)// >= 12 hours, mission reset
            {
                Haventcomplete(3);
                totalscore8000 = 0;
                PlayerPrefs.SetInt("Totalscore8000", totalscore8000);
                rwdclaimed4 = false;
                PlayerPrefs.SetInt("rwdfourclaimed", 0);
                initialtime4 = DateTime.MinValue;
                PlayerPrefs.SetString("inittime4", initialtime4.ToString());
            }
        }
        //5th achievement
        if (totalscore15000 < 15000 && !rwdclaimed5)
        {
            Haventcomplete(4);
            progresstext[4].text = totalscore15000.ToString() + " / 15000";
            float A = totalscore15000;
            progressSlider[4].value = (A / 15000) * 100;
        }
        if (totalscore15000 >= 15000 && !rwdclaimed5)
        {
            Completehaventclaim(4);
        }
        if (rwdclaimed5)
        {
            totalscore15000 = 0;
            PlayerPrefs.SetInt("Totalscore15000", totalscore15000);
            Activatetimer(4);
            //timer code
            TimeSpan timereset5 = System.DateTime.Now - initialtime5;
            int totalsecondspassed = Mathf.RoundToInt((float)timereset5.TotalSeconds);
            if (totalsecondspassed < 43200)//lesser than 12 hours
            {
                float totalsec5 = totalsecondspassed;
                int hour = 11 - (int)Mathf.Floor(totalsec5 / 3600);
                int min = 59 - (int)Mathf.Floor((totalsec5 % 3600) / 60);
                int second = 59 - (int)Mathf.Floor((totalsec5 % 3600) % 60);
                timertext[4].text = hour + " : " + min + " : " + second;
                if (second < 10 || min < 10 || hour < 10)
                {
                    SetTimerAddZero(4, second, min, hour);
                }
            }
            else if (totalsecondspassed >= 43200)// >= 12 hours, mission reset
            {
                Haventcomplete(4);
                totalscore15000 = 0;
                PlayerPrefs.SetInt("Totalscore15000", totalscore15000);
                rwdclaimed5 = false;
                PlayerPrefs.SetInt("rwdfiveclaimed", 0);
                initialtime5 = DateTime.MinValue;
                PlayerPrefs.SetString("inittime5", initialtime5.ToString());
            }
        }
        //6th achievement
        if (totaltime < 300 && !rwdclaimed6)
        {
            Haventcomplete(5);
            progresstext[5].text = Mathf.RoundToInt(totaltime).ToString() + " / 300";
            progressSlider[5].value = (totaltime / 300) * 100;
        }
        if (totaltime >= 300 && !rwdclaimed6)
        {
            Completehaventclaim(5);
        }
        if (rwdclaimed6)
        {
            totaltime = 0;
            PlayerPrefs.SetFloat("Totaltime", totaltime);
            Activatetimer(5);
            //timer code
            TimeSpan timereset6 = System.DateTime.Now - initialtime6;
            int totalsecondspassed = Mathf.RoundToInt((float)timereset6.TotalSeconds);
            if (totalsecondspassed < 43200)//lesser than 12 hours
            {
                float totalsec6 = totalsecondspassed;
                int hour = 11 - (int)Mathf.Floor(totalsec6 / 3600);
                int min = 59 - (int)Mathf.Floor((totalsec6 % 3600) / 60);
                int second = 59 - (int)Mathf.Floor((totalsec6 % 3600) % 60);
                timertext[5].text = hour + " : " + min + " : " + second;
                if (second < 10 || min < 10 || hour < 10)
                {
                    SetTimerAddZero(5, second, min, hour);
                }
            }
            else if (totalsecondspassed >= 43200)// >= 12 hours, mission reset
            {
                Haventcomplete(5);
                totaltime = 0;
                PlayerPrefs.SetFloat("Totaltime", totaltime);
                rwdclaimed6 = false;
                PlayerPrefs.SetInt("rwdsixclaimed", 0);
                initialtime6 = DateTime.MinValue;
                PlayerPrefs.SetString("inittime6", initialtime6.ToString());
            }
        }
        //7th achievement
        if (!breakhighscore && !rwdclaimed7)
        {
            Haventcomplete(6);            
            progresstext[6].text =  "0 / 1";
            progressSlider[6].value = 0;
        }
        if (breakhighscore && !rwdclaimed7)
        {
            Completehaventclaim(6);
        }
        if (rwdclaimed7)
        {
            breakhighscore = false;
            PlayerPrefs.SetInt("highscorebreaked", 0);
            Activatetimer(6);
            //timer code
            TimeSpan timereset7 = System.DateTime.Now - initialtime7;
            int totalsecondspassed = Mathf.RoundToInt((float)timereset7.TotalSeconds);
            if (totalsecondspassed < 10800)//lesser than 3 hours
            {
                float totalsec7 = totalsecondspassed;
                int hour = 2 - (int)Mathf.Floor(totalsec7 / 3600);
                int min = 59 - (int)Mathf.Floor((totalsec7 % 3600) / 60);
                int second = 59 - (int)Mathf.Floor((totalsec7 % 3600) % 60);
                timertext[6].text = hour + " : " + min + " : " + second;
                if (second < 10 || min < 10 || hour < 10)
                {
                    SetTimerAddZero(6, second, min, hour);
                }
            }
            else if (totalsecondspassed >= 10800)// >= 3 hours, mission reset
            {
                Haventcomplete(6);
                breakhighscore = false;
                PlayerPrefs.SetInt("highscorebreaked", 0);
                rwdclaimed7 = false;
                PlayerPrefs.SetInt("rwdsevenclaimed", 0);
                initialtime7 = DateTime.MinValue;
                PlayerPrefs.SetString("inittime7", initialtime7.ToString());
            }
        }
        //8th achievement
        if (threekfor3games < 3 && !rwdclaimed8)
        {
            Haventcomplete(7);
            progresstext[7].text = threekfor3games.ToString() + " / 3";
            float A = threekfor3games;
            progressSlider[7].value = (A / 3) * 100;
        }
        if (threekfor3games >= 3 && !rwdclaimed8)
        {
            Completehaventclaim(7);
        }
        if (rwdclaimed8)
        {
            threekfor3games = 0;
            PlayerPrefs.SetInt("3k3games", threekfor3games);
            Activatetimer(7);
            //timer code
            TimeSpan timereset8 = System.DateTime.Now - initialtime8;
            int totalsecondspassed = Mathf.RoundToInt((float)timereset8.TotalSeconds);
            if (totalsecondspassed < 43200)//lesser than 12 hours
            {
                float totalsec8 = totalsecondspassed;
                int hour = 11 - (int)Mathf.Floor(totalsec8 / 3600);
                int min = 59 - (int)Mathf.Floor((totalsec8 % 3600) / 60);
                int second = 59 - (int)Mathf.Floor((totalsec8 % 3600) % 60);
                timertext[7].text = hour + " : " + min + " : " + second;
                if (second < 10 || min < 10 || hour < 10)
                {
                    SetTimerAddZero(7, second, min, hour);
                }
            }
            else if (totalsecondspassed >= 43200)// >= 12 hours, mission reset
            {
                Haventcomplete(7);
                threekfor3games = 0;
                PlayerPrefs.SetInt("3k3games", threekfor3games);
                rwdclaimed8 = false;
                PlayerPrefs.SetInt("rwdeightclaimed", 0);
                initialtime8 = DateTime.MinValue;
                PlayerPrefs.SetString("inittime8", initialtime8.ToString());
            }
        }
        //9th achievement
        if (fivekfor3games < 3 && !rwdclaimed9)
        {
            Haventcomplete(8);
            progresstext[8].text = fivekfor3games.ToString() + " / 3";
            float A = fivekfor3games;
            progressSlider[8].value = (A / 3) * 100;
        }
        if (fivekfor3games >= 3 && !rwdclaimed9)
        {
            Completehaventclaim(8);
        }
        if (rwdclaimed9)
        {
            fivekfor3games = 0;
            PlayerPrefs.SetInt("5k3games", fivekfor3games);
            Activatetimer(8);
            //timer code
            TimeSpan timereset9 = System.DateTime.Now - initialtime9;
            int totalsecondspassed = Mathf.RoundToInt((float)timereset9.TotalSeconds);
            if (totalsecondspassed < 43200)//lesser than 12 hours
            {
                float totalsec9 = totalsecondspassed;
                int hour = 11 - (int)Mathf.Floor(totalsec9 / 3600);
                int min = 59 - (int)Mathf.Floor((totalsec9 % 3600) / 60);
                int second = 59 - (int)Mathf.Floor((totalsec9 % 3600) % 60);
                timertext[8].text = hour + " : " + min + " : " + second;
                if (second < 10 || min < 10 || hour < 10)
                {
                    SetTimerAddZero(8, second, min, hour);
                }
            }
            else if (totalsecondspassed >= 43200)// >= 12 hours, mission reset
            {
                Haventcomplete(8);
                fivekfor3games = 0;
                PlayerPrefs.SetInt("5k3games", fivekfor3games);
                rwdclaimed9 = false;
                PlayerPrefs.SetInt("rwdnineclaimed", 0);
                initialtime9 = DateTime.MinValue;
                PlayerPrefs.SetString("inittime9", initialtime9.ToString());
            }
        }
    }

    void Haventcomplete(int n)
    {
        content1[n].SetActive(true);
        PartA[n].SetActive(true);
        claimrwd[n].gameObject.SetActive(false);
        content2[n].SetActive(false);
    }

    void Completehaventclaim(int x)
    {
        content1[x].SetActive(true);
        PartA[x].SetActive(false);
        claimrwd[x].gameObject.SetActive(true);
        content2[x].SetActive(false);
    }

    void Activatetimer(int a)
    {
        content1[a].SetActive(false);
        content2[a].SetActive(true);
    }

    void SetTimerAddZero(int whichtimer, int second, int min, int hour)
    {
          if (second < 10 && min < 10 && hour < 10)
              timertext[whichtimer].text = "0" + hour + " : 0" + min + " : 0" + second;
          else if (second < 10 && min < 10)
              timertext[whichtimer].text = hour + " : 0" + min + " : 0" + second;
          else if (hour < 10 && min < 10)
              timertext[whichtimer].text = "0" + hour + " : 0" + min + " : " + second;
          else if (hour < 10 && second < 10)
              timertext[whichtimer].text = "0" + hour + " : " + min + " : 0" + second;
          else if (second < 10)
              timertext[whichtimer].text = hour + " : " + min + " : 0" + second;
          else if (min < 10)
              timertext[whichtimer].text = hour + " : 0" + min + " : " + second;
          else if (hour < 10)
              timertext[whichtimer].text = "0" + hour + " : " + min + " : " + second;
    }
    public void Claim1stAchievement()
    {
        if (isclaimingrwd)
        {
            StartCoroutine(Wait(1.1f));
            return;
        }
        else if (!isclaimingrwd)
        {
            isclaimingrwd = true;
            coinvibrate.SetBool("CoinAdded", false);
            goldtobeadded = 100;
            InvokeRepeating("AddGoldSlowly", 0f, 0.2f);
            rwdclaimed1 = true;
            PlayerPrefs.SetInt("rwdoneclaimed", 1);
            initialtime1 = System.DateTime.Now;
            PlayerPrefs.SetString("inittime1", initialtime1.ToString());
        }
    }
    public void Claim2ndAchievement()
    {
        if (isclaimingrwd)
        {
            StartCoroutine(Wait(1.1f));
            return;
        }
        else if (!isclaimingrwd)
        {
            isclaimingrwd = true;
            coinvibrate.SetBool("CoinAdded", false);
            goldtobeadded = 150;
            InvokeRepeating("AddGoldSlowly", 0f, 0.2f);
            rwdclaimed2 = true;
            PlayerPrefs.SetInt("rwdtwoclaimed", 1);
            initialtime2 = System.DateTime.Now;
            PlayerPrefs.SetString("inittime2", initialtime2.ToString());
        }
    }
    public void Claim3rdAchievement()
    {
        if (isclaimingrwd)
        {
            StartCoroutine(Wait(1.1f));
            return;
        }
        else if (!isclaimingrwd)
        {
            isclaimingrwd = true;
            coinvibrate.SetBool("CoinAdded", false);
            goldtobeadded = 150;
            InvokeRepeating("AddGoldSlowly", 0f, 0.2f);
            rwdclaimed3 = true;
            PlayerPrefs.SetInt("rwdthreeclaimed", 1);
            initialtime3 = System.DateTime.Now;
            PlayerPrefs.SetString("inittime3", initialtime3.ToString());
        }
    }
    public void Claim4thAchievement()
    {
        if (isclaimingrwd)
        {
            StartCoroutine(Wait(1.1f));
            return;
        }
        else if (!isclaimingrwd)
        {
            isclaimingrwd = true;
            coinvibrate.SetBool("CoinAdded", false);
            goldtobeadded = 200;
            InvokeRepeating("AddGoldSlowly", 0f, 0.2f);
            rwdclaimed4 = true;
            PlayerPrefs.SetInt("rwdfourclaimed", 1);
            initialtime4 = System.DateTime.Now;
            PlayerPrefs.SetString("inittime4", initialtime4.ToString());
        }
    }
    public void Claim5thAchievement()
    {
        if (isclaimingrwd)
        {
            StartCoroutine(Wait(1.1f));
            return;
        }
        else if (!isclaimingrwd)
        {
            isclaimingrwd = true;
            coinvibrate.SetBool("CoinAdded", false);
            goldtobeadded = 250;
            InvokeRepeating("AddGoldSlowly", 0f, 0.2f);
            rwdclaimed5 = true;
            PlayerPrefs.SetInt("rwdfiveclaimed", 1);
            initialtime5 = System.DateTime.Now;
            PlayerPrefs.SetString("inittime5", initialtime5.ToString());
        }
    }
    public void Claim6thAchievement()
    {
        if (isclaimingrwd)
        {
            StartCoroutine(Wait(1.1f));
            return;
        }
        else if (!isclaimingrwd)
        {
            isclaimingrwd = true;
            coinvibrate.SetBool("CoinAdded", false);
            goldtobeadded = 150;
            InvokeRepeating("AddGoldSlowly", 0f, 0.2f);
            rwdclaimed6 = true;
            PlayerPrefs.SetInt("rwdsixclaimed", 1);
            initialtime6 = System.DateTime.Now;
            PlayerPrefs.SetString("inittime6", initialtime6.ToString());
        }
    }
    public void Claim7thAchievement()
    {
        if (isclaimingrwd)
        {
            StartCoroutine(Wait(1.1f));
            return;
        }
        else if (!isclaimingrwd)
        {
            isclaimingrwd = true;
            coinvibrate.SetBool("CoinAdded", false);
            goldtobeadded = 500;
            InvokeRepeating("AddGoldSlowly", 0f, 0.2f);
            rwdclaimed7 = true;
            PlayerPrefs.SetInt("rwdsevenclaimed", 1);
            initialtime7 = System.DateTime.Now;
            PlayerPrefs.SetString("inittime7", initialtime7.ToString());
        }
    }
    public void Claim8thAchievement()
    {
        if (isclaimingrwd)
        {
            StartCoroutine(Wait(1.1f));
            return;
        }
        else if (!isclaimingrwd)
        {
            isclaimingrwd = true;
            coinvibrate.SetBool("CoinAdded", false);
            goldtobeadded = 200;
            InvokeRepeating("AddGoldSlowly", 0f, 0.2f);
            rwdclaimed8 = true;
            PlayerPrefs.SetInt("rwdeightclaimed", 1);
            initialtime8 = System.DateTime.Now;
            PlayerPrefs.SetString("inittime8", initialtime8.ToString());
        }
    }
    public void Claim9thAchievement()
    {
        if (isclaimingrwd)
        {
            StartCoroutine(Wait(1.1f));
            return;
        }
        else if (!isclaimingrwd)
        {
            isclaimingrwd = true;
            coinvibrate.SetBool("CoinAdded", false);
            goldtobeadded = 300;
            InvokeRepeating("AddGoldSlowly", 0f, 0.2f);
            rwdclaimed9 = true;
            PlayerPrefs.SetInt("rwdnineclaimed", 1);
            initialtime9 = System.DateTime.Now;
            PlayerPrefs.SetString("inittime9", initialtime9.ToString());
        }
    }
    IEnumerator Wait(float s)
    {
        yield return new WaitForSeconds(s);
    }
    void AddGoldSlowly()
    {
        addgoldhowmanytimes += 1;
        float A = goldtobeadded;
        goldamount += Mathf.RoundToInt(A / 5);
        PlayerPrefs.SetInt("Gold", goldamount);
        coinaddsound.Play();
        if (addgoldhowmanytimes >= 5)
        {
            CancelInvoke();
            addgoldhowmanytimes = 0;
            isclaimingrwd = false;
            coinvibrate.SetBool("CoinAdded", true);
            coinaddsound.Stop();
        }
    }
    void Startingposition()
    {
        achievementcontent.anchoredPosition = new Vector2(0, -6.1f);
    }
}
