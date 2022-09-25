using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XP : MonoBehaviour
{
    public Text xptext, leveltext; // for XP display in main menu
    public Slider XPslider;
    private int currentlevel, requiredXP;
    private float currentXP;
    // Start is called before the first frame update
    void Start()
    {
        currentXP = PlayerPrefs.GetFloat("currentxp", 0);
        currentlevel = PlayerPrefs.GetInt("level", 1);
        requiredXP = currentlevel * 300;
    }

    // Update is called once per frame
    void Update()
    {
        currentXP = Mathf.RoundToInt(PlayerPrefs.GetFloat("currentxp", 0));
        currentlevel = PlayerPrefs.GetInt("level", 1);
        requiredXP = currentlevel * 300;
        xptext.text = currentXP + "/" + requiredXP;
        leveltext.text = currentlevel.ToString();
        XPslider.value = (currentXP / requiredXP) * 100;
    }
}
