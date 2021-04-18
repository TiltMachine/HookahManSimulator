
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Smoke : MonoBehaviour
{
    BarOperations BarOperations;
    PlayerStats PlayerStats;
    Animator animator;
    GameObject tapToStart;
    GameObject bar;
    GameObject heart;
    TextMeshProUGUI heartbeat;
    Slider smoke_slider;
    Slider temperature_slider;

    AudioSource[] sounds;
    AudioSource smokingSound;
    AudioSource coughSound;
    private float smoke_speed;
    private float temperature_speed_up;
    private float temperature_speed_down;

    bool firstTouchToStartSmoking = true;
    bool inAnimationSmoking = false;
    bool isSmoking = false;
   
    [SerializeField]
    ParticleSystem smoke_Particles;

    public AudioSource CoughSound { get => coughSound; set => coughSound = value; }

    void Start()
    {
        BarOperations = GameObject.Find("Bar/PlayerBar").GetComponent<BarOperations>();
        PlayerStats = GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
        tapToStart = GameObject.Find("TapToStart");
        bar = GameObject.Find("Bar");
        heart = GameObject.Find("Heart");
        heartbeat = GameObject.Find("Heart/BPM").GetComponent<TextMeshProUGUI>();
        smoke_slider = bar.GetComponent<Slider>();
        temperature_slider = GameObject.Find("TemperatureSlider").GetComponent<Slider>();
        // smokingSound = GameObject.Find("Hookah").GetComponent<AudioSource>();
        bar.SetActive(false);
        
        
        sounds = GetComponents<AudioSource>();
        smokingSound = sounds[0];
        CoughSound = sounds[1];

        smoke_speed = PlayerStats.MaxSmokeCapacity;
        temperature_speed_up = PlayerStats.Temperature_speed_up;
        temperature_speed_down = PlayerStats.Temperature_speed_down;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(temperature_slider.value >= 1)
            Win();
        

        animator.ResetTrigger("trigger_start");
        animator.ResetTrigger("trigger_stop");

        heart.GetComponent<Animator>().speed = PlayerStats.CurrentHeartBeat;
        heartbeat.SetText(((int)(PlayerStats.CurrentHeartBeat*60)).ToString());

        if(!isSmoking){
            temperature_slider.value-=temperature_speed_down*Time.deltaTime;
            smoke_slider.value -= smoke_speed*Time.deltaTime;
            PlayerStats.DecreaseHeartBeat();
        }

        if(Input.GetMouseButtonDown(0) && firstTouchToStartSmoking){
            if(CoughSound.isPlaying)
                CoughSound.Stop();

            tapToStart.SetActive(false);
            bar.SetActive(true);
            firstTouchToStartSmoking = false;
            inAnimationSmoking = true;
            smoke_Particles.Stop();
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
        if(inAnimationSmoking){
            animator.SetTrigger("trigger_start");
            StartCoroutine(StartSlider());         
        }
        if(isSmoking){
            PlayerStats.IncreaseHeartBeat();
        }
        
        if(smoke_slider.value==0)
            smoke_Particles.Stop();
    }

    public void WhiteBlockTouched()
    {
        BarOperations.DestroyObject(BarOperations.colidedObject);
        temperature_slider.value += 0.05f;
    }

    public void RedBlockTouched()
    {
        PlayerStats.TakeDamage();
    }

    public void GreenBlockTouched()
    {
        AfterSmoking();
    }

    IEnumerator StartSlider(){
        
        yield return new WaitForSeconds(1);

        isSmoking = true;
        smoke_slider.value += smoke_speed*Time.deltaTime;
        temperature_slider.value+=temperature_speed_up*Time.deltaTime;
        if(!smokingSound.isPlaying)
            smokingSound.Play();
        
    }

    
    public void Win(){
        print("WIN");
        ResetAllValues();
    }

    public void Lose(){
        print("DEAD");
        ResetAllValues();
    }

    public void AfterSmoking(){
        if(smokingSound.isPlaying)
            smokingSound.Stop();
        
        smoke_Particles.Play();

        animator.SetTrigger("trigger_stop");
        StopAllCoroutines();

        isSmoking = false;
        firstTouchToStartSmoking = true;
        inAnimationSmoking = false;

        bar.SetActive(false);
        tapToStart.SetActive(true);
        BarOperations.StopSpawning();
    }

    public void ResetAllValues(){
        AfterSmoking();
        CoughSound.Stop();
        smoke_Particles.Stop();
        temperature_slider.value = 0;
        smoke_slider.value = 0;
        
        PlayerStats.ResetAllValues();

    }

    
    

}
