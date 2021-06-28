using UnityEngine;

// 상속용 싱글톤 클래스, 제네릭을 where절을 이용해 MonoBehavior를 상속받도록 제약
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public bool keepAlive = false; // 다른 씬에서도 유효한지 여부
    public static T instance
    {
        get
        {
            // 인스턴스가 없을 때
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>(); // T 클래스의 객체를 검색
                if (_instance == null) // 객체가 없을 때
                {
                    var singletonObject = new GameObject(); // 게임 오브젝트를 생성
                    singletonObject.name = typeof(T).ToString(); // 게임 오브젝트의 이름을 T로 대입
                    _instance = singletonObject.AddComponent<T>(); // 게임 오브젝트에 T 클래스 컴포넌트를 추가해 싱글톤 객체에 대입
                }
            }
            return _instance; // 싱글톤 객체 반환
        }
    }

    private static T _instance = null; // 싱글톤 객체

    static public bool isInstanceAlive
    {
        get { return _instance != null; }
    }


    public virtual void Awake()
    {
        if (_instance != null) // 싱글톤 객체가 이미 있는 경우
        {
            Destroy(gameObject); // 싱글톤 객체를 파괴
            return;
        }

        _instance = GetComponent<T>(); // T 클래스를 싱글톤 객체에 대입

        if (keepAlive) // 다른 씬에서도 유효할 때
        {
            DontDestroyOnLoad(gameObject); // 씬을 이동해도 파괴되지 않도록 설정
        }

        if (_instance == null)
        {
            return;
        }
    }

}
