using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    private List<int> metingen;
    private bool mayJump;
    public Image showLight;
    public Slider heatSlider;
    public List<Image> directions;
    public List<Image> attacks;
    private Text LDRtext;
    private Text ThermText;
    private float timer;

    void Start()
    {
        LDRtext = FindObjectsOfType<Text>()[0];
        ThermText = FindObjectsOfType<Text>()[1];
        timer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (metingen != null)
        {
            LDRtext.text = metingen[0].ToString();
            ThermText.text = metingen[1].ToString();


            //analoge metingen
            if (metingen[0] > 600)
            {
                showLight.color = new Color32(200, 200, 70, 255);
            }
            else
            {
                showLight.color = new Color32(0, 0, 0, 255);
            }
            heatSlider.value = metingen[1];

            //digitale metingen
            //voetplaten
            switch (metingen[2])
            {
                case 10:
                {
                    directions[0].color = Color.green;
                    directions[1].color = Color.white;
                    break;
                }
                case 01:
                {
                    directions[1].color = Color.green;
                    directions[0].color = Color.white;
                    break;
                }
                case 11:
                {
                    directions[0].color = Color.red;
                    directions[1].color = Color.red;
                    mayJump = true;
                    timer = 0;
                    break;
                }
                default:
                {
                    directions[0].color = Color.white;
                    directions[1].color = Color.white;
                    if(mayJump && timer < 0.5f)
                    {
                        //jump
                        Debug.Log("jump!");
                        mayJump = false;
                    }
                    break;
                }
            }
            timer += Time.deltaTime;
            //attacks
        }
    }

    public void GiveValues(List<int> values)
    {
        metingen = values;
    }
}
