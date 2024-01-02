using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private BottleScript curBottle;
    // Update is called once per frame
    private void Update()
    {
        if (Input.touchCount <= 0) return;
        var touch = Input.touches[0];
        if (touch.phase != TouchPhase.Ended) return;

        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;
        bool isHit = Physics.Raycast(ray, out hit, 15);
        if (isHit)
        {
            Transform objectHit = hit.transform;
            if (!IsUnderSheepsBottle(objectHit))
            {
                if (objectHit.GetComponent<BottleScript>())
                {
                    BottleScript newBottle = objectHit.GetComponent<BottleScript>();
                    if (curBottle == null && !BottleScript.isSelectionInProgress)
                    {
                        curBottle = newBottle;
                        curBottle.BottleClicked();
                    }
                    else
                    {
                        if (curBottle == newBottle && !BottleScript.isSelectionInProgress)
                        {
                            curBottle.BottleClicked();
                            curBottle = null;
                        }
                        else
                        {
                            if (!BottleScript.isSelectionInProgress)
                            {
                                List<int> liquidTransferList = CheckLiquidTransfer(curBottle.liquids, newBottle.liquids);
                                Debug.Log(string.Join(", ", liquidTransferList));
                                curBottle.LiquidTransfer(newBottle, liquidTransferList);
                                curBottle = null;
                            }
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Þiþe dokunulmaz!");
            }
            
        }
    }

    private bool IsUnderSheepsBottle(Transform objectTransform)
    {
        Transform parent = objectTransform.parent;

        while (parent != null)
        {
            if (parent.CompareTag("SheepsBottle"))
            {
                return true;
            }
            parent = parent.parent;
        }

        return false;
    }

    private List<int> CheckLiquidTransfer(List<int> curLiquids, List<int> newLiquids)
    {
        if (curLiquids.Count == 0)
        {
            return new List<int>();
        }
        else if (newLiquids.Count == 4) //
        {
            return new List<int>();
        }
        else
        {
            List<int> liquidTransferList = new List<int>();
            int oldLiquidCode = curLiquids[curLiquids.Count - 1];
            int i = 0;
            while (i < 4 - newLiquids.Count) { //
                int index = curLiquids.Count - 1 - i;
                if (oldLiquidCode != curLiquids[index]) return liquidTransferList;
                liquidTransferList.Add(curLiquids[index]);
                oldLiquidCode = curLiquids[index];
                i++;
            }
            return liquidTransferList;
        }
    }
}