using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using LevelData;

public class Sun : MonoBehaviour
{
    float springSolsticeDay = 79;        // 春分：通常在一年中的第79至80天（即3月20日或21日）
    float summerSolsticeDay = 171;        // 夏至：通常在一年中的第171至172天（即6月20日或21日）
    float autumnSolsticeDay = 264;        // 秋分：通常在一年中的第264至265天（即9月22日或23日）
    float winterSolsticeDay = 355;        // 冬至：通常在一年中的第355至356天（即12月21日或22日）
    //Debug.Log(" : " + );
    LV1Maneger lv1Maneger;

    public Transform sunTransform; // 定向光源对象的Transform组件
    public Transform sunTransformson;
    [SerializeField] private Vector3 initialRotation; // 初始旋转角度
    [SerializeField] private Vector3 initialRotationson; // 初始旋转角度
    [SerializeField] public static float TimeofDay = 0;//24小时时间
    [SerializeField] public static float TimeofYear = 0;//356日时间
    public float rotationSpeed = 10f; // 旋转速度
    void Start()
    {
        // 保存初始旋转角度
        initialRotation = sunTransform.localEulerAngles;
        initialRotationson = sunTransformson.localEulerAngles;
    }

    private void Update()
    {
        RotateSunDay(TimeofDay);
        RotateSunYear(TimeofYear);
    }
    public void RotateSunDay(float timeOfDay)
    {
        // 计算时间在一天中的百分比
        float currentTimeOfDay = Mathf.Repeat(timeOfDay, 24f) / 24f;
        // 根据时间计算太阳的旋转角度（从东升到西落）
        float rotationAngle = Mathf.Lerp(0f, 360f, currentTimeOfDay) + 180;
        // 应用旋转角度到定向光源对象
        float currentAngle = Mathf.MoveTowardsAngle(sunTransform.eulerAngles.z, rotationAngle, rotationSpeed * Time.deltaTime);
        sunTransform.rotation = Quaternion.Euler(initialRotation.x, initialRotation.y, currentAngle);

    }
    public void RotateSunYear(float timeOfYear)
    {
        //将 timeOfYear 限制在一年的范围内
        timeOfYear = Mathf.Repeat(timeOfYear, 365f);

        float rotationAngle = 0f;

        if (timeOfYear >= springSolsticeDay && timeOfYear < summerSolsticeDay)
        {
            // 春至到夏至期间
            rotationAngle = Mathf.Lerp(50f, 60f, (timeOfYear - springSolsticeDay) / (summerSolsticeDay - springSolsticeDay));
        }
        else if (timeOfYear >= summerSolsticeDay && timeOfYear < autumnSolsticeDay)
        {
            // 夏至到秋至期间
            rotationAngle = Mathf.Lerp(60f, 50f, (timeOfYear - summerSolsticeDay) / (autumnSolsticeDay - summerSolsticeDay));
        }
        else if (timeOfYear >= autumnSolsticeDay && timeOfYear < winterSolsticeDay)
        {
            // 秋至到冬至期间
            rotationAngle = Mathf.Lerp(50f, 40f, (timeOfYear - autumnSolsticeDay) / (winterSolsticeDay - autumnSolsticeDay));
        }
        else if (timeOfYear < springSolsticeDay)
        {
            // 新年到春分期间
            rotationAngle = Mathf.Lerp(40f, 50f, (timeOfYear +365 - winterSolsticeDay) / (365 - winterSolsticeDay + springSolsticeDay));
        }
        else if (timeOfYear >= winterSolsticeDay)
        {
            // 冬至到新年期间
            rotationAngle = Mathf.Lerp(40f, 50f, (timeOfYear - winterSolsticeDay) / (365 - winterSolsticeDay + springSolsticeDay));
        }
        //Debug.Log(rotationAngle);
        // 应用旋转角度到定向光源对象
        float currentAngle = Mathf.MoveTowardsAngle(sunTransformson.localEulerAngles.x, rotationAngle, rotationSpeed *0.05f * Time.deltaTime);

        // 将旋转应用到物体的本地坐标系上
        sunTransformson.localRotation = Quaternion.Euler(currentAngle, sunTransformson.localEulerAngles.y, sunTransformson.localEulerAngles.z);
    }

}

