using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermove : MonoBehaviour
{
    public float playerMoveSpeed = 5; //�÷��̾� �̵��ӵ�
    public float gravity = -20; //�̵��� ���߿� ������°��� �����ϴ� �߷°��ӵ�
    float yVelocity; //�߷�
    CharacterController cc;
    
    void Start()
    {
        cc = gameObject.GetComponent<CharacterController>(); //ĳ����������Ʈ�� �ҷ��´�
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal"); // ����� �Է�ó��
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v); //�����̴� ���� ����
        dir.Normalize(); //�밢���� �̵��ϸ� ���̳��� ���� (����ȭ)

        dir = Camera.main.transform.TransformDirection(dir); //�ٶ󺸴� �������� �̵�
        yVelocity = gravity + Time.deltaTime; // y�࿡ �߷��� ����
        dir.y = yVelocity;

        //transform.position += dir * playerMoveSpeed * Time.deltaTime;

        cc.Move(dir * playerMoveSpeed * Time.deltaTime); //ĳ���� ������Ʈ�� �̿��� �̵�����
    }
}