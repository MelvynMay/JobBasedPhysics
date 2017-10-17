using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public Vector2 m_SpawnRangeX = new Vector2(-10.0f, 10.0f);
    public Vector2 m_SpawnRangeY = new Vector2(-10.0f, 10.0f);
    public int m_MaxSpawn = 1000;
    public int m_SpawnPerFrame = 100;

	[Range (0.1f, 10.0f)]
	public float m_MinScale = 1.0f;

	[Range (0.1f, 10.0f)]
	public float m_MaxScale = 1.0f;

	[Range (0f, 100f)]
	public float m_GravityScale = 1.0f;

	[Range (0, 360)]
	public float m_RandomRotation = 0.0f;

	[Range (0, 31)]
	public int m_Layer;

	public bool m_UseRandomColor;

    public Transform m_SpawnParent;

    public GameObject m_SpawnItem;

    public int m_CurrentSpawn = 0;

	void Start ()
    {
		
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
				var rotation = Random.Range (0.0f, m_RandomRotation);
                var spawnObj = Instantiate(m_SpawnItem, pos, Quaternion.Euler (0f, 0f, rotation), m_SpawnParent);
                m_CurrentSpawn++;

				    // Set its random scale.
				    var randomScale = Random.Range (m_MinScale, m_MaxScale);
				    spawnObj.transform.localScale = new Vector3 (randomScale, randomScale);

				    // Set its layer.
				    spawnObj.layer = m_Layer;

                    var body = spawnObj.GetComponent<Rigidbody2D> ();
				    if (body)
				    {
					    body.gravityScale = m_GravityScale;
                        body.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
				    }

				    // Set a random sprite renderer color if required.
				    if (m_UseRandomColor)
				    {
					    var spriteRenderer = spawnObj.GetComponentInChildren<SpriteRenderer> ();
					    if (spriteRenderer)
						    spriteRenderer.color = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f), 1.0f);
				    }
            }
        }
	}
}
