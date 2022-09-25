using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPingame : MonoBehaviour
{
    public Text xptext, leveltext; // for XP display in main menu
    public Slider XPslider;
    public GameObject levelupcanvas, XPshield, XPbar;
    public Text currentleveltext, nextleveltext, requiredXPtext; // for levelupcanvas
    private int currentlevel, requiredXP;
    private float currentXP;
    private float repeatedhowmanytimes;
    public static bool XPgoingtozero;
    // Start is called before the first frame update
    void Start()
    {
        xptext.gameObject.SetActive(false);
        leveltext.gameObject.SetActive(false);
        XPslider.gameObject.SetActive(false);
        levelupcanvas.SetActive(false);
        XPshield.SetActive(false);
        XPbar.SetActive(false);
        currentXP = PlayerPrefs.GetFloat("currentxp", 0);
        currentlevel = PlayerPrefs.GetInt("level", 1);
        requiredXP = currentlevel * 300;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gameOver)
        {
            xptext.gameObject.SetActive(true);
            leveltext.gameObject.SetActive(true);
            XPslider.gameObject.SetActive(true);
            currentXP = PlayerPrefs.GetFloat("currentxp", 0);
            currentlevel = PlayerPrefs.GetInt("level", 1);
            requiredXP = currentlevel * 300;
            xptext.text = Mathf.RoundToInt(currentXP) + "/" + requiredXP;
            leveltext.text = currentlevel.ToString();
            float B = requiredXP;
            XPslider.value = (currentXP / B) * 100;
            if (currentXP >= requiredXP || GameController.canlevelup)
            {
                levelupcanvas.SetActive(true);
                currentleveltext.text = currentlevel.ToString();
                nextleveltext.text = (currentlevel + 1).ToString();
                currentXP = 0;
                XPslider.value = 0;
                currentlevel += 1;
                requiredXP = currentlevel * 300;
                requiredXPtext.text = requiredXP.ToString();
                PlayerPrefs.SetFloat("currentxp", currentXP);
                PlayerPrefs.SetInt("level", currentlevel);
                GameController.canlevelup = false;
                StartCoroutine(settozero());
                if (XPgoingtozero)
                    InvokeRepeating("repeattillzero", 0.1f, 0.3f);
            }
        }
        if (levelupcanvas.activeSelf)
        {
            if ((Input.touchCount > 0 && Input.GetTouch(0).position.y <= 780 && Input.GetTouch(0).position.y >= 10 && Input.GetTouch(0).position.x >= 10 && Input.GetTouch(0).position.x <= 380) || (Input.GetMouseButton(0) && Input.mousePosition.y <= 780 && Input.mousePosition.y >= 10 && Input.mousePosition.x <= 380 && Input.mousePosition.x >= 10))
            {
                levelupcanvas.SetActive(false);
            }
        }
    }
    void repeattillzero()
    {
        repeatedhowmanytimes += 1;
        currentXP = 0;
        PlayerPrefs.SetFloat("currentxp", currentXP);
        if (repeatedhowmanytimes >= 5)
        {
            repeatedhowmanytimes = 0;
            CancelInvoke();
        }
    }
    IEnumerator settozero()
    {
        yield return new WaitForSeconds(0.3f);
        currentXP = 0;
        PlayerPrefs.SetFloat("currentxp", currentXP);
    }
}
