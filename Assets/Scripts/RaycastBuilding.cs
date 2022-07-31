using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaycastBuilding : MonoBehaviour
{
    public bool Building;
    public GameObject[] Objs;
    public GameObject[] PlaceObjs;
    public GameObject SellCanvas;
    public TextMeshProUGUI SellCost;
    public int[] ObjCost;
    public float Rot = 0;
    public LayerMask WhatIsBuilding;
    public LayerMask CantBuildLayer;

    public HUDTextController Text;

    public Transform Player;
    public GameObject BuildingHUD;
    public GameObject NonBuildingHUD;

    [SerializeField] private AudioClip errorSound;
    [SerializeField] private AudioClip[] aClips;
    [SerializeField] private AudioClip[] hudClips;
    [SerializeField] private AudioSource aSource;

    private int _selected = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Building = !Building;
            if (Building)
                Select(_selected);
            else
                Objs[_selected].SetActive(false);
                SellCanvas.SetActive(false);
        }

        BuildingHUD.SetActive(Building);
        NonBuildingHUD.SetActive(!Building);

        if (Building)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Select(_selected + 1 < Objs.Length ? _selected + 1 : 0);
                aSource.clip = hudClips[Random.Range(0, hudClips.Length)];
                aSource.Play();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Select(_selected - 1 >= 0 ? _selected - 1 : Objs.Length-1);
                aSource.clip = hudClips[Random.Range(0, hudClips.Length)];
                aSource.Play();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Rot -= 90;
                Objs[_selected].transform.rotation = Quaternion.Euler(0,Rot,0);
                aSource.clip = hudClips[Random.Range(0, hudClips.Length)];
                aSource.Play();
            }
            SellCanvas.SetActive(false);
            RaycastHit hit = new();
            if (Physics.Raycast(transform.position + transform.forward * 1.8f + Vector3.up * 3f, Vector3.down, out hit, 6f, WhatIsBuilding))
            {
                if (((CantBuildLayer.value & (1 << hit.transform.gameObject.layer)) > 0))
                {
                    Objs[_selected].SetActive(false);
                    SellCanvas.SetActive(false);
                    return;
                }

                Vector3 place = new Vector3(Mathf.Clamp(Mathf.Round(hit.point.x), -11, 10), 0, Mathf.Clamp(Mathf.Round(hit.point.z), -11, 10));

                if (place != new Vector3(Mathf.Round(hit.point.x), 0, Mathf.Round(hit.point.z)))
                {
                    Objs[_selected].SetActive(false);
                    return;
                }

                if (hit.transform.gameObject.name == "Floor")
                {
                    Objs[_selected].SetActive(true);

                    Objs[_selected].transform.position = place;

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (Stats.Money >= ObjCost[_selected])
                        {
                            Stats.Money -= ObjCost[_selected];

                            Instantiate(PlaceObjs[_selected], place, Quaternion.Euler(0, Rot, 0));
                            Reference.Achievement.Unlock(7);

                            if (_selected == 4)
                            {
                                Stats.Generators++;
                                Reference.Achievement.Unlock(8);
                                if (Stats.Generators > 10)
                                {
                                    Reference.Achievement.Unlock(14);
                                }
                            }
                            if (_selected == 5)
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
                            Text.SendWarning("Not enough money.", 1f);
                        }
                    }
                }
                else
                {
                    SellCanvas.SetActive(true);
                    SellCanvas.transform.position = Vector3.Lerp(SellCanvas.transform.position, new Vector3(place.x, SellCanvas.transform.position.y, place.z), .05f);
                    SellCanvas.transform.LookAt(Player.position);

                    SellCost.text = "$" + Mathf.Round(ObjCost[GetBuildID(hit.transform)] / 2).ToString();

                    Objs[_selected].SetActive(false);
                    if (Input.GetMouseButtonDown(1))
                    {
                        if (DestroyBuilding(hit.transform))
                        {
                            Stats.Money += Mathf.RoundToInt(ObjCost[GetBuildID(hit.transform)] / 2);
                        }
                    }
                }
            }
            else
            {
                Objs[_selected].SetActive(false);
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
                    Text.SendWarning("You can't have less than one Generators.", 3f);
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
                    Text.SendWarning("You can't have less than one Benders.", 3f);
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
        Objs[_selected].SetActive(false);
        _selected = index;
        Objs[_selected].SetActive(true);
        Objs[_selected].transform.rotation = Quaternion.Euler(0, Rot, 0);
    }
}
