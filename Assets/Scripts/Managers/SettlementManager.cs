using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementManager : Singleton<SettlementManager>
{
    private int errorCount = 0;
    public void AddError() { errorCount++; }
}