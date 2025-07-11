/*using UnityEngine;

public class ColorFlipAndMove2D : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
   // private Color blackColor = Color.black;
    private Color greenColor = new Color(33f / 255f, 255f / 255f, 255f / 255f);
    private Color purpleColor = new Color(255f / 255f, 120f / 255f, 255f / 255f);
    //private Color whiteColor = Color.white;

    [Header("Movement Settings")]
    public float moveSpeed = 50f;

    [Header("Boundary Limits")]
    public float leftLimit = 0.9f;
    public float rightLimit = 7.05f;
    public float forwardLimit = 4.5f;
    public float backwardLimit = -4.5f;

    private Vector3 touchStartPos;
    private bool isDragging = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = greenColor;
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // Tap or spacebar flips color
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            FlipColor();
        }

        // Start drag
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            touchStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // Mobile drag
        if (Input.touchCount > 0)
        {
            isDragging = true;
            touchStartPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }

        if (isDragging)
        {
            Vector3 currentPos = Application.isMobilePlatform ?
                Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) :
                Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 delta = currentPos - touchStartPos;

            Vector3 newPos = transform.position + new Vector3(delta.x, delta.y, 0f);

            // Clamp both X and Y positions
            newPos.x = Mathf.Clamp(newPos.x, leftLimit, rightLimit);
            newPos.y = Mathf.Clamp(newPos.y, backwardLimit, forwardLimit);

            transform.position = Vector3.Lerp(transform.position, newPos, moveSpeed * Time.deltaTime);
            touchStartPos = currentPos;
        }
    }

    void FlipColor()
    {
        if (spriteRenderer.color == greenColor)
            spriteRenderer.color = purpleColor;
        else
            spriteRenderer.color = greenColor;
    }
}
*/
using UnityEngine;

public class ColorFlipAndMove2D : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private Color greenColor = new Color(33f / 255f, 255f / 255f, 255f / 255f);
    private Color purpleColor = new Color(255f / 255f, 120f / 255f, 255f / 255f);

    [Header("Movement Settings")]
    public float moveSpeed = 50f;

    [Header("Boundary Limits")]
    public float leftLimit = 0.9f;
    public float rightLimit = 7.05f;
    public float forwardLimit = 4.5f;
    public float backwardLimit = -4.5f;

    private Vector3 touchStartPos;
    private bool isDragging = false;

    // ? NEW: Toggle for input lock
    private bool inputEnabled = true;

    public void SetInputEnabled(bool enabled)
    {
        inputEnabled = enabled;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = greenColor;
    }

    void Update()
    {
        if (!inputEnabled) return;
        HandleInput();
    }

    void HandleInput()
    {
        // Tap or spacebar flips color
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            FlipColor();
            SoundManager.Instance.PlaySound(SoundManager.Instance.flipSound);
        }

        // Start drag (mouse or touch)
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            touchStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SoundManager.Instance.PlaySound(SoundManager.Instance.flipSound);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (Input.touchCount > 0)
        {
            isDragging = true;
            touchStartPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }

        if (isDragging)
        {
            Vector3 currentPos = Application.isMobilePlatform ?
                Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) :
                Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 delta = currentPos - touchStartPos;

            Vector3 newPos = transform.position + new Vector3(delta.x, delta.y, 0f);

            newPos.x = Mathf.Clamp(newPos.x, leftLimit, rightLimit);
            newPos.y = Mathf.Clamp(newPos.y, backwardLimit, forwardLimit);

            transform.position = Vector3.Lerp(transform.position, newPos, moveSpeed * Time.deltaTime);
            touchStartPos = currentPos;
        }
    }

    void FlipColor()
    {
        if (spriteRenderer.color == greenColor)
            spriteRenderer.color = purpleColor;
        else
            spriteRenderer.color = greenColor;
    }
}
