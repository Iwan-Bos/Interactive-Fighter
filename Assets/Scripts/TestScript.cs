using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    private int LDR;
    private int therm;
    private int movePlates;
    private int attackVal;
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
        LDRtext.text = LDR.ToString();
        ThermText.text = therm.ToString();


        //analoge metingen
        if (LDR > 600)
        {
            showLight.color = new Color32(200, 200, 70, 255);
        }
        else
        {
            showLight.color = new Color32(0, 0, 0, 255);
        }
        heatSlider.value = therm;

        //digitale metingen
        //voetplaten
        switch (movePlates)
        {
            case 1:
            {
                directions[1].color = Color.green;
                directions[0].color = Color.white;
                break;
            }
            case 2:
            {
                directions[0].color = Color.green;
                directions[1].color = Color.white;
                break;
            }
            case 3:
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
        switch (attackVal)
        {
            case 1:
            {
                attacks[0].color = Color.white;
                attacks[1].color = Color.green;
                attacks[2].color = Color.white;
                attacks[3].color = Color.white;
                break;
            }
            case 2:
            {
                attacks[0].color = Color.white;
                attacks[1].color = Color.white;
                attacks[2].color = Color.green;
                attacks[3].color = Color.white;
                break;
            }
            case 3:
            {
                attacks[0].color = Color.white;
                attacks[1].color = Color.white;
                attacks[2].color = Color.white;
                attacks[3].color = Color.green;
                break;
            }
            default:
            {
                attacks[0].color = Color.green;
                attacks[1].color = Color.white;
                attacks[2].color = Color.white;
                attacks[3].color = Color.white;
                break;
            }
        }
    }

    public void GiveValues(List<int> values)
    {
        LDR = values[0];
        therm = values[1];
        movePlates = values[2];
        attackVal = values[3];
    }
}
