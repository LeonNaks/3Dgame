using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Flashlight : MonoBehaviour
{
    public Light light;

    public TMP_Text text;

    public TMP_Text batteryText;

    public float lifetime = 100;

    public float batteryies = 0;

    public AudioSource flashON;
    public AudioSource flashOFF;
    

    private bool on;
    private bool off;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.text = "Flashlight : " + lifetime + "%";
        light = GetComponent<Light>();

        off = true;
        light.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        text.text = lifetime.ToString("0") + "%";
        batteryText.text = batteryies.ToString();

        if(Input.GetButtonDown("flashlight") && off)
        {
            flashON.Play();
            light.enabled = true;
            on = true;
            off = false;
        }
        else if (Input.GetButtonDown("flashlight") && on)
        {
            flashOFF.Play();
            light.enabled = false;
            on = false;
            off = true;
        }    

        if (on)
        {
            lifetime -= 1 * Time.deltaTime;
        }    
        if(lifetime <= 0)
        {
            light.enabled = false;
            on = false;
            off = true;
            lifetime = 0;
        }
        if (lifetime >= 100)
        {
            lifetime = 100;
        }

        if(Input.GetButtonDown("reload") && batteryies >= 1)
        {
            batteryies -= 1;
            lifetime += 50;
        }    
        if(Input.GetButtonDown("reload") && batteryies == 0)
        {
            return;
        }    
        if(batteryies <= 0)
        {
            batteryies = 0;
        }    
    }
}
