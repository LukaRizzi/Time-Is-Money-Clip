using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaycastBuilding : MonoBehaviour
{
    public bool building;
    public GameObject[] objs;
    public GameObject[] placeObjs;
    public GameObject sellCanvas;
    public TextMeshProUGUI sellCost;
    public int[] objCost;
    private int selected = 0;
    public float rot = 0;
    public LayerMask whatIsBuilding;
    public LayerMask cantBuildLayer;

    public HUDTextController text;

    public Transform player;
    public GameObject buildingHUD;
    public GameObject nonBuildingHUD;

    [SerializeField] private AudioClip errorSound;
    [SerializeField] private AudioClip[] aClips;
    [SerializeField] private AudioClip[] hudClips;
    [SerializeField] private AudioSource aSource;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            building = !building;
            if (building)
                Select(selected);
            else
                objs[selected].SetActive(false);
                sellCanvas.SetActive(false);
        }

        buildingHUD.SetActive(building);
        nonBuildingHUD.SetActive(!building);

        if (building)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Select(selected + 1 < objs.Length ? selected + 1 : 0);
                aSource.clip = hudClips[Random.Range(0, hudClips.Length)];
                aSource.Play();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Select(selected - 1 >= 0 ? selected - 1 : objs.Length-1);
                aSource.clip = hudClips[Random.Range(0, hudClips.Length)];
                aSource.Play();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                rot -= 90;
                objs[selected].transform.rotation = Quaternion.Euler(0,rot,0);
                aSource.clip = hudClips[Random.Range(0, hudClips.Length)];
                aSource.Play();
            }
            sellCanvas.SetActive(false);
            RaycastHit hit = new();
            if (Physics.Raycast(transform.position + transform.forward * 1.8f + Vector3.up * 3f, Vector3.down, out hit, 6f, whatIsBuilding))
            {
                if (((cantBuildLayer.value & (1 << hit.transform.gameObject.layer)) > 0))
                {
                    objs[selected].SetActive(false);
                    sellCanvas.SetActive(false);
                    return;
                }

                Vector3 place = new Vector3(Mathf.Clamp(Mathf.Round(hit.point.x), -11, 10), 0, Mathf.Clamp(Mathf.Round(hit.point.z), -11, 10));

                if (place != new Vector3(Mathf.Round(hit.point.x), 0, Mathf.Round(hit.point.z)))
                {
                    objs[selected].SetActive(false);
                    return;
                }

                if (hit.transform.gameObject.name == "Floor")
                {
                    objs[selected].SetActive(true);

                    objs[selected].transform.position = place;

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (Stats.Money >= objCost[selected])
                        {
                            Stats.Money -= objCost[selected];

                            Instantiate(placeObjs[selected], place, Quaternion.Euler(0, rot, 0));
                            Reference.Achievement.Unlock(7);

                            if (selected == 4)
                            {
                                Stats.Generators++;
                                Reference.Achievement.Unlock(8);
                                if (Stats.Generators > 10)
                                {
                                    Reference.Achievement.Unlock(14);
                                }
                            }
                            if (selected == 5)
                            {
                                Stats.Benders++;
                            }

                            aSource.clip = aClips[Random.Range(0, aClips.Length)];
                            aSource.Play();
                        }
                        else
                        {
                            aSource.clip = errorSound;
                            aSource.Play();
                            text.SendWarning("Not enough money.", 1f);
                        }
                    }
                }
                else
                {
                    sellCanvas.SetActive(true);
                    sellCanvas.transform.position = Vector3.Lerp(sellCanvas.transform.position, new Vector3(place.x, sellCanvas.transform.position.y, place.z), .05f);
                    sellCanvas.transform.LookAt(player.position);

                    sellCost.text = "$" + Mathf.Round(objCost[GetBuildID(hit.transform)] / 2).ToString();

                    objs[selected].SetActive(false);
                    if (Input.GetMouseButtonDown(1))
                    {
                        if (DestroyBuilding(hit.transform))
                        {
                            Stats.Money += Mathf.RoundToInt(objCost[GetBuildID(hit.transform)] / 2);
                        }
                    }
                }
            }
            else
            {
                objs[selected].SetActive(false);
            }
        }
    }

    public int GetBuildID(Transform _transform)
    {
        switch (_transform.name)
        {
            case "ConveyorBelt(Clone)":
                return 0;
            case "ConveyorBeltUpper(Clone)":
                return 1;
            case "ConveyorBeltCorner(Clone)":
                return 2;
            case "ConveyorBeltCornerInverted(Clone)":
                return 3;
            case "ClipGenerator(Clone)":
                return 4;
            case "ClipBender(Clone)":
                return 5;
            case "Divider(Clone)":
                return 6;
        }

        return -4;
    }

    public bool DestroyBuilding(Transform _transform)
    {
        switch (_transform.name)
        {
            case "ConveyorBelt(Clone)":
                aSource.clip = aClips[Random.Range(0, aClips.Length)];
                aSource.Play();
                Destroy(_transform.gameObject, 0f);
                return true;
            case "ConveyorBeltUpper(Clone)":
                aSource.clip = aClips[Random.Range(0, aClips.Length)];
                aSource.Play();
                Destroy(_transform.gameObject, 0f);
                return true;
            case "ConveyorBeltCorner(Clone)":
                aSource.clip = aClips[Random.Range(0, aClips.Length)];
                aSource.Play();
                Destroy(_transform.gameObject, 0f);
                return true;
            case "ConveyorBeltCornerInverted(Clone)":
                aSource.clip = aClips[Random.Range(0, aClips.Length)];
                aSource.Play();
                Destroy(_transform.gameObject, 0f);
                return true;
            case "ClipGenerator(Clone)":
                if (Stats.Generators > 1)
                {
                    Stats.Generators--;
                    Destroy(_transform.gameObject, 0f);
                    aSource.clip = aClips[Random.Range(0, aClips.Length)];
                    aSource.Play();
                    return true;
                }
                else
                {
                    aSource.clip = errorSound;
                    aSource.Play();
                    text.SendWarning("You can't have less than one Generators.", 3f);
                    return false;
                }
            case "ClipBender(Clone)":
                if (Stats.Benders > 1)
                {
                    Stats.Benders--;
                    Destroy(_transform.gameObject, 0f);
                    aSource.clip = aClips[Random.Range(0, aClips.Length)];
                    aSource.Play();
                    return true;
                }
                else
                {
                    aSource.clip = errorSound;
                    aSource.Play();
                    text.SendWarning("You can't have less than one Benders.", 3f);
                    return false;
                }
            case "Divider(Clone)":
                Destroy(_transform.gameObject, 0f);
                aSource.clip = aClips[Random.Range(0, aClips.Length)];
                aSource.Play();
                return true;
        }
        return false;
    }

    public void Select(int index)
    {
        objs[selected].SetActive(false);
        selected = index;
        objs[selected].SetActive(true);
        objs[selected].transform.rotation = Quaternion.Euler(0, rot, 0);
    }
}
