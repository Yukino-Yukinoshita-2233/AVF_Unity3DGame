using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Transform centerPoint; // ���ĵ�
    public float rotationSpeed = 10f; // ��ת�ٶ�
    public float orbitSpeed = 20f; // ��ת�ٶ�
    public float distanceFromCenter = 5f; // �������ĵ�ľ���
    public Transform objectToOrbit; // Χ�ƹ�ת�Ķ���

    void Update()
    {
        // ��ת
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // ����Χ�����ĵ��λ��
        Vector3 orbitPosition = (transform.position - centerPoint.position).normalized * distanceFromCenter + centerPoint.position;

        // ���¶���λ��
        transform.position = orbitPosition;

        // ��ת
        if (objectToOrbit != null)
        {
            transform.RotateAround(centerPoint.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }
    }
}
