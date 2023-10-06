using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [SerializeField] private Vector2 m_gridSize;
    [SerializeField] private Vector2 m_offset;
    [SerializeField] private EnemyController m_enemyPrefab;

    [SerializeField] float m_moveSpeed;
    [SerializeField] int m_direction = 1;

    public List<EnemyController> m_EnemyObjects =>m_enemyObjects;
    [SerializeField] List<EnemyController> m_enemyObjects;
    [SerializeField] float time, timeZone=1;
    int m_enemyIndex;




    private void Awake()
    {
        if (instance != this)
            instance = this;
        else
            Destroy(this);


    }


    // Start is called before the first frame update
    void Start()
    {
        CreateEnemies();
        SetPosition();
        EnemyAddList();

    }

    private void Update()
    {
        Move();

        time += Time.deltaTime;

        if (time >= timeZone)
        {
            m_enemyIndex = Random.Range(0, m_enemyObjects.Count);
            m_enemyObjects[m_enemyIndex].Shoot();
            time = 0;
            timeZone = Random.Range(1, 3);
            

        }
    }

    public void EnemyAddList()
    {
        m_enemyObjects = new List<EnemyController>();
        for (int i = 0; i < transform.childCount; i++)
        {
            m_enemyObjects.Add(transform.GetChild(i).transform.GetComponent<EnemyController>());
        }
    }

    public void ChangeDirection()
    {
        m_direction *= -1;

        var pos = transform.position;
        pos.y--;
        transform.position = pos;
    }

    [ContextMenu(nameof(CreateEnemies))]
    void CreateEnemies()
    {
        transform.position = Vector2.zero;
        var position = Vector2.zero;
        for (int i = 0; i < m_gridSize.x; i++)
        {
            position.x = i * m_offset.x;
            for (int y = 0; y < m_gridSize.y; y++)
            {
                position.y = y * m_offset.y;
                var obj = Instantiate(m_enemyPrefab, position, Quaternion.identity);
                obj.transform.name = $"Enemy [{i},{y}]";
                obj.transform.SetParent(transform);

                if (y == m_gridSize.y - 1)
                    obj.isLead = true;
            }
        }
    }

    [ContextMenu(nameof(RemoveChild))]
    void RemoveChild()
    {
        var tempArray = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            tempArray[i] = transform.GetChild(i).gameObject;
        }
        foreach (var item in tempArray)
        {
            DestroyImmediate(item);
        }
    }


    [ContextMenu(nameof(SetPosition))]
    void SetPosition()
    {
        var listChild = new List<Transform>();
        var position = Vector3.zero;

        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).transform;
            listChild.Add(child);
            position += child.position;
        }

        position /= transform.childCount;

        foreach (var item in listChild)
        {
            item.SetParent(transform.parent);
        }

        transform.position = position;

        foreach (var item in listChild)
        {
            item.SetParent(transform);
        }

        transform.position = new Vector3(0, transform.position.y, 0);
    }

    void Move()
    {
        var position = transform.position;
        position.x += m_moveSpeed * Time.deltaTime * m_direction;
        transform.position = position;

    }
}
