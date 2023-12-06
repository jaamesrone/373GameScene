using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject playerCharacterPrefab;
    public GameObject rcCarPrefab;

    private GameObject rcCar;
    private GameObject Player;


    void Start()
    {
        // rc car get spawned right when the scene starts
        rcCar = Instantiate(rcCarPrefab, transform.position, transform.rotation);
        rcCar.SetActive(false); // hide the rc car so you can toggle it's ability back and forth.

        Player = playerCharacterPrefab;

    }

    // Update is called once per frame
    void Update()
    {
        // press tab to switch from player to rc car.
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchItem();
        }
    }

    void SwitchItem()
    {
        //toggles player and rc on and off when switching
        Player.SetActive(!Player.activeSelf);
        rcCar.SetActive(!rcCar.activeSelf);

        // Set the position and rotation of the newly activated device
        rcCar.transform.position = playerCharacterPrefab.transform.position;
        rcCar.transform.rotation = playerCharacterPrefab.transform.rotation;

        // debug information
        Debug.Log("Switched to: " + rcCar.name);
    }
}
