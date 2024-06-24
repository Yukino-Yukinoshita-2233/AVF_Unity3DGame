using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBoxRotationController : MonoBehaviour
{
    //GameObject cameraObject;
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    // 获取当前物体的Transform组件
    //    Transform objectTransform = this.transform;

    //    // 获取主摄像机的Transform组件
    //    Transform cameraTransform = Camera.main.transform;

    //    // 设置物体的旋转，使其始终面向摄像机
    //    objectTransform.LookAt(cameraTransform);
    //    // 可选：如果你希望物体始终保持正面朝向摄像机，可以添加以下行
    //    objectTransform.rotation = Quaternion.Euler(0, objectTransform.rotation.eulerAngles.y, 0);
    //}

    // 需要检测的目标物体
    public GameObject ZhangHengObject;
    public GameObject DialogBoxObject;
    bool DialogBoxObjectActive;

    private void Start()
    {
        DialogBoxObjectActive = false;
        DialogBoxObject.SetActive(DialogBoxObjectActive);
    }
    void Update()
    {
        // 检测鼠标点击
        if (Input.GetMouseButtonDown(0))
        {
            // 创建一个从屏幕中心发出的射线
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            // 检测射线是否碰撞到任何物体
            if (Physics.Raycast(ray, out hit))
            {
                // 检查碰撞到的物体是否是目标物体
                if (hit.collider.gameObject == ZhangHengObject)
                {
                    // 在这里执行你的操作
                    ExecuteAction();
                }
            }
        }
    }

    void ExecuteAction()
    {
        DialogBoxObjectActive = DialogBoxObjectActive == false ? true : false;
        DialogBoxObject.SetActive(DialogBoxObjectActive);
        // 你的操作代码
        Debug.Log("Target object was clicked!");
    }
}
