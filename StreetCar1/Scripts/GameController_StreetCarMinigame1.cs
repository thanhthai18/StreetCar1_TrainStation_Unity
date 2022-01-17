using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController_StreetCarMinigame1 : MonoBehaviour
{
    public static GameController_StreetCarMinigame1 instance;

    public StreetCar_StreetCarMinigame1 myStreetCar;
    public Camera mainCamera;
    public List<Transform> listWayPoint = new List<Transform>();
    public int timeOnMainGame = 0;
    public Coroutine timeCoroutine;
    public bool isLockStage;
    public int waypointIndex = 0;
    public bool isDonKhach = true;
    public List<People_StreetCarMinigame1> listPeople = new List<People_StreetCarMinigame1>();
    public List<NotPeople_StreetCarMinigame1> listNotPeople = new List<NotPeople_StreetCarMinigame1>();
    [SerializeField] private People_StreetCarMinigame1 peoplePrefab;
    [SerializeField] private NotPeople_StreetCarMinigame1 thiefPrefab;
    [SerializeField] private NotPeople_StreetCarMinigame1 dogPrefab;
    public Vector3 mousePos;
    public RaycastHit2D[] hit;
    public int stage;
    public bool isHoldPeople, isHoldNotPeople;
    private Vector2 posBeginHold = new Vector2();
    public People_StreetCarMinigame1 tmpPeople;
    public NotPeople_StreetCarMinigame1 tmpNotPeople;
    public int num;
    private Vector2 holdMousePosInScreen;
    public bool isLose, isWin;
    private bool isPeople, isNotPeople;
    public GameObject VFXBoomPrefab;
    public GameObject tutorial1, tutorial2, tutorial3;
    private bool isTutorial1, isTutorial3;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(instance);
    }

    private void Start()
    {
        SetSizeCamera();
        holdMousePosInScreen = new Vector2(mainCamera.orthographicSize * (16f / 9) - 1, mainCamera.orthographicSize - 0.5f);
        stage = 0;
        num = 0;
        isLockStage = false;
        isHoldPeople = false;
        isHoldNotPeople = false;
        isLose = false;
        isWin = false;
        isPeople = false;
        isNotPeople = false;
        isTutorial1 = true;
        isTutorial3 = true;
        tutorial1.SetActive(false);
        tutorial2.SetActive(false);
        tutorial3.SetActive(false);
        myStreetCar.transform.position = listWayPoint[waypointIndex].position;
        //timeCoroutine = StartCoroutine(CountingTime());
    }

    void SetSizeCamera()
    {
        float f1 = 16.0f / 9;
        float f2 = Screen.width * 1.0f / Screen.height;

        mainCamera.orthographicSize *= f1 / f2;
    }

    //IEnumerator CountingTime()
    //{
    //    while (timeOnMainGame < 20 && isDonKhach)
    //    {
    //        timeOnMainGame++;
    //        yield return new WaitForSeconds(1);
    //    }
    //}

    public void SpawnObject(int stageIndex)
    {
        if (stageIndex == 1)
        {
            if (isTutorial1)
            {
                isTutorial1 = false;
                tutorial1.SetActive(true);
                tutorial1.transform.DOMove(myStreetCar.transform.position, 2).SetEase(Ease.InBack).SetLoops(-1);
            }

            listPeople.Clear();
            listNotPeople.Clear();
            listPeople.Add(Instantiate(peoplePrefab, new Vector2(-4.83f, 3.75f), Quaternion.identity));
            listPeople.Add(Instantiate(peoplePrefab, new Vector2(-0.16f, 3.69f), Quaternion.identity));
            listPeople.Add(Instantiate(peoplePrefab, new Vector2(3.02f, 3.92f), Quaternion.identity));
            listNotPeople.Add(Instantiate(thiefPrefab, new Vector2(4.06f, 4.96f), Quaternion.identity));

            listPeople[0].transform.DOMove(new Vector2(-2.23f, 3.73f), 3).OnComplete(() => { listPeople[0].isMove = false; });
            listPeople[0].isMove = true;
            num = listPeople.Count + listNotPeople.Count;
        }
        if (stageIndex == 2)
        {
            if (isTutorial3)
            {
                isTutorial3 = false;
                tutorial3.transform.position = new Vector2(13.92f -0.3f, -1.4f-0.3f);
                tutorial3.SetActive(true);
                tutorial3.transform.DOMove(new Vector2(6.41f, -5.8f), 1.5f).SetEase(Ease.InBack).SetLoops(-1);
            }
            listPeople.Clear();
            listNotPeople.Clear();
            listPeople.Add(Instantiate(peoplePrefab, new Vector2(7.99f, 1.23f), Quaternion.identity));
            listPeople.Add(Instantiate(peoplePrefab, new Vector2(10f, 1.03f), Quaternion.identity));
            listPeople.Add(Instantiate(peoplePrefab, new Vector2(12.11f, 0.89f), Quaternion.identity));
            listPeople.Add(Instantiate(peoplePrefab, new Vector2(16.58f, 1.3f), Quaternion.identity));
            listNotPeople.Add(Instantiate(thiefPrefab, new Vector2(7.71f, 4.65f), Quaternion.identity));
            listNotPeople.Add(Instantiate(thiefPrefab, new Vector2(7.88f, 0.35f), Quaternion.identity));
            listNotPeople.Add(Instantiate(dogPrefab, new Vector2(13.92f, -1.4f), Quaternion.identity));

            listPeople[3].transform.DOMove(new Vector2(14.43f, 1.3f), 2).OnComplete(() => { listPeople[3].isMove = false; });
            listPeople[3].isMove = true;
            listNotPeople[0].transform.DOMove(new Vector2(8.19f, 2.76f), 3).OnComplete(() => { listNotPeople[0].isMove = false; });
            listNotPeople[0].isMove = true;
            num = listPeople.Count + listNotPeople.Count;

        }
        if (stageIndex == 3)
        {
            listPeople.Clear();
            listNotPeople.Clear();
            listPeople.Add(Instantiate(peoplePrefab, new Vector2(5.23f, -1.02f), Quaternion.identity));
            listPeople.Add(Instantiate(peoplePrefab, new Vector2(12.66f, -1.98f), Quaternion.identity));
            listPeople.Add(Instantiate(peoplePrefab, new Vector2(11.84f, -6.61f), Quaternion.identity));
            listPeople.Add(Instantiate(peoplePrefab, new Vector2(10.27f, -6.64f), Quaternion.identity));
            listNotPeople.Add(Instantiate(thiefPrefab, new Vector2(0.04f, -6.68f), Quaternion.identity));
            listNotPeople.Add(Instantiate(thiefPrefab, new Vector2(8.97f, -6.82f), Quaternion.identity));
            listNotPeople.Add(Instantiate(dogPrefab, new Vector2(15.66f, -8.53f), Quaternion.identity));
            listNotPeople.Add(Instantiate(dogPrefab, new Vector2(11.71f, -8.05f), Quaternion.identity));

            listPeople[0].transform.DOMove(new Vector2(7.72f, -4.91f), 3).OnComplete(() => { listPeople[0].isMove = false; });
            listPeople[0].isMove = true;
            listPeople[1].transform.DOMove(new Vector2(13.07f, -4.84f), 3).OnComplete(() => { listPeople[1].isMove = false; });
            listPeople[1].isMove = true;
            listNotPeople[0].transform.DOMove(new Vector2(7.4f, -6.58f), 3).OnComplete(() => { listNotPeople[0].isMove = false; });
            listNotPeople[0].isMove = true;
            listNotPeople[1].transform.DOMove(new Vector2(15.07f, -4.84f), 3).OnComplete(() => { listNotPeople[1].isMove = false; });
            listNotPeople[1].isMove = true;
            listNotPeople[2].transform.DOMove(new Vector2(14.3f, -7.67f), 3).OnComplete(() => { listNotPeople[2].isMove = false; });
            listNotPeople[2].isMove = true;

            num = listPeople.Count + listNotPeople.Count;
        }
        if (stageIndex == 4)
        {
            listPeople.Clear();
            listNotPeople.Clear();
            listPeople.Add(Instantiate(peoplePrefab, new Vector2(4.92f, -3.95f), Quaternion.identity));
            listPeople.Add(Instantiate(peoplePrefab, new Vector2(6.25f, -5.21f), Quaternion.identity));
            listPeople.Add(Instantiate(peoplePrefab, new Vector2(4.55f, -7.39f), Quaternion.identity));
            listPeople.Add(Instantiate(peoplePrefab, new Vector2(1.55f, -7.39f), Quaternion.identity));
            listPeople.Add(Instantiate(peoplePrefab, new Vector2(-2.37f, -3.47f), Quaternion.identity));
            listPeople.Add(Instantiate(peoplePrefab, new Vector2(-5.13f, -8.24f), Quaternion.identity));
            listNotPeople.Add(Instantiate(thiefPrefab, new Vector2(3.11f, -6.89f), Quaternion.identity));
            listNotPeople.Add(Instantiate(thiefPrefab, new Vector2(-0.13f, -7.09f), Quaternion.identity));
            listNotPeople.Add(Instantiate(thiefPrefab, new Vector2(-4.36f, -6.2f), Quaternion.identity));
            listNotPeople.Add(Instantiate(dogPrefab, new Vector2(13.24f, -6.95f), Quaternion.identity));
            listNotPeople.Add(Instantiate(dogPrefab, new Vector2(3.62f, -8.07f), Quaternion.identity));
            listNotPeople.Add(Instantiate(dogPrefab, new Vector2(-2.82f, -8.14f), Quaternion.identity));
            listNotPeople.Add(Instantiate(dogPrefab, new Vector2(-3.16f, -4.97f), Quaternion.identity));

            listPeople[5].transform.DOMove(new Vector2(0.73f, -7.39f), 2).OnComplete(() => { listPeople[5].isMove = false; });
            listPeople[5].isMove = true;
            listNotPeople[3].transform.DOMove(new Vector2(6.59f, -7.05f), 3).OnComplete(() => { listNotPeople[3].isMove = false; });
            listNotPeople[3].isMove = true;
            listNotPeople[5].transform.localScale = new Vector3(-1.3f, 1.3f, 1.3f);
            listNotPeople[6].transform.localScale = new Vector3(-1.3f, 1.3f, 1.3f);
            num = listPeople.Count + listNotPeople.Count;
        }
        if (stageIndex == 5)
        {
            listPeople.Clear();
            listNotPeople.Clear();
            listPeople.Add(Instantiate(peoplePrefab, new Vector2(-11.82f, -3.91f), Quaternion.identity));
            listPeople.Add(Instantiate(peoplePrefab, new Vector2(-5.99f - 2f, -3.91f - 2f), Quaternion.identity));
            listPeople.Add(Instantiate(peoplePrefab, new Vector2(-5.55f, -4.66f), Quaternion.identity));
            listPeople.Add(Instantiate(peoplePrefab, new Vector2(-2.28f, -6.81f), Quaternion.identity));
            listPeople.Add(Instantiate(peoplePrefab, new Vector2(-3.98f - 2f, -7.73f - 2f), Quaternion.identity));
            listPeople.Add(Instantiate(peoplePrefab, new Vector2(-13.76f, -7.59f), Quaternion.identity));
            listNotPeople.Add(Instantiate(thiefPrefab, new Vector2(-9.64f, -6.88f), Quaternion.identity));
            listNotPeople.Add(Instantiate(thiefPrefab, new Vector2(-8.45f, -7.26f), Quaternion.identity));
            listNotPeople.Add(Instantiate(thiefPrefab, new Vector2(-6.88f, -7.4f), Quaternion.identity));
            listNotPeople.Add(Instantiate(dogPrefab, new Vector2(-13.59f + 2f, -4.39f + 2f), Quaternion.identity));
            listNotPeople.Add(Instantiate(dogPrefab, new Vector2(-3.6f, -8.45f), Quaternion.identity));
            listNotPeople.Add(Instantiate(dogPrefab, new Vector2(-1.59f - 2f, -8.25f - 2f), Quaternion.identity));
            listNotPeople.Add(Instantiate(dogPrefab, new Vector2(0.22f, -7.3f), Quaternion.identity));
            listNotPeople.Add(Instantiate(dogPrefab, new Vector2(-5.64f, -5.43f), Quaternion.identity));
            listNotPeople.Add(Instantiate(dogPrefab, new Vector2(-5.71f, -7.99f), Quaternion.identity));

            listNotPeople[3].transform.localScale = new Vector3(-1.3f, 1.3f, 1.3f);
            listNotPeople[5].transform.DOMove(new Vector2(-1.59f, -8.25f), 2).OnComplete(() => { listNotPeople[5].isMove = false; });
            listNotPeople[5].isMove = true;
            listNotPeople[3].transform.DOMove(new Vector2(-13.59f, -4.39f), 3).OnComplete(() => { listNotPeople[3].isMove = false; });
            listNotPeople[3].isMove = true;
            listPeople[4].transform.DOMove(new Vector2(-3.98f, -7.73f), 2).OnComplete(() => { listPeople[4].isMove = false; });
            listPeople[4].isMove = true;
            listPeople[1].transform.DOMove(new Vector2(-5.99f, -3.91f), 3).OnComplete(() => { listPeople[1].isMove = false; });
            listPeople[1].isMove = true;

            num = listPeople.Count + listNotPeople.Count;
        }
    }

    void VFXHeart()
    {
        myStreetCar.transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(1, 1).OnComplete(() =>
        {
            myStreetCar.transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0, 1);
        });
        myStreetCar.transform.GetChild(0).transform.DOPunchScale(new Vector2(2.2f, 2.2f), 1);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isLose && !isWin)
        {
            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.RaycastAll(mousePos, Vector2.zero);
            if (hit.Length != 0)
            {
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i] && hit[i].collider != null)
                    {
                        if (hit[i].collider.gameObject.CompareTag("People") && !isNotPeople)
                        {
                            isPeople = true;
                            posBeginHold = hit[i].collider.gameObject.transform.position;
                            tmpPeople = hit[i].collider.gameObject.GetComponent<People_StreetCarMinigame1>();
                            isHoldPeople = true;

                        }
                        if (hit[i].collider.gameObject.CompareTag("Trash") && !isPeople)
                        {
                            isNotPeople = true;
                            posBeginHold = hit[i].collider.gameObject.transform.position;
                            tmpNotPeople = hit[i].collider.gameObject.GetComponent<NotPeople_StreetCarMinigame1>();
                            isHoldNotPeople = true;

                        }
                    }
                }
            }
        }
        if (Input.GetMouseButtonUp(0) && !isLose && !isWin)
        {
            if (tmpPeople != null && isHoldPeople)
            {
                if (tmpPeople.isOnStreetCar && tmpPeople != null)
                {
                    if (tutorial1.activeSelf)
                    {
                        tutorial1.SetActive(false);
                        tutorial1.transform.DOKill();

                        tutorial2.SetActive(true);
                        tutorial2.transform.DOMove(new Vector2(12.65f, 6.42f), 1.5f).SetEase(Ease.InBack).SetLoops(-1);
                    }
                    Destroy(tmpPeople.gameObject);
                    num--;
                    VFXHeart();
                }
                if (!tmpPeople.isOnStreetCar && tmpPeople != null)
                {
                    tmpPeople.transform.position = posBeginHold;
                }
            }
            if (tmpNotPeople != null && isHoldNotPeople)
            {
                if (tmpNotPeople.isOnStreetCar && tmpNotPeople != null)
                {
                    Destroy(tmpNotPeople.gameObject);
                    // Thua luon vi cho trom, cho len xe
                    isLose = true;
                    Debug.Log("Thua");
                }
                if (!tmpNotPeople.isOnStreetCar && tmpNotPeople != null)
                {
                    if (Vector2.Distance(tmpNotPeople.transform.position, posBeginHold) >= 5)
                    {
                        if (tutorial2.activeSelf)
                        {
                            tutorial2.SetActive(false);
                            tutorial2.transform.DOKill();
                        }
                        if (tutorial3.activeSelf)
                        {
                            tutorial3.SetActive(false);
                            tutorial3.transform.DOKill();
                        }
                        Destroy(tmpNotPeople.gameObject);
                        num--;
                        GameObject VFXBoomObject = Instantiate(VFXBoomPrefab, mousePos, Quaternion.identity);
                        VFXBoomObject.GetComponent<SpriteRenderer>().DOFade(1, 0.5f).OnComplete(() =>
                        {
                            VFXBoomObject.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).OnComplete(() =>
                            {
                                Destroy(VFXBoomObject);
                            });

                        });
                    }
                    else
                    {
                        tmpNotPeople.transform.position = posBeginHold;
                    }

                }
            }

            isHoldPeople = false;
            isHoldNotPeople = false;
            isPeople = false;
            isNotPeople = false;
        }

        if (isHoldPeople)
        {
            if (tmpPeople.isMove)
            {
                tmpPeople.transform.DOKill();
            }
            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector2(Mathf.Clamp(mousePos.x, -holdMousePosInScreen.x, holdMousePosInScreen.x), Mathf.Clamp(mousePos.y, -holdMousePosInScreen.y, holdMousePosInScreen.y));
            tmpPeople.transform.position = mousePos;
        }
        if (isHoldNotPeople)
        {
            if (tmpNotPeople.isMove)
            {
                tmpNotPeople.transform.DOKill();
            }
            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector2(Mathf.Clamp(mousePos.x, -holdMousePosInScreen.x, holdMousePosInScreen.x), Mathf.Clamp(mousePos.y, -holdMousePosInScreen.y, holdMousePosInScreen.y));
            tmpNotPeople.transform.position = mousePos;
        }


        if (num == 0 && stage == 0 && !isLockStage)
        {
            isLockStage = true;
            isDonKhach = false;
            waypointIndex++;
            myStreetCar.transform.DOMove(listWayPoint[waypointIndex].position, 2).SetEase(Ease.Linear).OnComplete(() =>
            {
                waypointIndex++;
                myStreetCar.transform.DOMove(listWayPoint[waypointIndex].position, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    isDonKhach = true;
                    num = -1;
                    SpawnObject(++stage);
                });
            });
        }


        if (num == 0 && stage == 1 && isLockStage)
        {
            isLockStage = false;
            isDonKhach = false;
            waypointIndex++;
            myStreetCar.transform.DOMove(listWayPoint[waypointIndex].position, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                waypointIndex++;
                myStreetCar.transform.DOMove(listWayPoint[waypointIndex].position, 1.3f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    waypointIndex++;
                    myStreetCar.transform.DOMove(listWayPoint[waypointIndex].position, 0.7f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        isDonKhach = true;
                        num = -1;
                        SpawnObject(++stage);
                    });
                });
            });
        }
        if (num == 0 && stage == 2 && !isLockStage)
        {
            isLockStage = true;
            isDonKhach = false;
            waypointIndex++;
            myStreetCar.transform.DOMove(listWayPoint[waypointIndex].position, 0.7f).SetEase(Ease.Linear).OnComplete(() =>
            {
                waypointIndex++;
                myStreetCar.transform.DOMove(listWayPoint[waypointIndex].position, 1).SetEase(Ease.Linear).OnComplete(() =>
                {
                    waypointIndex++;
                    myStreetCar.transform.DOMove(listWayPoint[waypointIndex].position, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        waypointIndex++;
                        myStreetCar.transform.DOMove(listWayPoint[waypointIndex].position, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
                        {
                            isDonKhach = true;
                            num = -1;
                            SpawnObject(++stage);
                        });
                    });
                });
            });
        }
        if (num == 0 && stage == 3 && isLockStage)
        {
            isLockStage = false;
            isDonKhach = false;
            waypointIndex++;
            myStreetCar.transform.DOMove(listWayPoint[waypointIndex].position, 3.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                isDonKhach = true;
                num = -1;
                SpawnObject(++stage);
            }); ;
        }
        if (num == 0 && stage == 4 && !isLockStage)
        {
            isLockStage = true;
            isDonKhach = false;
            waypointIndex++;
            myStreetCar.transform.DOMove(listWayPoint[waypointIndex].position, 3.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                isDonKhach = true;
                num = -1;
                SpawnObject(++stage);
            }); ;
        }
        if (num == 0 && stage == 5 && isLockStage)
        {
            isLockStage = false;
            isDonKhach = false;
            waypointIndex++;
            myStreetCar.transform.DOMove(listWayPoint[waypointIndex].position, 1.3f).SetEase(Ease.Linear).OnComplete(() =>
            {
                waypointIndex++;
                myStreetCar.transform.DOMove(listWayPoint[waypointIndex].position, 1.3f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    waypointIndex = 0;
                    myStreetCar.transform.DOMove(listWayPoint[waypointIndex].position, 1.3f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        isDonKhach = true;
                        Debug.Log("Win");
                        isWin = true;
                        StopAllCoroutines();
                        myStreetCar.StopAllCoroutines();
                    });
                });
            });
        }
    }
}
