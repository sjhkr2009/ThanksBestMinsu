using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

public static class CompareTabDirector {
    private static string DataPathA { get; set; } = string.Empty;
    private static string DataPathB { get; set; } = string.Empty;
    public static List<Company> CompareDataA { get; private set; }
    public static List<Company> CompareDataB { get; private set; }

    public static bool CanAnalysis(out string reason) {
        reason = string.Empty;
        
        if (string.IsNullOrEmpty(DataPathA)) {
            reason = "데이터1 이 없습니다";
            return false;
        }

        if (string.IsNullOrEmpty(DataPathB)) {
            reason = "데이터2 이 없습니다";
            return false;
        }

        if (DataPathA == DataPathB) {
            reason = "분석할 데이터는 서로 달라야 합니다.";
            return false;
        }

        return true;
    }

    public static bool SetDataA(string path) {
        CompareDataA = null;
        DataPathA = string.Empty;
        
        CompareDataA = LoadCompanyData(path);
        if (CompareDataA != null)
            DataPathA = path;
        
        ShowDataLog();

        return CompareDataA != null;
    }
    
    public static bool SetDataB(string path) {
        CompareDataB = null;
        DataPathB = string.Empty;
        
        CompareDataB = LoadCompanyData(path);
        if (CompareDataB != null)
            DataPathB = path;
        
        ShowDataLog();

        return CompareDataB != null;
    }

    private static List<Company> LoadCompanyData(string path) {
        if (string.IsNullOrWhiteSpace(path))
            return null;

        try {
            var content = File.ReadAllText(path);
            var company = JsonConvert.DeserializeObject<List<Company>>(content);
            if (company == null || company.Count == 0)
                return null;

            return company;
        }
        catch (Exception e) {
            var logText = $"파일이 유효하지 않습니다: {path}({e.GetType().Name} - {e.Message})";
            MessageBox.Show(logText);
            return null;
        }
    }

    private static void ShowDataLog() {
        var log = $"[비교 대상 데이터]" +
                  $"\n데이터1 : {DataPathA} {(CompareDataA != null ? $"({CompareDataA.Count}개 회사)" : "(선택되지 않음)")}" +
                  $"\n데이터2 : {DataPathB} {(CompareDataB != null ? $"({CompareDataB.Count}개 회사)" : "(선택되지 않음)")}" +
                  $"\n{(CanAnalysis(out var reason) ? "(분석 가능)" : $"(분석 불가 - {reason}")}";
        UiHelper.ShowLog(log);
    }

    public static void StartCompare() {
        if (!CanAnalysis(out var failReason)) {
            UiHelper.ShowLog(failReason);
            MessageBox.Show(failReason);
            return;
        }

        // Test
        for (int i = 0; i < CompareDataA.Count; i++) {
            var companyA = CompareDataA[i];
            var companyB = CompareDataB.Find(_data => _data.Code == companyA.Code);

            if (companyB != null) {
                companyA.AnalysisAll();
                companyB.AnalysisAll();
                UiHelper.ShowLog($"{companyA.CompanyName}({companyB.Code}) : 점수 변화 {(companyA.RecommendPoint - companyA.WarningPoint)} -> {(companyB.RecommendPoint - companyB.WarningPoint)}");
                break;
            }
        }
    }
}
