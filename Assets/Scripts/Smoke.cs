using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Smoke : MonoBehaviour
{
    BarOperations BarOperations;
    PlayerStats PlayerStats;
    Animator animator;
    GameObject tapToStart;
    GameObject bar;
    Slider smoke_slider;
    Slider temperature_slider;

    AudioSource audioSource;

    public float smoke_speed = 0.5f;
    public float temperature_speed_up = 0.2f;
    public float temperature_speed_down = 0.2f;

    // bool firstStop = false;
    bool firstTouchToStartSmoking = true;
    bool isSmoking = false;
    bool StartSlider_isPlaying = false;
    // private float stopValue = 0;
    private int lose_count = 0;

    
    [SerializeField]
    ParticleSystem smoke;
    [SerializeField]
    Sprite health_full;
    [SerializeField]
    Sprite health_dead;
    private string pathToHearts;
    void Start()
    {
        BarOperations = GameObject.Find("Bar/PlayerBar").GetComponent<BarOperations>();
        PlayerStats = GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
        tapToStart = GameObject.Find("TapToStart");
        bar = GameObject.Find("Bar");
        smoke_slider = bar.GetComponent<Slider>();
        temperature_slider = GameObject.Find("TemperatureSlider").GetComponent<Slider>();
        audioSource = GameObject.Find("Hookah").GetComponent<AudioSource>();
        bar.SetActive(false);
        
        pathToHearts = "heart_HP/"+PlayerStats.currentHealth+"HP/";
        
        
        // pathToHearts = ""
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

        if(Input.GetMouseButtonDown(0) && firstTouchToStartSmoking){
            tapToStart.SetActive(false);
            bar.SetActive(true);
            firstTouchToStartSmoking = false;
            isSmoking = true;
            smoke.Stop();
            animator.SetTrigger("trigger_start");
            StartCoroutine(StartSlider());
            BarOperations.StartSpawning();
            
        }
        else if(Input.GetMouseButtonDown(0)){
            if(BarOperations.inCollisionG)
                GreenBlockTouched();
            else if(BarOperations.inCollisionR)
                RedBlockTouched();
            else if(BarOperations.inCollisionW)
                WhiteBlockTouched();
        }
        if(isSmoking){
            smoke.Stop();
            animator.SetTrigger("trigger_start");
            StartCoroutine(StartSlider());
            PlayerStats.IncreaseHeartBeat();
        }

        
        if(smoke_slider.value==0)
            smoke.Stop();

        // if(Input.GetMouseButtonDown(0)){

        // // if(Input.touchCount>0){
        //     smoke.Stop();
        //     animator.SetTrigger("trigger_start");
        //     StartCoroutine(StartSlider());
            
        // }

        // else{
        //     if(firstStop){
        //         stopValue = smoke_slider.value;
        //         firstStop = false;
        //         print(stopValue);

        //         // if(stopValue>=0.812 && stopValue <=0.873)
        //         //     Win();
        //         // else
        //         //     Lose();
        //         if(BarOperations.inCollisionG)
        //             Win();
        //         else if(BarOperations.inCollisionR)
        //             Lose();
        //         else if(BarOperations.inCollisionW)
        //             print("Neutral");
        //         //print(BarOperations.inCollisionG);
        //         if(audioSource.isPlaying)
        //             audioSource.Stop();
                
        //         smoke.Play();
                
        //     }
            
        //     animator.SetTrigger("trigger_stop");

        //     // StopCoroutine(StartSlider());
        //     StopAllCoroutines();
        //     StartSlider_isPlaying = false;
        //     if(smoke_slider.value==0)
        //         smoke.Stop();

            
        // }
        
    }

    public void WhiteBlockTouched()
    {
        print("W");
        BarOperations.DestroyObject(BarOperations.colidedObject);
    }

    public void RedBlockTouched()
    {
        print("R");
        Lose();
        if(audioSource.isPlaying)
            audioSource.Stop();
        smoke.Play();

        animator.SetTrigger("trigger_stop");
        StopAllCoroutines();
        StartSlider_isPlaying = false;
        firstTouchToStartSmoking = true;
        isSmoking = false;

        bar.SetActive(false);
        tapToStart.SetActive(true);
        BarOperations.StopSpawning();
    }

    public void GreenBlockTouched()
    {
        print("G");
    }

    IEnumerator StartSlider(){
        
        yield return new WaitForSeconds(1);
        // firstStop = true;
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
        // print("LOSE");
        PlayerStats.TakeDamage();
            // lose_count++;
            GameObject.Find("Heart").GetComponent<Image>().sprite = Resources.Load<Sprite>(pathToHearts + (PlayerStats.maxHealth - PlayerStats.currentHealth));
        // }
        // if(lose_count == 3)
        //     print("DEAD");
    }

    
    

}
