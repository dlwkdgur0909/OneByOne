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
        if (toDoor && !isClimbing) // 벽을 오르지 않는 동안만 목적지 설정
        {
            agent.SetDestination(door.position);
        }
        else if (toMainDoor && !isClimbing) // 벽을 오르지 않는 동안만 목적지 설정
        {
            agent.SetDestination(mainDoor.position);
        }

        if (isClimbing) // 벽을 오르는 중일 때만 벽을 따라 이동
        {
            // 벽을 따라 위로 이동
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
            // 벽을 오르는 도중에 벽과 충돌하면 벽 오르기를 중지함
            StopClimbing();
        }
    }
}
