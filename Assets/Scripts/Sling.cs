using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sling : MonoBehaviour
{
    public GameObject projectile; // ����ü ������
    public Transform originTransform; // ������ ����
    public float maxRadius; // �ִ� ���� ������
    public float force;

    bool isShoot;
    GameObject spawned; // ���� �߻��� ����ü
    Rigidbody spawnedRigidbody;
    TrailRenderer spawnedTrailRenderer;
    float pullForce; // ���� ���� ����
    Vector3 clickedPosition; // Ŭ���� ���۵� ���콺 ��ǥ
    Vector3 mousePosition; // ���� ���콺 ��ǥ
    KeyCode mouseShootButton = KeyCode.Mouse1; // �߻� ��ư

    void Awake()
    {
        isShoot = false;
        spawned = Instantiate(projectile); // �߻�ü �������� ��üȭ
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
        // ���� ���� �۾����̰ų� �߻�ü�� ���� ��
        if (LevelManager.instance.states.Count > 0 || isShoot)
        {
            return;
        }
        // ���� ���콺�� Ŭ������ ��
        if (Input.GetKeyDown(mouseShootButton))
        {
            // �ڷ� ���� �� �ʱ�ȭ
            pullForce = 0.0f;
            clickedPosition = mousePosition; // Ŭ���� ��ǥ�� ����
            clickedPosition.z = -pullForce; // z�� ���� �� ����
            spawned.transform.position = originTransform.position; // �߻�ü ��ġ�� �������� �ʱ�ȭ
            spawnedRigidbody.velocity = Vector3.zero;
            spawnedRigidbody.useGravity = false;
            spawnedTrailRenderer.enabled = false;
            spawned.SetActive(true);
            UpdatePredict();
        }
        // ���� ���콺�� Ŭ������ ��
        else if (Input.GetKey(mouseShootButton) && spawned.activeInHierarchy)
        {
            pullForce += 4.0f * Time.deltaTime; // ���� �� �߰�
            if (pullForce > maxRadius) // ���� ���� �ִ�ġ�� �Ѿ��� ��
            {
                pullForce = maxRadius; // ���� ���� �ִ��
            }
            mousePosition.z = -pullForce; // ���� ���콺���� ��
            Vector3 v = mousePosition - clickedPosition; // ��ġ�� ���̸� ����
            v = Vector3.ClampMagnitude(v, maxRadius); // ���̰� maxRadius�� �Ѿ�� �ʵ��� Ŭ����
            spawned.transform.position = originTransform.position + v; // �߻�ü�� ��ǥ�� Ŭ���ε� ����ŭ ������ǥ���� ����
            UpdatePredict();
        }
        // ���� ���콺�� �������� ��
        else if (Input.GetKeyUp(mouseShootButton))
        {
            Prediction.instance.OffPredict();
            spawnedRigidbody.useGravity = true;
            spawnedTrailRenderer.enabled = true;
            isShoot = true;
            spawnedRigidbody.AddForce(CalculateForce(), ForceMode.Impulse); // ����ü�� ���� ����
        }
    }

    Vector3 CalculateForce()
    {
        Vector3 currentMousePosition = mousePosition; // ���� ���콺 ��ǥ�� ����
        currentMousePosition.z = -pullForce; // z�� ���� �� ����
        //Vector3 direction = clickedPosition - currentMousePosition; // ��ǥ�� ���̷� ���� ��ǥ ���
        Vector3 direction = transform.forward; // ��ǥ�� ���̷� ���� ��ǥ ���
        return direction * force;
    }

    private void FixedUpdate()
    {
        // ���콺�� ��ǥ�� mousePosition�� ����
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
    }

    void UpdatePredict()
    {
        Prediction.instance.Predict(projectile, spawned.transform.position, CalculateForce());
    }
}