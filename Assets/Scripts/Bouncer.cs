using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public float power;

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody>(); // 닿은 객체의 Rigidbody컴포넌트를 가져옴
        if (rigidbody != null)
        {
            Vector3 direction = collision.contacts[0].normal; // 접점의 방향을 대입
            rigidbody.AddForce(direction * power, ForceMode.Impulse); // 닿은 객체에 충격을 가산
        }
    }
}
