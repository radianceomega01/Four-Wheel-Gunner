
using System.Threading.Tasks;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] CarsSO carSO;
    [SerializeField] ParticleSystem blast;

    public float Health { get; private set; }
    public CarsSO GetSO() => carSO;

    private void Awake()
    {
        Health = carSO.actualHP;
    }

    public void TakeDamage(float value)
    {
        Health -= value;
        GameManager.Instance.UpdateHealthBar(Health);
        if (Health <= 0)
        {
            Instantiate(blast, transform.position, transform.rotation);
            Task.Delay(2500);
            GameManager.Instance.OnCarDestroyed();
            Destroy(gameObject);
        }
    }
}
