using UnityEngine;

public class OnEnterGachapon : MonoBehaviour
{
    [SerializeField] private GameObject _key;
    [SerializeField] private int _gachaPrice = 50;
    [SerializeField] private bool canPull;
    [SerializeField] private bool gachaOnCooldown;
    [SerializeField] private GachaponBase _gacha;

    [SerializeField] private float currentGachaTime = 0;
    
    [SerializeField] private Transform _possiblePositions;

    private void Start()
    {
        canPull = false;
        gachaOnCooldown = false;

        _gacha.PrepareGacha();
    }
    private void Update()
    {
        if(canPull)
        {
            if (GameManager.Instance.GetCoins() >= _gachaPrice && Input.GetKey(KeyCode.E) && !gachaOnCooldown)
            {
                GameManager.Instance.RemoveCoins(_gachaPrice);
                // esto va cuando se quiera hacer un pull en el gachapon
                Upgrade up = _gacha.pull();

                gachaOnCooldown = true;

                if (up == null)
                    Debug.Log("no upgrade avaliable");
                else
                    Debug.Log(up.getName());

                updateUpgrades(up.getName());

                Transform parent = transform.parent;
                Transform newTr = _possiblePositions.GetChild(Random.Range(0, _possiblePositions.childCount));
                parent.position = newTr.position;
                parent.rotation = Quaternion.Euler(0, newTr.eulerAngles.y, 0);
            }

            if (gachaOnCooldown)
            {

                // cooldown
                if (_gacha.getCD() <= currentGachaTime)
                {
                    gachaOnCooldown=false;
                    currentGachaTime = 0;
                }
                else
                    currentGachaTime += Time.deltaTime;
            }
        }
    }

    // BULLETS, LIFE, SPEED, DAMAGE
    private void updateUpgrades(string up)
    {
        switch (up)
        {
            case "BULLETS":
                GameManager.Instance.UpgradeBullets();
                break;
            case "LIFE":
                GameManager.Instance.UpgradeLife();
                break;
            case "SPEED":
                GameManager.Instance.UpgradeSpeed();
                break;
            case "DAMAGE":
                GameManager.Instance.UpgradeDamage();
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // si es el player
        if (other.gameObject.GetComponentInParent<PlayerMovement>() != null)
        {
            _key.SetActive(true);
            // le permite tirar en el gachapon
            canPull = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // si es el player
        if (other.gameObject.GetComponentInParent<PlayerMovement>() != null)
        {
            _key.SetActive(false);
            // le permite tirar en el gachapon
            canPull = false;
        }
    }
}
