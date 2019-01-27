using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum StartPos
{
    center, right, left, top, bottom
}

public class UIElement : MonoBehaviour
{
    /// <summary>
    /// handles movement and fading for UI elements
    /// </summary>

    private const float moveSpeed = 5; //move speeed
    private const float colorSpeed = 2;
    [SerializeField] private StartPos startPos; //starting position of screen or in center
    [SerializeField] private Color startColor = Color.white; //starting color (for fades)

    private bool levelChange = false;
    [SerializeField] private string levelChangeName;

    private Image image; //ref to attached image componenet 
    private Text text; //ref to attached text component
    private Button button; //ref to attached button component

    private Color color; //element color 
    private Vector2 center; // on screen position of the element
    private Vector2 target; //target position for moving on and off screen

    // Use this for initialization
    private void Awake()
    {
        center = transform.localPosition;
        switch (startPos)
        {
            case StartPos.right:
                transform.localPosition += Screen.width * Vector3.right;
                break;
            case StartPos.left:
                transform.localPosition += Screen.width * Vector3.left;
                break;
            case StartPos.top:
                transform.localPosition += Screen.height * Vector3.up;
                break;
            case StartPos.bottom:
                transform.localPosition += Screen.height * Vector3.down;
                break;
            case StartPos.center:
            default:
                break;
        }
        target = transform.localPosition;

        image = GetComponent<Image>();
        text = GetComponent<Text>();
        button = GetComponent<Button>();

        color = startColor;
        if (image != null){
            image.color = color;
        }
        if(text != null)
        {
            text.color = color;
        }
        FadeIn();
    }

    /// <summary>
    /// moves the UI element off screen to right
    /// </summary>
    public void MoveOutRight()
    {
        MoveTo(new Vector2(center.x + Screen.width, center.y));
        FadeOut();
    }

    /// <summary>
    /// moves the UI element off screen to left
    /// </summary>
    public void MoveOutLeft()
    {
        MoveTo(new Vector2(center.x - Screen.width, center.y));
        FadeOut();
    }

    /// <summary>
    /// moves the UI element off screen to right
    /// </summary>
    public void MoveOutBottom()
    {
        MoveTo(new Vector2(center.x, center.y - Screen.height ));
        FadeOut();
    }

    /// <summary>
    /// moves the UI element off screen to top
    /// </summary>
    public void MoveOutTop()
    {
        MoveTo(new Vector2(center.x, center.y + Screen.height ));
        FadeOut();
    }

    /// <summary>
    /// moves the UI element off screen to bottom
    /// </summary>
    public void MoveIn()
    {
        MoveTo(center);
        FadeIn();
    }

    /// <summary>
    /// fades the UI element in to solid color
    /// </summary>
    public void FadeIn()
    {
        Color newColor = color;
        newColor.a = 1;
        color = newColor;

        Activate();
    }

    /// <summary>
    /// fades the UI element out to transparent
    /// </summary>
    public void FadeOut()
    {
        Color newColor = color;
        newColor.a = 0;
        color = newColor;

        Deactivate();
    }

    /// <summary>
    /// sets the target for moving 
    /// </summary>
    /// <param name="target">new position of UI</param>
    private void MoveTo(Vector2 target)
    {
        this.target = target;
    }

    /// <summary>
    /// makes the button unclickable
    /// </summary>
    private void Deactivate()
    {
        if(button != null)
        {
            button.targetGraphic.raycastTarget = false;
        }
    }

    /// <summary>
    /// makes the button clickable
    /// </summary>
    private void Activate()
    {
        if (button != null)
        {
            button.targetGraphic.raycastTarget = true;
        }
    }
   
    /// <summary>
    /// changes the level
    /// </summary>
    public void LevelChange()
    {
        levelChange = true;
    }

    private void Update()
    {
        transform.localPosition = Vector2.Lerp(transform.localPosition, target, Time.deltaTime * moveSpeed);
        if (((Vector2)transform.localPosition - target).magnitude < 1 && levelChange)
        {
            SceneManager.LoadScene(levelChangeName, LoadSceneMode.Single);
        }
        if (image != null)
        {
            image.color = Color.Lerp(image.color, color, Time.deltaTime * colorSpeed);
        }
        else if (text != null)
        {
            text.color = Color.Lerp(text.color, color, Time.deltaTime * colorSpeed);
        }
    }
}
