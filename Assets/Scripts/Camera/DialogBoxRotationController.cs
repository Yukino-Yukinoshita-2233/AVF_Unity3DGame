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
    //    // ��ȡ��ǰ�����Transform���
    //    Transform objectTransform = this.transform;

    //    // ��ȡ���������Transform���
    //    Transform cameraTransform = Camera.main.transform;

    //    // �����������ת��ʹ��ʼ�����������
    //    objectTransform.LookAt(cameraTransform);
    //    // ��ѡ�������ϣ������ʼ�ձ������泯����������������������
    //    objectTransform.rotation = Quaternion.Euler(0, objectTransform.rotation.eulerAngles.y, 0);
    //}

    // ��Ҫ����Ŀ������
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
        // ��������
        if (Input.GetMouseButtonDown(0))
        {
            // ����һ������Ļ���ķ���������
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            // ��������Ƿ���ײ���κ�����
            if (Physics.Raycast(ray, out hit))
            {
                // �����ײ���������Ƿ���Ŀ������
                if (hit.collider.gameObject == ZhangHengObject)
                {
                    // ������ִ����Ĳ���
                    ExecuteAction();
                }
            }
        }
    }

    void ExecuteAction()
    {
        DialogBoxObjectActive = DialogBoxObjectActive == false ? true : false;
        DialogBoxObject.SetActive(DialogBoxObjectActive);
        // ��Ĳ�������
        Debug.Log("Target object was clicked!");
    }
}
