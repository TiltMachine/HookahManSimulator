using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Smoke : MonoBehaviour
{
    Animator animator;
    Slider smoke_slider;
    Slider temperature_slider;

    AudioSource audioSource;

    public float smoke_speed = 0.5f;
    public float temperature_speed_up = 0.2f;
    public float temperature_speed_down = 0.2f;

    bool firstStop = false;
    bool StartSlider_isPlaying = false;
    private float stopValue = 0;
    private int lose_count = 0;

    
    [SerializeField]
    ParticleSystem smoke;
    [SerializeField]
    Sprite health_full;
    [SerializeField]
    Sprite health_dead;
    void Start()
    {
      animator = GetComponent<Animator>();
      smoke_slider = GameObject.Find("Bar").GetComponent<Slider>();
      temperature_slider = GameObject.Find("TemperatureSlider").GetComponent<Slider>();
      audioSource = GameObject.Find("Hookah").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.ResetTrigger("trigger_start");
        animator.ResetTrigger("trigger_stop");
        
        if(!StartSlider_isPlaying){
            temperature_slider.value-=temperature_speed_down*Time.deltaTime;
            smoke_slider.value -= smoke_speed*Time.deltaTime;
        }

        if(Input.touchCount>0){
            smoke.Stop();
            animator.SetTrigger("trigger_start");
            StartCoroutine(StartSlider());
            
        }
        else{
            if(firstStop){
                stopValue = smoke_slider.value;
                firstStop = false;
                print(stopValue);

                if(stopValue>=0.812 && stopValue <=0.873)
                    Win();
                else
                    Lose();
                if(audioSource.isPlaying)
                    //audioSource.Pause();
                    audioSource.Stop();
                
                smoke.Play();
                
            }
            
            animator.SetTrigger("trigger_stop");
            // StopCoroutine(StartSlider());
            StopAllCoroutines();
            StartSlider_isPlaying = false;
            if(smoke_slider.value==0)
                smoke.Stop();

            
        }
        
    }
    
    IEnumerator StartSlider(){
        
        yield return new WaitForSeconds(1);
        firstStop = true;
        StartSlider_isPlaying = true;
        smoke_slider.value += smoke_speed*Time.deltaTime;
        temperature_slider.value+=temperature_speed_up*Time.deltaTime;
        if(!audioSource.isPlaying)
            audioSource.Play();
        
        //StartSlider_isPlaying = false;
    }

    public void Win(){
       print("WIN");
    }

    public void Lose(){
        print("LOSE");
        if(lose_count == 3)
            ResetHearts();
        else{
        GameObject.Find("Canvas/HealthPool/heart"+(3-lose_count)).GetComponent<Image>().sprite = health_dead;
        lose_count++;
        }
    }
    public void ResetHearts(){
        GameObject.Find("Canvas/HealthPool/heart1").GetComponent<Image>().sprite = health_full;
        GameObject.Find("Canvas/HealthPool/heart2").GetComponent<Image>().sprite = health_full;
        GameObject.Find("Canvas/HealthPool/heart3").GetComponent<Image>().sprite = health_full;
        lose_count = 0;
    }

}
