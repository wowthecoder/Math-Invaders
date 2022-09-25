using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level4 : MonoBehaviour
{
    public GameObject rocketcontainer, rocketcontainer2;
    public GameObject playerplane, planeexplosion;
    private RocketMove movescript;
    private GameController gamescript;
    public GameObject whitebox;
    public Text question;
    public GameObject fractionquestion, fracresponse;
    public Text a, b, c, d;
    public Text response;
    public static bool rocketsmove;
    private int math, ranOp;
    private int cf, hcf, x, y, ans;
    private int aa, bb, cc, dd;
    private int score;
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
        rocketcontainer.SetActive(true);
        rocketcontainer2.SetActive(false);
        rocketcontainer.transform.position = Vector3.zero;
        movescript = rocketcontainer.GetComponent<RocketMove>();
        gamescript = gameObject.GetComponent<GameController>();
        score = gamescript.score;
        whitebox.SetActive(false);
        question.gameObject.SetActive(true);
        fractionquestion.gameObject.SetActive(false);
        fracresponse.SetActive(false);
        correctanswers = PlayerPrefs.GetInt("corques", 0);
        question.text = "";
        response.text = "";
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
        if (rocketcontainer.transform.position.z < -11)
        {
            //lives -= 1;
            gamescript.GameOver();
            Color color3 = new Color32(255, 52, 11, 255);
            response.color = color3;
            response.text = "Wrong! Ans: " + ans;
            whitebox.SetActive(false);
            question.text = "";
            rocketcontainer.transform.position = Vector3.zero;
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
        question.text = "";
        if (name == "AtomRocket" && aa == ans || name == "AtomRocket (1)" && bb == ans || name == "AtomRocket (2)" && cc == ans || name == "AtomRocket (3)" && dd == ans)
        {
            //correct answer
            Color color = new Color32(103, 255, 11, 255);
            response.color = color;
            response.text = "Correct! + 100";
            rocketcontainer.transform.position = Vector3.zero;
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
            response.color = color2;
            response.text = "Wrong! Ans: " + ans;
            //lives -= 1;
            gamescript.GameOver();
            whitebox.SetActive(false);
            question.text = "";
            Instantiate(planeexplosion, playerplane.transform.position, playerplane.transform.rotation);
            Destroy(playerplane);
            rocketcontainer.transform.position = Vector3.zero;
            gamescript.spawnWait = 0.5f;
            movescript.speed = 0;
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(2);
        response.text = "";
    }
    void mathquestion()
    {
        if (!GameController.gameOver)
        {
            gamescript.spawnWait = 1.5f;
            rocketcontainer.transform.position = Vector3.zero;
            question.text = "";
            response.text = "";
            math = Random.Range(1, 4);
            whitebox.SetActive(true);
            if (math == 3)
            {
                //LCM
                cf = Random.Range(1, 11);
                x = cf * Random.Range(1, 6);
                y = cf * Random.Range(1, 16);
                if (score >= 4000)
                    x = cf * Random.Range(1, 11);
                FindHCF();
                ans = (x * y) / hcf;
                question.text = "[LCM]  " + x + " ,  " + y;
            }
            else if (math == 2)
            {
                //HCF
                cf = Random.Range(1, 21);
                if (cf <= 10)
                {
                    x = cf * Random.Range(1, 21);
                    y = cf * Random.Range(1, 21);
                }
                else if (cf > 10)
                {
                    x = cf * Random.Range(1, 11);
                    y = cf * Random.Range(1, 11);
                }
                FindHCF();
                ans = hcf;
                question.text = "[HCF]  " + x + " ,  " + y;
            }
            else if (math == 1)
            {
                //percentage (x is number, y is percent)
                x = Random.Range(1, 101);
                if (score <= 3000)
                    y = Random.Range(1, 101);
                else if (score > 3000)
                    y = Random.Range(1, 301);
                if (y % 5 != 0)
                    y -= (y % 5);
                if (x % 5 != 0)
                    x -= (x % 5);
                if (y % 10 == 0 && y % 20 != 0)// if y is 10, 30, 50, ...
                {
                    if (y % 50 == 0)//if y is 50, 100, 150, ...
                    {
                        x = Random.Range(1, 201);
                        x -= (x % 2);
                    }
                    else if (x % 10 != 0)
                        x -= (x % 10);
                }
                if (y % 10 != 0)// if y is 5, 15, 25, ...
                {
                    if (y % 25 == 0)//if y is 25, 75, 125, ...
                    {
                        x = Random.Range(1, 201);
                        x -= (x % 4);
                    }
                    else if (x == 10)
                        x = 20;
                    else if (x % 20 != 0)
                        x -= (x % 20);
                }
                ans = (x * y) / 100;
                question.text = x + "  x  " + y + "%  =  ?";
                Debug.Log(x + ", " + y + ", " + ans);
            }
            if (score <= 5000)
                movescript.speed = 1.8f;
            else if (score > 5000)
                movescript.speed = 2f; 
            randomOp();
        }
    }
    void FindHCF()
    {
        factors.Clear();
        commfactors.Clear();
        for (int i = 1; i <= Mathf.Min(x, y); i++)
        {
            if (Mathf.Min(x, y) % i == 0)
            {
                factors.Add(i);
            }
        }
        for (int q = 0; q < factors.Count; q++)
        {
            if (Mathf.Max(x, y) % factors[q] == 0)
            {
                commfactors.Add(factors[q]);
            }
        }
        hcf = Mathf.Max(commfactors.ToArray());
    }
    void randomOp()
    {
        ranOp = Random.Range(1, 5);
        if (ranOp == 1)
        {
            aa = ans;
            if (math == 1)
            {
                bb = y * Random.Range(1, 11);
                cc = x * Random.Range(1, 11);
                dd = ans * Random.Range(2, 7);
            }
            else if (math == 2)
            {
                bb = ans * Random.Range(2, 5);
                cc = factors[Random.Range(0, factors.Count - 1)];
                if (x % 2 == 0 && y % 2 == 0)
                    dd = 2 * Random.Range(1, 11);
                else
                    dd = (2 * Random.Range(1, 11)) - 1;
            }
            else if (math == 3)
            {
                if (ans <= 20)
                {
                    if (ans == 0)
                    {
                        bb = Random.Range(1, 11);
                        cc = Random.Range(1, 11);
                        dd = Random.Range(1, 11);
                    }
                    bb = Random.Range(1, ans * 2 + 1);
                    cc = Random.Range(1, ans * 2 + 1);
                    dd = Random.Range(1, ans * 2 + 1);
                }
                else if (ans > 20)
                {
                    bb = Random.Range(Mathf.RoundToInt(ans / 2), Mathf.RoundToInt(ans * 1.5f) + 1);
                    cc = Random.Range(Mathf.RoundToInt(ans / 2), Mathf.RoundToInt(ans * 1.5f) + 1);
                    dd = Random.Range(Mathf.RoundToInt(ans / 2), Mathf.RoundToInt(ans * 1.5f) + 1);
                }
                if (y % 50 == 0)
                {
                    bb -= (bb % 2);
                    cc -= (bb % 2);
                    dd -= (bb % 2);
                }
                if (y % 25 == 0)
                {
                    bb -= (bb % 4);
                    cc -= (bb % 4);
                    dd -= (bb % 4);
                }
            }
            if (bb == ans)
                bb += Random.Range(1, 11);
            else if (cc == ans)
                cc += Random.Range(1, 11);
            else if (dd == ans)
                dd += Random.Range(1, 11);

        }
        else if (ranOp == 2)
        {
            bb = ans;
            if (math == 1)
            {
                aa = y * Random.Range(1, 11);
                cc = x * Random.Range(1, 11);
                dd = ans * Random.Range(2, 7);
            }
            else if (math == 2)
            {
                aa = ans * Random.Range(2, 5);
                cc = factors[Random.Range(0, factors.Count - 1)];
                if (x % 2 == 0 && y % 2 == 0)
                    dd = 2 * Random.Range(1, 11);
                else
                    dd = (2 * Random.Range(1, 11)) - 1;
            }
            else if (math == 3)
            {
                if (ans <= 20)
                {
                    if (ans == 0)
                    {
                        aa = Random.Range(1, 11);
                        cc = Random.Range(1, 11);
                        dd = Random.Range(1, 11);
                    }
                    aa = Random.Range(1, ans * 2 + 1);
                    cc = Random.Range(1, ans * 2 + 1);
                    dd = Random.Range(1, ans * 2 + 1);
                }
                else if (ans > 20)
                {
                    aa = Random.Range(Mathf.RoundToInt(ans / 2), Mathf.RoundToInt(ans * 1.5f) + 1);
                    cc = Random.Range(Mathf.RoundToInt(ans / 2), Mathf.RoundToInt(ans * 1.5f) + 1);
                    dd = Random.Range(Mathf.RoundToInt(ans / 2), Mathf.RoundToInt(ans * 1.5f) + 1);
                }
                if (y % 50 == 0)
                {
                    aa -= (bb % 2);
                    cc -= (bb % 2);
                    dd -= (bb % 2);
                }
                if (y % 25 == 0)
                {
                    aa -= (bb % 4);
                    cc -= (bb % 4);
                    dd -= (bb % 4);
                }
            }
            if (aa == ans)
                aa += Random.Range(1, 11);
            else if (cc == ans)
                cc += Random.Range(1, 11);
            else if (dd == ans)
                dd += Random.Range(1, 11);
        }
        else if (ranOp == 3)
        {
            cc = ans;
            if (math == 1)
            {
                bb = y * Random.Range(1, 11);
                aa = x * Random.Range(1, 11);
                dd = ans * Random.Range(2, 7);
            }
            else if (math == 2)
            {
                bb = ans * Random.Range(2, 5);
                aa = factors[Random.Range(0, factors.Count - 1)];
                if (x % 2 == 0 && y % 2 == 0)
                    dd = 2 * Random.Range(1, 11);
                else
                    dd = (2 * Random.Range(1, 11)) - 1;
            }
            else if (math == 3)
            {
                if (ans <= 20)
                {
                    if (ans == 0)
                    {
                        bb = Random.Range(1, 11);
                        cc = Random.Range(1, 11);
                        dd = Random.Range(1, 11);
                    }
                    bb = Random.Range(1, ans * 2 + 1);
                    aa = Random.Range(1, ans * 2 + 1);
                    dd = Random.Range(1, ans * 2 + 1);
                }
                else if (ans > 20)
                {
                    bb = Random.Range(Mathf.RoundToInt(ans / 2), Mathf.RoundToInt(ans * 1.5f) + 1);
                    aa = Random.Range(Mathf.RoundToInt(ans / 2), Mathf.RoundToInt(ans * 1.5f) + 1);
                    dd = Random.Range(Mathf.RoundToInt(ans / 2), Mathf.RoundToInt(ans * 1.5f) + 1);
                }
                if (y % 50 == 0)
                {
                    bb -= (bb % 2);
                    aa -= (bb % 2);
                    dd -= (bb % 2);
                }
                if (y % 25 == 0)
                {
                    bb -= (bb % 4);
                    aa -= (bb % 4);
                    dd -= (bb % 4);
                }
            }
            if (bb == ans)
                bb += Random.Range(1, 11);
            else if (aa == ans)
                aa += Random.Range(1, 11);
            else if (dd == ans)
                dd += Random.Range(1, 11);
        }
        else if (ranOp == 4)
        {
            dd = ans;
            if (math == 1)
            {
                bb = y * Random.Range(1, 11);
                cc = x * Random.Range(1, 11);
                aa = ans * Random.Range(2, 7);
            }
            else if (math == 2)
            {
                bb = ans * Random.Range(2, 5);
                cc = factors[Random.Range(0, factors.Count - 1)];
                if (x % 2 == 0 && y % 2 == 0)
                    aa = 2 * Random.Range(1, 11);
                else
                    aa = (2 * Random.Range(1, 11)) - 1;
            }
            else if (math == 3)
            {
                if (ans <= 20)
                {
                    if (ans == 0)
                    {
                        aa = Random.Range(1, 11);
                        cc = Random.Range(1, 11);
                        bb = Random.Range(1, 11);
                    }
                    bb = Random.Range(1, ans * 2 + 1);
                    cc = Random.Range(1, ans * 2 + 1);
                    aa = Random.Range(1, ans * 2 + 1);
                }
                else if (ans > 20)
                {
                    bb = Random.Range(Mathf.RoundToInt(ans / 2), Mathf.RoundToInt(ans * 1.5f) + 1);
                    cc = Random.Range(Mathf.RoundToInt(ans / 2), Mathf.RoundToInt(ans * 1.5f) + 1);
                    aa = Random.Range(Mathf.RoundToInt(ans / 2), Mathf.RoundToInt(ans * 1.5f) + 1);
                }
                if (y % 50 == 0)
                {
                    bb -= (bb % 2);
                    cc -= (bb % 2);
                    aa -= (bb % 2);
                }
                if (y % 25 == 0)
                {
                    bb -= (bb % 4);
                    cc -= (bb % 4);
                    aa -= (bb % 4);
                }
            }
            if (bb == ans)
                bb += Random.Range(1, 11);
            else if (cc == ans)
                cc += Random.Range(1, 11);
            else if (aa == ans)
                aa += Random.Range(1, 11);
        }
        a.text = aa.ToString();
        b.text = bb.ToString();
        c.text = cc.ToString();
        d.text = dd.ToString();
    }
}
