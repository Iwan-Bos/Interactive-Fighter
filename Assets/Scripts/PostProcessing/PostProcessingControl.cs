using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingControl : MonoBehaviour
{

    // ### FIELDS ###
    // post proccesing
    public Volume volume;
    public Vignette vignette;
    public LiftGammaGain liftGammaGain;
    public ChromaticAberration chromaticAberration;

    // bools
    bool darknessActive = false;
    bool coldnessActive = false;
    [SerializeField] bool flashlight = false;
    [SerializeField] bool frostbite = false;
    
    // 4 dark post-proccesing profiles:
    // - nothing
    private float xDxF_vig = 0.2f;
    private float xDxF_gain = 0f;
    bool xDxF = true;
    // - both
    private float DF_vig = 0.5f;
    private float DF_gain = 0f;
    bool DF = false;
    // - dark no flash
    private float DxF_vig = 0.5f;
    private float DxF_gain = -0.98f;
    bool DxF = false;
    // - no dark + flash
    private float xDF_vig = 0f;
    private float xDF_gain = 1f;
    bool xDF = false;

    // 2 cold post-proccesing profiles:
    // - cold
    private float cold_gain = 255f;
    private float cold_crom = 1f;
    // - no cold
    private float xCold_gain, xCold_crom = 0f;

    [SerializeField] float slideDuration;
    
    // timers
    private float timer = 0f;
    [SerializeField] float timer2 = 0f;
    


    // ### MAIN METHODS ###
    /*
    private void Start() {
        vignette = FindObjectOfType<Volume>().;
        liftGammaGain = FindObjectOfType<Volume>().GetComponent<LiftGammaGain>();
    }*/

    private void Update() 
    {
        DarkEffects();
        ColdEffects();
    }
 


    // ### METHODS ###
    // add & remove effects when stepping in and out of darkness trigger
    // this method is essentially a state switch machine
    private void DarkEffects() 
    {
        // getting the post-proccesing profile's overrides we need (if it cannot find them it will ignore them without throwing an error)
        if (volume.profile.TryGet<Vignette>(out Vignette _v) && 
            volume.profile.TryGet<LiftGammaGain>(out LiftGammaGain _lgg))
        {
            // inside trigger
            if (darknessActive)
            {
                // flashlight on
                if (flashlight && !DF)
                {
                    // only when xDF is active
                    if (xDF && timer < 1f)
                    {
                        // slide from profile "xDF" to "DF"
                        _v.intensity.value = Mathf.Lerp(xDF_vig, DF_vig, timer);
                        _lgg.gain.value = new Vector4(0f, 0f, 0f, Mathf.Lerp(xDF_gain, DF_gain, timer));
                        
                        // increment timer
                        timer += Time.deltaTime / slideDuration; 
                    }

                    // only when DxF is active
                    if (DxF && timer < 1f)
                    {
                        // slide from profile "DxF" to "DF"
                        _v.intensity.value = Mathf.Lerp(DxF_vig, DF_vig, timer);
                        _lgg.gain.value = new Vector4(0f, 0f, 0f, Mathf.Lerp(DxF_gain, DF_gain, timer));
                        
                        // increment timer
                        timer += Time.deltaTime / slideDuration; 
                    }
                    
                    // make profile active when it complete the slide
                    if (timer >= 1f) 
                    {
                        xDxF = false;
                        DF = true; // Darkness, Flashlight
                        DxF = false;
                        xDF = false;

                        // reset timer
                        timer = 0f;
                    }
                }

                // flashlight off
                else if (!flashlight && !DxF)
                {
                    // only when DF is active
                    if (DF && timer < 1f)
                    {
                        // slide from profile "DF" to "DxF"
                        _v.intensity.value = Mathf.Lerp(DF_vig, DxF_vig, timer);
                        _lgg.gain.value = new Vector4(0f, 0f, 0f, Mathf.Lerp(DF_gain, DxF_gain, timer));
                        
                        // increment timer
                        timer += Time.deltaTime / slideDuration; 
                    }
                    
                    // only when xDxF is active
                    if (xDxF && timer < 1f)
                    {
                        // slide from profile "xDxF" to "DxF"
                        _v.intensity.value = Mathf.Lerp(xDxF_vig, DxF_vig, timer);
                        _lgg.gain.value = new Vector4(0f, 0f, 0f, Mathf.Lerp(xDxF_gain, DxF_gain, timer));
                        
                        // increment timer
                        timer += Time.deltaTime / slideDuration; 
                    }

                    // make profile active when it complete the slide
                    if (timer >= 1f) 
                    {
                        xDxF = false;
                        DF = false; 
                        DxF = true; // Darkness, no Flashlight
                        xDF = false;

                        // reset timer
                        timer = 0f;
                    }
                }
            }
            // outside trigger
            else
            {
                // flashlight on
                if (flashlight && !xDF)
                {
                    // only when DF is active
                    if (DF && timer < 1f)
                    {
                        // slide from profile "DF" to "xDF"
                        _v.intensity.value = Mathf.Lerp(DF_vig, xDF_vig, timer);
                        _lgg.gain.value = new Vector4(0f, 0f, 0f, Mathf.Lerp(DF_gain, xDF_gain, timer));
                        
                        // increment timer
                        timer += Time.deltaTime / slideDuration; 
                    }

                    // only when xDxF is active
                    if (xDxF && timer < 1f)
                    {
                        // slide from profile "xDxF" to "xDF"
                        _v.intensity.value = Mathf.Lerp(xDxF_vig, xDF_vig, timer);
                        _lgg.gain.value = new Vector4(0f, 0f, 0f, Mathf.Lerp(xDxF_gain, xDF_gain, timer));
                        
                        // increment timer
                        timer += Time.deltaTime / slideDuration; 
                    }
                    
                    // make profile active when it complete the slide
                    if (timer >= 1f) 
                    {
                        xDxF = false;
                        DF = false;
                        DxF = false;
                        xDF = true; // no Darkness, Flashlight

                        // reset timer
                        timer = 0f;
                    }
                }

                // flashlight off
                else if (!flashlight && !xDxF)
                {
                    // only when xDF is active
                    if (xDF && timer < 1f)
                    {
                        // slide from profile "xDF" to "xDxF"
                        _v.intensity.value = Mathf.Lerp(xDF_vig, xDxF_vig, timer);
                        _lgg.gain.value = new Vector4(0f, 0f, 0f, Mathf.Lerp(xDF_gain, xDxF_gain, timer));
                        
                        // increment timer
                        timer += Time.deltaTime / slideDuration; 
                    }
                    
                    // only when DxF is active
                    if (DxF && timer < 1f)
                    {
                        // slide from profile "DxF" to "xDxF"
                        _v.intensity.value = Mathf.Lerp(DxF_vig, xDxF_vig, timer);
                        _lgg.gain.value = new Vector4(0f, 0f, 0f, Mathf.Lerp(DxF_gain, xDxF_gain, timer));
                        
                        // increment timer
                        timer += Time.deltaTime / slideDuration; 
                    }

                    // make profile active when it complete the slide
                    if (timer >= 1f) 
                    {
                        xDxF = true; // no Darkness, no Flashlight
                        DF = false; 
                        DxF = false;
                        xDF = false;

                        // reset timer
                        timer = 0f;
                    }
                }
            }
        }
    }

    // add & remove effects when stepping in and out of coldness trigger
    public void ColdEffects() 
    {  
        // getting the post-proccesing profile's overrides we need (if it cannot find them it will ignore them without throwing an error)
        if (volume.profile.TryGet<ChromaticAberration>(out ChromaticAberration _chrom) && 
            volume.profile.TryGet<LiftGammaGain>(out LiftGammaGain _lgg))
        {
            // cold
            if (coldnessActive)
            {
                if (timer2 < 1f)
                {
                    // slide from profile "xCold" to "Cold"
                    _chrom.intensity.value = Mathf.Lerp(xCold_crom, cold_crom, timer2);
                    _lgg.gain.value = new Vector4(0f, 0f, Mathf.Lerp(xCold_gain, cold_gain, timer2));

                    // increment timer
                    timer2 += Time.deltaTime / slideDuration;
                
                    // frostbite
                    frostbite = true;
                }
            }
            // no cold
            else if (!coldnessActive)
            {
                if (timer2 > 0f)
                {
                    // slide from profile "xCold" to "Cold"
                    _chrom.intensity.value = Mathf.Lerp(xCold_crom, cold_crom, timer2);
                    _lgg.gain.value = new Vector4(0f, 0f, Mathf.Lerp(xCold_gain, cold_gain, timer2));

                    // increment timer
                    timer2 -= Time.deltaTime / slideDuration;

                    // no frostbite
                    frostbite = false;
                }
            }
        }
    }

    // these Methods can most likely be removed
    // enables darkness
    public void AddDarkness() {
        darknessActive = true;
    }
    // disables darkness
    public void RemoveDarkness() {
        darknessActive = false;
    }
   
    // enables coldness
    public void AddColdness() {
        coldnessActive = true;
    }
    // disables coldness
    public void RemoveColdness() {
        coldnessActive = false;
    }
}
