using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Feeder : MonoBehaviour
{
    public GameObject ShuttlePrefab;
    public Transform SpawnPoint;

    public EShotType ShotType = EShotType.Clear;
    public EShotType ShotTypeFilters = EShotType.Smash;
    public List<int> PositionFilters;
    public List<Transform> Positions;

    public ShotData ClearData;
    public ShotData SmashData;

    public Slider TimingIntervalSlider;

    public Transform TargetTransform;

    public Slider AimOffsetSlider;
    public Slider HeightOffsetSlider;

    private void Start()
    {
        StartCoroutine(Run());
    }

    private void Update()
    {
        Application.runInBackground = true;

        if (Input.GetKeyDown(KeyCode.P))
        {
            Fire();
        }
    }

    public void Toggle(EShotType shotType)
    {
        ShotTypeFilters ^= shotType;
    }

    private IEnumerator Run()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimingIntervalSlider.value);
            Fire();
        }
    }

    [ContextMenu("Fire")]
    public void Fire()
    {
        // Choose between 6 corners. 
        // Just calculate the power required for each shot based on distance.
        // Get the shot types.
        // get the index 
        var selectedIndex = PositionFilters[Random.Range(0, PositionFilters.Count)];
        var selectedLocation = Positions[selectedIndex];

        TargetTransform.position = selectedLocation.position;

        var availableShots = Enum.GetValues(typeof(EShotType))
            .Cast<EShotType>()
            .Where(c => ShotTypeFilters.HasFlag(c))    // or use HasFlag in .NET4
            .ToArray();

        if (availableShots.Length == 0)
            return;

        ShotType = availableShots[Random.Range(0, availableShots.Length)];

        var shuttleInstance = Instantiate(ShuttlePrefab, SpawnPoint.position, SpawnPoint.rotation);
        var shuttle = shuttleInstance.GetComponent<Shuttle>();
        var shuttleRigidbody = shuttle.GetComponent<Rigidbody>();

        var power = Vector3.Distance(TargetTransform.position, SpawnPoint.position);

        switch (ShotType)
        {
            case EShotType.Clear:

                // 6 meters clear target?
                var clearPosition = TargetTransform.position + Vector3.up * (6 + HeightOffsetSlider.value);
                clearPosition += Random.insideUnitSphere.normalized * AimOffsetSlider.value;

                var clearDirection = clearPosition - SpawnPoint.position;
                clearDirection.Normalize();

                // power base on distance?
                shuttleRigidbody.velocity = clearDirection * power * ClearData.Power;

                break;
            case EShotType.Smash:

                var smashPosition = TargetTransform.position + Vector3.up * (2 + HeightOffsetSlider.value);
                smashPosition += Random.insideUnitSphere.normalized * AimOffsetSlider.value;

                var smashDirection = smashPosition - SpawnPoint.position;
                smashDirection.Normalize();

                shuttleRigidbody.velocity = smashDirection * power * SmashData.Power;

                break;
        }
    }

    private float GetDuration()
    {
        switch (ShotType)
        {
            case EShotType.Tap:
                return 2;
            case EShotType.Clear:
                return 3;
            case EShotType.Smash:
                return 1;
        }

        return 1;
    }



    [Flags]
    public enum EShotType
    {
        Tap = 1 << 0,
        Flat= 1 << 1,
        Clear = 1 << 2,
        Smash = 1 << 3
    }

    [Serializable]
    public class ShotData
    {
        public float Power => PowerSlider.value;
        public Slider PowerSlider;
    }

    public void TogglePosition(int index)
    {
        if (PositionFilters.Contains(index))
            PositionFilters.Remove(index);
        else
            PositionFilters.Add(index);
    }
}
