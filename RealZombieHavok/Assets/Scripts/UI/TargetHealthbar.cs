/*
TargetHealthbar
by Steven Pichelman
10/26/2024
manages single enemy healthbar functionality
*/

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TargetHealthbar : MonoBehaviour
{
    Ray LookingAt;
    [SerializeField] Slider SliderTargetHealth;
    [SerializeField] public Enemy Enemy;
    [SerializeField] GameObject TargetHealthLocation;
    float DetectionRange = 1000;
    float TargetHealthbarHeight = 0;
    int counter = 0;

    private void Start()
    {
        // get this at start because it differs based on screen size.
        TargetHealthbarHeight = SliderTargetHealth.gameObject.transform.position.y;
    }

    // FixedUpdate is used because no update is needed unless the game tick has advanced.
    void FixedUpdate()
    {
        //Debug.Log(Time.frameCount / Time.time); //this is simple fps counter

        if (counter >= 9) //this updates 50/10 times per second.
        {
        LookingAt = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(LookingAt, out RaycastHit hitInfo, DetectionRange))
        {
            if (hitInfo.transform.gameObject.TryGetComponent(out Enemy))
            {
                    if (Enemy.HP <= 0) //enemy is dead or in dying state, so don't show health
                    { 
                        //this moves healthbar out of view, probably better than enabling and disabling it constantly.
                        SliderTargetHealth.gameObject.transform.position = new Vector2(SliderTargetHealth.gameObject.transform.position.x, TargetHealthbarHeight+100);
                    }
                    else
                    {
                         //moves healthbar into view
                         SliderTargetHealth.gameObject.transform.position = new Vector2(SliderTargetHealth.gameObject.transform.position.x, TargetHealthbarHeight);
                    }
                //this is a ratio:       32/64 is 50%. also shows min of 1/64th health to be more visible.
                SliderTargetHealth.value = Math.Max(Enemy.HP * 64 / Enemy.MaxHP, 1);
            }
            else
            {
                //this moves healthbar out of view.
                SliderTargetHealth.gameObject.transform.position = new Vector2(SliderTargetHealth.gameObject.transform.position.x, TargetHealthbarHeight +100);
            }
        }
            counter = 0;
           
        }
        counter++;
    }
    //this is overkill, but this lets the target healthbar hide itself offscreen even as the screen is resized during runtime.
    public void ScreenSizeChanged()
    {
        if (!TargetHealthLocation.transform.parent.gameObject.activeSelf)
        {
            TargetHealthLocation.transform.parent.gameObject.SetActive(true);
            TargetHealthbarHeight = TargetHealthLocation.transform.position.y;
            TargetHealthLocation.transform.parent.gameObject.SetActive(false);
        }
        else 
        {         
            TargetHealthbarHeight = TargetHealthLocation.transform.position.y;
        }
    }
}
