using UnityEngine;

// ��ӿ� �̱��� Ŭ����, ���׸��� where���� �̿��� MonoBehavior�� ��ӹ޵��� ����
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public bool keepAlive = false; // �ٸ� �������� ��ȿ���� ����
    public static T instance
    {
        get
        {
            // �ν��Ͻ��� ���� ��
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>(); // T Ŭ������ ��ü�� �˻�
                if (_instance == null) // ��ü�� ���� ��
                {
                    var singletonObject = new GameObject(); // ���� ������Ʈ�� ����
                    singletonObject.name = typeof(T).ToString(); // ���� ������Ʈ�� �̸��� T�� ����
                    _instance = singletonObject.AddComponent<T>(); // ���� ������Ʈ�� T Ŭ���� ������Ʈ�� �߰��� �̱��� ��ü�� ����
                }
            }
            return _instance; // �̱��� ��ü ��ȯ
        }
    }

    private static T _instance = null; // �̱��� ��ü

    static public bool isInstanceAlive
    {
        get { return _instance != null; }
    }


    public virtual void Awake()
    {
        if (_instance != null) // �̱��� ��ü�� �̹� �ִ� ���
        {
            Destroy(gameObject); // �̱��� ��ü�� �ı�
            return;
        }

        _instance = GetComponent<T>(); // T Ŭ������ �̱��� ��ü�� ����

        if (keepAlive) // �ٸ� �������� ��ȿ�� ��
        {
            DontDestroyOnLoad(gameObject); // ���� �̵��ص� �ı����� �ʵ��� ����
        }

        if (_instance == null)
        {
            return;
        }
    }

}
