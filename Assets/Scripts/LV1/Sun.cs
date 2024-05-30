using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using LevelData;

public class Sun : MonoBehaviour
{
    private const float SpringSolsticeDay = 79;
    private const float SummerSolsticeDay = 171;
    private const float AutumnSolsticeDay = 264;
    private const float WinterSolsticeDay = 355;

    public Transform SunTransform;
    public Transform SunTransformSon;
    public static float TimeOfDay = 0;
    public static float TimeOfYear = 0;
    [SerializeField] private float SetSunTimeOfDay = 0;
    [SerializeField] private float SetSunTimeOfYear = 0;
    public float RotationSpeed = 10f;

    private Vector3 _initialRotation;
    private Vector3 _initialRotationSon;

    private void Start()
    {
        _initialRotation = SunTransform.localEulerAngles;
        _initialRotationSon = SunTransformSon.localEulerAngles;
    }

    private void Update()
    {
        //TimeOfDay = SetSunTimeOfDay;
        //TimeOfYear = SetSunTimeOfYear;
        RotateSunDay(TimeOfDay);
        RotateSunYear(TimeOfYear);

    }

    private void RotateSunDay(float timeOfDay)
    {
        float currentTimeOfDay = Mathf.Repeat(timeOfDay, 24f) / 24f;
        float rotationAngle = Mathf.Lerp(0f, 360f, currentTimeOfDay) + 180;
        float currentAngle = Mathf.MoveTowardsAngle(SunTransform.eulerAngles.z, rotationAngle, RotationSpeed * Time.deltaTime);
        SunTransform.rotation = Quaternion.Euler(_initialRotation.x, _initialRotation.y, currentAngle);
    }

    private void RotateSunYear(float timeOfYear)
    {
        timeOfYear = Mathf.Repeat(timeOfYear, 365f);
        float rotationAngle = GetSeasonalRotationAngle(timeOfYear);
        float currentAngle = Mathf.MoveTowardsAngle(SunTransformSon.localEulerAngles.x, rotationAngle, RotationSpeed * 0.05f * Time.deltaTime);
        SunTransformSon.localRotation = Quaternion.Euler(currentAngle, SunTransformSon.localEulerAngles.y, SunTransformSon.localEulerAngles.z);
    }

    private float GetSeasonalRotationAngle(float timeOfYear)
    {
        if (timeOfYear >= SpringSolsticeDay && timeOfYear < SummerSolsticeDay)
        {
            return Mathf.Lerp(50f, 60f, (timeOfYear - SpringSolsticeDay) / (SummerSolsticeDay - SpringSolsticeDay));
        }
        if (timeOfYear >= SummerSolsticeDay && timeOfYear < AutumnSolsticeDay)
        {
            return Mathf.Lerp(60f, 50f, (timeOfYear - SummerSolsticeDay) / (AutumnSolsticeDay - SummerSolsticeDay));
        }
        if (timeOfYear >= AutumnSolsticeDay && timeOfYear < WinterSolsticeDay)
        {
            return Mathf.Lerp(50f, 40f, (timeOfYear - AutumnSolsticeDay) / (WinterSolsticeDay - AutumnSolsticeDay));
        }
        if (timeOfYear < SpringSolsticeDay)
        {
            return Mathf.Lerp(40f, 50f, (timeOfYear + 365 - WinterSolsticeDay) / (365 - WinterSolsticeDay + SpringSolsticeDay));
        }
        return Mathf.Lerp(40f, 50f, (timeOfYear - WinterSolsticeDay) / (365 - WinterSolsticeDay + SpringSolsticeDay));
    }
}
