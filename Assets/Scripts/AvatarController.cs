using Photon.Pun;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    public bool  canMove = true;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    private PhotonView _photonView;
    private Camera _mainCamera;
    private Animator _avatarAnim;

    private bool _isWalking;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        _mainCamera = Camera.main; // 메인 카메라 할당
        _avatarAnim = GetComponent<Animator>(); // 아바타 애니메이터 할당
    }

    public void Update()
    {
        if (_photonView.IsMine == false)
            return;

        if (canMove == false)
        {
            _avatarAnim.SetBool("IsWalking", false);
            return;
        }

        // 입력값 가져오기
        float horizontal = Input.GetAxis("Horizontal"); // A, D 키
        float vertical = Input.GetAxis("Vertical");     // W, S 키

        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        _isWalking = inputDirection.magnitude >= 0.1f;

        if (_isWalking)
        {
            // 카메라 기준 이동 방향 계산
            Vector3 cameraForward = _mainCamera.transform.forward;
            Vector3 cameraRight = _mainCamera.transform.right;

            // Y축만 유지 (평면 이동)
            cameraForward.y = 0f;
            cameraRight.y = 0f;
            cameraForward.Normalize();
            cameraRight.Normalize();

            // 입력 방향을 카메라 방향에 맞게 변환
            Vector3 moveDirection = cameraForward * inputDirection.z + cameraRight * inputDirection.x;

            // 캐릭터 회전
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // 캐릭터 이동
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        }
        
        // 아바타 걷기 애니메이션 재생
        _avatarAnim.SetBool("IsWalking", _isWalking);
    }
}