using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class Upgrade
{
    [SerializeField]
    private string name;
    [SerializeField]
    private float weight;
    [SerializeField]
    private float initialWeight;

    public string getName() { return name; }
    public float getWeight() { return weight; }
    public float getInitialWeight() { return initialWeight; }
    public void setWeight(float w) { weight = w; }

    // info de lo que sea de las mejoras
}


[CreateAssetMenu(fileName = "Upgrades", menuName = "Gachapon")]
public class GachaponBase : ScriptableObject
{
    [SerializeField] private float gachaCD;
    // This list is populated from the editor
    [SerializeField] private List<Upgrade> _pool;

    public float getCD() {  return gachaCD; }

    // This is NonSerialized as we need it false everytime we run the game.
    // Without this tag, once set to true it will be true even after closing and restarting the game
    // Which means any future modification of our item list is not properly considered
    [System.NonSerialized] private bool isInitialized = false;

    private float _totalWeight;


    public void PrepareGacha()
    {
        foreach (var i in _pool)
        {
            i.setWeight(i.getInitialWeight());
            Debug.Log("wight: " + i.getWeight());
        }
        Debug.Log("setting weight");
    }
    private void Initialize()
    {
        if (!isInitialized)
        {
            _totalWeight = _pool.Sum(item => item.getInitialWeight());
  
            isInitialized = true;
        }
    }


    #region Alternative Initialize()
    // An alternative version that does the same operation, puts in _totalWeight the sum of the weight of each item
    private void AltInitialize()
    {
        if (!isInitialized)
        {
            _totalWeight = 0;
            foreach (var item in _pool)
            {
                _totalWeight += item.getWeight();
                //_totalWeight = _totalWeight + item.weight;
            }

            isInitialized = true;
        }
    }
    #endregion

    /// <summary>
    /// devuelve null si no hay upgrades disponibles y el upgrade resultante si va.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="System.Exception"></exception>
    public Upgrade pull()
    {
        // Make sure it is initalized
        Initialize();

        // Roll our dice with _totalWeight faces
        float diceRoll = Random.Range(0f, _totalWeight);



        if(_totalWeight == 0)
            return null;

        // Cycle through our items
        foreach (var item in _pool)
        {
            // If item.weight is greater (or equal) than our diceRoll, we take that item and return
            if (item.getWeight() >= diceRoll)
            {
                // bajan la probabilidad
                item.setWeight(item.getWeight() - 5);

                // recalcula el peso
                _totalWeight = _pool.Sum(item => item.getWeight());

                // Return here, so that the cycle doesn't keep running
                return item;
            }

            // If we didn't return, we substract the weight to our diceRoll and cycle to the next item
            diceRoll -= item.getWeight();
        }

        // As long as everything works we'll never reach this point, but better be notified if this happens!
        throw new System.Exception("Reward generation failed!");
    }
}


