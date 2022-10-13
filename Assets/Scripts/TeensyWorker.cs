using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeensyWorker : MonoBehaviour
{
    //value fields
    private int LDR;
    private int therm;
    private int movePlates;
    private int attackVal;

    //display fields
    public Slider attackSlider;

    //movement fields
    private float timer;
    private bool mayJump;

    //attack fields
    private float attackTimer;
    private bool attacked;

    //movement script
    private PlayerMovement player;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        timer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //analoge metingen//
        //LDR

        //Thermo

        //digitale metingen//
        //voetplaten
        switch (movePlates)
        {
            case 1:
            {
                player.move("right");
                break;
            }
            case 2:
            {
                player.move("left");
                break;
            }
            case 3:
            {
                mayJump = true;
                timer = 0;
                break;
            }
            default:
            {
                if(mayJump && timer < 0.5f)
                {
                    //jump
                    player.Jump();
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
                if (!attacked)
                {
                    attackSlider.gameObject.SetActive(true);
                    attackSlider.value = attackTimer * 4;
                }
                //delay 0.5 seconds
                if (attackTimer > 0.5 && !attacked)
                {
                    player.BasicAttack();
                    attacked = true;
                }
                break;
            }
            case 2:
            {
                if (!attacked)
                {
                    attackSlider.gameObject.SetActive(true);
                    attackSlider.value = attackTimer;
                }
                //delay 2 seconds
                if (attackTimer > 2 && !attacked)
                {
                    player.BasicAttack();
                    attacked = true;
                }
                break;
            }
            case 3:
            {
                //reset attack values
                attackSlider.gameObject.SetActive(false);
                attackTimer = 0f;
                attacked = true;

                //open pausemenu
                //implementation

                break;
            }
            default:
            {
                attackTimer = 0;
                attacked = false;
                attackSlider.value = 0;
                attackSlider.gameObject.SetActive(false);
                break;
            }
        }
        attackTimer += Time.deltaTime;
    }

    public void GiveValues(List<int> values)
    {
        LDR = values[0];
        therm = values[1];
        movePlates = values[2];
        attackVal = values[3];
    }
}
