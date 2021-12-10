using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FadeDirection
{
    FadeIn,
    FadeOut
}

public class BlackFade : MonoBehaviour
{
    [SerializeField] public FadeDirection fadeDirection;
    [SerializeField] [Min(0)] public float fadeSpeed;

    //Private fields
    private bool fadingIn;
    private Color color;
    private bool isFinished = false;
    private float currentAlpha;

    // Start is called before the first frame update
    void Start()
    {
        if (fadeDirection == FadeDirection.FadeIn)
        {
            fadingIn = true;
        }
        else
        {
            fadingIn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFinished)
        {
            //Creates a reference to the current color
            color = GetComponent<Image>().color;

            if (fadingIn)
            {
                //Determines the amount to change the color by
                currentAlpha = color.a - ((fadeSpeed / 10) * Time.deltaTime);
                GetComponent<Image>().color = new Color(color.r, color.b, color.g, currentAlpha);

                //Determines when the fade is finished
                if (color.a < 0)
                {
                    isFinished = true;
                }
            }
            else
            {
                //Determines the amount to change the color by
                currentAlpha = color.a + ((fadeSpeed / 10) * Time.deltaTime);
                GetComponent<Image>().color = new Color(color.r, color.b, color.g, currentAlpha);

                //Determines when the fade is finished
                if (color.a > 255)
                {
                    isFinished = true;
                }
            }
        }
    }
}
