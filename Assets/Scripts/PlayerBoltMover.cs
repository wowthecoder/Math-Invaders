using System.Collections;
using UnityEngine;

public class PlayerBoltMover : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private GameObject playerplane;
    void Start () {
        rb = GetComponent<Rigidbody>();
        Quaternion moveAng = Quaternion.Euler(-3, 0, 0);
        rb.velocity = moveAng * (Vector3.forward * speed);
        rb.rotation = Quaternion.Euler(90, 0.0f, 0.0f);
        if (gameObject.name == "Red Laser(Clone)")
        {
            playerplane = GameObject.Find("StarSparrowBlack");
        }
        if (gameObject.name == "Gold Laser(Clone)")
        {
            playerplane = GameObject.Find("StarSparrowSilver");
        }
        if (gameObject.name == "PurplePlasma(Clone)")
        {
            playerplane = GameObject.Find("StarSparrowSlimGreen");
        }
        if (gameObject.name == "Red Laser(Clone)" && gameObject.transform.position.x > playerplane.transform.position.x)
        {
            rb.rotation = Quaternion.Euler(90, 20, 0.0f);
            moveAng = Quaternion.Euler(-3, 20, 0);
            rb.velocity = moveAng * (Vector3.forward * speed);
        }
        if (gameObject.name == "Red Laser(Clone)" && gameObject.transform.position.x < playerplane.transform.position.x)
        {
            rb.rotation = Quaternion.Euler(90, -20, 0.0f);
            moveAng = Quaternion.Euler(-3, -20, 0);
            rb.velocity = moveAng * (Vector3.forward * speed);
        }
        if (gameObject.name == "Gold Laser(Clone)" && playerplane.transform.position.x >= 0)
        {
            rb.rotation = Quaternion.Euler(90, -20, 0.0f);
            moveAng = Quaternion.Euler(-3, -20, 0);
            rb.velocity = moveAng * (Vector3.forward * speed);
        }
        if (gameObject.name == "Gold Laser(Clone)" && playerplane.transform.position.x < 0)
        {
            rb.rotation = Quaternion.Euler(90, 20, 0.0f);
            moveAng = Quaternion.Euler(-3, 20, 0);
            rb.velocity = moveAng * (Vector3.forward * speed);
        }
        if (gameObject.name == "PurplePlasma(Clone)" && gameObject.transform.position.x > playerplane.transform.position.x)
        {
            rb.rotation = Quaternion.Euler(90, 30, 0.0f);
            moveAng = Quaternion.Euler(-3, 30, 0);
            rb.velocity = moveAng * (Vector3.forward * speed);
        }
        if (gameObject.name == "PurplePlasma(Clone)" && gameObject.transform.position.x < playerplane.transform.position.x)
        {
            rb.rotation = Quaternion.Euler(90, -30, 0.0f);
            moveAng = Quaternion.Euler(-3, -30, 0);
            rb.velocity = moveAng * (Vector3.forward * speed);
        }
    }
}
