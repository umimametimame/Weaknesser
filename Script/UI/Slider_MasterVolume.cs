using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider_MasterVolume : SliderClass
{
    protected override void Start()
    {
        base.Start();
        slider.value = AudioListener.volume;
    }
    protected override void OnValueChanged(float sliderValue)
    {
        base.OnValueChanged(sliderValue);
        AudioListener.volume = sliderValue;
    }

    protected override void Apply()
    {
        base.Apply();
        Setting.masterVolume.Save(slider.value);
    }
}

