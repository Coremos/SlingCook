using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sling : MonoBehaviour
{
    public GameObject projectile; // 투사체 프리팹
    public Transform originTransform; // 새총의 원점
    public float maxRadius; // 최대 영역 반지름
    public float force;

    bool isShoot;
    GameObject spawned; // 현재 발사할 투사체
    Rigidbody spawnedRigidbody;
    TrailRenderer spawnedTrailRenderer;
    float pullForce; // 당기는 힘의 세기
    Vector3 clickedPosition; // 클릭이 시작된 마우스 좌표
    Vector3 mousePosition; // 현재 마우스 좌표
    KeyCode mouseShootButton = KeyCode.Mouse1; // 발사 버튼

    void Awake()
    {
        isShoot = false;
        spawned = Instantiate(projectile); // 발사체 프리팹을 객체화
        spawnedRigidbody = spawned.GetComponent<Rigidbody>();
        spawnedTrailRenderer = spawned.GetComponent<TrailRenderer>();
        spawned.SetActive(false);
    }

    void Update()
    {
        if (!spawned.activeInHierarchy)
        {
            isShoot = false;
        }
        // 현재 레벨 작업중이거나 발사체가 있을 때
        if (LevelManager.instance.states.Count > 0 || isShoot)
        {
            return;
        }
        // 왼쪽 마우스가 클릭됐을 때
        if (Input.GetKeyDown(mouseShootButton))
        {
            // 뒤로 당기는 힘 초기화
            pullForce = 0.0f;
            clickedPosition = mousePosition; // 클릭한 좌표를 저장
            clickedPosition.z = -pullForce; // z에 당기는 힘 대입
            spawned.transform.position = originTransform.position; // 발사체 위치를 원점으로 초기화
            spawnedRigidbody.velocity = Vector3.zero;
            spawnedRigidbody.useGravity = false;
            spawnedTrailRenderer.enabled = false;
            spawned.SetActive(true);
            UpdatePredict();
        }
        // 왼쪽 마우스가 클릭중일 때
        else if (Input.GetKey(mouseShootButton) && spawned.activeInHierarchy)
        {
            pullForce += 4.0f * Time.deltaTime; // 당기는 힘 추가
            if (pullForce > maxRadius) // 당기는 힘이 최대치를 넘었을 때
            {
                pullForce = maxRadius; // 당기는 힘을 최대로
            }
            mousePosition.z = -pullForce; // 현재 마우스당기는 힘
            Vector3 v = mousePosition - clickedPosition; // 위치의 차이를 대입
            v = Vector3.ClampMagnitude(v, maxRadius); // 차이가 maxRadius를 넘어서지 않도록 클램핑
            spawned.transform.position = originTransform.position + v; // 발사체의 좌표를 클램핑된 값만큼 원점좌표에서 덧셈
            UpdatePredict();
        }
        // 왼쪽 마우스가 놓아졌을 때
        else if (Input.GetKeyUp(mouseShootButton))
        {
            Prediction.instance.OffPredict();
            spawnedRigidbody.useGravity = true;
            spawnedTrailRenderer.enabled = true;
            isShoot = true;
            spawnedRigidbody.AddForce(CalculateForce(), ForceMode.Impulse); // 투사체에 힘을 가산
        }
    }

    Vector3 CalculateForce()
    {
        Vector3 currentMousePosition = mousePosition; // 현재 마우스 좌표를 저장
        currentMousePosition.z = -pullForce; // z에 당기는 힘 대입
        //Vector3 direction = clickedPosition - currentMousePosition; // 좌표의 차이로 방향 좌표 계산
        Vector3 direction = transform.forward; // 좌표의 차이로 방향 좌표 계산
        return direction * force;
    }

    private void FixedUpdate()
    {
        // 마우스의 좌표를 mousePosition에 저장
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
    }

    void UpdatePredict()
    {
        Prediction.instance.Predict(projectile, spawned.transform.position, CalculateForce());
    }
}