/*
TargetHealthbar
by Steven Pichelman
10/26/2024
manages single enemy healthbar functionality
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetHealthbar : MonoBehaviour
{
    Ray LookingAt;
    [SerializeField] Slider SliderTargetHealth;
    [SerializeField] public Enemy Enemy;
    float DetectionRange = 1000;
    int counter = 0;

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
               // SliderTargetHealth.gameObject.SetActive(true);
                SliderTargetHealth.gameObject.transform.position = new Vector2(SliderTargetHealth.gameObject.transform.position.x, 488.3f);


                //this is a ratio:       32/64 is 50%. also shows min of 1/64th health
                SliderTargetHealth.value = Math.Max(Enemy.HP * 64 / Enemy.MaxHP, 1);
            }
            else
            {
                // i think this causes lag.
                //SliderTargetHealth.gameObject.SetActive(false);
               SliderTargetHealth.gameObject.transform.position = new Vector2(SliderTargetHealth.gameObject.transform.position.x, 588.3f);

            }
        }
            counter = 0;
        }
            counter++;
    }
}
