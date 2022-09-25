using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject instructiongameobjects;// playerplane, enemyplane, playerlaser, playerlaser2, enemylaser, asteroid, rocket;
    public GameObject panel;
    public GameObject plane;
    public GameObject gamename;
    public GameObject playbutton, controlsbutton, shopbutton, creditsbutton, backbutton, acheivementbutton, leaderboardbutton;
    public GameObject simplemathinfo, hardmathinfo, fractioninfo, LCMinfo;
    public GameObject lock1, lock2, lock3;
    public GameObject instructionstext, creditstext, coinbox, XPbox;
    public GameObject mainmenu, shop, selectmodecanvas, selectplanecanvas, congratulations, achievementcanvas, usernamecanvas, leaderboardcanvas, controlscanvas;
    public Button tilttomovebutton, joysticktomovebutton;
    public RectTransform greentick;
    public GameObject hardmathbox, fractionbox, lcmbox; //are you sure box
    public GameObject hardplay, fractionplay, lcmplay, unlockhard, unlockfraction, unlocklcm ; //play buttons and unlock buttons
    public GameObject sorrynomoney, successbuy;
    public GameObject planecontainer;
    public GameObject leaderboardcontroller;
    public Text usernametext;
    private int hardmath, fraction, lcm;
    public static int planeNumber;
    public static int gamecontrollerNumber;
    private int gold;
    public Text goldtext1, goldtext2, goldtext3;
    private bool firsttimeplaying = true;
    private string username, playerID;
    public InputField usernameinput;
    public Text errortext;
    private int whichcontrol;
    private string gameId = "3278241";
    bool testmode = true;
    // Start is called before the first frame update
    void Start()
    {
        Show();
        planeNumber = 1;
        gamecontrollerNumber = 1;
        whichcontrol = PlayerPrefs.GetInt("Control", 2);
        /*PlayerPrefs.SetFloat("currentxp", 280);
        PlayerPrefs.SetInt("level", 1);*/
        firsttimeplaying = (PlayerPrefs.GetInt("firstplay", 1) == 1) ? true : false;
        username = PlayerPrefs.GetString("name", "");
        //put this in first time play
        playerID = PlayerPrefs.GetString("userid", "");
        print(playerID);
        if (firsttimeplaying)
        {
            usernamecanvas.SetActive(true);
            errortext.gameObject.SetActive(false);
            int playerIDlength = Random.Range(15, 31);
            char[] characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890".ToCharArray();//store all the letters and numbers
            for (int i = 0; i <= playerIDlength; i++)
            {
                int index = Random.Range(0, characters.Length);
                playerID += characters[index];
            }
            print(playerID);
            PlayerPrefs.SetString("userid", playerID);
            PlayerPrefs.SetInt("Gold", 1000);
        }
        else if (!firsttimeplaying)
        {
            usernamecanvas.SetActive(false);
            Show();
        }
    }
    public void Submitusername()
    {
        username = usernameinput.gameObject.transform.Find("Text").GetComponent<Text>().text;
        Debug.Log(username);
        firsttimeplaying = false;
        PlayerPrefs.SetInt("firstplay", 0);
        char[] textholder = username.ToCharArray();
        for (int i = 0; i < textholder.Length; i++)
        {
            if (textholder[i] == ' ' || textholder[i] == '|' || textholder[i] == '*' || textholder[i] == '♣')
            {
                errortext.gameObject.SetActive(true);
                return;
            }
        }
        if (username == "")//empty username
        {
            errortext.gameObject.SetActive(true);
            return;
        }
        else
        {
            PlayerPrefs.SetString("name", username);
            usernamecanvas.SetActive(false);
            Show();
        }
    }
    // Update is called once per frame
    void Update()
    {
        gold = PlayerPrefs.GetInt("Gold", 1000);
        usernametext.text = username;
        if (gold < 0)
        {
            gold = 0;
            PlayerPrefs.SetInt("Gold", gold);
        }
        goldtext1.text = gold.ToString();
        goldtext2.text = gold.ToString();
        goldtext3.text = gold.ToString();
        hardmath = PlayerPrefs.GetInt("hardmaths", 0);
        fraction = PlayerPrefs.GetInt("fractions", 0);
        lcm = PlayerPrefs.GetInt("hcf", 0);
        if (selectmodecanvas.activeSelf && hardmath == 0)
        {
            unlockhard.SetActive(true);
            hardplay.SetActive(false);
            lock1.SetActive(true);
        }
        else if (selectmodecanvas.activeSelf && hardmath == 1)
        {
            unlockhard.SetActive(false);
            hardplay.SetActive(true);
            lock1.SetActive(false);
        }
        if (selectmodecanvas.activeSelf && fraction == 0)
        {
            unlockfraction.SetActive(true);
            fractionplay.SetActive(false);
            lock2.SetActive(true);
        }
        else if (selectmodecanvas.activeSelf && fraction == 1)
        {
            unlockfraction.SetActive(false);
            fractionplay.SetActive(true);
            lock2.SetActive(false);
        }
        if (selectmodecanvas.activeSelf && lcm == 0)
        {
            unlocklcm.SetActive(true);
            lcmplay.SetActive(false);
            lock3.SetActive(true);
        }
        else if (selectmodecanvas.activeSelf && lcm == 1)
        {
            unlocklcm.SetActive(false);
            lcmplay.SetActive(true);
            lock3.SetActive(false);
        }
        if (controlscanvas.activeSelf)
        {
            whichcontrol = PlayerPrefs.GetInt("Control", 2);
            if (whichcontrol == 1)
            {
                tilttomovebutton.Select();
                tilttomovebutton.gameObject.GetComponent<Image>().color = new Color32(45, 255, 0, 255);//light green
                joysticktomovebutton.gameObject.GetComponent<Image>().color = new Color32(255, 160, 0, 255);//light orange
                greentick.anchoredPosition = new Vector2(-180, 10);
            }
            if (whichcontrol == 2)
            {
                joysticktomovebutton.Select();
                joysticktomovebutton.gameObject.GetComponent<Image>().color = new Color32(45, 255, 0, 255);//light green
                tilttomovebutton.gameObject.GetComponent<Image>().color = new Color32(255, 160, 0, 255);//light orange
                greentick.anchoredPosition = new Vector2(-180, -300);
            }
        }
    }
    public void OpenAchievement()
    {
        achievementcanvas.SetActive(true);
    }
    public void OpenLeaderboard()
    {
        leaderboardcanvas.SetActive(true);
        leaderboardcontroller.GetComponent<Leaderboards>().CreateHighScoreLeaderboard();
    }
    public void OpenControls()
    {
        controlscanvas.SetActive(true);
    }
    public void ChooseTiltControl()
    {
        PlayerPrefs.SetInt("Control", 1);
        whichcontrol = PlayerPrefs.GetInt("Control");
    }
    public void ChooseJoystickControl()
    {
        PlayerPrefs.SetInt("Control", 2);
        whichcontrol = PlayerPrefs.GetInt("Control");
    }
    public void Playgame()
    {
        //SceneManager.LoadScene("Main");
        mainmenu.SetActive(false);
        selectmodecanvas.SetActive(true);
        selectplanecanvas.SetActive(false);
    }
    public void Opensimplemathinfo()
    {
        simplemathinfo.SetActive(true);
    }
    public void Openhardmathinfo()
    {
        hardmathinfo.SetActive(true);
    }
    public void Openfractionnfo()
    {
        fractioninfo.SetActive(true);
    }
    public void OpenLCMinfo()
    {
        LCMinfo.SetActive(true);
    }
    public void Closesimplemathinfo()
    {
        simplemathinfo.SetActive(false);
    }
    public void Closehardmathinfo()
    {
        hardmathinfo.SetActive(false);
    }
    public void Closefractioninfo()
    {
        fractioninfo.SetActive(false);
    }
    public void CloseLCMinfo()
    {
        LCMinfo.SetActive(false);
    }
    public void UnlockHardMath()
    {
        if (!hardmathbox.activeSelf)
            hardmathbox.SetActive(true);
        else if (hardmathbox.activeSelf)
            hardmathbox.SetActive(false);
    }
    public void UnlockFraction()
    {
        if (!fractionbox.activeSelf)
            fractionbox.SetActive(true);
        else if (fractionbox.activeSelf)
            fractionbox.SetActive(false);
    }
    public void UnlockLcm()
    {
        if (!lcmbox.activeSelf)
            lcmbox.SetActive(true);
        else if (lcmbox.activeSelf)
            lcmbox.SetActive(false);
    }
    public void ConfirmUnlockHardMath()
    {
        hardmathbox.SetActive(false);
        if (gold >= 3000)
        {
            successbuy.SetActive(true);
            gold -= 3000;
            PlayerPrefs.SetInt("Gold", gold);
            PlayerPrefs.SetInt("hardmaths", 1);
        }
        else if (gold < 3000)
        {
            sorrynomoney.SetActive(true);
        }
    }
    public void ConfirmUnlockFraction()
    {
        fractionbox.SetActive(false);
        if (gold >= 5000)
        {
            successbuy.SetActive(true);
            gold -= 5000;
            PlayerPrefs.SetInt("Gold", gold);
            PlayerPrefs.SetInt("fractions", 1);
        }
        else if (gold < 5000)
        {
            sorrynomoney.SetActive(true);
        }
    }
    public void ConfirmUnlockLcm()
    {
        lcmbox.SetActive(false);
        if (gold >= 5000)
        {
            successbuy.SetActive(true);
            gold -= 5000;
            PlayerPrefs.SetInt("Gold", gold);
            PlayerPrefs.SetInt("hcf", 1);
        }
        else if (gold < 5000)
        {
            sorrynomoney.SetActive(true);
        }
    }
    public void DismissNoMoney()
    {
        sorrynomoney.SetActive(false);
    }
    public void DismissSuccessBuy()
    {
        successbuy.SetActive(false);
    }
    public void PlaySimpleMath()
    {
        gamecontrollerNumber = 1;
    }
    public void PlayHardMath()
    {
        if (hardmath == 1)
            gamecontrollerNumber = 2;
    }
    public void PlayFraction()
    {
        if (fraction == 1)
            gamecontrollerNumber = 3;
    }
    public void PlayLcm()
    {
        if (lcm == 1)
            gamecontrollerNumber = 4;
    }

    public void selectplane()
    {
        selectmodecanvas.SetActive(false);
        selectplanecanvas.SetActive(true);
        planecontainer.SetActive(true);
    }
    public void LoadGame()
    {
        planeNumber = gameObject.GetComponent<Shop>().selectedplanenumber;
        LoadingScreen.instance.Show(SceneManager.LoadSceneAsync("Main"));
    }
    /*public void Instructions()
    {
        Hide();
        backbutton.transform.localScale = new Vector3(0.7f, 0.7f, 1);
        backbutton.GetComponent<RectTransform>().anchoredPosition = new Vector2(150, 70);
        panel.SetActive(false);
        instructionstext.SetActive(true);
        instructiongameobjects.SetActive(true);
    }*/
    public void Shop()
    {
        mainmenu.SetActive(false);
        shop.SetActive(true);
        planecontainer.SetActive(true);
    }
    public void Credit()
    {
        Hide();
        creditstext.SetActive(true);
    }
    public void Back()
    {
        Show();
    }
    public void Backtomainmenu()
    {
        mainmenu.SetActive(true);
        if (selectmodecanvas.activeSelf)
            selectmodecanvas.SetActive(false);
        if (shop.activeSelf)
            shop.SetActive(false);
    }
    void Show()
    {
        backbutton.transform.localScale = new Vector3(1, 1, 1);
        backbutton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 110);
        playbutton.SetActive(true);
        controlsbutton.SetActive(true);
        shopbutton.SetActive(true);
        creditsbutton.SetActive(true);
        acheivementbutton.SetActive(true);
        leaderboardbutton.SetActive(true);
        plane.SetActive(true);
        gamename.SetActive(true);
        panel.SetActive(true);
        instructionstext.SetActive(false);
        creditstext.SetActive(false);
        backbutton.SetActive(false);
        instructiongameobjects.SetActive(false);
        coinbox.SetActive(true);
        mainmenu.SetActive(true);
        shop.SetActive(false);
        selectmodecanvas.SetActive(false);
        selectplanecanvas.SetActive(false);
        congratulations.SetActive(false);
        planecontainer.SetActive(false);
        achievementcanvas.SetActive(false);
        leaderboardcanvas.SetActive(false);
        controlscanvas.SetActive(false);
        XPbox.SetActive(true);
        usernametext.gameObject.SetActive(true);
    }
    void Hide()
    {
        playbutton.SetActive(false);
        controlsbutton.SetActive(false);
        shopbutton.SetActive(false);
        creditsbutton.SetActive(false);
        acheivementbutton.SetActive(false);
        leaderboardbutton.SetActive(false);
        plane.SetActive(false);
        gamename.SetActive(false);
        backbutton.SetActive(true);
        coinbox.SetActive(false);
        planecontainer.SetActive(false);
        XPbox.SetActive(false);
        usernametext.gameObject.SetActive(false);
    }
    public void Addgold()
    {
        gold += 1000;
        PlayerPrefs.SetInt("Gold", gold);
    }
    public void Resetgold()
    {
        gold = 2000;
        PlayerPrefs.SetInt("Gold", gold);
    }
}
