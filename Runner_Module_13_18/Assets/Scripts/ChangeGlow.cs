using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGlow : MonoBehaviour
{
    public Color[] colors;
    public float duration = 1.5f;

    private float changeTime;
    private int index = 0;
    private bool black = false;
    private bool change = false;

    private Color color;
    private Material material;
    private Color startColor;
    private Color endColor;
    // Start is called before the first frame update
    void Start()
    {
        startColor = colors[index];
        endColor = Color.black;
        material = GetComponent<Renderer>().material;
        GetComponent<Renderer>().material.SetColor("_EmissionColor", colors[0] * 3);
    }

    // Update is called once per frame
    void Update()
    {
        var ratio = (Time.time - changeTime) / duration;
        ratio = Mathf.Clamp01(ratio);
        //color = Color.Lerp(startColor, endColor, ratio);
        //color = Color.Lerp(startColor, endColor, Mathf.Sqrt(ratio)); // A cool effect
        if (!black)
        {
            color = Color.Lerp(startColor, endColor, ratio * ratio);// Another cool effect
            change = true;
        }
        else
        {
            color = Color.Lerp(endColor, startColor, ratio * ratio);
        }
        
        if (ratio == 1f)
        {
            if (change)
            {
                // Switch colors
                if (index != colors.Length)
                {
                    startColor = colors[index++];
                }
                else
                {
                    index = 0;
                    startColor = colors[0];
                }
            }
            black = !black;
            change = false;
            changeTime = Time.time;


            //Color tmp = startColor;
            //startColor = endColor;
            //endColor = tmp;

        }

        //StartCoroutine(ColorChangerr(durationToBlack));
        material.SetColor("_EmissionColor", color * 3);

    }

    private void ChageIndex()
    {
        index++;
    }


    //private IEnumerator ColorChangerr(float duration)
    //{
        
    //    while (true)
    //    {
            
    //        Debug.Log("First color");
    //        if (!firstColor)
    //        {
    //            color = Color.Lerp(colors[0], Color.black, t_1);

                
    //            if (t_1 < 1)
    //            {
    //                t_1 += Time.deltaTime / duration;
    //            }
    //        }

    //        yield return new WaitForSeconds(duration);
    //        Debug.Log("Second color");
    //        if (!secondColor)
    //        {
    //            firstColor = true;

                
    //            color = Color.Lerp(Color.black, colors[1], t_2);

    //            if (t_2 < 1)
    //            {
    //                t_2 += Time.deltaTime / durationToColor;
    //            }
    //        }
    //        yield return new WaitForSeconds(durationToColor);

    //        secondColor = true;
    //        firstColor = false;
    //        t_1 = 0;
    //        t_2 = 0;
    //    }
        
    //}
    //IEnumerator onCoroutine()
    //{
    //    while (true)
    //    {
    //        Debug.Log("OnCoroutine: " + (int)Time.time);
    //        yield return new WaitForSeconds(1f);
    //    }
    //}
}
