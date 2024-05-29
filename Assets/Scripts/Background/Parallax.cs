using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] Vector2 scrollMultiplier;

    private Transform cameraTrasnform;
    private Vector3 lastCameraPosition;
    private float textureUnitSizeX;
    void Start()
    {
        cameraTrasnform = Camera.main.transform;
        lastCameraPosition = cameraTrasnform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }

    void Update()
    {
        Vector3 cameraMovement = cameraTrasnform.position - lastCameraPosition;
        transform.position += new Vector3(scrollMultiplier.x * cameraMovement.x, scrollMultiplier.y * cameraMovement.y);
        lastCameraPosition = cameraTrasnform.position;

        if (Mathf.Abs(cameraTrasnform.position.x - transform.position.x) >= textureUnitSizeX)
        {
            float offset = (cameraTrasnform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(cameraTrasnform.position.x + offset, transform.position.y);
        }

    }
}
