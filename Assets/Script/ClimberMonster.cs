using UnityEngine;

public class ClimberMonster : Monster
{
    public float climbSpeed = 5f;
    private bool isClimbing = false;

    public LayerMask climbableMask;

    Transform door;
    Transform mainDoor;

    new void OnEnable()
    {
        door = GameManager.instance.door;
        mainDoor = GameManager.instance.mainDoor;
    }

    void Update()
    {
        if (toDoor && !isClimbing) // ���� ������ �ʴ� ���ȸ� ������ ����
        {
            agent.SetDestination(door.position);
        }
        else if (toMainDoor && !isClimbing) // ���� ������ �ʴ� ���ȸ� ������ ����
        {
            agent.SetDestination(mainDoor.position);
        }

        if (isClimbing) // ���� ������ ���� ���� ���� ���� �̵�
        {
            // ���� ���� ���� �̵�
            Vector3 climbDirection = Vector3.up * climbSpeed * Time.deltaTime;
            transform.Translate(climbDirection);
        }
    }

    public void TryClimb()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f, climbableMask))
        {
            StartClimbing();
        }
    }

    private void StartClimbing()
    {
        isClimbing = true;
        agent.isStopped = true;
    }

    private void StopClimbing()
    {
        isClimbing = false;
        agent.isStopped = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isClimbing && collision.gameObject.layer == LayerMask.NameToLayer("Climbable"))
        {
            // ���� ������ ���߿� ���� �浹�ϸ� �� �����⸦ ������
            StopClimbing();
        }
    }
}
