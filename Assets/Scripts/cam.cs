using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    public float rotSpeed = 1000; //ī�޶��� �̵��ӵ�(�ΰ���)

    float mx = 0.0f;  //���콺 x����
    float my = 0.0f; //���콺 y����
    void Start()
    {
        
    }

    
    void Update()
    {
        float h = Input.GetAxis("Mouse X");  //���� ���콺�� x,y���� �������� ����
        float v = Input.GetAxis("Mouse Y");

        mx += v * rotSpeed * Time.deltaTime; //�������� ������ ������ ����
        my += h * rotSpeed * Time.deltaTime;

        mx = Mathf.Clamp(mx, -90, 90); // ������ ȸ���� ����
        transform.eulerAngles = new Vector3(-mx, my, 0); //ȸ���� ǥ��
    }
}
