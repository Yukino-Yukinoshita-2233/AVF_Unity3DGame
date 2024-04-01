using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using LevelData;

public class Sun : MonoBehaviour
{
    float springSolsticeDay = 79;        // ���֣�ͨ����һ���еĵ�79��80�죨��3��20�ջ�21�գ�
    float summerSolsticeDay = 171;        // ������ͨ����һ���еĵ�171��172�죨��6��20�ջ�21�գ�
    float autumnSolsticeDay = 264;        // ��֣�ͨ����һ���еĵ�264��265�죨��9��22�ջ�23�գ�
    float winterSolsticeDay = 355;        // ������ͨ����һ���еĵ�355��356�죨��12��21�ջ�22�գ�
    //Debug.Log(" : " + );
    LV1Maneger lv1Maneger;

    public Transform sunTransform; // �����Դ�����Transform���
    public Transform sunTransformson;
    [SerializeField] private Vector3 initialRotation; // ��ʼ��ת�Ƕ�
    [SerializeField] private Vector3 initialRotationson; // ��ʼ��ת�Ƕ�
    [SerializeField] public static float TimeofDay = 0;//24Сʱʱ��
    [SerializeField] public static float TimeofYear = 0;//356��ʱ��
    public float rotationSpeed = 10f; // ��ת�ٶ�
    void Start()
    {
        // �����ʼ��ת�Ƕ�
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
        // ����ʱ����һ���еİٷֱ�
        float currentTimeOfDay = Mathf.Repeat(timeOfDay, 24f) / 24f;
        // ����ʱ�����̫������ת�Ƕȣ��Ӷ��������䣩
        float rotationAngle = Mathf.Lerp(0f, 360f, currentTimeOfDay) + 180;
        // Ӧ����ת�Ƕȵ������Դ����
        float currentAngle = Mathf.MoveTowardsAngle(sunTransform.eulerAngles.z, rotationAngle, rotationSpeed * Time.deltaTime);
        sunTransform.rotation = Quaternion.Euler(initialRotation.x, initialRotation.y, currentAngle);

    }
    public void RotateSunYear(float timeOfYear)
    {
        //�� timeOfYear ������һ��ķ�Χ��
        timeOfYear = Mathf.Repeat(timeOfYear, 365f);

        float rotationAngle = 0f;

        if (timeOfYear >= springSolsticeDay && timeOfYear < summerSolsticeDay)
        {
            // �����������ڼ�
            rotationAngle = Mathf.Lerp(50f, 60f, (timeOfYear - springSolsticeDay) / (summerSolsticeDay - springSolsticeDay));
        }
        else if (timeOfYear >= summerSolsticeDay && timeOfYear < autumnSolsticeDay)
        {
            // �����������ڼ�
            rotationAngle = Mathf.Lerp(60f, 50f, (timeOfYear - summerSolsticeDay) / (autumnSolsticeDay - summerSolsticeDay));
        }
        else if (timeOfYear >= autumnSolsticeDay && timeOfYear < winterSolsticeDay)
        {
            // �����������ڼ�
            rotationAngle = Mathf.Lerp(50f, 40f, (timeOfYear - autumnSolsticeDay) / (winterSolsticeDay - autumnSolsticeDay));
        }
        else if (timeOfYear < springSolsticeDay)
        {
            // ���굽�����ڼ�
            rotationAngle = Mathf.Lerp(40f, 50f, (timeOfYear +365 - winterSolsticeDay) / (365 - winterSolsticeDay + springSolsticeDay));
        }
        else if (timeOfYear >= winterSolsticeDay)
        {
            // �����������ڼ�
            rotationAngle = Mathf.Lerp(40f, 50f, (timeOfYear - winterSolsticeDay) / (365 - winterSolsticeDay + springSolsticeDay));
        }
        //Debug.Log(rotationAngle);
        // Ӧ����ת�Ƕȵ������Դ����
        float currentAngle = Mathf.MoveTowardsAngle(sunTransformson.localEulerAngles.x, rotationAngle, rotationSpeed *0.05f * Time.deltaTime);

        // ����תӦ�õ�����ı�������ϵ��
        sunTransformson.localRotation = Quaternion.Euler(currentAngle, sunTransformson.localEulerAngles.y, sunTransformson.localEulerAngles.z);
    }

}

