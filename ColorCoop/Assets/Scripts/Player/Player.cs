using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    private InputSystem_Actions _inputActions;
    private Rigidbody2D _rbody;
    private Vector2 _moveInput;

    public GameObject linePrefab;
    Line activeLine;

    private bool _toggleDraw = false;

    void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _rbody = GetComponent<Rigidbody2D>();
        if (_rbody is null) Debug.Log("Rigidbody2D not found!");
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();
    }

    void Update()
    {
        if (!_toggleDraw)
        {
            activeLine = null;
        }

        if (_toggleDraw)
        {
            activeLine.UpdateLine(transform.position);
        }
    }

    public void OnToggleDraw()
    {
        _toggleDraw = !_toggleDraw;
            if (_toggleDraw)
            {
                GameObject newLine = Instantiate(linePrefab);
                activeLine = newLine.GetComponent<Line>();
            }
    }

    void FixedUpdate()
    {
        _moveInput = _inputActions.Player.Move.ReadValue<Vector2>();
        _rbody.linearVelocity = _moveInput * _speed;
    }
}
