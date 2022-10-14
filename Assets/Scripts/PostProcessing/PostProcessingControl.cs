using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingControl : MonoBehaviour
{

    // ### FIELDS ###

    public Volume volume;
    Vignette vignette;
    LiftGammaGain liftGammaGain;

    // bools
    bool darknessActive = false;
    bool coldnessActive = false;
    bool flashlight = false;
    

    [SerializeField] float vigStart;
    [SerializeField] float vigEnd;
    [SerializeField] float gainStart;
    [SerializeField] float gainEnd;

    [SerializeField] float darkDuration;
    
    private float darkTimer;
    


    // ### MAIN METHODS ###
    private void Update() 
    {
        DarkEffects();
    }
        
    

    


    // ### METHODS ###
    
    // add & remove effects when stepping in and out of darkness trigger
    private void DarkEffects() 
    {
        if (volume.profile.TryGet<Vignette>(out Vignette vignette) && 
            volume.profile.TryGet<LiftGammaGain>(out LiftGammaGain liftGammaGain))
        {
            // when inside trigger: Slide %vigStart% to %vigEnd%
            if (darknessActive && darkTimer < 1f)
            {
                vignette.intensity.value = Mathf.Lerp(vigStart, vigEnd, darkTimer);
                liftGammaGain.gain.value = new Vector4(0f, 0f, 0f, Mathf.Lerp(gainStart, gainEnd, darkTimer));
                darkTimer += Time.deltaTime / darkDuration;
            }

            // when outside trigger: do the opposite
            else if (!darknessActive && darkTimer > 0f)
            {
                vignette.intensity.value = Mathf.Lerp(vigStart, vigEnd, darkTimer);
                liftGammaGain.gain.value = new Vector4(0f, 0f, 0f, Mathf.Lerp(gainStart, gainEnd, darkTimer));
                darkTimer -= Time.deltaTime / darkDuration;
            }
        }
    }

    // add & remove effects when stepping in and out of coldness trigger
    public void ColdEffects() {

    }
    
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
        darknessActive = true;
    }
    // disables coldness
    public void RemoveColdness() {
        darknessActive = false;
    }
}
