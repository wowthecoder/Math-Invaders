using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBarExtra : MonoBehaviour
{
    public RectTransform lcmcontent, fractioncontent;
    public Scrollbar lcmbar, fractionbar;
    // Start is called before the first frame update
    void Start()
    {
        lcmcontent.anchoredPosition = new Vector2(0, -80);
        fractioncontent.anchoredPosition = new Vector2(70, -80);
        Invoke("Startingposition", 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        if (lcmcontent.anchoredPosition.x != -60)
            lcmcontent.anchoredPosition = new Vector2(-60, lcmcontent.anchoredPosition.y);
        if (lcmcontent.anchoredPosition.y < -10)
            lcmcontent.anchoredPosition = new Vector2(lcmcontent.anchoredPosition.x, -10);
        if (lcmcontent.anchoredPosition.y > 1750)
            lcmcontent.anchoredPosition = new Vector2(lcmcontent.anchoredPosition.x, 1750);
        if (fractioncontent.anchoredPosition.x != 70)
            fractioncontent.anchoredPosition = new Vector2(70, fractioncontent.anchoredPosition.y);
        if (fractioncontent.anchoredPosition.y < -80)
            fractioncontent.anchoredPosition = new Vector2(fractioncontent.anchoredPosition.x, -80);
        if (fractioncontent.anchoredPosition.y > 2000)
            fractioncontent.anchoredPosition = new Vector2(fractioncontent.anchoredPosition.x, 2000);
        fractionbar.value = (fractioncontent.anchoredPosition.y + 90)/ 1830;
        lcmbar.value = (lcmcontent.anchoredPosition.y + 10) / 1580;
    }
    void Startingposition()
    {
        lcmcontent.anchoredPosition = new Vector2(-60, -10);
        fractioncontent.anchoredPosition = new Vector2(70, -80);
    }
}
