using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarderMath : MonoBehaviour
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
    private int x, y, z, ans;
    private int aa, bb, cc, dd;
    private int score;
    List<int> zchoices = new List<int>(); // for math == 23
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
            math = Random.Range(1, 11);
            whitebox.SetActive(true);
            if (score <= 5000)
            {
                generateQ();
            }
            else if (score > 5000)
            {
                math = Random.Range(1, 31);
                if (math <= 14)
                {
                    generateQ();
                }
                else if (math == 15)
                {
                    //++
                    generateXYZ(1, 100, 1, 100, 1, 100);
                    ans = x + y + z;
                    question.text = x + " + " + y + " + " + z + " = ?";
                }
                else if (math == 16)
                {
                    //--
                    generateXYZ(700, 1000, 1, 400, 1, 300);
                    ans = x - y - z;
                    question.text = x + " - " + y + " - " + z + " = ?";
                }
                else if (math == 17)
                {
                    //xx
                    generateXYZ(1, 11, 2, 10, 1, 6);
                    ans = x * y * z;
                    question.text = x + " x " + y + " x " + z + " = ?";
                }
                else if (math == 18)
                {
                    //÷÷ 
                    generateXYZ(1, 11, 2, 10, 1, 6);
                    int multiply = x * y * z;
                    int chance = Random.Range(1, 4);
                    if (chance == 1)
                    {
                        ans = x;
                        question.text = multiply + " ÷ " + y + " ÷ " + z + " = ?";
                    }
                    else if (chance == 2)
                    {
                        ans = y;
                        question.text = multiply + " ÷ " + x + " ÷ " + z + " = ?";
                    }
                    else if (chance == 3)
                    {
                        ans = z;
                        question.text = multiply + " ÷ " + x + " ÷ " + y + " = ?";
                    }
                }
                else if (math == 19)
                {
                    //+-
                    generateXYZ(1, 500, 1, 500, 1, 500);
                    if (x == Mathf.Max(x, y, z))
                    {
                        ans = x + y - z;
                        question.text = x + " + " + y + " - " + z + " = ?";
                    }
                    else if (y == Mathf.Max(x, y, z))
                    {
                        ans = y + z - x;
                        question.text = y + " + " + z + " - " + x + " = ?";
                    }
                    else if (z == Mathf.Max(x, y, z))
                    {
                        ans = z + x - y;
                        question.text = z+ " + " + x + " - " + y + " = ?";
                    }
                }
                else if (math == 20)
                {
                    //-+
                    generateXYZ(1, 500, 1, 500, 1, 500);
                    if (x == Mathf.Max(x, y, z))
                    {
                        ans = x - y + z;
                        question.text = x + " - " + y + " + " + z + " = ?";
                    }
                    else if (y == Mathf.Max(x, y, z))
                    {
                        ans = y - z + x;
                        question.text = y + " - " + z + " + " + x + " = ?";
                    }
                    else if (z == Mathf.Max(x, y, z))
                    {
                        ans = z - x + y;
                        question.text = z + " - " + x + " + " + y + " = ?";
                    }
                }
                else if (math == 21)
                {
                    //x÷
                    generateXYZ(1, 12, 1, 10, 1, 1);//z not generated here
                    int multiply = x * y;
                    for (int i = 1; i <= multiply; i++)
                    {
                        if (multiply % i == 0)// if multiply can be divided perfectly by i
                        {
                            zchoices.Add(i);
                        }
                    }
                    int zindex = Random.Range(0, zchoices.Count + 1);
                    z = zchoices[zindex];
                    ans = (x * y) / z;
                    question.text = x + " x " + y + " ÷ " + z + " = ?";
                }
                else if (math == 22)
                {
                    //÷x
                    generateXYZ(1, 1, 1, 12, 1, 10);//x not generated here
                    x = y * Random.Range(1, 13);
                    ans = (x / y) * z;
                    question.text = x + " ÷ " + y + " x " + z + " = ?";
                }
                else if (math == 23)
                {
                    //+x
                    generateXYZ(1, 300, 1, 12, 1, 10);
                    ans = x + (y * z);
                    question.text =  x + " + " + y + " x " + z + " = ?";
                }
                else if (math == 24)
                {
                    //-x
                    generateXYZ(1, 300, 1, 12, 1, 10);
                    ans = x - (y * z);
                    question.text = x + " - " + y + " x " + z + " = ?";
                }
                else if (math == 25)
                {
                    //x+
                    generateXYZ(1, 12, 1, 10, 1, 300);
                    ans = (x * y) + z;
                    question.text = x + " x " + y + " + " + z + " = ?";
                }
                else if (math == 26)
                {
                    //x-
                    generateXYZ(1, 12, 1, 10, 1, 300);
                    ans = (x * y) - z;
                    question.text = x + " x " + y + " - " + z + " = ?";
                }
                else if (math == 27)
                {
                    //+÷
                    generateXYZ(1, 300, 1, 1, 1, 6);// y not generated here
                    y = z * Random.Range(1, 31);
                    ans = x + (y / z);
                    question.text = x + " + " + y + " ÷ " + z + " = ?";
                }
                else if (math == 28)
                {
                    //-÷
                    generateXYZ(1, 300, 1, 1, 1, 6);// y not generated here
                    y = z * Random.Range(1, 31);
                    ans = x - (y / z);
                    question.text = x + " - " + y + " ÷ " + z + " = ?";
                }
                else if (math == 29)
                {
                    //÷+
                    generateXYZ(1, 6, 1, 1, 1, 300);// y not generated here
                    y = x * Random.Range(1, 31);
                    ans = (y / x) + z;
                    question.text = x + " ÷ " + y + " + " + z + " = ?";
                }
                else if (math == 30)
                {
                    //÷-
                    generateXYZ(1, 50, 1, 1, 1, 100);// y not generated here
                    y = x * Random.Range(2, 11);
                    ans = (y / x) - z;
                    question.text = x + " ÷ " + y + " - " + z + " = ?";
                }
            }
            movescript.speed = 1.8f;
            randomOp();
        }
    }
    void generateXYZ(int a, int b, int c, int d, int e, int f)
    {
        x = Random.Range(a, b + 1);
        y = Random.Range(c, d + 1);
        z = Random.Range(e, f + 1);
    }
    void generateQ()
    {
        if (math <= 3)
        {
            x = Random.Range(1, 501);
            if (score <= 2000)
                y = Random.Range(1, 201);
            else if (score > 2000)
                y = Random.Range(1, 501);
            ans = x + y;
            question.text = x + "   +   " + y + "  =  ?";
        }
        else if (math > 3 && math <= 6)
        {
            x = Random.Range(1, 501);
            if (score <= 2000)
                y = Random.Range(1, 201);
            else if (score > 2000)
                y = Random.Range(1, 501);
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
                x = Random.Range(1, 51);
            else if (score > 2000)
                x = Random.Range(1, 101);
            y = Random.Range(1, 11);
            ans = x * y;
            question.text = x + "   x   " + y + "  =  ?";
        }
        else if (math > 8)
        {
            x = Random.Range(1, 11);
            if (score <= 2000)
                y = x * Random.Range(1, 51);
            else if (score > 2000)
                y = x * Random.Range(1, 101);
            ans = y / x;
            question.text = y + "   ÷   " + x + "  =  ?";
        }
    }
    void randomOp()
    {
        ranOp = Random.Range(1, 5);
        if (ranOp == 1)
        {
            aa = ans;
            if (ans >= 200)
            {
                bb = Random.Range(ans - 100, ans + 101);
                cc = Random.Range(ans - 100, ans + 101);
                dd = Random.Range(ans - 100, ans + 101);
            }
            else if (ans < 200 && ans > 50)
            {
                bb = Random.Range(ans - 50, ans + 51);
                cc = Random.Range(ans - 50, ans + 51);
                dd = Random.Range(ans - 50, ans + 51);
            }
            else if (ans <= 50 && ans > 0)
            {
                bb = Random.Range(1, 81);
                cc = Random.Range(1, 81);
                dd = Random.Range(1, 81);
            }
            else if (ans == 0)
            {
                bb = ans + Random.Range(1, 11);
                cc = ans + Random.Range(1, 11);
                dd = ans + Random.Range(1, 11);
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
            if (ans >= 200)
            {
                aa = Random.Range(ans - 100, ans + 101);
                cc = Random.Range(ans - 100, ans + 101);
                dd = Random.Range(ans - 100, ans + 101);
            }
            else if (ans < 200 && ans > 50)
            {
                aa = Random.Range(ans - 50, ans + 51);
                cc = Random.Range(ans - 50, ans + 51);
                dd = Random.Range(ans - 50, ans + 51);
            }
            else if (ans <= 50 && ans > 0)
            {
                aa = Random.Range(1, 81);
                cc = Random.Range(1, 81);
                dd = Random.Range(1, 81);
            }
            else if (ans == 0)
            {
                aa = ans + Random.Range(1, 11);
                cc = ans + Random.Range(1, 11);
                dd = ans + Random.Range(1, 11);
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
            if (ans >= 200)
            {
                bb = Random.Range(ans - 100, ans + 101);
                aa = Random.Range(ans - 100, ans + 101);
                dd = Random.Range(ans - 100, ans + 101);
            }
            else if (ans < 200 && ans > 50)
            {
                bb = Random.Range(ans - 50, ans + 51);
                aa = Random.Range(ans - 50, ans + 51);
                dd = Random.Range(ans - 50, ans + 51);
            }
            else if (ans <= 50 && ans > 0)
            {
                bb = Random.Range(1, 81);
                aa = Random.Range(1, 81);
                dd = Random.Range(1, 81);
            }
            else if (ans == 0)
            {
                bb = ans + Random.Range(1, 11);
                aa = ans + Random.Range(1, 11);
                dd = ans + Random.Range(1, 11);
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
            if (ans >= 200)
            {
                bb = Random.Range(ans - 100, ans + 101);
                cc = Random.Range(ans - 100, ans + 101);
                aa = Random.Range(ans - 100, ans + 101);
            }
            else if (ans < 200 && ans > 50)
            {
                bb = Random.Range(ans - 50, ans + 51);
                cc = Random.Range(ans - 50, ans + 51);
                aa = Random.Range(ans - 50, ans + 51);
            }
            else if (ans <= 50 && ans > 0)
            {
                bb = Random.Range(1, 81);
                cc = Random.Range(1, 81);
                aa = Random.Range(1, 81);
            }
            else if (ans == 0)
            {
                bb = ans + Random.Range(1, 11);
                cc = ans + Random.Range(1, 11);
                aa = ans + Random.Range(1, 11);
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