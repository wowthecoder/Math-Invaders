using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fractions : MonoBehaviour
{
    public GameObject rocketcontainer, rocketcontainer2;
    public GameObject playerplane, planeexplosion;
    private RocketMove movescript;
    private GameController gamescript;
    public GameObject whitebox;
    public Text question;
    public GameObject fractionquestion, fracresponse;
    public Text[] text;
    public Text a1, a2, b1, b2, c1, c2, d1, d2;
    public Text[] response;
    public static bool rocketsmove;
    private int math, ranOp;
    private int u, v, w, x, y, z, ans1, ans2, ans3;
    private int aa1, aa2, bb1, bb2, cc1, cc2, dd1, dd2;
    private int score;
    private int hcf;
    List<int> factors = new List<int>();
    List<int> commfactors = new List<int>();
    private int correctanswers;
    //public GameObject life1, life2;
    //public float lives = 2;
    // Start is called before the first frame update
    void Start()
    {
        /*lives = 2;
        life1.SetActive(true);
        life2.SetActive(true);*/
        rocketcontainer.SetActive(false);
        rocketcontainer2.SetActive(true);
        rocketcontainer2.transform.position = Vector3.zero;
        movescript = rocketcontainer2.GetComponent<RocketMove>();
        gamescript = gameObject.GetComponent<GameController>();
        whitebox.SetActive(false);
        question.gameObject.SetActive(false);
        fractionquestion.gameObject.SetActive(true);
        fracresponse.SetActive(true);
        correctanswers = PlayerPrefs.GetInt("corques", 0);
        for (int i = 0; i < text.Length; i++)
        {
            text[i].text = "";
        }
        for (int i = 0; i < response.Length; i++)
        {
            response[i].text = "";
        }
        InvokeRepeating("mathquestion", 3f, 7f);
    }

    // Update is called once per frame
    void Update()
    {
        score = gamescript.score;
        //touch math rocket
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycasthit = new RaycastHit();
            int layermask = 1 << 9;
            if (Physics.Raycast(raycast.origin, raycast.direction, out raycasthit, Mathf.Infinity, layermask))
            {
                checkanswer(raycasthit.collider.name);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycasthit = new RaycastHit();
            int layermask = 1 << 9;
            if (Physics.Raycast(raycast.origin, raycast.direction, out raycasthit, Mathf.Infinity, layermask))
            {
                checkanswer(raycasthit.collider.name);
            }
        }
        if (rocketcontainer2.transform.position.z < -11)
        {
            //lives -= 1;
            gamescript.GameOver();
            Color color3 = new Color32(255, 52, 11, 255);
            for (int i = 0; i < response.Length; i++)
            {
                response[i].color = color3;
            }
            response[0].text = "Wrong! Ans: ";
            response[1].text = ans1.ToString();
            response[2].text = "—";
            response[3].text = ans2.ToString();
            whitebox.SetActive(false);
            for (int i = 0; i < text.Length; i++)
            {
                text[i].text = "";
            }
            rocketcontainer2.transform.position = Vector3.zero;
            movescript.speed = 0;
            Instantiate(planeexplosion, playerplane.transform.position, playerplane.transform.rotation);
            Destroy(playerplane);
        }
        /*if (lives == 2)
        {
            life1.SetActive(true);
            life2.SetActive(true);
        }
        else if (lives == 1)
        {
            life1.SetActive(true);
            life2.SetActive(false);
        }
        else if (lives == 0 && playerplane != null)
        {
            life1.SetActive(false);
            life2.SetActive(false);
            gamescript.GameOver();
            whitebox.SetActive(false);
            question.text = "";
            rocketcontainer.transform.position = Vector3.zero;
            movescript.speed = 0;
            Instantiate(planeexplosion, playerplane.transform.position, playerplane.transform.rotation);
            Destroy(playerplane);
        }*/
    }
    void checkanswer(string name)
    {
        whitebox.SetActive(false);
        for (int i = 0; i < text.Length; i++)
        {
            text[i].text = "";
        }
        if (name == "AtomRocket" && aa1 == ans1 && aa2 == ans2 || name == "AtomRocket (1)" && bb1 == ans1 && bb2 == ans2 || name == "AtomRocket (2)" && cc1 == ans1 && cc2 == ans2 || name == "AtomRocket (3)" && dd1 == ans1 && dd2 == ans2)
        {
            //correct answer
            Color color = new Color32(103, 255, 11, 255);
            response[0].color = color;
            response[0].text = "Correct! + 100";
            for (int i = 1; i < response.Length; i++)
            {
                response[i].text = "";
            }
            rocketcontainer2.transform.position = Vector3.zero;
            gamescript.AddScore(100);
            gamescript.spawnWait = 0.5f;
            movescript.speed = 0;
            correctanswers += 1;
            PlayerPrefs.SetInt("corques", correctanswers);
        }
        else
        {
            //wrong answer
            Color color2 = new Color32(255, 52, 11, 255);
            for (int i = 0; i < response.Length; i++)
            {
                response[i].color = color2;
            }
            response[0].text = "Wrong! Ans: ";
            response[1].text = ans1.ToString();
            response[2].text = "—";
            response[3].text = ans2.ToString();
            //lives -= 1;
            gamescript.GameOver();
            whitebox.SetActive(false);
            for (int i = 0; i < text.Length; i++)
            {
                text[i].text = "";
            }
            Instantiate(planeexplosion, playerplane.transform.position, playerplane.transform.rotation);
            Destroy(playerplane);
            rocketcontainer2.transform.position = Vector3.zero;
            gamescript.spawnWait = 0.5f;
            movescript.speed = 0;
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < response.Length; i++)
        {
            response[i].text = "";
        }
    }
    void mathquestion()
    {
        if (!GameController.gameOver)
        {
            gamescript.spawnWait = 1.5f;
            whitebox.SetActive(true);
            rocketcontainer2.transform.position = Vector3.zero;
            for (int i = 0; i < text.Length; i++)
            {
                text[i].text = "";
            }
            for (int i = 0; i < response.Length; i++)
            {
                response[i].text = "";
            }
            math = Random.Range(1, 5);
            if (math == 1)//+
            {
                if (score <= 3000)
                {
                    x = Random.Range(1, 11);
                    z = x * Random.Range(1, 11);
                    w = Random.Range(1, x + 1);
                    y = Random.Range(1, z + 1);
                    if (x == z)
                    {
                        ans1 = w + y;
                        ans2 = x;
                    }
                    else if (x != z)
                    {
                        ans1 = (w * (z / x)) + y;
                        ans2 = z;
                    }
                }
                else if (score > 3000 && score <= 6000)
                {
                    x = Random.Range(1, 14);
                    z = x * Random.Range(1, 13);
                    w = Random.Range(1, x + Mathf.RoundToInt(x / 2) + 1);
                    y = Random.Range(1, z + Mathf.RoundToInt(z / 2) + 1);
                    if (x == z)
                    {
                        ans1 = w + y;
                        ans2 = x;
                    }
                    else if (x != z)
                    {
                        ans1 = (w * (z / x)) + y;
                        ans2 = z;
                    }
                }
                else if (score > 6000)
                {
                    int chance = Random.Range(1, 3);
                    if (chance == 1)
                    {
                        x = Random.Range(1, 14);
                        z = x * Random.Range(1, 13);
                        w = Random.Range(1, x + Mathf.RoundToInt(x / 2) + 1);
                        y = Random.Range(1, z + Mathf.RoundToInt(z / 2) + 1);
                        u = Random.Range(1, 11);
                        v = Random.Range(1, 11);
                        if (x == z)
                        {
                            if (w + y >= x)
                            {
                                ans1 = w + y - x;
                                ans2 = x;
                                ans3 = u + v + Mathf.FloorToInt((w + y) / x);
                            }
                            else if (w + y < x)
                            {
                                ans1 = w + y;
                                ans2 = x;
                                ans3 = u + v;
                            }
                        }
                        else if (x != z)
                        {
                            if (w * (z / x) + y >= z)
                            {
                                ans1 = w * (z / x) + y - z;
                                ans2 = z;
                                ans3 = u + v + Mathf.FloorToInt((w * (z / x) + y) / z);
                            }
                            else if (w * (z / x) + y < z)
                            {
                                ans1 = w * (z / x) + y;
                                ans2 = z;
                                ans3 = u + v;
                            }
                        }
                        text[0].text = w.ToString();
                        text[1].text = "⁠—";
                        text[2].text = x.ToString();
                        text[3].text = "⁠  +  ";
                        text[4].text = y.ToString();
                        text[5].text = "⁠—";
                        text[6].text = z.ToString();
                        text[7].text = "   =   ?";
                        text[8].text = u.ToString();
                        text[9].text = v.ToString();
                    }
                    else if (chance == 2)
                    {
                        x = Random.Range(1, 14);
                        z = x * Random.Range(1, 13);
                        w = Random.Range(1, x + Mathf.RoundToInt(x / 2) + 1);
                        y = Random.Range(1, z + Mathf.RoundToInt(z / 2) + 1);
                        if (x == z)
                        {
                            ans1 = w + y;
                            ans2 = z;
                        }
                        else if (x != z)
                        {
                            ans1 = (w * (z / x)) + y;
                            ans2 = z;
                        }
                    }
                }
                text[0].text = w.ToString();
                text[1].text = "⁠—";
                text[2].text = x.ToString();
                text[3].text = "⁠  +  ";
                text[4].text = y.ToString();
                text[5].text = "⁠—";
                text[6].text = z.ToString();
                text[7].text = "   =   ?";
            }
            if (math == 2)// - 
            {
                if (score <= 3000)
                {
                    x = Random.Range(1, 11);
                    z = x * Random.Range(1, 11);
                    w = Random.Range(1, x + 1);
                    y = Random.Range(1, z + 1);
                    if (x == z)
                    {
                        if (w >= y)
                        {
                            ans1 = w - y;
                            ans2 = x;
                        }
                        else if (w < y)
                        {
                            ans1 = y - w;
                            ans2 = x;
                        }
                    }
                    else if (x != z)
                    {
                        if (w * (z / x) >= y)
                        {
                            ans1 = (w * (z / x)) - y;
                            ans2 = z;
                        }
                        if (w * (z / x) < y)
                            ans1 = y - (w * (z / x));
                        ans2 = z;
                    }
                }
                else if (score > 3000 && score <= 6000)
                {
                    x = Random.Range(1, 14);
                    z = x * Random.Range(1, 13);
                    w = Random.Range(1, x + Mathf.RoundToInt(x / 2) + 1);
                    y = Random.Range(1, z + Mathf.RoundToInt(z / 2) + 1);
                    if (x == z)
                    {
                        if (w >= y)
                        {
                            ans1 = w - y;
                            ans2 = x;
                        }
                        else if (w < y)
                        {
                            ans1 = y - w;
                            ans2 = x;
                        }
                    }
                    else if (x != z)
                    {
                        if (w * (z / x) >= y)
                        {
                            ans1 = (w * (z / x)) - y;
                            ans2 = z;
                        }
                        if (w * (z / x) < y)
                            ans1 = y - (w * (z / x));
                        ans2 = z;
                    }
                }
                else if (score > 6000)
                {
                    int chance = Random.Range(1, 3);
                    if (chance == 1)
                    {
                        x = Random.Range(1, 14);
                        z = x * Random.Range(1, 13);
                        w = Random.Range(1, x + Mathf.RoundToInt(x / 2) + 1);
                        y = Random.Range(1, z + Mathf.RoundToInt(z / 2) + 1);
                        v = Random.Range(1, 7);
                        u = v + Random.Range(1, 5);
                        if (x == z)
                        {
                            if (w - y >= x)
                            {
                                ans1 = w - y - x;
                                ans2 = x;
                                ans3 = u - v + Mathf.FloorToInt((w - y) / x);
                            }
                            else if (w - y < x)
                            {
                                ans1 = w - y;
                                ans2 = x;
                                ans3 = u - v;
                            }
                        }
                        else if (x != z)
                        {
                            if (w * (z / x) - y >= z)
                            {
                                ans1 = w * (z / x) - y - z;
                                ans2 = z;
                                ans3 = u - v + Mathf.FloorToInt((w * (z / x) - y) / z);
                            }
                            else if (w * (z / x) - y < z)
                            {
                                ans1 = w * (z / x) - y;
                                ans2 = z;
                                ans3 = u - v;
                            }
                        }
                        float wFloat = w;
                        float xFloat = x;
                        float yFloat = y;
                        float zFloat = z;
                        if (u + (wFloat / xFloat) >= v + (yFloat / zFloat))
                        {
                            text[0].text = w.ToString();
                            text[1].text = "⁠—";
                            text[2].text = x.ToString();
                            text[3].text = "⁠  -  ";
                            text[4].text = y.ToString();
                            text[5].text = "⁠—";
                            text[6].text = z.ToString();
                            text[7].text = "   =   ?";
                            text[8].text = u.ToString();
                            text[9].text = v.ToString();
                        }
                        if (u + (wFloat / xFloat) < v + (yFloat / zFloat))
                        {
                            text[0].text = y.ToString();
                            text[1].text = "⁠—";
                            text[2].text = z.ToString();
                            text[3].text = "⁠  -  ";
                            text[4].text = w.ToString();
                            text[5].text = "⁠—";
                            text[6].text = x.ToString();
                            text[7].text = "   =   ?";
                            text[8].text = v.ToString();
                            text[9].text = u.ToString();
                        }
                    }
                    else if (chance == 2)
                    {
                        x = Random.Range(1, 14);
                        z = x * Random.Range(1, 13);
                        w = Random.Range(1, x + Mathf.RoundToInt(x / 2) + 1);
                        y = Random.Range(1, z + Mathf.RoundToInt(z / 2) + 1);
                        if (x == z)
                        {
                            if (w >= y)
                            {
                                ans1 = w - y;
                                ans2 = x;
                            }
                            else if (w < y)
                            {
                                ans1 = y - w;
                                ans2 = x;
                            }
                        }
                        else if (x != z)
                        {
                            if (w * (z / x) >= y)
                            {
                                ans1 = (w * (z / x)) - y;
                                ans2 = z;
                            }
                            if (w * (z / x) < y)
                                ans1 = y - (w * (z / x));
                            ans2 = z;
                        }
                        float wFloat = w;
                        float xFloat = x;
                        float yFloat = y;
                        float zFloat = z;
                        if ((wFloat / xFloat) >= (yFloat / zFloat))
                        {
                            text[0].text = w.ToString();
                            text[1].text = "⁠—";
                            text[2].text = x.ToString();
                            text[3].text = "⁠  -  ";
                            text[4].text = y.ToString();
                            text[5].text = "⁠—";
                            text[6].text = z.ToString();
                            text[7].text = "   =   ?";
                        }
                        else if ((wFloat/ xFloat) < (yFloat / zFloat))
                        {
                            text[0].text = y.ToString();
                            text[1].text = "⁠—";
                            text[2].text = z.ToString();
                            text[3].text = "⁠  -  ";
                            text[4].text = w.ToString();
                            text[5].text = "⁠—";
                            text[6].text = x.ToString();
                            text[7].text = "   =   ?";
                        }
                    }
                }
                if (score < 6000)
                {
                    float wFloat = w;
                    float xFloat = x;
                    float yFloat = y;
                    float zFloat = z;
                    if ((wFloat / xFloat) >= (yFloat / zFloat))
                    {
                        text[0].text = w.ToString();
                        text[1].text = "⁠—";
                        text[2].text = x.ToString();
                        text[3].text = "⁠  -  ";
                        text[4].text = y.ToString();
                        text[5].text = "⁠—";
                        text[6].text = z.ToString();
                        text[7].text = "   =   ?";
                    }
                    else if ((wFloat / xFloat) < (yFloat / zFloat))
                    {
                        text[0].text = y.ToString();
                        text[1].text = "⁠—";
                        text[2].text = z.ToString();
                        text[3].text = "⁠  -  ";
                        text[4].text = w.ToString();
                        text[5].text = "⁠—";
                        text[6].text = x.ToString();
                        text[7].text = "   =   ?";
                    }
                }
            }
            if (math == 3)// x
            {
                if (score <= 3000)
                {
                    x = Random.Range(1, 11);
                    z = Random.Range(1, 11);
                    w = Random.Range(1, x + 1);
                    y = Random.Range(1, z + 1);
                }
                else if (score > 3000)
                {
                    x = Random.Range(1, 31);
                    z = Random.Range(1, 11);
                    w = Random.Range(1, x + Mathf.RoundToInt(x / 2) + 1);
                    y = Random.Range(1, z + 1);
                }
                if ((w % x == 0) || (y % z == 0) || (w % x  == 0 && y % z == 0))// one or both of the fraction is integer
                {
                    if (w % x == 0 && y % z == 0)
                    {
                        ans1 = (w / x) * (y / z);
                        ans2 = 1;
                    }
                    else if (w % x == 0)
                    {
                        ans1 = (w / x) * y;
                        ans2 = z;
                    }
                    else if (y % z == 0)
                    {
                        ans1 = (y / z) * w;
                        ans2 = x;
                    }
                }
                else
                {
                    ans1 = w * y;
                    ans2 = x * z;
                }
                text[0].text = w.ToString();
                text[1].text = "⁠—";
                text[2].text = x.ToString();
                text[3].text = "⁠  x  ";
                text[4].text = y.ToString();
                text[5].text = "⁠—";
                text[6].text = z.ToString();
                text[7].text = "   =   ?";
            }
            if (math == 4)// ÷
            {
                if (score <= 3000)
                {
                    x = Random.Range(1, 21);
                    z = Random.Range(1, 11);
                    w = Random.Range(1, x + 1);
                    y = Random.Range(1, z + 1);
                }
                else if (score > 3000)
                {
                    x = Random.Range(1, 14);
                    z = x * Random.Range(1, 13);
                    w = Random.Range(1, x + Mathf.RoundToInt(x / 2) + 1);
                    y = Random.Range(1, z + Mathf.RoundToInt(z / 2) + 1);
                }
                if ((w % x == 0) || (y % z == 0) || (w % x == 0 && y % z == 0))// one or both of the fraction is integer
                {
                    if (w % x == 0 && y % z == 0)
                    {
                        if ((w / x) % (y / z) == 0)
                        {
                            ans1 = (w / x) / (y / z);
                            ans2 = 1;
                        }
                        else
                        {
                            ans1 = w / x;
                            ans2 = y / z;
                        }
                    }
                    else if (w % x == 0)
                    {
                        ans1 = (w / x) * z;
                        ans2 = y;
                    }
                    else if (y % z == 0)
                    {
                        ans1 = w;
                        ans2 = (y / z) * x;
                    }
                }
                else
                {
                    ans1 = w * z;
                    ans2 = x * y;
                }
                text[0].text = w.ToString();
                text[1].text = "⁠—";
                text[2].text = x.ToString();
                text[3].text = "⁠  ÷  ";
                text[4].text = y.ToString();
                text[5].text = "⁠—";
                text[6].text = z.ToString();
                text[7].text = "   =   ?";
            }
            movescript.speed = 1.8f;
            if (score >= 6000)
                movescript.speed = 2f;
            randomOp();
        }
    }
    void Simplify()
    {
        factors.Clear();
        commfactors.Clear();
        for (int i = 1; i <= Mathf.Min(ans1, ans2); i++)
        {
            if (Mathf.Min(ans1, ans2) % i == 0)
            {
                    factors.Add(i);
            }
        }
        for (int i = 0; i < factors.Count; i++)
        {
            if (Mathf.Max(ans1, ans2) % factors[i] == 0)
            {
                    commfactors.Add(factors[i]);
            }
        }
        hcf = Mathf.Max(commfactors.ToArray());
        ans1 = ans1 / hcf;
        ans2 = ans2 / hcf;
    }
        void randomOp()
        {
            ranOp = Random.Range(1, 5);
            if (ranOp == 1)
            {
                aa1 = ans1;
                aa2 = ans2;
                if (ans1 <= 10)
                {
                    bb1 = Random.Range(1, 16);
                    cc1 = Random.Range(1, 16);
                    dd1 = Random.Range(1, 16);
                }
                else if (ans1 > 10)
                {
                    bb1 = Random.Range(ans1 - 5, ans1 + 11);
                    cc1 = Random.Range(ans1 - 5, ans1 + 11);
                    dd1 = Random.Range(ans1 - 5, ans1 + 11);
                }
                bb2 = Random.Range(1, 11) * Random.Range(1, 13);
                cc2 = ans2 * Random.Range(1, 4);
                dd2 = Random.Range(1, 11) * Random.Range(1, 13);
                if (ans1 == 0)
                {
                    if (bb1 == 0)
                        bb1 += Random.Range(1, 6);
                    if (cc1 == 0)
                        cc1 += Random.Range(1, 6);
                    if (dd1 == 0)
                        dd1 += Random.Range(1, 6);
                }
                else if (bb1 / ans1 == bb2 / ans2)
                {
                    bb1 += Random.Range(1, 6);
                }
                else if (cc1 / ans1 == cc2 / ans2)
                {
                    cc1 += Random.Range(1, 6);
                }
                else if (dd1 / ans1 == dd2 / ans2)
                {
                    dd1 += Random.Range(1, 6);
                }
            }
            else if (ranOp == 2)
            {
                bb1 = ans1;
                bb2 = ans2;
                if (ans1 <= 10)
                {
                    aa1 = Random.Range(1, 16);
                    cc1 = Random.Range(1, 16);
                    dd1 = Random.Range(1, 16);
                }
                else if (ans1 > 10)
                {
                    aa1 = Random.Range(ans1 - 5, ans1 + 11);
                    cc1 = Random.Range(ans1 - 5, ans1 + 11);
                    dd1 = Random.Range(ans1 - 5, ans1 + 11);
                }
                aa2 = Random.Range(1, 11) * Random.Range(1, 13);
                cc2 = ans2 * Random.Range(1, 4);
                dd2 = Random.Range(1, 11) * Random.Range(1, 13);
                if (ans1 == 0)
                {
                    if (aa1 == 0)
                        aa1 += Random.Range(1, 6);
                    if (cc1 == 0)
                        cc1 += Random.Range(1, 6);
                    if (dd1 == 0)
                        dd1 += Random.Range(1, 6);
                }
                else if (aa1 / ans1 == aa2 / ans2)
                {
                    aa1 += Random.Range(1, 6);
                }
                else if (cc1 / ans1 == cc2 / ans2)
                {
                    cc1 += Random.Range(1, 6);
                }
                else if (dd1 / ans1 == dd2 / ans2)
                {
                    dd1 += Random.Range(1, 6);
                }
            }
            else if (ranOp == 3)
            {
                cc1 = ans1;
                cc2 = ans2;
                if (ans1 <= 10)
                {
                    aa1 = Random.Range(1, 16);
                    bb1 = Random.Range(1, 16);
                    dd1 = Random.Range(1, 16);
                }
                else if (ans1 > 10)
                {
                    aa1 = Random.Range(ans1 - 5, ans1 + 11);
                    bb1 = Random.Range(ans1 - 5, ans1 + 11);
                    dd1 = Random.Range(ans1 - 5, ans1 + 11);
                }
                aa2 = Random.Range(1, 11) * Random.Range(1, 13);
                bb2 = ans2 * Random.Range(1, 4);
                dd2 = Random.Range(1, 11) * Random.Range(1, 13);
                if (ans1 == 0)
                {
                    if (aa1 == 0)
                        aa1 += Random.Range(1, 6);
                    if (bb1 == 0)
                        bb1 += Random.Range(1, 6);
                    if (dd1 == 0)
                        dd1 += Random.Range(1, 6);
                }
                else if (aa1 / ans1 == aa2 / ans2)
                {
                    aa1 += Random.Range(1, 6);
                }
                else if (bb1 / ans1 == bb2 / ans2)
                {
                    bb1 += Random.Range(1, 6);
                }
                else if (dd1 / ans1 == dd2 / ans2)
                {
                    dd1 += Random.Range(1, 6);
                }
            }
            else if (ranOp == 4)
            {
                dd1 = ans1;
                dd2 = ans2;
                if (ans1 <= 10)
                {
                    aa1 = Random.Range(1, 16);
                    bb1 = Random.Range(1, 16);
                    cc1 = Random.Range(1, 16);
                }
                else if (ans1 > 10)
                {
                    aa1 = Random.Range(ans1 - 5, ans1 + 11);
                    bb1 = Random.Range(ans1 - 5, ans1 + 11);
                    cc1 = Random.Range(ans1 - 5, ans1 + 11);
                }
                aa2 = Random.Range(1, 11) * Random.Range(1, 13);
                bb2 = ans2 * Random.Range(1, 6);
                cc2 = Random.Range(1, 11) * Random.Range(1, 13);
                if (ans1 == 0)
                {
                    if (aa1 == 0)
                        aa1 += Random.Range(1, 6);
                    if (bb1 == 0)
                        bb1 += Random.Range(1, 6);
                    if (cc1 == 0)
                        cc1 += Random.Range(1, 6);
                }
                else if (aa1 / ans1 == aa2 / ans2)
                {
                    aa1 += Random.Range(1, 6);
                }
                else if (bb1 / ans1 == bb2 / ans2)
                {
                    bb1 += Random.Range(1, 6);
                }
                else if (cc1 / ans1 == cc2 / ans2)
                {
                    cc1 += Random.Range(1, 6);
                }
            }
            a1.text = aa1.ToString();
            a2.text = aa2.ToString();
            b1.text = bb1.ToString();
            b2.text = bb2.ToString();
            c1.text = cc1.ToString();
            c2.text = cc2.ToString();
            d1.text = dd1.ToString();
            d2.text = dd2.ToString();
        }
}