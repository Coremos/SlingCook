using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public float power;

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody>(); // ���� ��ü�� Rigidbody������Ʈ�� ������
        if (rigidbody != null)
        {
            Vector3 direction = collision.contacts[0].normal; // ������ ������ ����
            rigidbody.AddForce(direction * power, ForceMode.Impulse); // ���� ��ü�� ����� ����
        }
    }
}
