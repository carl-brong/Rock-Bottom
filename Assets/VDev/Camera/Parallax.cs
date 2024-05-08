using UnityEngine;

// Source:  https://www.youtube.com/watch?v=zit45k6CUMk

// Vincent Lee
// 5/2/24

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxEffect;
    private Camera _camera;
    private float _startPos, _length;
    private float _travel => _camera.transform.position.x - _startPos;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        _startPos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        var temp = _camera.transform.position.x * (1 - parallaxEffect);
        var position = _startPos + _travel * parallaxEffect;

        var transform1 = transform;
        var position1 = transform1.position;
        position1 = new Vector3(position, position1.y, position1.z);
        transform1.position = position1;

        if (temp > _startPos + _length)
        {
            _startPos += _length;
        }
        else if (temp < _startPos - _length)
        {
            _startPos -= _length;
        }
    }
}
