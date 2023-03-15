using UnityEngine;

public class Emoji : MonoBehaviour
{
    [SerializeField] private Sprite[] _crashEmoji;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private int RandomNumber()
    {
        return Random.Range(0, _crashEmoji.Length);
    }

    public void RandomEmoji()
    {
        _spriteRenderer.sprite = _crashEmoji[RandomNumber()];
    }
}