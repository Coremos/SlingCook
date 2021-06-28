using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class tablemove : MonoBehaviour
{
    public float starTime; //���� �����̴� �ð�
    public float minX, maxX; //�� �ּ�,�ִ� x��ǥ

    [Range(1,100)] //�ν�����â ���� �����̵�� ���������ϰ���
    public float moveSpeed; //�� �ӵ�
    private int sign = -1;

    void Update()
    {
        if(Time.time >= starTime)
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime * sign, 0, 0); //���ӵ��� ����� ����̵�
           
            if(transform.position.x <= minX || transform.position.x >= maxX)  //��� �̵��� �ǰ� �ּҰ����� �۰� �ִ밪���� ũ�� �·��̵�
            {
                sign *= -1;
            }
        }
    }
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "bullet") //target�̶�� �±� �������v �浹�Ұ��
        {
            
            ScoreManager.scoreValue -= 10; //ScoreManager������ scoreValue������ �ҷ��� 10�� �����Ѵ�.
        }
    }
}
