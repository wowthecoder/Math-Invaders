using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Text scoreText;
    public int score;
    public Text bestScoreText;
    private int bestScore;
    public Button restartbutton;
    public Button backtomenubutton;
    public Button pausebutton;
    public Image settingbox;
    public Text GameOverText;
    private bool restart;
    [HideInInspector]
    public static bool gameOver;
    private int timesplayed = 0;
    private int timesclicked = 0;
    private string gameId = "3278241";
    private bool testmode;
    public float adinterval = 0;
    public GameObject joystick;
    private float XP;
    public Text XPplustext;
    public GameObject XPshield, XPbar, XPslider;
    public GameObject XPshieldsprite;
    private int XPfunctioncalled;
    private int AddXPhowmanytimes;
    private int xplefttolevelup, XPafterlevelup;
    public static bool XPshieldspritetouched, canlevelup;
    private int sumofscore1, sumofscore2, threek3games, fivek3games;
    private int addsumofscoreOnce = 0;
    private bool previousover3000, previousover5000;
    private float totaltimer;//timer for counting total time of survival
    private int deactivateBtnOnce = 0;
    private int checkScoreOnce = 0;
    void Start ()
    {
        Time.timeScale = 1;
        gameOver = false;
        restart = false;
        GameOverText.text = "";
        XPplustext.text = "";
        restartbutton.gameObject.SetActive(false);
        settingbox.gameObject.SetActive(false);
        backtomenubutton.gameObject.SetActive(false);
        joystick.SetActive(true);
        score = 0;
        bestScore = PlayerPrefs.GetInt("highScore");
        timesclicked = PlayerPrefs.GetInt("timeclick");
        timesplayed = PlayerPrefs.GetInt("timeplay");
        adinterval = PlayerPrefs.GetFloat("adinter");
        UpdateScore();
        StartCoroutine (SpawnWaves());
        XP = PlayerPrefs.GetFloat("currentxp", 0);
        XPfunctioncalled = 0;
        XPshieldsprite.SetActive(false);
        XPslider.SetActive(false);
        XPshieldspritetouched = false;
        AddXPhowmanytimes = 0;
        sumofscore1 = PlayerPrefs.GetInt("Totalscore8000", 0);
        sumofscore2 = PlayerPrefs.GetInt("Totalscore15000", 0);
        totaltimer = PlayerPrefs.GetFloat("Totaltime", 0);
        threek3games = PlayerPrefs.GetInt("3k3games", 0);
        fivek3games = PlayerPrefs.GetInt("5k3games", 0);
        previousover3000 = (PlayerPrefs.GetInt("previousgot3000", 0) == 1) ? true : false;
        previousover5000 = (PlayerPrefs.GetInt("previousgot5000", 0) == 1) ? true : false;
    }
    void Update()
    {
        //Debug.Log(PlayerPrefs.GetInt("asteroiddestroy", 0));
        adinterval += Time.deltaTime;
        PlayerPrefs.SetFloat("adinter", adinterval);
        if (!gameOver)
            totaltimer += Time.deltaTime;
        if (gameOver)
        {
            if (score > bestScore)
            {
                bestScore = score;
                bestScoreText.text = "Best Score:" + bestScore.ToString();
                PlayerPrefs.SetInt("highScore", bestScore);
                Achievement.breakhighscore = true;
                PlayerPrefs.SetInt("highscorebreaked", 1);
            }
            CheckScore();
            if (addsumofscoreOnce < 1)
            {
                addsumofscoreOnce += 1;
                sumofscore1 += score;
                sumofscore2 += score;
            }
            PlayerPrefs.SetInt("Totalscore8000", sumofscore1);
            PlayerPrefs.SetInt("Totalscore15000", sumofscore2);
            PlayerPrefs.SetFloat("Totaltime", totaltimer);
            XPshield.SetActive(true);
            XPbar.SetActive(true);
            CreateXPShield();
            XPplustext.text = "+ " + (score / 10) + " XP";
            restartbutton.gameObject.SetActive(true);
            backtomenubutton.gameObject.SetActive(true);
            if (deactivateBtnOnce < 1)
            {
                deactivateBtnOnce += 1;
                restartbutton.enabled = false;
                backtomenubutton.enabled = false;
            }
            restart = true;
        }
    }

    void CheckScore()
    {
        checkScoreOnce += 1;
        if (checkScoreOnce < 2)//check only once
        {
            if (score >= 3000)
            {
                previousover3000 = true;
                PlayerPrefs.SetInt("previousgot3000", 1);
                if (threek3games < 3)
                {
                    threek3games += 1;
                    PlayerPrefs.SetInt("3k3games", threek3games);
                }
                else if (threek3games >= 3)
                {
                    if (!previousover3000)
                    {
                        threek3games = 1;
                        PlayerPrefs.SetInt("3k3games", threek3games);
                    }
                    else if (previousover3000)
                    {
                        threek3games += 1;
                        PlayerPrefs.SetInt("3k3games", threek3games);
                    }
                }
                if (score >= 5000)
                {
                    fivek3games += 1;
                    PlayerPrefs.SetInt("5k3games", fivek3games);
                    if (fivek3games < 3)
                    {
                        fivek3games += 1;
                        PlayerPrefs.SetInt("5k3games", fivek3games);
                    }
                    else if (fivek3games >= 3)
                    {
                        if (!previousover5000)
                        {
                            fivek3games = 1;
                            PlayerPrefs.SetInt("5k3games", fivek3games);
                        }
                        else if (previousover5000)
                        {
                            fivek3games += 1;
                            PlayerPrefs.SetInt("5k3games", fivek3games);
                        }
                    }
                }
            }
            else if (score < 3000)
            {
                if (threek3games < 3)
                {
                    threek3games = 0;
                    PlayerPrefs.SetInt("3k3games", threek3games);
                }
                else if (threek3games >= 3)
                {
                    previousover3000 = false;
                    PlayerPrefs.SetInt("previousgot3000", 0);
                }
            }
            else if (score < 5000)
            {
                if (fivek3games < 3)
                {
                    fivek3games = 0;
                    PlayerPrefs.SetInt("5k3games", fivek3games);
                }
                else if (fivek3games >= 3)
                {
                    previousover5000 = false;
                    PlayerPrefs.SetInt("previousgot5000", 0);
                }
            }
        }
    }

    void CreateXPShield()
    {
        XPfunctioncalled += 1;
        if (XPfunctioncalled < Random.Range(7, 14))
        {
            XPshieldsprite.SetActive(true);
            Instantiate(XPshieldsprite, new Vector2(Random.Range(200, 250), 450), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
        }
        if (XPshieldspritetouched)
        {
            restartbutton.enabled = true;
            backtomenubutton.enabled = true;
            int currentXP = Mathf.RoundToInt(PlayerPrefs.GetFloat("currentxp", 0));
            int requiredXP = PlayerPrefs.GetInt("level", 1) * 300;
            xplefttolevelup = requiredXP - currentXP;
            int addhowmuchXP = score / 10;
            if (addhowmuchXP > xplefttolevelup)//going to level up, xp exceeds amount needed
            {
                AddXPhowmanytimes = 0;
                InvokeRepeating("AddXPuntillevelup", 0f, 0.1f);//AddXPuntillevelup() will call AddXPuntillevelup() after it finished
                XPafterlevelup = addhowmuchXP - xplefttolevelup;
                requiredXP = PlayerPrefs.GetInt("level", 1) * 300;
                if (XPafterlevelup > requiredXP)//level up 2nd time
                {
                    AddXPhowmanytimes = 0;
                    xplefttolevelup = requiredXP;
                    InvokeRepeating("AddXPuntillevelup", 0f, 0.1f);//AddXPuntillevelup() will call AddXPuntillevelup() after it finished
                    XPafterlevelup = addhowmuchXP - xplefttolevelup;
                }
            }
            else if (addhowmuchXP == xplefttolevelup)//going to level up, xp equals amount needed
            {
                AddXPhowmanytimes = 0;
                InvokeRepeating("AddXP", 0f, 0.1f);
                XPingame.XPgoingtozero = true;
            }
            else if (addhowmuchXP < xplefttolevelup)//not going to level up
            {
                AddXPhowmanytimes = 0;
                InvokeRepeating("AddXP", 0f, 0.1f);
            }
            XPshieldspritetouched = false;
        }
    }
    void AddXPuntillevelup()
    {
        AddXPhowmanytimes += 1;
        AddingXP(xplefttolevelup);
        if (AddXPhowmanytimes >= 10)
        {
            XP = 0;
            PlayerPrefs.SetFloat("currentxp", XP);
            canlevelup = true;
            CancelInvoke();
            AddXPhowmanytimes = 0;
            InvokeRepeating("AddXPafterlevelup", 1f, 0.1f);
        }
    }
    void AddXPafterlevelup()
    {
        AddXPhowmanytimes += 1;
        AddingXP(XPafterlevelup);
        if (AddXPhowmanytimes >= 10)
        {
            XP = Mathf.RoundToInt(XP);
            PlayerPrefs.SetFloat("currentxp", XP);
            CancelInvoke();
            AddXPhowmanytimes = 0;
        }
    }
    void AddXP()
    {
        AddXPhowmanytimes += 1;
        AddingXP(score / 10);
        if (AddXPhowmanytimes >= 10)
        {
            XP = Mathf.RoundToInt(XP);
            PlayerPrefs.SetFloat("currentxp", XP);
            CancelInvoke();
            AddXPhowmanytimes = 0;
        }
    }
    void AddingXP(float S)
    {
        float xptoadd = S / 10;
        XP += xptoadd;
        PlayerPrefs.SetFloat("currentxp", XP);
    }
    public void backtomenu()
    {
        Time.timeScale = 1;
        timesclicked += 1;
        PlayerPrefs.SetInt("timeclick", timesclicked);

        if (PlayerPrefs.GetInt("timeclick") >= 2 && PlayerPrefs.GetFloat("adinter") >= 15)
        {
            timesclicked = 0;
            PlayerPrefs.SetInt("timeclick", timesclicked);
            adinterval = 0;
            PlayerPrefs.SetFloat("adinter", adinterval);
        }
       LoadingScreen.instance.Show(SceneManager.LoadSceneAsync("Menu"));
    }
    public void restartclicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//Application.LoadLevel(Application.loadedlevel);
    }
    public void pauseplay()
    {
        //going to pause
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pausebutton.gameObject.SetActive(false);
            settingbox.gameObject.SetActive(true);
        }
        //going to play
        else
        {
            Time.timeScale = 1;
            pausebutton.gameObject.SetActive(true);
            settingbox.gameObject.SetActive(false);
        }
    }
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (gameOver)
            {
                break;
            }
        }
    }
    public void AddScore(int newScoreValue)
    {
        if (!gameOver)
        {
            score += newScoreValue;
            UpdateScore();
        }
    }
    void UpdateScore()
    {
        scoreText.text = "Score:" + score.ToString();
        bestScoreText.text = "Best Score:" + bestScore.ToString();
    }
    public void GameOver()
    {
        timesplayed += 1;
        PlayerPrefs.SetInt("timeplay", timesplayed);
        if (PlayerPrefs.GetInt("timeplay") >= 2 && PlayerPrefs.GetFloat("adinter") >= 50)
        {
            timesplayed = 0;
            PlayerPrefs.SetInt("timeplay", timesplayed);
            adinterval = 0;
            PlayerPrefs.SetFloat("adinter", adinterval);           
        }
        bestScoreText.text = "Best score:" + bestScore.ToString();
        GameOverText.text = "Game Over";
        gameOver = true;
    }
}
