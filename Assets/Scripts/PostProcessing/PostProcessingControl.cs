using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingControl : MonoBehaviour
{

    // ### FIELDS ###
    public Volume volume;
    
    // vignette
    [SerializeField] float vigTarget = 0.5f;

    // darknessSpeed
    [SerializeField] float Dspeed = 0.5f;
    [SerializeField] Vector4 v4_Dspeed = new Vector4(0f, 0f, 0f, 0.96f);

    // bools
    bool darknessActive = false;
    bool coldnessActive = false;
    
    
    
    // ### MAIN METHODS ###
    private void Update() 
    {
        DarkEffects();
    }



    // ### METHODS ###
    // add & remove effects when stepping in and out of darkness trigger
    public void DarkEffects() {

        // if effects exist, output them to variables
        if (volume.profile.TryGet<Vignette>(out Vignette vignette) && 
            volume.profile.TryGet<LiftGammaGain>(out LiftGammaGain liftGammaGain))
        {
            // if inside darkness trigger
            // + vignette not at target value yet
            if (darknessActive && vignette.intensity.value < vigTarget)
            {
                // add vignette
                vignette.intensity.value += Dspeed * Time.deltaTime;

                // reduce gain
                liftGammaGain.gain.value -= v4_Dspeed * Time.deltaTime;
            }
            
            // if outside darkness trigger
            if (!darknessActive)
            {
                // reduce effects until at normal value
                if (vignette.intensity.value > 0f)
                {
                    // reduce vignette
                    vignette.intensity.value -= Dspeed * Time.deltaTime;

                    // add gain
                    liftGammaGain.gain.value += v4_Dspeed * Time.deltaTime;
                }
            }
        }
    }

    // add & remove effects when stepping in and out of coldness trigger
    public void ColdEffects() {

        // if effects exist, output them to variables
        if (volume.profile.TryGet<Vignette>(out Vignette vignette) && 
            volume.profile.TryGet<LiftGammaGain>(out LiftGammaGain liftGammaGain))
        {
            // if inside coldness trigger
            // + vignette not at target value yet
            if (coldnessActive /*&& vignette.intensity.value < vigTarget*/)
            {
                // // add vignette
                // vignette.intensity.value += speed * Time.deltaTime;

                // // reduce gain
                // liftGammaGain.gain.value -= v4_speed * Time.deltaTime;
            }
            
            // if outside coldness trigger
            if (!coldnessActive)
            {
                // // reduce effects until at normal value
                // if (vignette.intensity.value > 0f)
                // {
                //     // reduce vignette
                //     vignette.intensity.value -= speed * Time.deltaTime;

                //     // add gain
                //     liftGammaGain.gain.value += v4_speed * Time.deltaTime;
                // }
            }
        }
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
