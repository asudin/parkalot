using UnityEngine;

public class Emoji : MonoBehaviour 
{
    [SerializeField] private Sprite[] _crashEmoji;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        RandomEmoji();
    }

    private void RandomEmoji()
    {
        _spriteRenderer.sprite = _crashEmoji[RandomNumber()];
    }

    private int RandomNumber()
    {
        return Random.Range(0, _crashEmoji.Length);
    }
}
