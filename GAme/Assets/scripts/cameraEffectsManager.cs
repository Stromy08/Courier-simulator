using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System;


public class PostProcessingController : MonoBehaviour
{
    public PostProcessProfile postProcessProfile;
    
    public Slider brightnessSlider;
    [SerializeField] TMP_Text BrightnessText;
    [SerializeField] TMP_Text DOFText;
    [SerializeField] TMP_Text FixColorsText;
    [SerializeField] TMP_Text MotionBlurText;
    [SerializeField] TMP_Text AmbientOcclusionText;

    bool toggleDOF;
    bool bumpvibrance;
    bool toggleMotionBlur;
    bool toggleAmbientOcclusion;
    float brightness;

    private ColorGrading colorgrading;
    private DepthOfField depthoffield;
    private MotionBlur motionblur;
    private AmbientOcclusion ambientocclusion;

    void Start()
    {
        // Get the Bloom settings
        postProcessProfile.TryGetSettings(out colorgrading);
        postProcessProfile.TryGetSettings(out depthoffield);
        postProcessProfile.TryGetSettings(out motionblur);
        postProcessProfile.TryGetSettings(out ambientocclusion);

        toggleDOF = depthoffield.active;
        bumpvibrance = colorgrading.active;
        toggleMotionBlur = motionblur.active;
        toggleAmbientOcclusion = ambientocclusion.active;

        DOFText.text = "Depth of field\n" + toggleDOF;
        FixColorsText.text = "Fix Colors\n" + bumpvibrance;
        MotionBlurText.text = "Motion Blur\n" + toggleMotionBlur;
        AmbientOcclusionText.text = "Ambient Occlusion\n" + toggleAmbientOcclusion;
    }

    void Update()
    {
        setBrightness();
    }

    public void toggledof()
    {   
        toggleDOF = !toggleDOF;
        DOFText.text = "Depth of field\n" + toggleDOF;
        depthoffield.active = toggleDOF;
    }

    public void fixcolors()
    {
        bumpvibrance = !bumpvibrance;
        FixColorsText.text = "Fix Colors\n" + bumpvibrance;
        colorgrading.active = bumpvibrance;
    }

    void setBrightness()
    {
        brightness = brightnessSlider.value;
        BrightnessText.text = "Brightness: " + ((int)Math.Round(brightness * 100));
        colorgrading.gain.value = new Vector4(brightness, brightness, brightness, brightness);
    }

    public void toggeMotionBlur()
    {
        toggleMotionBlur = !toggleMotionBlur;
        MotionBlurText.text = "Motion Blur\n" + toggleMotionBlur;
        motionblur.active = toggleMotionBlur;
    }

    public void toggeambientocclusion()
    {
        toggleAmbientOcclusion = !toggleAmbientOcclusion;
        AmbientOcclusionText.text = "Ambient Occlusion\n" + toggleAmbientOcclusion;
        ambientocclusion.active = toggleAmbientOcclusion;
    }

    public void setHighPreset()
    {
        toggleDOF = true;
        DOFText.text = "Depth of field\n" + toggleDOF;
        depthoffield.active = toggleDOF;

        toggleAmbientOcclusion = true;
        AmbientOcclusionText.text = "Ambient Occlusion\n" + toggleAmbientOcclusion;
        ambientocclusion.active = toggleAmbientOcclusion;

        toggleMotionBlur = true;
        MotionBlurText.text = "Motion Blur\n" + toggleMotionBlur;
        motionblur.active = toggleMotionBlur;

        bumpvibrance = true;
        FixColorsText.text = "Fix Colors\n" + bumpvibrance;
        colorgrading.active = bumpvibrance;
    }

    public void setLowPreset()
    {
        toggleDOF = false;
        DOFText.text = "Depth of field\n" + toggleDOF;
        depthoffield.active = toggleDOF;

        toggleAmbientOcclusion = false;
        AmbientOcclusionText.text = "Ambient Occlusion\n" + toggleAmbientOcclusion;
        ambientocclusion.active = toggleAmbientOcclusion;

        toggleMotionBlur = false;
        MotionBlurText.text = "Motion Blur\n" + toggleMotionBlur;
        motionblur.active = toggleMotionBlur;

        bumpvibrance = false;
        FixColorsText.text = "Fix Colors\n" + bumpvibrance;
        colorgrading.active = bumpvibrance;
    }
}