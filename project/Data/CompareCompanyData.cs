using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using OpenQA.Selenium;

[Serializable]
public class CompareCompanyData {
    public Company TargetDataA { get; set; }
    public Company TargetDataB { get; set; }

    public string CompanyName;
    public int Code;
    
    public float PriceChangeRate;
    public int ScoreChangeValue;

    public static CompareCompanyData CompareData(Company _companyA, Company _companyB) {
        if (_companyA == null || _companyB == null) {
            return null;
        }

        if (_companyA.Code != _companyB.Code) {
            return null;
        }

        var data = new CompareCompanyData() {
            TargetDataA = _companyA,
            TargetDataB = _companyB,
            Code = _companyA.Code,
            CompanyName = _companyA.CompanyName,
        };
        
        data.CompareData();
        return data;
    }

    private void CompareData() {
        if (TargetDataA.CurrentPrice != null && TargetDataB.CurrentPrice != null) {
            var priceDiff = TargetDataB.CurrentPrice - TargetDataA.CurrentPrice;
            PriceChangeRate = ((float)priceDiff / TargetDataA.CurrentPrice.Value) * 100f;
        }

        ScoreChangeValue = TargetDataB.TotalScore - TargetDataA.TotalScore;
    }

    public override string ToString() {
        return $"{CompanyName} ({Code:000000}) | " +
               $"주가변화 {(PriceChangeRate > 0 ? "+" : string.Empty)}{PriceChangeRate:0.00}% | " +
               $"점수변화 {ScoreChangeValue} ({TargetDataA.TotalScore} -> {TargetDataB.TotalScore})";
    }
}
