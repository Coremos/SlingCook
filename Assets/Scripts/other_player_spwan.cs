using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class other_player_spwan : MonoBehaviour
{
    public float timer = 0.0f; //����Ÿ�̸�
    public int waitingTime = 5; //������ �ð�
    public List<GameObject> otherplayers = new List<GameObject>();
    private int randspwan; //���������� �ϱ����� ����������
    public int maxpeople = 5; //�ִ� �մԼ�
    public static int count = 0; //�մԼ� ī��Ʈ
    private void Start()
    {
        Spwan();
    }
    void Update()
    {
        Spwan();
    }
    void Spwan()
    {
        timer += Time.deltaTime;
        if (timer > waitingTime)
        {
            randspwan = Random.Range(0, otherplayers.Count); //����Ǿ� �ִ� �������� ����������

            if (count < maxpeople)
            {
                Instantiate(otherplayers[randspwan]); //������Ʈ ����
               
                count++;
            }

            
           
                timer = 0;
            
        }
    }
}
