using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}
public class PlayerControl : MonoBehaviour 
{
    public float speed;
    public float tilt;
    public Boundary boundary;
    public GameObject shot;
    private GameObject shotclone1, shotclone2, shotclone3;
    public Transform[] shotSpawn;
    public float fireRate;
    private float nextFire;
    private Rigidbody rb;
    private AudioSource audiosource;
    public RectTransform joystick;
    public Joystick realjoystick;
    private int leftorright;
    private int whichcontrol;
    private void Start () {
        rb = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
        realjoystick = FindObjectOfType<Joystick>();
        whichcontrol = PlayerPrefs.GetInt("Control", 2);
	}
    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire && (Input.mousePosition.x > joystick.sizeDelta.x || Input.mousePosition.y > joystick.sizeDelta.y))
        {
            nextFire = Time.time + fireRate;
            if (gameObject.name == "StarSparrowSlimGreen")
            {
                StarSparrowSlimGreen();
            }
            else if (gameObject.name == "StarSparrowYwingRed")
            {
                StarSparrowYwingRed();
            }
            else
            {
                for (int i = 0; i < shotSpawn.Length; i++)
                {
                    Instantiate(shot, shotSpawn[i].position, Quaternion.Euler(new Vector3(30, 0, 0)));
                }
            }
            audiosource.Play();
        }
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began && Time.time > nextFire && (touch.position.x > joystick.sizeDelta.x || touch.position.y > joystick.sizeDelta.y))
            {
                nextFire = Time.time + fireRate;
                if (gameObject.name == "StarSparrowSlimGreen")
                {
                    StarSparrowSlimGreen();
                }
                else if (gameObject.name == "StarSparrowYwingRed")
                {
                    StarSparrowYwingRed();
                }
                else
                {
                    for (int i = 0; i < shotSpawn.Length; i++)
                    {
                        Instantiate(shot, shotSpawn[i].position, Quaternion.Euler(new Vector3(30, 0, 0)));
                    }
                }
                audiosource.Play();
            }
        }
    }
    private void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal2 = Input.acceleration.x;
        float moveVertical2 = Input.acceleration.y;

        /*Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;*/
        if (whichcontrol == 2) // joystick
        {
            realjoystick.gameObject.SetActive(true);
            rb.velocity = new Vector3(realjoystick.Horizontal * 5f, 0.0f, realjoystick.Vertical * 7f);
        }
        if (whichcontrol == 1)
        {
            realjoystick.gameObject.SetActive(false);
            Vector3 movement3 = new Vector3(moveHorizontal2 * 1.6f, 0.0f, moveVertical2 * 3f);
            rb.velocity = movement3 * speed;
        }
        // mobile input
        //Vector3 movement3 = new Vector3(moveHorizontal2 * 1.6f, 0.0f, moveVertical2 * 3f);
        //rb.velocity = movement3 * speed;
        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
        if (gameObject.tag == "Player")
        {
            rb.rotation = Quaternion.Euler(40, 0.0f, rb.velocity.x * -tilt);
        }
    }
    void StarSparrowSlimGreen()
    {
        shotclone1 = Instantiate(shot, shotSpawn[0].position, Quaternion.Euler(new Vector3(30, 0, 0)));
        shotclone2 = Instantiate(shot, shotSpawn[1].position, Quaternion.Euler(new Vector3(30, 0, 0)));
        shotclone3 = Instantiate(shot, shotSpawn[2].position, Quaternion.Euler(new Vector3(30, 0, 0)));
        leftorright = Random.Range(1, 3);
        if (leftorright == 1)
        {   //shoot on left, destroy right
            Destroy(shotclone2);
            shotclone3.GetComponent<Rigidbody>().rotation = Quaternion.Euler(90, -30, 0.0f);
            Quaternion moveAng = Quaternion.Euler(-3, -30, 0);
            shotclone3.GetComponent<Rigidbody>().velocity = moveAng * (Vector3.forward * speed);
        }
        else if (leftorright == 2)
        {
            Destroy(shotclone3);
            shotclone2.GetComponent<Rigidbody>().rotation = Quaternion.Euler(90, 30, 0.0f);
            Quaternion moveAng = Quaternion.Euler(-3, 30, 0);
            shotclone2.GetComponent<Rigidbody>().velocity = moveAng * (Vector3.forward * speed);
        }
    }
    void StarSparrowYwingRed()
    {
        shotclone1 = Instantiate(shot, shotSpawn[0].position, Quaternion.Euler(new Vector3(30, 0, 0)));
        shotclone2 = Instantiate(shot, shotSpawn[1].position, Quaternion.Euler(new Vector3(30, 0, 0)));
        leftorright = Random.Range(1, 3);
        if (leftorright == 1)   //shoot on left, destroy right
            Destroy(shotclone1);
        else if (leftorright == 2) //shoot on right, destroy left
            Destroy(shotclone2);
    }
}
