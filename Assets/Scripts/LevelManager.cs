using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    public enum State { None, Move, Fill, Pop };
    public Text text;
    public GameObject block; // 블럭 프리팹
    public List<MaterialType> materialPool = new List<MaterialType>(); // 등장 재료
    public Dictionary<MaterialType, int> materialValue = new Dictionary<MaterialType, int>(); // 재료 개수
    public List<State> states = new List<State>(); // 현재 처리과정
    public bool turn;
    public int popCount;
    public int money;

    const int MAXBLOCK = 6; // 최대 가로세로 길이
    static GameObject[,] blocks = new GameObject[MAXBLOCK, MAXBLOCK]; // 스테이지의 블럭들
    List<GameObject> blockPool = new List<GameObject>();

    void Start()
    {
        materialPool.Add(MaterialType.Dough);
        materialPool.Add(MaterialType.Cream);
        materialPool.Add(MaterialType.Strawberry);
        materialPool.Add(MaterialType.Chocolate);
        materialPool.Add(MaterialType.Blueberry);
        materialPool.Add(MaterialType.Lime);
        InitializeMaterialValue(); // 재료 개수 초기화
        InitializeBlock(); // 스테이지의 블럭 초기화
    }

    void InitializeMaterialValue()
    {
        materialValue.Clear(); // 재료 개수 초기화
        for (int i = 0; i < materialPool.Count; i++) // 스테이지에서 사용하는 재료들의 종류만큼 반복
        {
            materialValue.Add(materialPool[i], 10); // 재료 개수의 
        }
    }

    void InitializeBlock()
    {
        // 2차원으로 MAXBLOCK * MAXBLOCK 만큼반복
        for (int y = 0; y < MAXBLOCK; y++)
        {
            for (int x = 0; x < MAXBLOCK; x++)
            {
                var dummy = Instantiate(block); // 블럭 생성
                dummy.transform.parent = transform; // 블럭의 부모를 현재 객체로 대입
                dummy.transform.position = new Vector3(-10f + 4 * x, 0f + 4 * y, 20); // 블럭의 위치를 설정
                blocks[x, y] = dummy; // 블럭을 좌표에 연결
            }
        }
    }

    public void ReturnBlock(GameObject block)
    {
        block.SetActive(false);
        for (int y = 0; y < MAXBLOCK; y++)
        {
            for (int x = 0; x < MAXBLOCK; x++)
            {
                if (blocks[x, y] == block)
                {
                    blocks[x, y] = null;
                }
            }
        }
        blockPool.Add(block);
    }

    void Update()
    {
        //UpdateText();
    }

    void UpdateText()
    {
        string dummyText = "";
        for (int i = 0; i < materialPool.Count; i++)
        {
            dummyText += materialPool[i] + " * " + materialValue[materialPool[i]] + " ";
        }
        text.text = dummyText;
    }

    WaitForSeconds wait = new WaitForSeconds(0.1f);

    IEnumerator UpdateStage()
    {
        int combo = -1;
        turn = false;
        do
        {
            // yield return을 붙임으로써 앞의 코루틴이 모두 끝날 때까지 대기
            states.Clear();
            yield return StartCoroutine(CheckEmptyBlock());
            while (states.Count > 0) // 이동 완료 대기
            {
                yield return null;
            }
            yield return StartCoroutine(FillEmptyBlock());
            yield return wait;
            yield return StartCoroutine(CheckNeighborBlock());
            yield return null;
            combo += 1;
        } while (states.Contains(State.Pop));
        if (combo > 1)
        {
            Debug.LogWarning(combo + "콤보!!!");
        }
    }

    private void FixedUpdate()
    {
        if (turn)
        {
            StartCoroutine(UpdateStage());
        }
    }

    IEnumerator CheckEmptyBlock()
    {
        for (int x = 0; x < MAXBLOCK; x++)
        {
            int index = 0;
            for (int y = 0; y < MAXBLOCK; y++)
            {
                if (blocks[x, y] != null)
                {
                    if (y != index) // 이동할 곳이 현재 위치가 아닐 경우
                    {
                        blocks[x, index] = blocks[x, y];
                        blocks[x, y] = null;
                        StartCoroutine(MoveBlock(blocks[x, index], new Vector3(-10f + 4 * x, 0f + 4 * index, 20)));
                    }
                    index += 1;
                }
            }
        }
        yield return null;
    }

    struct Coordinate
    {
        public int x, y;
        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    IEnumerator CheckNeighborBlock()
    {
        int searchX, searchY;
        int[] addX = { -1, 1, 0, 0 };
        int[] addY = { 0, 0, -1, 1 };
        Stack<Coordinate> open = new Stack<Coordinate>(); // 탐색할 좌표 리스트
        Stack<Coordinate> close = new Stack<Coordinate>(); // 탐색된 좌표 리스트
        Stack<Coordinate> explored = new Stack<Coordinate>(); // 탐색한 좌표 리스트

        // (0~MAXBLOCK, 0 ~ MAXBLOCK)범위의 모든 좌표들을 
        for (int y = 0; y < MAXBLOCK; y++)
        {
            for (int x = 0; x < MAXBLOCK; x++)
            {
                var coordinate = new Coordinate(x, y);
                // 탐색했거나 해당 좌표에 블럭이 없을 경우 통과
                if (explored.Contains(coordinate) || blocks[x, y] == null)
                {
                    continue;
                }
                var type = blocks[x, y].GetComponent<CookMaterial>().type;
                close.Clear();
                open.Clear();
                open.Push(coordinate);
                while (open.Count > 0)
                {
                    Coordinate current = open.Pop();
                    close.Push(current);
                    searchX = current.x;
                    searchY = current.y;
                    for (int index = 0; index < 4; index++)
                    {
                        // 실존 좌표일 때
                        if ((searchX + addX[index] >= 0 && searchX + addX[index] <= MAXBLOCK - 1) && (searchY + addY[index] >= 0 && searchY + addY[index] <= MAXBLOCK - 1))
                        {
                            var block = blocks[searchX + addX[index], searchY + addY[index]];
                            if (block != null && block.GetComponent<CookMaterial>().type == type)
                            {
                                Coordinate coord = new Coordinate(searchX + addX[index], searchY + addY[index]);
                                // 해당 좌표가 탐색 리스트에 들어가있거나 이미 탐색한 좌표가 아닌경우
                                if (!open.Contains(coord) && !close.Contains(coord) && !explored.Contains(coord))
                                {
                                    open.Push(coord);
                                }
                            }
                        } 
                    }
                }
               
                if (close.Count >= popCount)
                {
                    while (close.Count > 0)
                    {
                        Coordinate coord = close.Pop();
                        explored.Push(coord);
                        blocks[coord.x, coord.y].GetComponent<CookMaterial>().Collect();
                    }
                    states.Add(State.Pop);
                }
            }
        }
        yield return null;
    }

    IEnumerator FillEmptyBlock()
    {
        for (int x = 0; x < MAXBLOCK; x++)
        {
            for (int y = 0; y < MAXBLOCK; y++)
            {
                if (blocks[x, y] == null)
                {
                    blocks[x, y] = GetBlock();
                    blocks[x, y].SetActive(true);
                    blocks[x, y].transform.position = new Vector3(-10f + 4 * x, 0f + 4 * y, 20);
                }
            }
        }
        yield return null;
    }

    GameObject GetBlock()
    {
        if (blockPool.Count > 0)
        {
            var block = blockPool[0];
            blockPool.RemoveAt(0);
            return block;
        }
        return null;
    }

    IEnumerator MoveBlock(GameObject block, Vector3 position)
    {
        states.Add(State.Move);
        Collider collider = block.GetComponent<BoxCollider>();
        collider.enabled = false;
        while (block.transform.position != position)
        {
            block.transform.position = Vector3.Lerp(block.transform.position, position, 0.1f);
            yield return null;
        }
        collider.enabled = true;
        states.Remove(State.Move);
    }
}
