using System.Collections.Generic;
using UnityEngine;

public class SpawnPyramid : MonoBehaviour
{
    [Range(1, 10000)]
    public int m_SpawnCount = 20;

    [Range(0, 10)]
    public float m_SpacingX = 1;

    [Range(0, 10)]
    public float m_SpacingY = 1;

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

    public GameObject[] m_SpawnItems;

	// Use this for initialization
	void Start ()
    {
        Spawn();
	}

    void Spawn()
    {
        if ( m_SpawnItems.Length > 0)
        {
            var x = 0.0f;
            var y = 0.0f;

		    for (var i = 0; i < m_SpawnCount; ++i)
		    {
			    x = i * m_SpacingX * 0.5f;

			    for (var j = i; j < m_SpawnCount; ++j)
			    {
			        var obj = m_SpawnItems[Random.Range (0, m_SpawnItems.Length)];
			        if (obj)
			        {
				        var position = transform.TransformPoint (new Vector3 (x, y, transform.position.z));
				        var rotation = Random.Range (0.0f, m_RandomRotation);

				        // Create the spawn object at the random position & rotation.
				        var spawnObj = Instantiate (obj, position, Quaternion.Euler (0f, 0f, rotation), m_SpawnParent ? m_SpawnParent : transform);

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

				    x += m_SpacingX;
			    }

			    y += m_SpacingY;
		    }
        }
    }
}
