using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        Destroy(gameObject, gameObject.GetComponent<ParticleSystem>().duration + 1f); //�ڵ����� ������ �ȵǴ� ��ƼŬ�� 1�ʵڿ� ������ ������Ų��.
    }
}
