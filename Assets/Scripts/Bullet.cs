using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float DestroyTime = 7.0f; // �ڵ����� �Ѿ��� ������� �ð�
    public GameObject explosion;

    void Start()
    {
        Destroy(gameObject, DestroyTime);
    }
    
   
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "target") //target�̶�� �±� �������v �浹�Ұ��
        {
            Debug.Log("�浹");
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(coll.gameObject); //�浹�� ��ü�� ���ش�
            Destroy(gameObject); //�浹�� �Ѿ��� ���ش�
            ScoreManager.scoreValue += 10; //ScoreManager������ scoreValue������ �ҷ��� 10�� �߰��Ѵ�.
        }
    }
}
