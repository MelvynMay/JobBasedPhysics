using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressSimulation : MonoBehaviour
{
    public Transform m_SpawnParent;
    public Vector2 m_SpawnRangeX = new Vector2(-10.0f, 10.0f);
    public Vector2 m_SpawnRangeY = new Vector2(-10.0f, 10.0f);
    public int m_MaxSpawn = 1000;
    public int m_SpawnPerFrame = 100;
    public GameObject m_SpawnItem;
    public bool m_PerFrameUpdate = true;

    public int m_CurrentSpawn = 0;

	void Start ()
    {
		
	}

    void FixedUpdate()
    {
        if (m_PerFrameUpdate)
            return;

        RunSimulation(Time.fixedDeltaTime);
    }
	
	void Update ()
    {
        // Spawn.
        if (m_CurrentSpawn < m_MaxSpawn)
        {
            var spawnCount = Mathf.Min(m_SpawnPerFrame, m_MaxSpawn - m_CurrentSpawn);            
            for (var i = 0; i < spawnCount; ++i)
            {
                var pos = new Vector3(Random.Range(m_SpawnRangeX.x, m_SpawnRangeX.y), Random.Range(m_SpawnRangeY.x, m_SpawnRangeY.y), 0.0f);
                var go = Instantiate(m_SpawnItem, pos, Quaternion.identity, m_SpawnParent);
                go.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                m_CurrentSpawn++;
            }
        }

        if (!m_PerFrameUpdate)
            return;

        RunSimulation(Time.deltaTime);
	}

    void RunSimulation(float timeDelta)
    {
        // Simulate.
        Physics2D.Simulate(timeDelta);
    }
}
