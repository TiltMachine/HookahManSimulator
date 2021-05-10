
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Smoke : MonoBehaviour
{
    BarOperations BarOperations;
    Player Player;
    Animator animator;
    // GameObject startButton; 
    GameObject tapToStart;

    GameObject bar;
    GameObject temperature_slider_object;
    GameObject heart;
    GameObject exitSmokingButton;

    GameObject [] playingUI;
    GameObject headerUI;
    TextMeshProUGUI heartbeat;
    TextMeshProUGUI moneyValue;

    Slider smoke_slider;
    Slider temperature_slider;

    AudioSource[] sounds;
    AudioSource smokingSound;
    AudioSource coughSound;
    private float smoke_speed;
    private float temperature_speed_up;
    private float temperature_speed_down;

    bool firstTouchToStartSmoking = false;
    bool inAnimationSmoking = false;
    bool isSmoking = false;
    bool in_phase_game = false;
    bool is_ready_to_smoke = false;
    bool exitButtonClicked = false;
    
    // bool isIdle = false;
    [SerializeField]
    ParticleSystem smoke_Particles;

    public AudioSource CoughSound { get => coughSound; set => coughSound = value; }

    void Start()
    {
        BarOperations = GameObject.Find("Bar/PlayerBar").GetComponent<BarOperations>();
        Player = GetComponent<Player>();
        animator = GetComponent<Animator>();
        tapToStart = GameObject.Find("TapToStart");
        tapToStart.SetActive(false);
        // startButton = GameObject.Find("HeaderUI/");
        bar = GameObject.Find("Bar");
        headerUI = GameObject.Find("HeaderUI");
        heart = GameObject.Find("Heart");
        exitSmokingButton = GameObject.Find("ExitSmoking");
        heartbeat = GameObject.Find("Heart/BPM").GetComponent<TextMeshProUGUI>();
        moneyValue = GameObject.Find("HeaderUI/MoneyParent/MoneyValue").GetComponent<TextMeshProUGUI>();
        smoke_slider = bar.GetComponent<Slider>();
        temperature_slider_object = GameObject.Find("TemperatureSlider");
        temperature_slider = GameObject.Find("TemperatureSlider").GetComponent<Slider>();
        // smokingSound = GameObject.Find("Hookah").GetComponent<AudioSource>();
        // bar.SetActive(false);
        
        sounds = GetComponents<AudioSource>();
        smokingSound = sounds[0];
        CoughSound = sounds[1];

        smoke_speed = Player.MaxSmokeCapacity;
        temperature_speed_up = Player.Temperature_speed_up;
        temperature_speed_down = Player.Temperature_speed_down;

        playingUI = new GameObject[4];
        playingUI[0] = bar;
        playingUI[1] = heart;
        playingUI[2] = temperature_slider_object;
        playingUI[3] = exitSmokingButton;

        foreach(GameObject obj in playingUI){
            obj.SetActive(false);
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        animator.ResetTrigger("trigger_start");
        animator.ResetTrigger("trigger_stop");

        if(temperature_slider.value >= 1)
            Win();
        
        else if(exitButtonClicked){
            exitButtonClicked = false;
            Lose();
        }
        else{
                
            heart.GetComponent<Animator>().speed = Player.CurrentHeartBeat;
            heartbeat.SetText(((int)(Player.CurrentHeartBeat*60)).ToString());
            moneyValue.SetText((Player.Money).ToString());

            if(!isSmoking){
                temperature_slider.value-=temperature_speed_down*Time.deltaTime;
                smoke_slider.value -= smoke_speed*Time.deltaTime;
                Player.DecreaseHeartBeat();
            }

            // if(Input.GetMouseButtonDown(0) && firstTouchToStartSmoking){
            if(firstTouchToStartSmoking){
                if(CoughSound.isPlaying)
                    CoughSound.Stop();
                is_ready_to_smoke = false;
                tapToStart.SetActive(false);
                headerUI.SetActive(false);
                // bar.SetActive(true);

                foreach(GameObject obj in playingUI){
                    obj.SetActive(true);
                }

                firstTouchToStartSmoking = false;
                inAnimationSmoking = true;
                smoke_Particles.Stop();
                animator.SetTrigger("trigger_start");
                StartCoroutine(StartSlider());
                BarOperations.StartSpawning();
            }
            else if(Input.GetMouseButtonDown(0) && isSmoking){
                // print("down");
                if(BarOperations.inCollisionG)
                    GreenBlockTouched();
                else if(BarOperations.inCollisionR)
                    RedBlockTouched();
                else if(BarOperations.inCollisionW)
                    WhiteBlockTouched();
            }
            // GetMouseButtonUp или палец после отпускания
            else if(is_ready_to_smoke && Input.GetMouseButtonUp(0)){
                print("helllo");
                PlayButtonTouched();
            }
            if(inAnimationSmoking){
                animator.SetTrigger("trigger_start");
                StartCoroutine(StartSlider());         
            }
            if(isSmoking){
                Player.IncreaseHeartBeat();
            }
            
            if(smoke_slider.value==0)
                smoke_Particles.Stop();
            
            // isIdle = !(isSmoking || inAnimationSmoking);
            // if(isIdle && Input.GetMouseButtonDown(0)){
            //     print("helllo");
            //     PlayButtonTouched();
            // }
        }

    }
    
    public void PlayButtonTouched(){
        firstTouchToStartSmoking = true;
        in_phase_game = true;
        // exitButtonClicked = false;
    }
    public void WhiteBlockTouched()
    {
        BarOperations.DestroyObject(BarOperations.colidedObject);
        temperature_slider.value += 0.05f;
    }

    public void RedBlockTouched()
    {
        Player.TakeDamage();
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
        Player.Money += 1000;
        Player.SavePlayer();
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
        firstTouchToStartSmoking = false;
        inAnimationSmoking = false;

        bar.SetActive(false);

        // foreach(GameObject obj in playingUI){
        //     obj.SetActive(false);
        // }

        // tapToStart.SetActive(true);
        // headerUI.SetActive(true);

        BarOperations.StopSpawning();
        if(in_phase_game)
            Invoke("SmokeCooldown", 2);
    }

    public void ResetAllValues(){
        in_phase_game = false;
        AfterSmoking();
        CoughSound.Stop();
        smoke_Particles.Stop();
        temperature_slider.value = 0;
        smoke_slider.value = 0;
        
        Player.ResetAllValues();
        foreach(GameObject obj in playingUI){
            obj.SetActive(false);
        }
        headerUI.SetActive(true);

    }
    public void SmokeCooldown(){
        if(in_phase_game){
            is_ready_to_smoke = true;
            tapToStart.SetActive(true);
        }
    }
    public void ExitButton(){
        exitButtonClicked = true;
        tapToStart.SetActive(false);
    }

    
    

}
