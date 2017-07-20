using UnityEngine;

public class AnimationManager : MonoBehaviour {

    public Animator potatoIndicatorAnimator;
    public Animator acceleratorEffectAnimator;

    //GameManager _gameManager;

    //public CameraShaker.Properties collisionShakeProps;

    static AnimationManager _instance;

    void Start()
    {
        //_gameManager = GetComponent<GameManager>();
        //CollisionShake();
       // CollisionShake();
    }

    public static AnimationManager instance
    {
        
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AnimationManager>();
            }
            return _instance;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            CollisionShake();
    }

    public void PotatoIndicator()
    {
        potatoIndicatorAnimator.SetTrigger("TookPotato");
        //potatoIndicatorAnimator.ResetTrigger("TookPotato");
    }

    public void PoisonedPotatoIndicator()
    {
        potatoIndicatorAnimator.SetTrigger("TookPP");
        //potatoIndicatorAnimator.ResetTrigger("TookPP");
    }

    public void CollisionShake()
    {
        Camera.main.GetComponent<CameraShaker>().ShakeCamera(2f, 0.2f * Time.deltaTime);
    }

    public void AcceleratorEffectOn()
    {
        acceleratorEffectAnimator.SetBool("IsAccelerating", true);
        acceleratorEffectAnimator.transform.GetChild(0).GetComponent<Animator>().SetBool("IsAccelerating", true);
    }

    public void AcceleratorEffectOff()
    {
        acceleratorEffectAnimator.SetBool("IsAccelerating", false);
        acceleratorEffectAnimator.transform.GetChild(0).GetComponent<Animator>().SetBool("IsAccelerating", false);
    }
}
