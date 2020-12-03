using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour
{
    #region Variables

    public StateMachine movementSM;
    public IdleState idle;
    public InvulnerableState invulnerable;
    private int health;
    private SpriteRenderer spriteRenderer;
    private Coroutine firingCoroutine;

    //MoveBoundaries
    private float xMin;

    private float xMax;
    private float yMin;
    private float yMax;

    [SerializeField]
    private PlayerDataSO data;

    public UnityAction OnGameOverEvent;

    #endregion Variables

    #region Methods

    public void Move(float horizontalSpeed, float verticalSpeed)
    {
        var deltaX = horizontalSpeed * Time.deltaTime * data.moveSpeed;
        var newXPos = transform.position.x + deltaX;
        // transform.position = new Vector2(newXPos, transform.position.y);

        var deltaY = verticalSpeed * Time.deltaTime * data.moveSpeed;
        var newYPos = transform.position.y + deltaY;
        transform.position = new Vector2(Mathf.Clamp(newXPos, xMin, xMax), Mathf.Clamp(newYPos, yMin, yMax));
    }

    public void Shoot()
    {
        IEnumerator FireContinuously()
        {
            while (true)
            {
                GameObject firedLaser = Instantiate(
                    data.laser,
                    transform.position,
                    Quaternion.identity) as GameObject;
                firedLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, data.laserSpeed);
                AudioSource.PlayClipAtPoint(data.shootSound, Camera.main.transform.position, data.shootSoundVolume);
                yield return new WaitForSeconds(data.projectileFiringPeriod);
            }
        }
        firingCoroutine = StartCoroutine(FireContinuously());
    }

    public void StopShoot()
    {
        if (firingCoroutine != null)
            StopCoroutine(firingCoroutine);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        movementSM.CurrentState.OnTriggerEnter2D(other);
    }

    public void ProcessHit(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        // Prevent Null Reference Exception if game object has no DamageDealer Component
        if (!damageDealer)
        {
            return;
        }

        health -= damageDealer.GetDamage();
        if (health < 0)
        {
            health = 0;
        }
        damageDealer.Hit();
        if (health <= 0)
        {
            Die(other);
        }

        movementSM.ChangeState(invulnerable);
    }

    private void Die(Collider2D other)
    {
        OnGameOverEvent?.Invoke();
        Destroy(this.gameObject);
        Destroy(other.gameObject);
        AudioSource.PlayClipAtPoint(data.enemyExplosionSFX, Camera.main.transform.position, data.explosionSoundVolume);
    }

    public int GetHealth()
    {
        return health;
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + data.padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - data.padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + data.padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - data.padding;
    }

    public void BecomeInvulnerable()
    {
        IEnumerator StayInvulnerable()
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(data.invulnerablePeriod);
            spriteRenderer.color = Color.white;
            movementSM.ChangeState(idle);
        }
        StartCoroutine(StayInvulnerable());
    }

    #endregion Methods

    #region MonoBehaviour Callbacks

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetUpMoveBoundaries();
        health = data.maxHealth;
        movementSM = new StateMachine();
        idle = new IdleState(this, movementSM);
        invulnerable = new InvulnerableState(this, movementSM);
        movementSM.Initialize(idle);
    }

    private void Update()
    {
        movementSM.CurrentState.HandleInput();

        movementSM.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        movementSM.CurrentState.PhysicsUpdate();
    }

    #endregion MonoBehaviour Callbacks
}