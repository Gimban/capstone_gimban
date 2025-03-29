using UnityEngine;

public class Player_Interaction : MonoBehaviour
{
    public float interactionDistance = 3f; // ��ȣ�ۿ� ������ �Ÿ�
    public LayerMask interactableLayer; // ��ȣ�ۿ� ������ ������Ʈ�� ���̾�

    private Camera playerCamera; // �÷��̾� ī�޶�
    private Interactable currentInteractable; // ���� ������ ��ȣ�ۿ� ������ ������Ʈ
    public GameObject interactionUI;
    public float interactionMaxCooldown = 0.5f;
    private float interactionCooldown = 0f;

    void Start()
    {
        playerCamera = Camera.main; // ���� ī�޶� ������
        interactionUI.SetActive(false);
    }

    void Update()
    {
        if (currentInteractable != null && false)
        {
            Debug.Log(currentInteractable.name);
        }

        CheckForInteractable(); // ��ȣ�ۿ� ������ ������Ʈ ����
        HandleInteraction(); // �÷��̾� �Է� ó��

        if (interactionCooldown > 0f)
        {
            interactionCooldown -= Time.deltaTime;
        }
    }

    void CheckForInteractable()
    {
        // �÷��̾� ī�޶󿡼� ���� �������� ���� ����
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        // ���̰� ��ȣ�ۿ� ������ ������Ʈ�� �浹�ߴ��� Ȯ��
        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            // �浹���� �� ��ȣ�ۿ��� �� �ִ� ��Ÿ���� Ȯ���ϰ� UI ǥ��
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
                // ���Ӱ� ������ ������Ʈ��� ���� ������Ʈ�� ���Ͽ� ����
                if (currentInteractable != interactable)
                {
                    if (currentInteractable != null)
                        currentInteractable.OnLookAway(); // ���� ������Ʈ���� �ü��� ������ �� ȣ��

                    currentInteractable = interactable;
                    currentInteractable.OnLookAt(); // ���ο� ������Ʈ�� �ٶ� �� ȣ��
                }
                return;
            }
        }

        // ������ ������Ʈ�� ������ ���� ������Ʈ���� �ü��� ���ȴٰ� ó��
        if (currentInteractable != null)
        {
            currentInteractable.OnLookAway();
            currentInteractable = null;
            interactionUI.SetActive(false);
        }
    }

    void HandleInteraction()
    {
        // ���� ������ ������Ʈ�� �ְ�, ��ȣ�ۿ� Ű�� ������ �� ��ȣ�ۿ� ����
        if (currentInteractable != null && Input.GetButtonDown("Interact") && interactionCooldown <= 0f)
        {
            currentInteractable.OnInteract();
            interactionCooldown = interactionMaxCooldown;
        }
    }
}
