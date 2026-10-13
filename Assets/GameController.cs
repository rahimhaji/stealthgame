using UnityEngine;

public class GameController : MonoBehaviour
{
    public float moveSpeed = 5f;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            moveDir = Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            moveDir = Vector3.back;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            moveDir = Vector3.left;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            moveDir = Vector3.right;
        }

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
