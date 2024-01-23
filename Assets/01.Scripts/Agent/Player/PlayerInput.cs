using UnityEngine;
using UnityEngine.Events;

// 움직이는 방향으로 회전하는데 
// 만약 움직이지 않는다면 반대로 회전

public class PlayerInput : MonoBehaviour
{
    [Header("플레이어 설정")]
    [SerializeField] private KeyCode rev_key = KeyCode.G; //키
    [SerializeField] private float rev_coolTime = 3f; //쿨타임

    [Header("연결 이벤트")]
    [SerializeField] private UnityEvent<Vector3> OnReverseGravityEvent = null;
    [SerializeField] private UnityEvent<float, float> OnRotateEvent = null;
    [SerializeField] private UnityEvent<Vector3> OnMovementEvent = null;

    #region 프로퍼티 변수
    #endregion

    #region 일반 변수

    private float rev_curTime = 0f;
    private Vector3 moveDirection = Vector3.zero;
    #endregion

    private void Update()
    {
        //플레이어 중력 반전
        PlayerReverseGravity();
        //움직임 처리
        PlayerMovement();
        // 회전 처리
        PlayerRotate();
    }

    private void PlayerReverseGravity()
    {
        rev_curTime += Time.deltaTime;
        if (rev_curTime > rev_coolTime
            && Input.GetKeyDown(rev_key))
        {
            OnReverseGravityEvent?.Invoke(moveDirection);

            rev_curTime = 0f;
        }
    }

    private void PlayerMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // 이동 방향 설정
        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        OnMovementEvent?.Invoke(moveDirection);
    }
    private void PlayerRotate()
    {
        // 마우스 입력 받기
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        Debug.Log($"마우스 X : {mouseX} , 마우스 Y : {mouseY}");

        OnRotateEvent?.Invoke(mouseX, mouseY);
    }
}
