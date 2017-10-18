using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Drops random objects at the mouse-position (second mouse button).
public class Dropper : MonoBehaviour
{
	[Range (0.1f, 10.0f)]
	public float m_MinScale = 1.0f;

	[Range (0.1f, 10.0f)]
	public float m_MaxScale = 1.0f;

	[Range (0f, 100f)]
	public float m_GravityScale = 1.0f;

	[Range (0, 360)]
	public float m_RandomRotation = 0.0f;

    [Range(0f, 10f)]
    public float m_Lifetime = 1.0f;

	[Range (0, 31)]
	public int m_Layer;

	public bool m_UseRandomColor;

    public GameObject[] m_SpawnItems;

	public Transform m_SpawnParent;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		// Calculate the world position for the mouse.
		var position = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		if (Input.GetMouseButtonDown (1))
		{
		    var obj = m_SpawnItems[Random.Range (0, m_SpawnItems.Length)];
		    if (obj)
		    {
			    position.z = transform.position.z;
			    var rotation = Random.Range (0.0f, m_RandomRotation);

			    // Create the spawn object at the position & rotation.
			    var spawnObj = Instantiate (obj, position, Quaternion.Euler (0f, 0f, rotation), m_SpawnParent ? m_SpawnParent : transform);

                // Set its lifetime.
                if (m_Lifetime > 0f)
                    DestroyObject(spawnObj, m_Lifetime);

			    // Set its random scale.
			    var randomScale = Random.Range (m_MinScale, m_MaxScale);
			    spawnObj.transform.localScale = new Vector3 (randomScale, randomScale);

			    // Set its layer.
			    spawnObj.layer = m_Layer;

                var body = spawnObj.GetComponent<Rigidbody2D> ();
			    if (body)
			    {
				    body.gravityScale = m_GravityScale;
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
