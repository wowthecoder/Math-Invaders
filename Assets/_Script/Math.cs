using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Math : MonoBehaviour
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
    private int x, y, ans;
    private int aa, bb, cc, dd;
    private int score;
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
            if (Physics.Raycast(raycast.origin, raycast.direction,  out raycasthit, Mathf.Infinity, layermask))
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
        if (name == "AtomRocket" && aa == ans || name == "AtomRocket (1)" && bb == ans || name == "AtomRocket (2)" && cc == ans  || name == "AtomRocket (3)" && dd == ans)
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
            math = Random.Range(1, 11);
            whitebox.SetActive(true);
            if (math <= 3)
            {
                if (score <= 2000)
                {
                    x = Random.Range(1, 101);
                    y = Random.Range(1, 101);
                }
                else if (score > 2000 && score <= 5000)
                {
                    x = Random.Range(1, 201);
                    y = Random.Range(1, 101);
                }
                else if (score > 5000 && score <= 8000)
                {
                    x = Random.Range(1, 901);
                    y = Random.Range(1, 101);
                }
                else if (score > 8000)
                {
                    x = Random.Range(1, 501);
                    y = Random.Range(1, 501);
                }
                ans = x + y;
                question.text = x + "   +   " + y + "  =  ?";
            }
            else if (math > 3 && math <= 6)
            {
                if (score <= 2000)
                {
                    x = Random.Range(1, 101);
                    y = Random.Range(1, 101);
                }
                else if (score > 2000 && score <= 5000)
                {
                    x = Random.Range(1, 201);
                    y = Random.Range(1, 101);
                }
                else if (score > 5000 && score <= 8000)
                {
                    x = Random.Range(1, 1001);
                    y = Random.Range(1, 101);
                }
                else if (score > 8000)
                {
                    x = Random.Range(1, 501);
                    y = Random.Range(1, 501);
                }
                if (x >= y)
                {
                    ans = x - y;
                    question.text = x + "   -   " + y + "  =  ?";
                }
                else if (y > x)
                {
                    ans = y - x;
                    question.text = y + "   -   " + x + "  =  ?";
                }
            }
            else if (math > 6 && math <= 8)
            {
                if (score <= 2000)
                {
                    x = Random.Range(1, 16);
                    y = Random.Range(1, 11);
                }
                else if (score > 2000 && score <= 5000)
                {
                    x = Random.Range(1, 31);
                    y = Random.Range(1, 11);
                }
                else if (score > 5000 && score <= 8000)
                {
                    x = Random.Range(1, 51);
                    y = Random.Range(1, 11);
                }
                else if (score > 8000)
                {
                    x = Random.Range(1, 101);
                    y = Random.Range(1, 11);
                }
                ans = x * y;
                question.text = x + "   x   " + y + "  =  ?";
            }
            else if (math > 8)
            {
                if (score <= 2000)
                {
                    x = Random.Range(1, 11);
                    y = x * Random.Range(1, 11);
                }
                else if (score > 2000 && score <= 5000)
                {
                    x = Random.Range(1, 11);
                    y = x * Random.Range(1, 13);
                }
                else if (score > 5000 && score <= 8000)
                {
                    x = Random.Range(1, 11);
                    y = x * Random.Range(1, 21);
                }
                else if (score > 8000)
                {
                    x = Random.Range(2, 7);
                    y = x * Random.Range(1, 101);
                }
                ans = y / x;
                question.text = y + "   ÷   " + x + "  =  ?";
            }
            movescript.speed = 1.8f;
            randomOp();
        }
    }
    void randomOp()
    {
        ranOp = Random.Range(1, 4);
        if (ranOp == 1)
        {
            aa = ans;
            if (ans >= 200)
            {
                bb = Random.Range(ans - 100, ans + 100);
                cc = Random.Range(ans - 100, ans + 100);
                dd = Random.Range(ans - 100, ans + 100);
            }
            else if (ans < 200 && ans > 50)
            {
                bb = Random.Range(ans - 50, ans + 50);
                cc = Random.Range(ans - 50, ans + 50);
                dd = Random.Range(ans - 50, ans + 50);
            }
            else if (ans <= 50 && ans > 0)
            {
                bb = Random.Range(1, 80);
                cc = Random.Range(1, 80);
                dd = Random.Range(1, 80);
            }
            else if (ans == 0)
            {
                bb = ans + Random.Range(1, 10);
                cc = ans + Random.Range(1, 10);
                dd = ans + Random.Range(1, 10);
            }
            if (bb == ans)
                bb += Random.Range(1, 10);
            else if (cc == ans)
                cc += Random.Range(1, 10);
            else if (dd == ans)
                dd += Random.Range(1, 10);

        }
        else if (ranOp == 2)
        {
            bb = ans;
            if (ans >= 200)
            {
                aa = Random.Range(ans - 100, ans + 100);
                cc = Random.Range(ans - 100, ans + 100);
                dd = Random.Range(ans - 100, ans + 100);
            }
            else if (ans < 200 && ans > 50)
            {
                aa = Random.Range(ans - 50, ans + 50);
                cc = Random.Range(ans - 50, ans + 50);
                dd = Random.Range(ans - 50, ans + 50);
            }
            else if (ans <= 50 && ans > 0)
            {
                aa = Random.Range(1, 80);
                cc = Random.Range(1, 80);
                dd = Random.Range(1, 80);
            }
            else if (ans == 0)
            {
                aa = ans + Random.Range(1, 10);
                cc = ans + Random.Range(1, 10);
                dd = ans + Random.Range(1, 10);
            }
            if (aa == ans)
                aa += Random.Range(1, 10);
            else if (cc == ans)
                cc += Random.Range(1, 10);
            else if (dd == ans)
                dd += Random.Range(1, 10);
        }
        else if (ranOp == 3)
        {
            cc = ans;
            if (ans >= 200)
            {
                bb = Random.Range(ans - 100, ans + 100);
                aa = Random.Range(ans - 100, ans + 100);
                dd = Random.Range(ans - 100, ans + 100);
            }
            else if (ans < 200 && ans > 50)
            {
                bb = Random.Range(ans - 50, ans + 50);
                aa = Random.Range(ans - 50, ans + 50);
                dd = Random.Range(ans - 50, ans + 50);
            }
            else if (ans <= 50 && ans > 0)
            {
                bb = Random.Range(1, 80);
                aa = Random.Range(1, 80);
                dd = Random.Range(1, 80);
            }
            else if (ans == 0)
            {
                bb = ans + Random.Range(1, 10);
                aa = ans + Random.Range(1, 10);
                dd = ans + Random.Range(1, 10);
            }
            if (bb == ans)
                bb += Random.Range(1, 10);
            else if (aa == ans)
                aa += Random.Range(1, 10);
            else if (dd == ans)
                dd += Random.Range(1, 10);
        }
        else if (ranOp == 4)
        {
            dd = ans;
            if (ans >= 200)
            {
                bb = Random.Range(ans - 100, ans + 100);
                cc = Random.Range(ans - 100, ans + 100);
                aa = Random.Range(ans - 100, ans + 100);
            }
            else if (ans < 200 && ans > 50)
            {
                bb = Random.Range(ans - 50, ans + 50);
                cc = Random.Range(ans - 50, ans + 50);
                aa = Random.Range(ans - 50, ans + 50);
            }
            else if (ans <= 50 && ans > 0)
            {
                bb = Random.Range(1, 80);
                cc = Random.Range(1, 80);
                aa = Random.Range(1, 80);
            }
            else if (ans == 0)
            {
                bb = ans + Random.Range(1, 10);
                cc = ans + Random.Range(1, 10);
                aa = ans + Random.Range(1, 10);
            }
            if (bb == ans)
                bb += Random.Range(1, 10);
            else if (cc == ans)
                cc += Random.Range(1, 10);
            else if (aa == ans)
                aa += Random.Range(1, 10);
        }
        a.text = aa.ToString();
        b.text = bb.ToString();
        c.text = cc.ToString();
        d.text = dd.ToString();
    }
}
