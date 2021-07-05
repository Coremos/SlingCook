using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public float rotSpeed = 1000; //ī�޶��� �̵��ӵ�(�ΰ���)

    float mx = 0.0f;  //���콺 x����
    float my = 0.0f; //���콺 y����
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
           return;
        }
        float h = Input.GetAxis("Mouse X");  //���� ���콺�� x,y���� �������� ����
        float v = Input.GetAxis("Mouse Y");
        if (Input.GetKey(KeyCode.UpArrow))
        {
            v += .5f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            v -= .5f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            h -= .5f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            h += .5f;
        }

        mx += v * rotSpeed * Time.deltaTime; //�������� ������ ������ ����
        my += h * rotSpeed * Time.deltaTime;

        mx = Mathf.Clamp(mx, -90, 90); // ������ ȸ���� ����
        transform.eulerAngles = new Vector3(-mx, my, 0); //ȸ���� ǥ��
    }
}
