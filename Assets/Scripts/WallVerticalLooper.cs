using System.Collections.Generic;
using UnityEngine;

public class WallVerticalLooper : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private List<Transform> segments;
    [SerializeField] private float xPosition;
    [SerializeField] private float extraBuffer = 0.2f;
    private float segmentHeight;
    void Awake()
    {
        if (cameraTransform == null) cameraTransform = Camera.main.transform;

        SpriteRenderer sr = segments[0].GetComponent<SpriteRenderer>();
        segmentHeight = sr.bounds.size.y;

        for (int i = 0; i < segments.Count; i++)
        {
            Vector3 p = segments[i].position;
            segments[i].position = new Vector3(xPosition, p.y - i * segmentHeight, p.z);
        }
    }

    void LateUpdate()
    {
        var cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camTop = cameraTransform.position.y + camHeight * 0.5f + extraBuffer;

        for (int i = 0; i < segments.Count; i++)
        {
            var seg = segments[i];
            float segBottom = seg.position.y - segmentHeight * 0.5f;

            if (segBottom < camTop) 
                continue;

            float highestY = segments[0].position.y;
            float lowestY = segments[0].position.y;
            for (int j = 1; j < segments.Count; j++)
            {
                if (segments[j].position.y > highestY)
                    highestY = segments[j].position.y;
                if (segments[j].position.y < lowestY)
                    lowestY = segments[j].position.y;
            }

            seg.position = new Vector3(xPosition, lowestY - segmentHeight, seg.position.z);
        }
    }

}
