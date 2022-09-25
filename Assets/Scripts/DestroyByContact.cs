using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameController gameController;
    private int asteroiddestroyed, enplanedestroyed;
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameContoller' object");
        }
        asteroiddestroyed = PlayerPrefs.GetInt("asteroiddestroy", 0);
        enplanedestroyed = PlayerPrefs.GetInt("enplanedestroy", 0);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy") || other.CompareTag("Math rocket"))
        {
            return;
        }
        if (explosion != null)// collide with player lasers
        {
            Instantiate(explosion, transform.position, transform.rotation);
            if (gameObject.tag == "Enemy" && (gameObject.name == "Asteroid(Clone)" || gameObject.name == "Asteroid02(Clone)" || gameObject.name == "Asteroid03(Clone)"))
            {
                asteroiddestroyed += 1;
                PlayerPrefs.SetInt("asteroiddestroy", asteroiddestroyed);
            }
            if (gameObject.tag == "Enemy" && gameObject.name == "Enemy Ship(Clone)")
            {
                enplanedestroyed += 1;
                PlayerPrefs.SetInt("enplanedestroy", enplanedestroyed);
            }
        }
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }
        if (other.tag == "Player2")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
            Destroy(other.transform.parent.transform.parent.gameObject);
        }
        gameController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
