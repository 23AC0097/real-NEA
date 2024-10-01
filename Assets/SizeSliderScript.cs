using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeSliderScript : MonoBehaviour
{
    public Slider sizeSlider;
    public float value;
    public void SizeSliderValue()
    {
        value = sizeSlider.value;
    }
}
