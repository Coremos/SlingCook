using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class tablemove : MonoBehaviour
{
    public float starTime; //블럭이 움직이는 시간
    public float minX, maxX; //블럭 최소,최대 x좌표

    [Range(1,100)] //인스펙터창 값을 슬라이드로 조절가능하게함
    public float moveSpeed; //블럭 속도
    private int sign = -1;

    void Update()
    {
        if(Time.time >= starTime)
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime * sign, 0, 0); //블럭속도에 비례해 우로이동
           
            if(transform.position.x <= minX || transform.position.x >= maxX)  //우로 이동이 되고 최소값보다 작고 최대값보다 크면 좌로이동
            {
                sign *= -1;
            }
        }
    }
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "bullet") //target이라는 태그 오브젝틍 충돌할경우
        {
            
            ScoreManager.scoreValue -= 10; //ScoreManager파일의 scoreValue변수를 불러와 10을 감소한다.
        }
    }
}
