using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider healthbar;

    public void UpdateHealth(int health) 
    {
        healthbar.value = health;
    }
}
