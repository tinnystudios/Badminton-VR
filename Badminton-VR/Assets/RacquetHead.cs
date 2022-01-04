using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RacquetHead : MonoBehaviour
{
    public VelocityEstimator VelocityEstimator;

    public float StrengthFactor = 1;
    public Slider StrengthSlider;

    private void OnTriggerEnter(Collider col)
    {
        StrengthFactor = StrengthSlider.value;

        var shuttle = col.GetComponentInParent<Shuttle>();
        if (shuttle != null && !shuttle.Done)
        {
            Debug.Log("Hit shuttle");
            // for now just take the angle of this
            var shuttleRigidbody = shuttle.GetComponent<Rigidbody>();
            shuttleRigidbody.velocity =  VelocityEstimator.Velocity * StrengthFactor;
            shuttle.Done = true;
        }
    }
}