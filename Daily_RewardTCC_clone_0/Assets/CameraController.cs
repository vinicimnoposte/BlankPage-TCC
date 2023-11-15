using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float swipeSpeed = 2f; // Velocidade do movimento da câmera
    private Vector2 touchStartPosition; // Posição inicial do toque

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 direction = touch.position - touchStartPosition;
                float swipeDistance = direction.magnitude;

                if (swipeDistance > 0)
                {
                    Vector3 swipeDirection = new Vector3(direction.x, 0f, direction.y).normalized;
                    Vector3 cameraMovement = swipeDirection * swipeSpeed * Time.deltaTime;

                    Camera.main.transform.position += cameraMovement;
                }
            }
        }
    }
}