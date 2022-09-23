using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPspritemovement : MonoBehaviour
{
    public RectTransform XPbar;
    private Vector2 initialpos;
    private static bool firsttouch = true;
    // Start is called before the first frame update
    void Start()
    {
        initialpos = transform.position;
        firsttouch = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 transformpos = transform.position;
        Vector2 targetpos = XPbar.transform.position;
        transformpos = Vector2.MoveTowards(transformpos, targetpos, 800 * Time.deltaTime);
        transform.position = transformpos;
        if (transformpos.x >= (XPbar.transform.position.x - 5) && transformpos.x <= (XPbar.transform.position.x + 5) && transformpos.y >= (XPbar.transform.position.y - 5) && transformpos.y <= (XPbar.transform.position.y + 5))
        {
            gameObject.SetActive(false);
            transform.position = initialpos;
            if (firsttouch)
            {
                Debug.Log("yay2");
                GameController.XPshieldspritetouched = true;
                firsttouch = false;
            }
        }
    }
}
