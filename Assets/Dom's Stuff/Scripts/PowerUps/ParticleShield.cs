using UnityEngine;

public class ParticleShield : MonoBehaviour
{
    public float MaxCapacity;
    [SerializeField] private float DrainSpeed = 0;
    [SerializeField] private float RechargeSpeed = 0;
    [Tooltip("The PS when the shield is activated")] public ParticleSystem ShieldPS;
    [Tooltip("The PS activated when something hits the shield")] public ParticleSystem DeflectPS;
    public float CurrentCapacity;
    private Rigidbody ShieldBody;
    private BoxCollider Collider;
    private MeshRenderer MeshRend;


    //Added By Ricardo for U.I.
    [HideInInspector] public bool shieldOn = false;
    //public AudioSource ShieldSound;


    //Added By Ricardo For U.I.
    //public Scriptforui scriptForUI;
    //public GameSounds gameSounds;


    private void Start()
    {
        CurrentCapacity = MaxCapacity;

        ShieldBody = GetComponent<Rigidbody>();
        Collider = GetComponent<BoxCollider>();
        MeshRend = GetComponent<MeshRenderer>();

        // Disable meshrenderer and collider at start
        Collider.enabled = false;
        MeshRend.enabled = false;


        //Added By Ricardo For U.I.
        //shieldOn = false;
        //scriptForUI = GameObject.FindObjectOfType<Scriptforui>();
        //gameSounds = GameObject.FindObjectOfType<GameSounds>();
        
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            shieldOn = true;
        }
        else if(CurrentCapacity <= 0)
        {
            shieldOn = false;
        }
        else
        {
            shieldOn = false;
        }

        if (shieldOn)
        {
            CurrentCapacity -= DrainSpeed * Time.deltaTime;

            MeshRend.enabled = true;
            Collider.enabled = true;
            ShieldPS.Play();
            //ShieldSound.Play();
        }

        if (!shieldOn)
        {
            CurrentCapacity += RechargeSpeed * Time.deltaTime;

            MeshRend.enabled = false;
            Collider.enabled = false;
            ShieldPS.Stop();
            //ShieldSound.Stop();
        }

        if(CurrentCapacity >= MaxCapacity)
        {
            CurrentCapacity = MaxCapacity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            //Find where the point of contact
            ContactPoint Contact = collision.contacts[0];

            //Play PS at point of contact
            //Instantiate(DeflectPS, new Vector3(Contact.point.x, Contact.point.y, Contact.point.z), Quaternion.identity);

            //Destroy object hit
            Destroy(collision.gameObject);
        }
    }
}
