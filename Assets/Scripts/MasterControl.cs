using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterControl : MonoBehaviour
{
    public GameObject[] alltheplanes;
    public GameObject[] gamecontrollers;
    void Start()
    {
        for (int i = 0; i < alltheplanes.Length; i++)
        {
            alltheplanes[i].SetActive(false);
        }
        ChooseOnePlane(MenuController.planeNumber);
        for (int i = 0; i < gamecontrollers.Length; i++)
        {
            gamecontrollers[i].SetActive(false);
        }
        ChooseOneGameController(MenuController.gamecontrollerNumber);
    }

    void ChooseOnePlane(int planenumber)
    {
        alltheplanes[planenumber - 1].SetActive(true);
    }

    void ChooseOneGameController(int gamecontrollernumber)
    {
        gamecontrollers[gamecontrollernumber - 1].SetActive(true);
    }
}
