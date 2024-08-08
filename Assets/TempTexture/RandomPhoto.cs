using UnityEngine;
using UnityEngine.UI;

public class RandomPhoto : MonoBehaviour {
    [SerializeField] private RawImage image;
    [SerializeField] private Texture2D[] textures;

    void Start() {
        int index = Random.Range(0, textures.Length);
        image.texture = textures[index];
    }
}
