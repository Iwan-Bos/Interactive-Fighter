using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    List<int> metingen;
    public Image showLight;
    public Slider heatSlider;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (metingen != null)
        {
            if (metingen[0] > 600)
            {
                showLight.color = new Color32(200, 200, 70, 255);
            }
            else
            {
                showLight.color = new Color32(0, 0, 0, 255);
            }
            heatSlider.value = metingen[1];
        }
    }

    public void GiveValues(List<int> values)
    {
        metingen = values;
    }
}
