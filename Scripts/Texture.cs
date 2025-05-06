using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ImageOverlay : MonoBehaviour
{
    public List<GameObject> targets;
    public List<Sprite> possibleSprites;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Sprite chosenSprite = possibleSprites[Random.Range(0, possibleSprites.Count)];

        foreach (GameObject obj in targets)
        {
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            if (sr == null) continue;

            Vector2 originalSize = sr.bounds.size;

            sr.sprite = chosenSprite;

            Vector2 newSize = sr.bounds.size;
            if (newSize.x == 0 || newSize.y == 0) continue;

            Vector3 scale = obj.transform.localScale;
            scale.x *= originalSize.x / newSize.x;
            scale.y *= originalSize.y / newSize.y;
            obj.transform.localScale = scale;
        }
    }
}
