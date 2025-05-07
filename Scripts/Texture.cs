using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ImageOverlay : MonoBehaviour
{
    public List<GameObject> targets;
    public List<Sprite> possibleSprites;

    // Scene "allocation"
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Scene "deallocation"
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // When scene loads
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Pick random sprite
        Sprite chosenSprite = possibleSprites[Random.Range(0, possibleSprites.Count)];

        // For each prefab
        foreach (GameObject obj in targets)
        {
            // Get obj sprite
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            // Get rect size
            Vector2 originalSize = sr.bounds.size;
            // Set sprite
            sr.sprite = chosenSprite;
            // Get new sprite size
            Vector2 newSize = sr.bounds.size;

            // Scale new sprite to be size of original
            Vector3 scale = obj.transform.localScale;
            scale.x *= originalSize.x / newSize.x;
            scale.y *= originalSize.y / newSize.y;

            // Set sprite to new scale
            obj.transform.localScale = scale;
        }
    }
}
