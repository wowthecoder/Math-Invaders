using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen instance;
    AsyncOperation currentoperation;
    private bool isloading;
    [SerializeField]
    private Slider barfill;
    [SerializeField]
    private Text percentText;
    private const float minTimetoShow = 2f;
    private float timepassed;
    private Animator animator;
    // Flag whether the fade out animation was triggered.
    private bool didTriggerFadeOutAnimation;

    void Awake()
    {
        //Singleton logic
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        animator = GetComponent<Animator>();
        Hide();
    }
    //public to set animator event
    public void Hide()
    {
        gameObject.SetActive(false);
        isloading = false;
        currentoperation = null;
    }

    void SetProgress(float progress)
    {
        /*if (cansetfull)
        {
            barfill.value = 100;
            percentText.text = "100%";
        }*/
        barfill.value = progress * 100;
        percentText.text = Mathf.CeilToInt(progress * 100).ToString() + "%";
    }

    public void Show(AsyncOperation loadingoperation)
    {
        gameObject.SetActive(true);
        currentoperation = loadingoperation;
        currentoperation.allowSceneActivation = false;
        // Reset the UI
        isloading = true;
        timepassed = 0;
        SetProgress(0f);
        animator.SetTrigger("Show");
        // Reset the fade out animation flag:
        didTriggerFadeOutAnimation = false;
    }

    void Update()
    {
        if (isloading)
        {
            // Get the progress and update the UI. Goes from 0 (start) to 1 (end):
            SetProgress(currentoperation.progress);
            if (currentoperation.isDone && !didTriggerFadeOutAnimation)
            {
                animator.SetTrigger("Hide");
                didTriggerFadeOutAnimation = true;
            }
            else
            {
                timepassed += Time.deltaTime;
                if (timepassed >= minTimetoShow)
                {
                    // The loading screen has been showing for the minimum time required.
                    // Allow the loading operation to formally finish:
                    currentoperation.allowSceneActivation = true;
                }
            }

        }
    }
}
