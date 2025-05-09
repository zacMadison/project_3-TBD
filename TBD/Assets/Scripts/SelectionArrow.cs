using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    private RectTransform rect;
    private int currentPosition = 0;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // change arrow position
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangePosition(-1);
        }
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangePosition(1);
        }

        // interact with options
        if(Input.GetKeyDown(KeyCode.Return))
        {
            interact();
        }
    }

    private void ChangePosition(int _change)
    {
        currentPosition += _change;

        if(currentPosition < 0)
        {
            currentPosition = options.Length - 1;
        }else if(currentPosition > options.Length-1)
        {
            currentPosition = 0;
        }

        // Assign new y position for arrow
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
    }

    private void interact()
    {
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
