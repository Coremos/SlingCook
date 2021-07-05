using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public float playerMoveSpeed = 5; //플레이어 이동속도
    public float gravity = -20; //이동중 공중에 띄워지는것을 방지하는 중력가속도
    float yVelocity; //중력
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
        cc = gameObject.GetComponent<CharacterController>(); //캐릭터컴포넌트를 불러온다
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal"); // 사용자 입력처리
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v); //움직이는 방향 제한
        dir.Normalize(); //대각으로 이동하면 길이남을 방지 (정규화)

        dir = Camera.main.transform.TransformDirection(dir); //바라보는 방향으로 이동
        yVelocity = gravity + Time.deltaTime; // y축에 중력을 적용
        dir.y = yVelocity;

        //transform.position += dir * playerMoveSpeed * Time.deltaTime;

        cc.Move(dir * playerMoveSpeed * Time.deltaTime); //캐릭터 컴포넌트를 이용해 이동구현
    }
}
