using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 10f; // 自转速度
    public float orbitSpeed = 20f; // 公转速度
    public float distanceFromCenter = 5f; // 距离中心点的距离
    public Transform objectToOrbit; // 围绕公转的对象

    private Transform centerPoint; // 中心点

    private void Start()
    {
        centerPoint = transform.Find("Galaxy");
    }

    void Update()
    {
        // 自转
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // 计算围绕中心点的位置
        Vector3 orbitPosition = (transform.position - centerPoint.position).normalized * distanceFromCenter + centerPoint.position;

        // 更新对象位置
        transform.position = orbitPosition;

        // 公转
        if (objectToOrbit != null)
        {
            transform.RotateAround(centerPoint.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }
    }
}
