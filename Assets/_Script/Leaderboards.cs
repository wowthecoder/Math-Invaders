using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Leaderboards : MonoBehaviour
{
    const string privateCode = "gEog_AcnU0KENiE4vhW_uAaw7aIk7vI0irGyUr5xjMHg";
    const string publicCode = "5e71ae97fe232612b8ba6df0";
    const string webUrl = "http://dreamlo.com/lb/";
    public Highscore[] highScoresList = new Highscore[1000];
    public GameObject scorebox;//the prefab to instantiate
    public RectTransform scoreboxreflocation;
    public Sprite Greenbox, Yellowbox;
    public Text loadingtext;
    public Scrollbar leaderboardscrollbar;
    public RectTransform leaderboardcontent;
    public Button Highscorelbbutton;
    public GameObject HighScoreLB, LevelandXPLB, HighScoreLBcontent, LevelandXPLBcontent;
    private UnityWebRequest www1, www2;
    private int Formattedhowmanytimes;
    private string playerusername, playerID;
    private int playerhighscore;
    private int numberofentries;
    private string rndname, rndID;
    private int yay = 0;
    private char[] characters;
    void Start()
    {
        playerusername = PlayerPrefs.GetString("name", "");
        playerID = PlayerPrefs.GetString("userid", "");
        playerhighscore = PlayerPrefs.GetInt("highScore", 0);
        Invoke("Startingposition", 0.05f);
        characters = "123456wreyjxbafqdnmigou".ToCharArray();
    }
    private void Update()
    {
        //print(leaderboardcontent.position.y + " , " + leaderboardcontent.anchoredPosition.y);
        if (leaderboardcontent.anchoredPosition.x != 0)
            leaderboardcontent.anchoredPosition = new Vector2(0, leaderboardcontent.anchoredPosition.y);
        if (leaderboardcontent.anchoredPosition.y < 305)
            leaderboardcontent.anchoredPosition = new Vector2(leaderboardcontent.anchoredPosition.x, 305);
        if (numberofentries <= 6)//don't move
        {
            leaderboardscrollbar.size = 1;
            if (leaderboardcontent.anchoredPosition.y > 310)
                leaderboardcontent.anchoredPosition = new Vector2(leaderboardcontent.anchoredPosition.x, 310);

        }
        else if (numberofentries > 6 && numberofentries < 1000)
        {
            float A = numberofentries;
            if (numberofentries < 50)
                leaderboardscrollbar.size = 1 - (A * 10) / 1000;
            else if (numberofentries >= 50 && numberofentries < 100)
                leaderboardscrollbar.size = 1 - (A * 5)/ 1000;
            else if (numberofentries >= 100 && numberofentries < 500)
                leaderboardscrollbar.size = 1 - (A * 2) / 1000;
            else if (numberofentries >= 500)
                leaderboardscrollbar.size = 1 - A / 1000;
            int lengthofcontent = numberofentries * 100 + (numberofentries - 1) * 15;
            if (leaderboardcontent.anchoredPosition.y > lengthofcontent)
                leaderboardcontent.anchoredPosition = new Vector2(leaderboardcontent.anchoredPosition.x, lengthofcontent);
            leaderboardscrollbar.value = (leaderboardcontent.anchoredPosition.y - 305) / lengthofcontent;
        }
        else if (numberofentries == 1000)
        {
            leaderboardscrollbar.size = 0.001f;
            int lengthofcontent = numberofentries * 100 + (numberofentries - 1) * 15;
            if (leaderboardcontent.anchoredPosition.y > lengthofcontent)
                leaderboardcontent.anchoredPosition = new Vector2(leaderboardcontent.anchoredPosition.x, lengthofcontent);
            leaderboardscrollbar.value = (leaderboardcontent.anchoredPosition.y - 305) / lengthofcontent;
        }
        if (HighScoreLB.activeSelf)
        {
            Color32 lightblue = new Color32(0, 199, 255, 255);
            Highscorelbbutton.gameObject.GetComponent<Image>().color = lightblue;
        }
        else
        {
            Color32 white = new Color32(255, 255, 255, 255);
            Highscorelbbutton.gameObject.GetComponent<Image>().color = white;
        }
        /*if (yay <= 753)
        {
            yay += 1;
            rndname = "";
            rndID = "";
            for (int i = 0; i <= 10; i++)
            {
                int index = Random.Range(0, characters.Length);
                rndname += characters[index];
            }
            for (int i = 0; i <= 20; i++)
            {
                int index = Random.Range(0, characters.Length);
                rndID += characters[index];
            }
            string uploadname1 = rndname + "♣" + rndID;
            AddNewHighScore(uploadname1, Random.Range(1, 10000));
        }*/
    }
    public void AddNewHighScore(string username, int score)
    {
        StartCoroutine(UploadNewHighScore(username, score));
    }

    IEnumerator UploadNewHighScore(string usernameandID,  int score)
    {
        www1 = new UnityWebRequest(webUrl + privateCode + "/add/" + UnityWebRequest.EscapeURL(usernameandID) + "/" + score);
        yield return www1.SendWebRequest();
        if (string.IsNullOrEmpty(www1.error))
            print("Upload Successful");
        else
            loadingtext.text = "Error uploading: " + www1.error;
    }

    public void DownloadHighScores()
    {
        StartCoroutine(DownloadHighScoresFromDatabase());
    }
    IEnumerator DownloadHighScoresFromDatabase()
    {
        for (int i = 0; i < 10; i++)
        {
            www2 = UnityWebRequest.Get(webUrl + publicCode + "/pipe-dsc/" + (i * 100) + "/100");
            loadingtext.text = "Downloading scores, please wait...(about 7 seconds)";
            yield return www2.SendWebRequest();
            if (string.IsNullOrEmpty(www2.error))
                FormatHighScores(www2.downloadHandler.text);
            else
                loadingtext.text = "Error downloading: " + www2.error;
        }
    }
    void FormatHighScores(string textStream)
    {
        Formattedhowmanytimes += 1;
        string[] entries = textStream.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
        numberofentries += entries.Length;
        print(numberofentries);
        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryinfo = entries[i].Split(new char[] {'|'});
            string[] usernameandID = entryinfo[0].Split(new char[] {'♣'});
            int score = int.Parse(entryinfo[1]);
            string username = usernameandID[0];
            string userID = usernameandID[1];
            highScoresList[(i + ((Formattedhowmanytimes - 1) * 100))] = new Highscore(username, userID, score);
            print(highScoresList[(i + ((Formattedhowmanytimes - 1) * 100))].username + " , " + highScoresList[(i + ((Formattedhowmanytimes - 1) * 100))].score);
        }
    }
    public void CreateHighScoreLeaderboard()
    {
        //if still uploading or downloading, display loading text
        playerusername = PlayerPrefs.GetString("name", "");
        playerID = PlayerPrefs.GetString("userid", "");
        playerhighscore = PlayerPrefs.GetInt("highScore", 0);
        foreach (Transform child in HighScoreLBcontent.transform)
        {
            if (child.name == "Scorebox(Clone)")
                Destroy(child.gameObject);
        }
        scoreboxreflocation.anchoredPosition = new Vector2(0, -120);
        StartCoroutine(LoadHighScoreLeaderboard());
        Highscorelbbutton.Select();
        HighScoreLB.SetActive(true);
        LevelandXPLB.SetActive(false);
    }
    IEnumerator LoadHighScoreLeaderboard()
    {
        loadingtext.gameObject.SetActive(true);
        Formattedhowmanytimes = 0;
        numberofentries = 0;
        string uploadname = playerusername + "♣" + playerID;
        yield return StartCoroutine(UploadNewHighScore(uploadname, playerhighscore));
        yield return StartCoroutine(DownloadHighScoresFromDatabase());
        if (string.IsNullOrEmpty(www1.error) || string.IsNullOrEmpty(www2.error))//if there is no error uploading and downloading
        {
            loadingtext.gameObject.SetActive(false);
        }
        else
        {
            loadingtext.gameObject.SetActive(true);
        }
        for (int i = 0; i < numberofentries; i++)
        {
            Vector2 desiredlocationvector = new Vector2(scoreboxreflocation.anchoredPosition.x, scoreboxreflocation.anchoredPosition.y - 115);
            scoreboxreflocation.anchoredPosition = desiredlocationvector;
            GameObject clone = Instantiate(scorebox, scoreboxreflocation);
            clone.transform.parent = leaderboardcontent;
            clone.transform.Find("numbercircle").GetComponentInChildren<Text>().text = (i + 1).ToString();//pos number
            clone.transform.Find("username").GetComponent<Text>().text = highScoresList[i].username;
            clone.transform.Find("score").GetComponent<Text>().text = (highScoresList[i].score).ToString();
            if (highScoresList[i].username == playerusername || highScoresList[i].userID == playerID)
            {
                clone.GetComponent<Image>().sprite = Yellowbox;
            }
            else
            {
                clone.GetComponent<Image>().sprite = Greenbox;
            }
        }
    }
    void Startingposition()
    {
        leaderboardcontent.anchoredPosition = new Vector2(leaderboardcontent.anchoredPosition.x, 305);
    }
}

public struct Highscore
{
    public string username;
    public string userID;
    public int score;
    public Highscore(string _username, string _userID, int _score)
    {
        username = _username;
        userID = _userID;
        score = _score;
    }
}

