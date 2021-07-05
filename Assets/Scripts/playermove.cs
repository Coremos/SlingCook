using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public float playerMoveSpeed = 5; //�÷��̾� �̵��ӵ�
    public float gravity = -20; //�̵��� ���߿� ������°��� �����ϴ� �߷°��ӵ�
    float yVelocity; //�߷�
    CharacterController cc;

    public static playermove Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

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
