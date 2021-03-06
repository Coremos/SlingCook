using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public float rotSpeed = 1000; //카메라의 이동속도(민감도)

    float mx = 0.0f;  //마우스 x각도
    float my = 0.0f; //마우스 y각도
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
           return;
        }
        float h = Input.GetAxis("Mouse X");  //현재 마우스의 x,y축의 움직임을 감지
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

        mx += v * rotSpeed * Time.deltaTime; //움직임을 감지된 각도를 누적
        my += h * rotSpeed * Time.deltaTime;

        mx = Mathf.Clamp(mx, -90, 90); // 상하의 회전각 제한
        transform.eulerAngles = new Vector3(-mx, my, 0); //회전을 표현
    }
}
