using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class XPleaderboard : MonoBehaviour
{
    const string privateCode = "7eQX3NAicECCzebqd32iDQ97bQJpCTRkum7TuZyD1O4A";
    const string publicCode = "5e75dc26fe232612b8c88c3d";
    const string webUrl = "http://dreamlo.com/lb/";
    public LevelandXP[] levelandXPlist = new LevelandXP[1000];
    public GameObject XPbox;//the prefab to instantiate
    public RectTransform XPboxreflocation;
    public Sprite Greenbox, Yellowbox;
    public Text loadingtext;
    public Scrollbar leaderboardscrollbar;
    public RectTransform leaderboardcontent;
    public Button XPlbbutton;
    public GameObject HighScoreLB, LevelandXPLB, HighScoreLBcontent, LevelandXPLBcontent;
    private UnityWebRequest www1, www2;
    private int Formattedhowmanytimes;
    private string playerusername, playerID;
    private int playerlevel;
    private float playercurrentXP;
    private int requiredXP;
    private int numberofentries;
    private string rndname, rndID;
    private int yay = 0;
    private char[] characters;
    void Start()
    {
        playerusername = PlayerPrefs.GetString("name", "");
        playerID = PlayerPrefs.GetString("userid", "");
        playerlevel = PlayerPrefs.GetInt("level", 1);
        playercurrentXP = PlayerPrefs.GetFloat("currentxp", 0);
        requiredXP = playerlevel * 300;
        Invoke("Startingposition", 0.05f);
        characters = "1234567890wreyjxbafqdnmigouh".ToCharArray();
    }
    private void Update()
    {
        //print(leaderboardcontent.position.y + " , " + leaderboardcontent.anchoredPosition.y);
        if (leaderboardcontent.anchoredPosition.x != 0)
            leaderboardcontent.anchoredPosition = new Vector2(0, leaderboardcontent.anchoredPosition.y);
        if (leaderboardcontent.anchoredPosition.y < 268)
            leaderboardcontent.anchoredPosition = new Vector2(leaderboardcontent.anchoredPosition.x, 268);
        if (numberofentries <= 6)//don't move
        {
            leaderboardscrollbar.size = 1;
            if (leaderboardcontent.anchoredPosition.y > 275)
                leaderboardcontent.anchoredPosition = new Vector2(leaderboardcontent.anchoredPosition.x, 275);

        }
        else if (numberofentries > 6 && numberofentries < 1000)
        {
            float A = numberofentries;
            if (numberofentries < 50)
                leaderboardscrollbar.size = 1 - (A * 10) / 1000;
            else if (numberofentries >= 50 && numberofentries < 100)
                leaderboardscrollbar.size = 1 - (A * 5) / 1000;
            else if (numberofentries >= 100 && numberofentries < 500)
                leaderboardscrollbar.size = 1 - (A * 2) / 1000;
            else if (numberofentries >= 500)
                leaderboardscrollbar.size = 1 - A / 1000;
            int lengthofcontent = numberofentries * 100 + (numberofentries - 1) * 15;
            if (leaderboardcontent.anchoredPosition.y > lengthofcontent)
                leaderboardcontent.anchoredPosition = new Vector2(leaderboardcontent.anchoredPosition.x, lengthofcontent);
            leaderboardscrollbar.value = (leaderboardcontent.anchoredPosition.y - 268) / lengthofcontent;
        }
        else if (numberofentries == 1000)
        {
            leaderboardscrollbar.size = 0.001f;
            int lengthofcontent = numberofentries * 100 + (numberofentries - 1) * 15;
            if (leaderboardcontent.anchoredPosition.y > lengthofcontent)
                leaderboardcontent.anchoredPosition = new Vector2(leaderboardcontent.anchoredPosition.x, lengthofcontent);
            leaderboardscrollbar.value = (leaderboardcontent.anchoredPosition.y - 268) / lengthofcontent;
        }
        if (LevelandXPLB.activeSelf)
        {
            Color32 lightblue = new Color32(0, 199, 255, 255);
            XPlbbutton.gameObject.GetComponent<Image>().color = lightblue;
        }
        else
        {
            Color32 white = new Color32(255, 255, 255, 255);
            XPlbbutton.gameObject.GetComponent<Image>().color = white;
        }
        /*if (yay <= 353)
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
            int rndlevel = Random.Range(1, 100);
            int rndXP = Random.Range(0, (rndlevel - 1) * 300);//using the formula n(n + 1)/2
            AddNewLevelandXP(uploadname1, (((rndlevel - 1) * (rndlevel) / 2) * 300) + rndXP, rndlevel, rndXP);
        }*/
    }
    public void AddNewLevelandXP(string username, int totalXP, int level, int XP)
    {
        StartCoroutine(UploadNewLevelandXP(username, totalXP, level, XP));
    }

    IEnumerator UploadNewLevelandXP(string usernameandID, int totalXP, int level, int XP)
    {
        www1 = new UnityWebRequest(webUrl + privateCode + "/add/" + UnityWebRequest.EscapeURL(usernameandID) + "/" + totalXP + "/" + level + "/" + XP);
        yield return www1.SendWebRequest();
        if (string.IsNullOrEmpty(www1.error))
            print("Upload Successful");
        else
            loadingtext.text = "Error uploading: " + www1.error;
    }

    public void DownloadLevelandXP()
    {
        StartCoroutine(DownloadLevelandXPFromDatabase());
    }
    IEnumerator DownloadLevelandXPFromDatabase()
    {
        for (int i = 0; i < 10; i++)
        {
            www2 = UnityWebRequest.Get(webUrl + publicCode + "/pipe-dsc/" + (i * 100) + "/100");
            loadingtext.text = "Downloading XP amounts, please wait...(about 7 seconds)";
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
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        numberofentries += entries.Length;
        print(numberofentries);
        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryinfo = entries[i].Split(new char[] { '|' });
            string[] usernameandID = entryinfo[0].Split(new char[] { '♣' });
            int level = int.Parse(entryinfo[2]);
            int XP = int.Parse(entryinfo[3]);
            string username = usernameandID[0];
            string userID = usernameandID[1];
            levelandXPlist[(i + ((Formattedhowmanytimes - 1) * 100))] = new LevelandXP(username, userID, level, XP);
            print(levelandXPlist[(i + ((Formattedhowmanytimes - 1) * 100))].username + " , " + levelandXPlist[(i + ((Formattedhowmanytimes - 1) * 100))].level + " , " + levelandXPlist[(i + ((Formattedhowmanytimes - 1) * 100))].XP);
        }
    }
    public void CreateXPLeaderboard()
    {
        //if still uploading or downloading, display loading text
        playerusername = PlayerPrefs.GetString("name", "");
        playerID = PlayerPrefs.GetString("userid", "");
        playerlevel = PlayerPrefs.GetInt("level", 1);
        foreach (Transform child in LevelandXPLBcontent.transform)
        {
            if (child.name == "XPbox(Clone)")
                Destroy(child.gameObject);
        }
        XPboxreflocation.anchoredPosition = new Vector2(0, -120);
        StartCoroutine(LoadXPLeaderboard());
        XPlbbutton.Select();
        HighScoreLB.SetActive(false);
        LevelandXPLB.SetActive(true);
    }
    IEnumerator LoadXPLeaderboard()
    {
        loadingtext.gameObject.SetActive(true);
        Formattedhowmanytimes = 0;
        numberofentries = 0;
        string uploadname = playerusername + "♣" + playerID;
        int playertotalXP = (int)((((playerlevel - 1) * (playerlevel) / 2) * 300) + playercurrentXP);//using the formula n(n+1)/2
        yield return StartCoroutine(UploadNewLevelandXP(uploadname, playertotalXP, playerlevel, (int)playercurrentXP));
        yield return StartCoroutine(DownloadLevelandXPFromDatabase());
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
            Vector2 desiredlocationvector = new Vector2(XPboxreflocation.anchoredPosition.x, XPboxreflocation.anchoredPosition.y - 115);
            XPboxreflocation.anchoredPosition = desiredlocationvector;
            GameObject clone = Instantiate(XPbox, XPboxreflocation);
            clone.transform.parent = leaderboardcontent;
            clone.transform.Find("numbercircle").GetComponentInChildren<Text>().text = (i + 1).ToString();//pos number
            clone.transform.Find("username").GetComponent<Text>().text = levelandXPlist[i].username;
            clone.transform.Find("Level").GetComponent<Text>().text = (levelandXPlist[i].level).ToString();
            clone.transform.Find("XP").GetComponent<Text>().text = (levelandXPlist[i].XP).ToString();
            if (levelandXPlist[i].username == playerusername || levelandXPlist[i].userID == playerID)
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
        leaderboardcontent.anchoredPosition = new Vector2(leaderboardcontent.anchoredPosition.x, 268);
    }
}
public struct LevelandXP
{
    public string username;
    public string userID;
    public int level;
    public int XP;
    public LevelandXP(string _username, string _userID, int _level, int _XP)
    {
        username = _username;
        userID = _userID;
        level = _level;
        XP = _XP;
    }
}
