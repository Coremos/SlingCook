using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int scoreValue; // ����
    Text text; 
    void Awake()
    {
        text = GetComponent<Text>(); //�ؽ�Ʈ ������Ʈ �ҷ�����
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Score: " + scoreValue; //������Ʈ�� �̿��� ���������� �����ش�
    }
}
