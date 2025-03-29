using UnityEngine;

public class Player_Interaction : MonoBehaviour
{
    public float interactionDistance = 3f; // 상호작용 가능한 거리
    public LayerMask interactableLayer; // 상호작용 가능한 오브젝트의 레이어

    private Camera playerCamera; // 플레이어 카메라
    private Interactable currentInteractable; // 현재 감지된 상호작용 가능한 오브젝트
    public GameObject interactionUI;
    public float interactionMaxCooldown = 0.5f;
    private float interactionCooldown = 0f;

    void Start()
    {
        playerCamera = Camera.main; // 메인 카메라를 가져옴
        interactionUI.SetActive(false);
    }

    void Update()
    {
        if (currentInteractable != null && false)
        {
            Debug.Log(currentInteractable.name);
        }

        CheckForInteractable(); // 상호작용 가능한 오브젝트 감지
        HandleInteraction(); // 플레이어 입력 처리

        if (interactionCooldown > 0f)
        {
            interactionCooldown -= Time.deltaTime;
        }
    }

    void CheckForInteractable()
    {
        // 플레이어 카메라에서 정면 방향으로 레이 생성
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        // 레이가 상호작용 가능한 오브젝트에 충돌했는지 확인
        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            // 충돌했을 때 상호작용할 수 있는 쿨타임을 확인하고 UI 표시
            if (interactionCooldown <= 0f)
            {
                interactionUI.SetActive(true);
            } else
            {
                interactionUI.SetActive(false);
            }
            
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                // 새롭게 감지된 오브젝트라면 기존 오브젝트와 비교하여 변경
                if (currentInteractable != interactable)
                {
                    if (currentInteractable != null)
                        currentInteractable.OnLookAway(); // 이전 오브젝트에서 시선을 돌렸을 때 호출

                    currentInteractable = interactable;
                    currentInteractable.OnLookAt(); // 새로운 오브젝트를 바라볼 때 호출
                }
                return;
            }
        }

        // 감지된 오브젝트가 없으면 기존 오브젝트에서 시선을 돌렸다고 처리
        if (currentInteractable != null)
        {
            currentInteractable.OnLookAway();
            currentInteractable = null;
            interactionUI.SetActive(false);
        }
    }

    void HandleInteraction()
    {
        // 현재 감지된 오브젝트가 있고, 상호작용 키를 눌렀을 때 상호작용 실행
        if (currentInteractable != null && Input.GetButtonDown("Interact") && interactionCooldown <= 0f)
        {
            currentInteractable.OnInteract();
            interactionCooldown = interactionMaxCooldown;
        }
    }
}
