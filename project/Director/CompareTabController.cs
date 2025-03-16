using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

public static class CompareTabController {
    private static string DataPathA { get; set; } = string.Empty;
    private static string DataPathB { get; set; } = string.Empty;
    public static List<Company> CompareTargetA { get; private set; }
    public static List<Company> CompareTargetB { get; private set; }

    public static List<CompareCompanyData> CompareData { get; } = new();

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
        CompareTargetA = null;
        DataPathA = string.Empty;
        
        CompareTargetA = LoadCompanyData(path);
        if (CompareTargetA != null)
            DataPathA = path;
        
        ShowDataLog();

        return CompareTargetA != null;
    }
    
    public static bool SetDataB(string path) {
        CompareTargetB = null;
        DataPathB = string.Empty;
        
        CompareTargetB = LoadCompanyData(path);
        if (CompareTargetB != null)
            DataPathB = path;
        
        ShowDataLog();

        return CompareTargetB != null;
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
                  $"\n데이터1 : {DataPathA} {(CompareTargetA != null ? $"({CompareTargetA.Count}개 회사)" : "(선택되지 않음)")}" +
                  $"\n데이터2 : {DataPathB} {(CompareTargetB != null ? $"({CompareTargetB.Count}개 회사)" : "(선택되지 않음)")}" +
                  $"\n{(CanAnalysis(out var reason) ? "(분석 가능)" : $"(분석 불가 - {reason}")}";
        UiHelper.ShowLog(log);
    }

    public static void StartCompare() {
        if (!CanAnalysis(out var failReason)) {
            UiHelper.ShowLog(failReason);
            MessageBox.Show(failReason);
            return;
        }
        
        CompareData.Clear();
        
        CompareTargetA.SortByCompanyPoint();
        foreach (var target in CompareTargetA) {
            if (target == null || target.Code == default)
                continue;
            
            var data = CompareCompanyData.CompareData(target, CompareTargetB.Find(_co => _co.Code == target.Code));
            if (data != null)
                CompareData.Add(data);
        }
        
        SaveHelper.SaveCompareData($"Data A: {DataPathA}\nData B: {DataPathB}\n", CompareData);
        
        UiHelper.ShowLog($"분석이 완료되었습니다.\n({CompareData.Count}개 분석 데이터)\n" +
                         $"{SaveHelper.GetPath(SaveHelper.Type.CompareData)}");
    }
}
