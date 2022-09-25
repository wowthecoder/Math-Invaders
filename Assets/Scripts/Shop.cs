using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject planecontainer;
    public GameObject leftbutton, rightbutton, leftbutton2, rightbutton2;
    private int WideRed, Blue, Gold, Black, YwingBlue, White, WeirdGreen, Silver, SlimGreen, YwingRed, Purple;
    public GameObject owned, needtobuy3000, needtobuy5000, buybox, sorrynomoney, congratulations, shopcanvas;
    public Text price, planename, goldminustext;
    public GameObject locked, select, selected, sorrydidntown, shop, selectplanecanvas;
    public int selectedplanenumber = 1;
    private int gold;
    void Start()
    {
        planecontainer.transform.rotation = Quaternion.Euler(0, 0, 0);
        planecontainer.transform.position = new Vector3(0, 1.6f, 20);
        buybox.SetActive(false);
        sorrynomoney.SetActive(false);
        congratulations.SetActive(false);
    }

    public void turnright()
    {
        planecontainer.transform.Rotate(0, 15, 0, Space.Self);            
    }

    public void turnleft()
    {
        planecontainer.transform.Rotate(0, -15, 0, Space.Self);
    }

   void Update()
    {
        gold = PlayerPrefs.GetInt("Gold", 1000);
        if (planecontainer.transform.eulerAngles.y >= 165)
        {
            rightbutton.SetActive(false);
            rightbutton2.SetActive(false);
            planecontainer.transform.rotation = Quaternion.Euler(0, 165, 0);
        }
        else
        {
            rightbutton.SetActive(true);
            rightbutton2.SetActive(true);
        }
        if (planecontainer.transform.eulerAngles.y <= 0 || planecontainer.transform.eulerAngles.y >= 180)
        {
            leftbutton.SetActive(false);
            leftbutton2.SetActive(false);
            planecontainer.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            leftbutton.SetActive(true);
            leftbutton2.SetActive(true);
        }
        if (planecontainer.transform.eulerAngles.y % 15 != 0)
        {
            float x = planecontainer.transform.eulerAngles.y - (planecontainer.transform.eulerAngles.y % 15);
            planecontainer.transform.rotation = Quaternion.Euler(0, x, 0);
        }
        WideRed = PlayerPrefs.GetInt("widered", 0);
        Blue = PlayerPrefs.GetInt("blue", 0);
        Gold = PlayerPrefs.GetInt("goldplane", 0);
        Black = PlayerPrefs.GetInt("black", 0);
        YwingBlue = PlayerPrefs.GetInt("YBlue", 0);
        White = PlayerPrefs.GetInt("white", 0);
        WeirdGreen = PlayerPrefs.GetInt("Wgreen", 0);
        Silver = PlayerPrefs.GetInt("silver", 0);
        SlimGreen = PlayerPrefs.GetInt("Sgreen", 0);
        YwingRed = PlayerPrefs.GetInt("Yred", 0);
        Purple = PlayerPrefs.GetInt("purple", 0);
        Ownorbuy();
    }
    public void SelectPlane()
    {
        if (planecontainer.transform.eulerAngles.y >= 0 && planecontainer.transform.eulerAngles.y <= 5)
            selectedplanenumber = 1;
        //WideRed
        if (WideRed == 1 && planecontainer.transform.eulerAngles.y >= 15 && planecontainer.transform.eulerAngles.y <= 20)
            selectedplanenumber = 2;
        //blue
        if (Blue == 1 && planecontainer.transform.eulerAngles.y >= 30 && planecontainer.transform.eulerAngles.y <= 35)
            selectedplanenumber = 3;
        //gold
        if (Gold == 1 && planecontainer.transform.eulerAngles.y >= 45 && planecontainer.transform.eulerAngles.y <= 50)
            selectedplanenumber = 4;
        //black
        if (Black == 1 && planecontainer.transform.eulerAngles.y >= 60 && planecontainer.transform.eulerAngles.y <= 65)
            selectedplanenumber = 5;
        //YwingBlue
        if (YwingBlue == 1 && planecontainer.transform.eulerAngles.y >= 75 && planecontainer.transform.eulerAngles.y <= 80)
            selectedplanenumber = 6;
        //white
        if (White == 1 && planecontainer.transform.eulerAngles.y >= 90 && planecontainer.transform.eulerAngles.y <= 95)
            selectedplanenumber = 7;
        //WeirdGreen
        if (WeirdGreen == 1 && planecontainer.transform.eulerAngles.y >= 105 && planecontainer.transform.eulerAngles.y <= 110)
            selectedplanenumber = 8;
        //silver
        if (Silver == 1 && planecontainer.transform.eulerAngles.y >= 120 && planecontainer.transform.eulerAngles.y <= 125)
            selectedplanenumber = 9;
        //slimgreen
        if (SlimGreen == 1 && planecontainer.transform.eulerAngles.y >= 135 && planecontainer.transform.eulerAngles.y <= 140)
            selectedplanenumber = 10;
        //Ywingred
        if (YwingRed == 1 && planecontainer.transform.eulerAngles.y >= 150 && planecontainer.transform.eulerAngles.y <= 155)
            selectedplanenumber = 11;
        //purple
        if (Purple == 1 && planecontainer.transform.eulerAngles.y >= 165 && planecontainer.transform.eulerAngles.y <= 170)
            selectedplanenumber = 12;
    }
    void Ownorbuy()
    {
        if (planecontainer.transform.eulerAngles.y >= 0 && planecontainer.transform.eulerAngles.y <= 5)
        {
            Owned();
            if (selectedplanenumber == 1)
            {
                select.SetActive(false);
                selected.SetActive(true);
            }
            else if (selectedplanenumber != 1)
            {
                select.SetActive(true);
                selected.SetActive(false);
            }
        }
        //WideRed
        if (WideRed == 0 && planecontainer.transform.eulerAngles.y >= 15 && planecontainer.transform.eulerAngles.y <= 20)
        {
            needtobuy3000.SetActive(true);
            needtobuy5000.SetActive(false);
            owned.SetActive(false);
            locked.SetActive(true);
            select.SetActive(false);
            selected.SetActive(false);
        }
        else if (planecontainer.transform.eulerAngles.y >= 15 && planecontainer.transform.eulerAngles.y <= 20)
        {
            Owned();
            if (selectedplanenumber == 2)
            {
                select.SetActive(false);
                selected.SetActive(true);
            }
            else if (selectedplanenumber != 2)
            {
                select.SetActive(true);
                selected.SetActive(false);
            }
        }
        //blue
        if (Blue == 0 && planecontainer.transform.eulerAngles.y >= 30 && planecontainer.transform.eulerAngles.y <= 35)
        {
            HaventBuy();
        }
        else if (planecontainer.transform.eulerAngles.y >= 30 && planecontainer.transform.eulerAngles.y <= 35)
        {
            Owned();
            if (selectedplanenumber == 3)
            {
                select.SetActive(false);
                selected.SetActive(true);
            }
            else if (selectedplanenumber != 3)
            {
                select.SetActive(true);
                selected.SetActive(false);
            }
        }
        //gold
        if (Gold == 0 && planecontainer.transform.eulerAngles.y >= 45 && planecontainer.transform.eulerAngles.y <= 50)
        {
            HaventBuy();
        }
        else if (planecontainer.transform.eulerAngles.y >= 45 && planecontainer.transform.eulerAngles.y <= 50)
        {
            Owned();
            if (selectedplanenumber == 4)
            {
                select.SetActive(false);
                selected.SetActive(true);
            }
            else if (selectedplanenumber != 4)
            {
                select.SetActive(true);
                selected.SetActive(false);
            }
        }
        //black
        if (Black == 0 && planecontainer.transform.eulerAngles.y >= 60 && planecontainer.transform.eulerAngles.y <= 65)
        {
            HaventBuy();
        }
        else if (planecontainer.transform.eulerAngles.y >= 60 && planecontainer.transform.eulerAngles.y <= 65)
        {
            Owned();
            if (selectedplanenumber == 5)
            {
                select.SetActive(false);
                selected.SetActive(true);
            }
            else if (selectedplanenumber != 5)
            {
                select.SetActive(true);
                selected.SetActive(false);
            }
        }
        //YwingBlue
        if (YwingBlue == 0 && planecontainer.transform.eulerAngles.y >= 75 && planecontainer.transform.eulerAngles.y <= 80)
        {
            HaventBuy();
        }
        else if (planecontainer.transform.eulerAngles.y >= 75 && planecontainer.transform.eulerAngles.y <= 80)
        {
            Owned();
            if (selectedplanenumber == 6)
            {
                select.SetActive(false);
                selected.SetActive(true);
            }
            else if (selectedplanenumber != 6)
            {
                select.SetActive(true);
                selected.SetActive(false);
            }
        }
        //white
        if (White == 0 && planecontainer.transform.eulerAngles.y >= 90 && planecontainer.transform.eulerAngles.y <= 95)
        {
            HaventBuy();
        }
        else if (planecontainer.transform.eulerAngles.y >= 90 && planecontainer.transform.eulerAngles.y <= 95)
        {
            Owned();
            if (selectedplanenumber == 7)
            {
                select.SetActive(false);
                selected.SetActive(true);
            }
            else if (selectedplanenumber != 7)
            {
                select.SetActive(true);
                selected.SetActive(false);
            }
        }
        //WeirdGreen
        if (WeirdGreen == 0 && planecontainer.transform.eulerAngles.y >= 105 && planecontainer.transform.eulerAngles.y <= 110)
        {
            HaventBuy();
        }
        else if (planecontainer.transform.eulerAngles.y >= 105 && planecontainer.transform.eulerAngles.y <= 110)
        {
            Owned();
            if (selectedplanenumber == 8)
            {
                select.SetActive(false);
                selected.SetActive(true);
            }
            else if (selectedplanenumber != 8)
            {
                select.SetActive(true);
                selected.SetActive(false);
            }
        }
        //silver
        if (Silver == 0 && planecontainer.transform.eulerAngles.y >= 120 && planecontainer.transform.eulerAngles.y <= 125)
        {
            HaventBuy();
        }
        else if (planecontainer.transform.eulerAngles.y >= 120 && planecontainer.transform.eulerAngles.y <= 125)
        {
            Owned();
            if (selectedplanenumber == 9)
            {
                select.SetActive(false);
                selected.SetActive(true);
            }
            else if (selectedplanenumber != 9)
            {
                select.SetActive(true);
                selected.SetActive(false);
            }
        }
        //slimgreen
        if (SlimGreen == 0 && planecontainer.transform.eulerAngles.y >= 135 && planecontainer.transform.eulerAngles.y <= 140)
        {
            HaventBuy();
        }
        else if (planecontainer.transform.eulerAngles.y >= 135 && planecontainer.transform.eulerAngles.y <= 140)
        {
            Owned();
            if (selectedplanenumber == 10)
            {
                select.SetActive(false);
                selected.SetActive(true);
            }
            else if (selectedplanenumber != 10)
            {
                select.SetActive(true);
                selected.SetActive(false);
            }
        }
        //Ywingred
        if (YwingRed == 0 && planecontainer.transform.eulerAngles.y >= 150 && planecontainer.transform.eulerAngles.y <= 155)
        {
            HaventBuy();
        }
        else if (planecontainer.transform.eulerAngles.y >= 150 && planecontainer.transform.eulerAngles.y <= 155)
        {
            Owned();
            if (selectedplanenumber == 11)
            {
                select.SetActive(false);
                selected.SetActive(true);
            }
            else if (selectedplanenumber != 11)
            {
                select.SetActive(true);
                selected.SetActive(false);
            }
        }
        //purple
        if (Purple == 0 && planecontainer.transform.eulerAngles.y >= 165 && planecontainer.transform.eulerAngles.y <= 170)
        {
            HaventBuy();
        }
        else if (planecontainer.transform.eulerAngles.y >= 165 && planecontainer.transform.eulerAngles.y <= 170)
        {
            Owned();
            if (selectedplanenumber == 12)
            {
                select.SetActive(false);
                selected.SetActive(true);
            }
            else if (selectedplanenumber != 12)
            {
                select.SetActive(true);
                selected.SetActive(false);
            }
        }
    }
    void HaventBuy()
    {
        needtobuy3000.SetActive(false);
        needtobuy5000.SetActive(true);
        owned.SetActive(false);
        locked.SetActive(true);
        select.SetActive(false);
        selected.SetActive(false);
    }
    void Owned()
    {
        needtobuy3000.SetActive(false);
        needtobuy5000.SetActive(false);
        owned.SetActive(true);
        locked.SetActive(false);
    }
    public void Buy()
    {
        buybox.SetActive(true);
        //widered
        if (planecontainer.transform.eulerAngles.y >= 15 && planecontainer.transform.eulerAngles.y <= 20)
        {
            price.text = "for 3000";
            planename.text = "Red Y-Wing";
        }
        if (planecontainer.transform.eulerAngles.y >= 30 && planecontainer.transform.eulerAngles.y <= 170)
            price.text = "for 5000";
        if (planecontainer.transform.eulerAngles.y >= 30 && planecontainer.transform.eulerAngles.y <= 35)
            planename.text = "Blue F-27";
        if (planecontainer.transform.eulerAngles.y >= 45 && planecontainer.transform.eulerAngles.y <= 50)
            planename.text = "Gold Eagle A1";
        if (planecontainer.transform.eulerAngles.y >= 60 && planecontainer.transform.eulerAngles.y <= 65)
            planename.text = "Black Destroyer B8";
        if (planecontainer.transform.eulerAngles.y >= 75 && planecontainer.transform.eulerAngles.y <= 80)
            planename.text = "Single-Engine Pacific Bomber";
        if (planecontainer.transform.eulerAngles.y >= 90 && planecontainer.transform.eulerAngles.y <= 95)
            planename.text = "Sleek White J-56 Fighter";
        if (planecontainer.transform.eulerAngles.y >= 105 && planecontainer.transform.eulerAngles.y <= 110)
            planename.text = "Double Engine Supreme Green";
        if (planecontainer.transform.eulerAngles.y >= 120 && planecontainer.transform.eulerAngles.y <= 125)
            planename.text = "All-Star White Elite";
        if (planecontainer.transform.eulerAngles.y >= 135 && planecontainer.transform.eulerAngles.y <= 140)
            planename.text = "Dark Green Tornado Fighter";
        if (planecontainer.transform.eulerAngles.y >= 150 && planecontainer.transform.eulerAngles.y <= 155)
            planename.text = "Dark Red War Machine";
        if (planecontainer.transform.eulerAngles.y >= 165 && planecontainer.transform.eulerAngles.y <= 170)
            planename.text = "Fat Purple Rocket Bomber";
    }
    public void Nothanks()
    {
        buybox.SetActive(false);
    }
    public void ConfirmBuy()
    {
        buybox.SetActive(false);
        if (planecontainer.transform.eulerAngles.y >= 15 && planecontainer.transform.eulerAngles.y <= 20)
        {
            if (gold >= 3000)
            {
                gold -= 3000;
                PlayerPrefs.SetInt("Gold", gold);
                congratulations.SetActive(true);
                shopcanvas.SetActive(false);
                goldminustext.text = "Gold: -3000";
                PlayerPrefs.SetInt("widered", 1);
            }
            else
            {
                sorrynomoney.SetActive(true);
            }
        }
        if (planecontainer.transform.eulerAngles.y >= 30 && planecontainer.transform.eulerAngles.y <= 170)
        {
            if (gold >= 5000)
            {
                gold -= 5000;
                PlayerPrefs.SetInt("Gold", gold);
                congratulations.SetActive(true);
                shopcanvas.SetActive(false);
                goldminustext.text = "Gold: -5000";
                if (planecontainer.transform.eulerAngles.y >= 30 && planecontainer.transform.eulerAngles.y <= 35)
                    PlayerPrefs.SetInt("blue", 1);
                if (planecontainer.transform.eulerAngles.y >= 45 && planecontainer.transform.eulerAngles.y <= 50)
                    PlayerPrefs.SetInt("goldplane", 1);
                if (planecontainer.transform.eulerAngles.y >= 60 && planecontainer.transform.eulerAngles.y <= 65)
                    PlayerPrefs.SetInt("black", 1);
                if (planecontainer.transform.eulerAngles.y >= 75 && planecontainer.transform.eulerAngles.y <= 80)
                    PlayerPrefs.SetInt("YBlue", 1);
                if (planecontainer.transform.eulerAngles.y >= 90 && planecontainer.transform.eulerAngles.y <= 95)
                    PlayerPrefs.SetInt("white", 1);
                if (planecontainer.transform.eulerAngles.y >= 105 && planecontainer.transform.eulerAngles.y <= 110)
                    PlayerPrefs.SetInt("Wgreen", 1);
                if (planecontainer.transform.eulerAngles.y >= 120 && planecontainer.transform.eulerAngles.y <= 125)
                    PlayerPrefs.SetInt("silver", 1);
                if (planecontainer.transform.eulerAngles.y >= 135 && planecontainer.transform.eulerAngles.y <= 140)
                    PlayerPrefs.SetInt("Sgreen", 1);
                if (planecontainer.transform.eulerAngles.y >= 150 && planecontainer.transform.eulerAngles.y <= 155)
                    PlayerPrefs.SetInt("Yred", 1);
                if (planecontainer.transform.eulerAngles.y >= 165 && planecontainer.transform.eulerAngles.y <= 170)
                    PlayerPrefs.SetInt("purple", 1);
            }
            else
            {
                sorrynomoney.SetActive(true);
            }
        }
    }
    public void Planelocked()
    {
        sorrydidntown.SetActive(true);
    }
    public void Gotoshop()
    {
        sorrydidntown.SetActive(false);
        selectplanecanvas.SetActive(false);
        shop.SetActive(true);
    }
    public void DismissSorryDidntOwn()
    {
        sorrydidntown.SetActive(false);
    }
    public void DismissNoMoneyBox()
    {
        sorrynomoney.SetActive(false);
    }
    public void DismissCongrats()
    {
        congratulations.SetActive(false);
        shopcanvas.SetActive(true);
    }
}
