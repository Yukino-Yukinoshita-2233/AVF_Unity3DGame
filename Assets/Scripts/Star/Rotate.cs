using System.Collections.Generic;
using UnityEngine;


namespace StarRotate
{
    public class Rotate : MonoBehaviour
    {
        public Transform centerPoint; // 中心点
        public float rotationSpeed = 10f; // 自转速度
        public float orbitSpeed = 20f; // 公转速度
        public float distanceFromCenter = 5f; // 距离中心点的距离

        void Update()
        {
            OnRenderObject();
        }

        private void OnRenderObject()
        {
            // 自转
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

            // 计算围绕中心点的位置
            Vector3 orbitPosition = (transform.position - centerPoint.position).normalized * distanceFromCenter + centerPoint.position;

            // 更新对象位置
            transform.position = orbitPosition;

            // 公转
            if (centerPoint != null)
            {
                transform.RotateAround(centerPoint.position, Vector3.up, orbitSpeed * Time.deltaTime);
            }

        }
    }

}
